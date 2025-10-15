using Chip8Emu.cpu.Operations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chip8Emu.cpu;

partial class Cpu
{
    private IO.Chip8Screen _screen;
    private IO.Keyboard _keyboard;
    private Memory _mem;
    private FunctionRunner _function_runner;
    //Registers//////////////////////////////////////////////
    private ushort _ir;
    private ushort[] _v;//registrys there are 16
    private ushort _mar;//is supsed to be 12 bits long it's also the I in the docs, but
    //I'm implementing the thing so I called as it's called in my text book for computer
    //architecture
    private byte _sp; //stack pointer
    public ushort _delay_timer;
    public ushort _sound_timer;
    ///////////////////////////////////////////////////////////
    

    public Cpu()
    {
        _mem = new();
        ///Registers 
        _ir = 0x200;
        _v = new ushort[0xF];
        _mar = 0;
        _sp = 0;
        _delay_timer = 0;
        _sound_timer = 0;
    }
    public void Instruction_cicle()
    {
        ushort raw_inst = fetch();
        Chip8DecodedInst inst = decode(raw_inst);
        execute(inst);
    }
    private ushort fetch() => _mem.Read(_ir);
    private Chip8DecodedInst decode(ushort inst)
    {
        byte op_family = (byte)((inst & (0x07 << 0x0C)) >> 0x0C);
        ushort args = (ushort)(inst & ~(0x07 << 0x0C));
        return new(op_family, args);
    }
    /// <summary>
    /// Look, I know this is kinda dificult to follow,
    /// but Every instruction familie has it's how conditions to run X or Y method,
    /// And way of getting it's data from it, so I decided that life is easier if you have
    /// one guy whos job is to decode all that, whe here just get the instruction family 
    /// (because I like to stick to the instruction cicle) and tell it to do it's magic
    /// </summary>
    /// <param name="inst"></param>
    private void execute(Chip8DecodedInst inst)
    {
        switch ((OP_FAMILY)inst.OpFamily)
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
    public void Update(GameTime t)
    {
        
    }

    public void Load(ContentManager c)
    {
        
    }
    
    public void Draw(SpriteBatch s)
    {
        
    }

}