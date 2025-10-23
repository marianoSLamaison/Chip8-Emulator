using System;
using System.Collections;

namespace Chip8Emu.cpu.Operations;

class FunctionRunner
{
    private const ushort _nible_mask = 0xF;
    private const ushort _byte_mask = 0xFF;
    private const ushort _nible_size = 0x4;
    private enum OP_FAMILY : byte
    {
        OP0, OP1, OP2, OP3,
        OP4, OP5, OP6, OP7,
        OP8, OP9, OPA, OPB,
        OPC, OPD, OPE, OPF
    }
    public void ExecuteInstruction(Cpu c, Chip8DecodedInst inst)
    {
        Console.WriteLine("Instruction code = {0:x} ; Instruction arguments = {1:x}", inst.OpFamily, inst.Args);
        switch ((OP_FAMILY)inst.OpFamily)
        {
            case OP_FAMILY.OP0:
                Family0Execute(c, inst.Args);
                break;
            case OP_FAMILY.OP1:
                Family1Execute(c, inst.Args);
                break;
            case OP_FAMILY.OP2:
                Family2Execute(c, inst.Args);
                break;
            case OP_FAMILY.OP3:
                Family3Execute(c, inst.Args);
                break;
            case OP_FAMILY.OP4:
                Family4Execute(c, inst.Args);
                break;
            case OP_FAMILY.OP5:
                Family5Execute(c, inst.Args);
                break;
            case OP_FAMILY.OP6:
                Family6Execute(c, inst.Args);
                break;
            case OP_FAMILY.OP7:
                Family7Execute(c, inst.Args);
                break;
            case OP_FAMILY.OP8:
                Family8Execute(c, inst.Args);
                break;
            case OP_FAMILY.OP9:
                Family9Execute(c, inst.Args);
                break;
            case OP_FAMILY.OPA:
                FamilyAExecute(c, inst.Args);
                break;
            case OP_FAMILY.OPB:
                FamilyBExecute(c, inst.Args);
                break;
            case OP_FAMILY.OPC:
                FamilyCExecute(c, inst.Args);
                break;
            case OP_FAMILY.OPD:
                FamilyDExecute(c, inst.Args);
                break;
            case OP_FAMILY.OPE:
                FamilyEExecute(c, inst.Args);
                break;
            case OP_FAMILY.OPF:
                FamilyFExecute(c, inst.Args);
                break;
            default:
                throw new("Invalide OP code");
        }
    }
    public void Family0Execute(Cpu context, ushort args)
    {
        switch (args)
        {
            case 0x0E0:
                context.ClearScreen();
                break;
            case 0x0EE:
                context.PopStack();
                break;
            default:
                //se queda como un NOOP por ahora
                break;
        }
    }
    public void Family1Execute(Cpu c, ushort args)
    {
        c.InternalJump(args);
    }
    public void Family2Execute(Cpu c, ushort args)
    {
        c.ExecuteInternalSubroutine(args);
    }
    public void Family3Execute(Cpu c, ushort args)
    {
        byte reg_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        byte data = (byte)BitHelper.GetMaskValue(args, _byte_mask, 0x0);   

        c.EsquipIfEcuals(reg_id, data);
    }
    public void Family4Execute( Cpu c, ushort args)
    {
        byte reg_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        byte data = (byte)BitHelper.GetMaskValue(args, _byte_mask, 0x0);
        c.EsquipIfNotEcuals(reg_id, data);
    }
    public void Family5Execute(Cpu c, ushort args)
    {
        byte regX_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        byte regY_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size);
        c.EsquipIfRegXEcualsRegY(regX_id, regY_id);
    }
    public void Family6Execute(Cpu c, ushort args)
    {
        byte reg_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        byte data = (byte)BitHelper.GetMaskValue(args, _byte_mask, 0x0);
        c.StoreInReg(reg_id, data);
    }
    public void Family7Execute(Cpu c, ushort args)
    {
        byte reg_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        byte data = (byte)BitHelper.GetMaskValue(args, _byte_mask, 0x0);
        c.AddToReg(reg_id, data);
    }
    public void Family8Execute(Cpu c, ushort args)
    {
        byte regy_id, regx_id;
        byte sub_family_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, 0x0);

        regx_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        regy_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size);
        switch (sub_family_id)
        {
            case 0x00: 
                c.StroeRegYinRegX(regx_id, regy_id);
                break;
            case 0x01:
                c.LogicalOrRegXY(regx_id, regy_id);
                break;
            case 0x02:
                c.LogicalAndRegXY(regx_id, regy_id);
                break;
            case 0x03:
                c.LogicalXorRegXY(regx_id, regy_id);
                break;
            case 0x04:
                c.AddRegisterYtoX(regx_id, regy_id);
                break;
            case 0x05:
                c.SubtractRegYfromX(regx_id, regy_id);
                break;
            case 0x06:
                c.StoreRigthShiftedVYInVX(regx_id, regy_id);
                break;
            case 0x07:
                c.SubtracRegXfromRegY(regx_id, regy_id);
                break;
            case 0x0E:
                c.StoreLefthShifthedVYinVX(regx_id, regy_id);
                break;
        }
            
    }
    public void Family9Execute(Cpu c, ushort args)
    {
        c.EsquipIfRegXNotEqualToRegY(
            (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2),
            (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size)
        );
    }
    public void FamilyAExecute(Cpu c, ushort args)
    {
        c.StoreMemoryAdress(args);
    }
    public void FamilyBExecute(Cpu c, ushort args)
    {
        c.JumpBaseV0(args);
    }
    public void FamilyCExecute( Cpu c, ushort args)
    {
        byte reg_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        byte data = (byte)BitHelper.GetMaskValue(args, _byte_mask, 0x0);
        c.RandomizeReg(reg_id, data);
    }
    public void FamilyDExecute(Cpu c, ushort args)
    {
        byte regX_id, regY_id, img_heigth;
        regX_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        regY_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size);
        img_heigth = (byte)BitHelper.GetMaskValue(args, _nible_mask, 0x0);
        c.DrawToScreen(regX_id, regY_id, img_heigth);
    }
    public void FamilyEExecute(Cpu c, ushort args)
    {
        byte reg_id = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        switch (args & 0x00FF)
        {
            case 0x009E:
                c.SkipIfKeyPressed(reg_id);
                break;
            case 0x00A1:
                c.SkipIfKeyNotPressed(reg_id);
                break;
        }
    }
    public void FamilyFExecute(Cpu c, ushort args)
    {
        byte reg = (byte)BitHelper.GetMaskValue(args, _nible_mask, _nible_size * 2);
        byte code = (byte)BitHelper.GetMaskValue(args, _byte_mask, 0x0);
        switch(code)
        {
            case 0x07:
                c.GetDelayTimer(reg);
                break;
            case 0x15://set or get the delay timer
                c.SetDelayTimer(reg);
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
}