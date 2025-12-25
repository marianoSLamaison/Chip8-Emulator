namespace Chip8Emu.cpu;

partial class Cpu
{
    
    private const byte _stack_limit = 0x0C;
    private const ushort _stack_start = 0x100;
    private const ushort _text_heigth = 0x05;
    private const ushort _inst_size = 2;
}