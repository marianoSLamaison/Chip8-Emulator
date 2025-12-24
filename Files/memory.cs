using System;
using System.Collections;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace Chip8Emu.cpu;

class Memory
{
    //NOTE: the quirks test uses more memory than 4K so it will not run here
    private const ushort _mem_size = 4096;//4096;
    private const ushort _display_space_start = 4096 - 64*32;//0xE8F ;
    private const ushort _user_space_start = 0x200;
    private const ushort _font_set_end = 0x80;
    private byte[] _mem;
    const byte _by_mask = 0xFF;
    const byte _by_size = 0x08;
    private static byte[] _font_set = new byte[]
    {
        0xF0, 0x90, 0x90, 0x90, 0xF0,
        0x20, 0x60, 0x20, 0x20, 0x70,
        0xF0, 0x10, 0xF0, 0x80, 0xF0,
        0xF0, 0x10, 0xF0, 0x10, 0xF0,
        0x90, 0x90, 0xF0, 0x10, 0x10,
        0xF0, 0x80, 0xF0, 0x10, 0xF0,
        0xF0, 0x80, 0xF0, 0x90, 0xF0,
        0xF0, 0x10, 0x20, 0x40, 0x40,
        0xF0, 0x90, 0xF0, 0x90, 0xF0,
        0xF0, 0x90, 0xF0, 0x10, 0xF0,
        0xF0, 0x90, 0xF0, 0x90, 0x90,
        0xE0, 0x90, 0xE0, 0x90, 0xE0,
        0xF0, 0x80, 0x80, 0x80, 0xF0,
        0xE0, 0x90, 0x90, 0x90, 0xE0,
        0xF0, 0x80, 0xF0, 0x80, 0xF0,
        0xF0, 0x80, 0xF0, 0x80, 0x80
    };
    /// <summary>
    /// Helper class for the memory managment
    /// The stack is a construct of the CPU so it's the cpu's problem
    /// </summary>
    public Memory()
    {
        _mem = new byte[_mem_size];
        _font_set.CopyTo(_mem,0);
    }
    /// <summary>
    /// Function that returns the data refered to the screen
    /// </summary>
    /// <returns>A reference to the screen data with Span</returns>
    public Span<byte> GetScreenState() => new Span<byte>(_mem, _display_space_start, _mem_size - _display_space_start);
    /// <summary>
    /// Stores the given data in an arbitrary memory direction it only forbids the 
    /// screen section and the font set
    /// </summary>
    /// <param name="dir">direction of the data forbiden from being in the font set or screen</param>
    /// <param name="arg">Argument that will be stored in big endian fashion</param>
    public void Write(ushort dir, ushort arg)
    {
        //storing big endian way

        _mem[dir % _mem_size] = (byte)BitHelper.GetMaskValue(arg, _by_mask, _by_size);
        _mem[(dir + 1) % _mem_size] = (byte)BitHelper.GetMaskValue(arg, _by_mask, 0x00);
    }
    public void WriteByte(ushort dir, byte arg)
    {
        _mem[dir % _mem_size] = arg;
    }
    /// <summary>
    /// Reads a data from the memory
    /// </summary>
    /// <param name="dir">Direction that is also forbiden from velonging in the main screen or font set</param>
    /// <returns></returns>
    public ushort Read(ushort dir)
    {
        return (ushort)((ushort)(_mem[dir % _mem_size] << _by_size) + (ushort)_mem[(dir + 1) % _mem_size]);
    }
    public byte ReadByte(ushort dir)
    {
        return _mem[dir % _mem_size];
    }

    public Span<byte> GetSprite(ushort init_pos, byte sprite_heigth)
    {
        if (init_pos + sprite_heigth >= _mem_size)
        {
            //tenemos que partir la imagen
            Span<byte> ret, helper;
            byte[] data = new byte[sprite_heigth];
            int first_size = _mem_size - init_pos;
            helper = new(_mem, init_pos, first_size);
            helper.ToArray().CopyTo(data, 0);
            helper = new(_mem, 0, sprite_heigth - first_size);
            helper.ToArray().CopyTo(data, first_size);
            ret = new(data);
            return ret;
        }
        return new(_mem, init_pos, sprite_heigth);
    }

    public void Load(string file_name, ContentManager c)
    {
        Span<byte> mem_slice = new(_mem, _user_space_start, _display_space_start - _user_space_start);
        byte[] file_data = File.ReadAllBytes(Path.Combine(c.RootDirectory, file_name));
        file_data.CopyTo(mem_slice);
        //byte[] screen_start = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
        //screen_start.CopyTo(GetScreenState());
    }

}