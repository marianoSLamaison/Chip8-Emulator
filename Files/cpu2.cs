using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Chip8Emu.cpu;
partial class Cpu
{
    public void LoadStateUpToReg(byte reg)
    {
        for (int i=0; i<=reg; i++)
            _v[i] = _mem.ReadByte((ushort)(_mar + i));
    }
    public void SaveStateUpToReg(byte reg)
    {
        for (int i=0; i<=reg; i++)
            _mem.WriteByte((ushort)(_mar + i), _v[i]); 
    }
    public void StoreBinaryCodedDecimalFromReg(byte reg)
    {
        byte hundreds, tens, ones;
        hundreds = (byte)(_v[reg] / 100);
        tens = (byte)((_v[reg] / 10) % 10);
        ones = (byte)(_v[reg] % 10);
        _mem.WriteByte(_mar, hundreds);
        _mem.WriteByte((ushort)(_mar + 1), tens);
        _mem.WriteByte((ushort)(_mar + 2), ones);
    }
    public void SetMarToSpriteData(byte reg)
    {
        _mar = (ushort)(_v[reg] * _text_heigth);
    }
    public void AddToMar(byte reg)
    {
        _mar += _v[reg];
    }
    public void SetSoundTimer(byte reg)
    {
        _sound_timer = _v[reg];
    }
    public void StoreNextKey(byte reg)
    {
        
    }
    public void GetDelayTimer(byte reg)
    {
        _v[reg] = _delay_timer;
    }
    public void SetDelayTimer(byte reg)
    {
        _delay_timer = _v[reg];
    }
    public void SkipIfKeyNotPressed(byte key_code)
    {
        if (!_keyboard.IsKeyPadPressed(key_code))
            _ir +=  _inst_size;
    }
    public void SkipIfKeyPressed(byte key_code)
    {
        if (_keyboard.IsKeyPadPressed(key_code))
            _ir += _inst_size;
    }
    public void DrawToScreen(byte regX_id, byte regY_id, byte img_heigth)
    {
        _v[0x0F] = (byte)(_screen.SetPixel(_mem.GetScreenState(), _mem.GetSprite(_mar, img_heigth), _v[regX_id], _v[regY_id]) ? 1 : 0);
    }
    public void RandomizeReg(byte reg_id, byte data)
    {
        Random rngGen = new();
        _v[reg_id] = (byte)(rngGen.Next() & data);
    }
    public void JumpBaseV0(ushort args)
    {
        _ir = (ushort)(args + _v[0x00]);
        _automatically_increment = false;
    }
    public void StoreMemoryAdress(ushort args)
    {
        _mar = args;
    }
    public void EsquipIfRegXNotEqualToRegY(byte RegXId, byte RegYId)
    {
        if (_v[RegXId] != _v[RegYId])
            _ir +=  _inst_size;
    }
    public void StoreLefthShifthedVYinVX(byte regx_id, byte regy_id)
    {
        byte holder = (byte)((_v[regy_id] & 0x80) >> 0x7);
        _v[regx_id] = (byte)(_v[regy_id] << 0x01);
        _v[regy_id] = _v[regx_id];
        _v[0x0F] = holder;
    }
    public void SubtracRegXfromRegY(byte regx_id, byte regy_id)
    {
        ushort oldv1 = _v[regx_id], oldv2 = _v[regy_id];
        unchecked
        {
            _v[regx_id] = (byte)(_v[regy_id] - _v[regx_id]);
        }
        Console.WriteLine("Needs a burrow to work = {0:b}", BitHelper.SubtractionNeedsBorrows(oldv2, oldv1));
        if (BitHelper.SubtractionNeedsBorrows(oldv2, oldv1))
            _v[0xF] = 0;
        else
            _v[0xF] = 1;
    }
    public void StoreRigthShiftedVYInVX(byte regx_id, byte regy_id)
    {
        byte holder = (byte)(_v[regy_id] & 0x01);
        _v[regx_id] = (byte)(_v[regy_id] >> 0x01);
        _v[regy_id] = _v[regx_id];
        _v[0xF] = holder;
    }
    public void SubtractRegYfromX(byte regx_id, byte regy_id)
    {
        ushort oldv1 = _v[regx_id], oldv2 = _v[regy_id];
        unchecked
        {
            _v[regx_id] -= _v[regy_id];
        }
        Console.WriteLine("Needs a burrow to work = {0:b}", BitHelper.SubtractionNeedsBorrows(oldv1, oldv2));
        if (BitHelper.SubtractionNeedsBorrows(oldv1, oldv2))
            _v[0xF] = 0;
        else
            _v[0xF] = 1;
    }
    public void AddRegisterYtoX(byte regx_id, byte regy_id)
    {
        byte oldv1 = _v[regx_id], oldv2 = _v[regy_id];
        unchecked
        {
            _v[regx_id] += _v[regy_id];
        }
        if (BitHelper.ResultsInCarry(oldv1, _v[regx_id]))
            _v[0xF] = 0x01;
        else
            _v[0xF] = 0x00;
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
            _ir += _inst_size;
    }
    public void EsquipIfNotEcuals(byte reg_id, byte data)
    {
        if (_v[reg_id] != data)
            _ir += _inst_size;
    }
    public void EsquipIfEcuals(byte reg_id, byte data)
    {
        if (_v[reg_id] == data)
            _ir += _inst_size;
    }
    public void ExecuteInternalSubroutine(ushort direction)
    {
        _mem.Write((ushort)(_stack_start + (_sp) * _inst_size), (ushort)(_ir + _inst_size));
        _sp++;
        _ir = direction;
        _automatically_increment = false;
    }
    public void InternalJump(ushort direction)
    {
        _ir = direction;
        _automatically_increment = false;
    }
    public void ClearScreen()
    {
        _screen.Clear(_mem.GetScreenState());
    }
    public void PopStack()
    {
        if (_sp >= 1)
        {
            _ir = _mem.Read((ushort)(_stack_start + (--_sp)*_inst_size));
            _automatically_increment = false;
        }
        else
            throw new("Atempted to pop an empty stack (CHIP8)");
    }
}