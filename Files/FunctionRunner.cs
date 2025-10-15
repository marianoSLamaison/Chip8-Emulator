using System.Collections;

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
    public void FamilyAExecute(ref Cpu c, ushort args)
    {
        c.StoreMemoryAdress(args);
    }
    public void FamilyBExecute(ref Cpu c, ushort args)
    {
        c.JumpBaseV0(args);
    }
    public void FamilyCExecute(ref Cpu c, ushort args)
    {
        c.RandomizeReg(args);
    }
    public void FamilyDExecute(ref Cpu c, ushort args)
    {
        c.DrawToScreen((byte)(args & 0x0700), (byte)(args & 0x0070), (byte)(args & 0x0007));
    }
    public void FamilyEExecute(ref Cpu c, ushort args)
    {
        switch (args & 0x00FF)
        {
            case 0x009E:
                c.SkipIfKeyPressed((byte)(args & 0x0F00));
                break;
            case 0x00A1:
                c.SkipIfKeyNotPressed((byte)(args & 0x0F00));
                break;
        }
    }
    public void FamilyFExecute(ref Cpu c, ushort args)
    {
        byte reg = (byte)(args & 0x0F00);
        byte code = (byte)(args & 0x00FF);
        switch(code)
        {
            case 0x07:
                c.SetDelayTimer(reg);
                break;
            case 0x15://set or get the delay timer
                c.GetDelayTimer(reg);
                break;
            case 0x0A://Stores last key pressed after instruction
                c.StoreNextKey(reg);
                break;
            case 0x18://set sound timer
                c.SetSoundTimer(reg);
                break;
            case 0x1E://increments mar
                c.AddToMar(reg);
                break;
            case 0x29://
                c.SetMarToSpriteData(reg);
                /*
                Your emulator initialization routine includes writing the sprite font data to memory 
                anywhere between 0x0 and 0x199. 
                FX29 grabs the value in vX, ANDs with 0xF, and then based on the resulting 
                hexadecimal digit, your emulator sets I to the memory address that points to the 
                sprite data corresponding to that hex digit 

                For example, if the instruction is F209, you take the value in v2, AND it with 0xF,
                and then if the resulting digit is perhaps 0xA, 
                you set I to where you stored the font data for the hex digit A

                --ArkoSammy12--
                */
                break;
            case 0x33:
                //Store the binary-coded decimal equivalent of the value stored in register VX at addresses I, 
                // I + 1, and I + 2
                c.StoreBinaryCodedDecimalFromReg(reg);
                break;
            case 0x55://saves the procesor state (only v registers) starting at I
                c.SaveStateUpToReg(reg);
                break;
            case 0x65://loads a procesor state (only v regs) starting at I
                c.LoadStateUpToReg(reg);
                break;
        }
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