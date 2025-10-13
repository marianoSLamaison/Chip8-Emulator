using Microsoft.Xna.Framework.Content;

namespace Chip8Emu.cpu;

class cpu
{
    private enum OP_FAMILY : byte
    {
        OP0, OP1, OP2, OP3,
        OP4, OP5, OP6, OP7,
        OP8, OP9, OPA, OPB,
        OPC, OPD, OPE, OPF
    }
    private BinaryFileReader _file_reader;
    private CPUContext _context;
    
    public cpu()
    {
        _file_reader = new();
        _context = new();

    }
    public void Instruction_cicle()
    {
        ushort raw_inst = fetch();
        Chip8DecodedInst inst = decode(raw_inst);
        execute(inst);
    }
    private ushort fetch() => _file_reader.GetInstruction(_context.ir);
    private Chip8DecodedInst decode(ushort inst)
    {
        byte op_family = (byte)((inst & (0x07 << 0x0C)) >> 0x0C);
        ushort args = (ushort)(inst & ~(0x07 << 0x0C));
        return new(op_family, args);
    }
    private void execute(Chip8DecodedInst inst)
    {
        switch((OP_FAMILY)inst.OpFamily)
        {
            case OP_FAMILY.OP0:
                break;
            case OP_FAMILY.OP1:
                break;
            case OP_FAMILY.OP2:
                break;
            case OP_FAMILY.OP3:
                break;
            case OP_FAMILY.OP4:
                break;
            case OP_FAMILY.OP5:
                break;
            case OP_FAMILY.OP6:
                break;
            case OP_FAMILY.OP7:
                break;
            case OP_FAMILY.OP8:
                break;
            case OP_FAMILY.OP9:
                break;
            case OP_FAMILY.OPA:
                break;
            case OP_FAMILY.OPB:
                break;
            case OP_FAMILY.OPC:
                break;
            case OP_FAMILY.OPD:
                break;
            case OP_FAMILY.OPE:
                break;
            case OP_FAMILY.OPF:
                break;
            default:
                throw new("Invalide OP code");
        }
    }
    public void Load(ContentManager c)
    {
        _file_reader.Load(c, "tests/1-chip8-logo.ch8");
    }
}