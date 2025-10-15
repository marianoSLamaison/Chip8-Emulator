namespace Chip8Emu.cpu;

partial class Cpu
{
    private enum OP_FAMILY : byte
    {
        OP0, OP1, OP2, OP3,
        OP4, OP5, OP6, OP7,
        OP8, OP9, OPA, OPB,
        OPC, OPD, OPE, OPF
    }
    private const byte _stack_limit = 0x0C;
    private const ushort _stack_start = 0x100;
}