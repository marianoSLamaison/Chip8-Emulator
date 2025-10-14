using Microsoft.Xna.Framework.Content;

namespace Chip8Emu.cpu;

class Cpu
{
    private BinaryFileReader _file_reader;
    private output.Chip8Screen screen;
    
    private ushort _ir;
    private ushort[] _v;//registrys there are 16
    private byte[] _memory;//4096 size memory programs read from 512 onwards
    private ushort _mar;//is supsed to be 12 bits long it's also the I in the docs, but
    //I'm implementing the thing so I called as it's called in my text book for computer
    //architecture
    private ushort[] _stack;//48 in length and up to 12 levels of nesting
    private byte _sp; //stack pointer
    public ushort _delay_timer;
    public ushort _sound_timer;
    

    private enum OP_FAMILY : byte
    {
        OP0, OP1, OP2, OP3,
        OP4, OP5, OP6, OP7,
        OP8, OP9, OPA, OPB,
        OPC, OPD, OPE, OPF
    }

    public Cpu()
    {
        _file_reader = new();

        _ir = 0;
        _v = new ushort[0xF];
        _mar = 0;
        _memory = new byte[0x1000];
        _stack = new ushort[0x30];//48 en exa
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
    private ushort fetch() => _file_reader.GetInstruction(_ir);
    private Chip8DecodedInst decode(ushort inst)
    {
        byte op_family = (byte)((inst & (0x07 << 0x0C)) >> 0x0C);
        ushort args = (ushort)(inst & ~(0x07 << 0x0C));
        return new(op_family, args);
    }
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
    public void EsquipIfRegXNotEqualToRegY(ushort regs)
    {
        if (_v[regs & (0x07<<0x08)] != _v[regs & (0x07 << 0x04)])
            _ir += 2;
    }
    public void ExecuteFlagedInterRegisterOperation(byte Op_code, byte Reg1Id, byte Reg2Id)
    {
        unchecked//because otherwise C# will be trowing exeptions
        {
            switch (Op_code)
            {
                case 0x04:
                    if ((_v[Reg1Id] & _v[Reg2Id]) != 0x00)//if there is a carry
                        _v[0x0F] = 0x01;
                    else
                        _v[0x0F] = 0x00;
                    _v[Reg1Id] += _v[Reg2Id];
                    break;
                case 0x05:
                    _v[0x0F] = (ushort)(((_v[Reg1Id] ^ _v[Reg1Id]) & _v[Reg1Id]) == 0x00 ? 0 : 1);//if there will be a borrow
                    _v[Reg1Id] -= _v[Reg2Id];
                    break;
                case 0x06:
                    _v[0x0F] = (ushort)(_v[Reg2Id] & 0x01);
                    _v[Reg1Id] = (ushort)(_v[Reg2Id] >> 0x01);
                    break;
                case 0x07:
                    _v[0x0F] = (ushort)(((_v[Reg1Id] ^ _v[Reg2Id]) & _v[Reg2Id]) == 0x00 ? 0 : 1);
                    _v[Reg1Id] = (ushort)(_v[Reg2Id] - _v[Reg1Id]);
                    break;
                case 0x0E:
                    _v[0x0F] = (ushort)((_v[Reg2Id] & 0x8000) >> 0x0F);
                    _v[Reg1Id] = (ushort)(_v[Reg2Id] >> 0x01);
                    break;
            }
        }
    }
    public void ExeuteNoneFlagedInterRegistyOperation(byte Op_code, byte Reg1ID, byte Reg2ID)
    {
        switch(Op_code)
        {
            case 0x00:
                _v[Reg1ID] = _v[Reg2ID];
                break;
            case 0x01:
                _v[Reg1ID] |= _v[Reg2ID];
                break;
            case 0x02:
                _v[Reg1ID] &= _v[Reg2ID];
                break;
            case 0x03:
                _v[Reg1ID] ^= _v[Reg2ID];
                break;
        }
    }
    public void AddToReg(ushort args)
    {
        _v[(args & 0x07 << 0x08) >> 0x08] += (ushort)(args & 0xFF);
    }
    public void StoreInReg(ushort args)
    {
        _v[(args & 0x07 << 0x08) >> 0x08] = (ushort)(args & 0xFF);
    }
    public void EsquipIfRegXEcualsRegY(ushort args)
    {
        if (_v[(args & 0x07<<0x08) >> 0x08] != _v[(args & 0x07<<0x04) >> 0x04])
        {
            _ir += 2;
        }
    }
    public void EsquipIfNotEcuals(ushort args)
    {
        if (_v[(args & 0x07<<0x08) >> 0x08] != (args & 0xFF))
        {
            _ir += 2;
        }
    }
    public void EsquipIfEcuals(ushort args)
    {
        if (_v[(args & 0x07<<0x08) >> 0x08] == (args & 0xFF))
        {
            _ir += 2;
        }
    }
    public void ExecuteInternalSubroutine(ushort direction)
    {
        //store the direction of my next instruction in the stack
        _stack[_sp] = ++_ir;
        _ir = direction;
    }
    public void InternalJump(ushort direction)
    {
        _ir = direction;
    }

    public void Load(ContentManager c)
    {
        _file_reader.Load(c, "tests/1-chip8-logo.ch8");
    }
    public void ClearScreen()
    {
        screen.Clear();
    }
    public void PopStack()
    {
        _ir = _stack[--_sp];//take the last value from the stack
    }

}