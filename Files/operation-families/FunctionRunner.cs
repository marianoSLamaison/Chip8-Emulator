namespace Chip8Emu.cpu.Operations;

class FunctionRunner
{

    public void Famly0Execute(ref Cpu context, ushort args)
    {
        switch (args)
        {
            case 0x0E0:
                ClearScren(ref context);
                break;
            case 0x0EE:
                Return(ref context);
                break;
            default:
                ExecuteMachineSub(ref context, args);
                break;
        }
    }
    public void Family1Execute(ref Cpu c, ushort args)
    {
        InternalJump(ref c, args);
    }
    public void Family2Execute(ref Cpu c, ushort args)
    {
        c.ExecuteInternalSubroutine(args);
    }
    public void Family3Execute(ref Cpu c, ushort args)
    {
        c.EsquipIfEcuals(args);
    }
    public void Family4Execute(ref Cpu c, ushort args)
    {
        c.EsquipIfNotEcuals(args);
    }
    public void Family5Execute(ref Cpu c, ushort args)
    {
        c.EsquipIfRegXEcualsRegY(args);
    }
    public void Family6Execute(ref Cpu c, ushort args)
    {
        c.StoreInReg(args);
    }
    public void Family7Execute(ref Cpu c, ushort args)
    {
        c.AddToReg(args);
    }
    public void Family8Execute(ref Cpu c, ushort args)
    {
        switch (args & 0x7)
        {
            case 0x00:
            case 0x01:
            case 0x02:
            case 0x03:
                c.ExeuteNoneFlagedInterRegistyOperation(
                    (byte)(args & 0x7),//operation
                    (byte)(args & (0x7 << 0x04)),
                    (byte)(args & (0x7 << 0x08))
                );
                break;
            case 0x04:
            case 0x05:
            case 0x06:
            case 0x07:
            case 0x0E:
                c.ExecuteFlagedInterRegisterOperation(
                    (byte)(args & 0x7),//operation
                    (byte)(args & (0x7 << 0x04)),
                    (byte)(args & (0x7 << 0x08))
                );
                break;
        }
    }
    public void Family9Execute(ref Cpu c, ushort args)
    {
        c.EsquipIfRegXNotEqualToRegY(args);
    }
    //Produces an internal jump in the program, it does not store previous place
    //onli changes curren position to execution.
    private void InternalJump(ref Cpu c, ushort args)
    {
        c.InternalJump(args);
    }
    private void ClearScren(ref Cpu c)
    {
        c.ClearScreen();
    }
    private void Return(ref Cpu context)
    {
        context.PopStack();
    }
    private void ExecuteMachineSub(ref Cpu context, ushort args)
    {
        //I'm not simulating the full Cosmac-Vip so this will function as a NOOP
    }
}