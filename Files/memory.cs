using System;

namespace Chip8Emu.cpu;

class Memory
{
    BitHelper helper;
    private const ushort _mem_size = 4096;
    private const ushort _display_space_start = 0xE8F;
    private const ushort _user_space_start = 0x200;
    private const ushort _font_set_end = 0x80;
    private byte[] _mem;
    const byte _by_mask = 0xFF;
    const byte _by_size = 0x08;
    /// <summary>
    /// Helper class for the memory managment
    /// The stack is a construct of the CPU so it's the cpu's problem
    /// </summary>
    public Memory()
    {
        _mem = new byte[_mem_size];
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
        if (dir > _font_set_end && dir < _display_space_start)
        {
            //storing big endian way
            _mem[dir] = (byte)helper.GetMaskValue(arg, _by_mask, _by_size);
            _mem[dir + 1] = (byte)helper.GetMaskValue(arg, _by_mask, 0x00);
        }
        else
        {
            throw new("Illegal acces to Chip8 memory!");
        }
    }
    /// <summary>
    /// Reads a data from the memory
    /// </summary>
    /// <param name="dir">Direction that is also forbiden from velonging in the main screen or font set</param>
    /// <returns></returns>
    public ushort Read(ushort dir)
    {
        if (dir > _font_set_end && dir < _display_space_start)
        {
            return (ushort)(_mem[dir] << _by_size + _mem[dir + 1]);
        }
        throw new("Invalid acces to Chip8Memory!");
    }

    
}