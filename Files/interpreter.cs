using System.IO;
using Microsoft.Xna.Framework.Content;

namespace Chip8Emu;

class Interpreter
{
    FileStream file;
    byte[] buffer;
    const short buffer_size = (short)1024;
    public Interpreter(ContentManager content, string file_name)
    {
        buffer = new byte[buffer_size];
        file = new(Path.Combine(content.RootDirectory, file_name), FileMode.Open);
        file.Read(buffer, 0, buffer_size);
    }
    public ushort GetInstruction(int inst)
    {
        if (inst % 2 != 0)
            throw new("Instruction not valid, must be an even number");
        
        ushort ret = 0;

        ret |= (ushort)(buffer[inst] << 8);
        ret |= buffer[inst + 1];
        return ret;
    }
}