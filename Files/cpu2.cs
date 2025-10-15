namespace Chip8Emu.cpu;
partial class Cpu
{
    public void LoadStateUpToReg(byte reg)
    {   
    }
    public void SaveStateUpToReg(byte reg)
    {   
    }
    public void StoreBinaryCodedDecimalFromReg(byte reg)
    {   
    }
    public void SetMarToSpriteData(byte reg)
    {   
    }
    public void AddToMar(byte reg)
    {   
    }
    public void SetSoundTimer(byte reg)
    {   
    }
    public void StoreNextKey(byte reg)
    {   
    }
    public void GetDelayTimer(byte reg)
    {   
    }
    public void SetDelayTimer(byte reg)
    {   
    }
    public void SkipIfKeyNotPressed(byte key_code)
    {   
    }
    public void SkipIfKeyPressed(byte key_code)
    {   
    }
    public void DrawToScreen(byte regX_id, byte regY_id, byte img_heigth)
    {   
    }
    public void RandomizeReg(byte reg_id, byte data)
    {
    }
    public void JumpBaseV0(ushort args)
    {
    }
    public void StoreMemoryAdress(ushort args)
    {
    }
    public void EsquipIfRegXNotEqualToRegY(byte RegXId, byte RegYId)
    {
    }
    public void StoreLefthShifthedVYinVX(byte regx_id, byte regy_id)
    {

    }
    public void SubtracRegXfromRegY(byte regx_id, byte regy_id)
    {
        
    }
    public void StoreRigthShiftedVYInVX(byte regx_id, byte regy_id)
    {  
         
    }
    public void SubtractRegYfromX(byte regx_id, byte regy_id)
    {
        ushort oldv1 = _v[regx_id], oldv2 = _v[regy_id];
        unchecked
        {
            _v[regx_id] -= _v[regy_id];
        }
        if (BitHelper.SubtractionNeedsBorrows(oldv1, oldv2))
            _v[0xF] = 0;
        else
            _v[0xF] = 1;
    }
    public void AddRegisterYtoX(byte regx_id, byte regy_id)
    {
        ushort oldv1 = _v[regx_id], oldv2 = _v[regy_id];
        unchecked
        {

            _v[regx_id] += _v[regy_id];
            
        }
        if (BitHelper.ResultsInCarry(oldv1, oldv2))
            _v[0xF] = 1;
        else
            _v[0xF] = 0;
    }
    public void LogicalXorRegXY(byte regx_id, byte regy_id)
    {
        _v[regx_id] ^= _v[regy_id];
    }
    public void LogicalAndRegXY(byte regx_id, byte regy_id)
    {
        _v[regx_id] &= _v[regy_id];        
    }
    public void LogicalOrRegXY(byte regx_id, byte regy_id)
    {
        _v[regx_id] |= _v[regy_id];
    }
    public void StroeRegYinRegX(byte regx_id, byte regy_id)
    {
        _v[regx_id] = _v[regy_id];
    }
    public void AddToReg(byte reg_id, byte data)
    {
        _v[reg_id] += data;
    }
    public void StoreInReg(byte reg_id, byte data)
    {
        _v[reg_id] = data;
    }
    public void EsquipIfRegXEcualsRegY(byte regX_id, byte regY_id)
    {
        if (_v[regX_id] == _v[regY_id])
            _ir += 2;
    }
    public void EsquipIfNotEcuals(byte reg_id, byte data)
    {
        if (_v[reg_id] != data)
            _ir += 2;
    }
    public void EsquipIfEcuals(byte reg_id, byte data)
    {
        if (_v[reg_id] == data)
            _ir += 2;
    }
    public void ExecuteInternalSubroutine(ushort direction)
    {
        //escribimos en el stack
        _mem.Write((ushort)(_stack_start + _sp), ++_ir);
        _ir = direction;
    }
    public void InternalJump(ushort direction)
    {
        _ir = direction;
    }
    public void ClearScreen()
    {
        _screen.Clear(_mem.GetScreenState());
    }
    public void PopStack()
    {
        if (_sp >= 1)
            _ir = _mem.Read((ushort)(_stack_start + --_sp));
        else
            throw new("Atempted to pop an empty stack (CHIP8)");
    }
}