using System.IO;
using Microsoft.Xna.Framework.Content;

namespace Chip8Emu.cpu;

class BinaryFileReader
{
    FileStream file;
    byte[] buffer;
    const short buffer_size = (short)1024;
    public BinaryFileReader()
    {
        buffer = new byte[buffer_size];
    }
    public void Load(ContentManager c, string file_name)
    {
        file = new(Path.Combine(c.RootDirectory, file_name), FileMode.Open);
        file.Read(buffer, 0, buffer_size);
    }
    public ushort GetInstruction(ushort inst)
    {
        if (inst % 2 != 0)
            throw new("Instruction not valid, must be an even number");

        ushort ret = 0;

        ret |= (ushort)(buffer[inst] << 0x08);
        ret |= buffer[inst + 1];
        return ret;
    }
}