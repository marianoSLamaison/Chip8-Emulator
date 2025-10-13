namespace Chip8Emu.cpu;

struct Chip8DecodedInst
{
    private byte _op_family;
    public byte OpFamily{ get => _op_family; }
    private ushort _args;
    public ushort Args{ get => _args; }
    public Chip8DecodedInst(byte op_family, ushort args)
    {
        _op_family = op_family;
        _args = args;
    }

}