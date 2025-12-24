

using System;
using Microsoft.Xna.Framework.Input;

namespace Chip8Emu.IO;

class Chip8Keyboard
{
    private ushort _pressedKeys = 0x0000, _oldState = 0x0000;
    private const byte not_a_chip8_key = 182;
    public static readonly byte NOINPUTS = 0xFF;
    public void Update()
    {
        KeyboardState kstat =  Keyboard.GetState();
        Keys[] pressedKeys = kstat.GetPressedKeys();
        byte key_code;
        ushort current_state = 0x0000;
        foreach (Keys k in pressedKeys)
        {
            key_code = GetKeyCode(k);
            if (key_code != not_a_chip8_key)
                current_state |= (ushort)(0x1 << key_code);//decimos que el boton esta apretado
        }  
        _oldState = _pressedKeys;
        _pressedKeys = current_state;
    }
    public bool AreKeysPressed() => _pressedKeys != 0;
    public bool IsKeyPressed(byte id) => (_pressedKeys & (0x1 << id)) != 0;
    public byte GetFirstKeyPressed() 
    {
        for (byte i=0; i< sizeof(uint); i++)
        {
            if (((0x1 << i) & _pressedKeys) != 0)
                return i;
        }
        return NOINPUTS;
    }
    public byte GetFirstKeyReleased()
    {
        uint releasedKeys = (uint)((_pressedKeys ^ _oldState) & _oldState);//si algo estaba prendido en old
                                                                    //pero no en new es que 
                                                                    //fue liberado
        //luego solo hacemos lo mismo que antes
        for (byte i=0; i< sizeof(uint); i++)
        {
            if (((0x1 << i) & releasedKeys) != 0)
                return i;
        }
        return NOINPUTS;      
    }
    //continuar con esto mañana
    private byte GetKeyCode(Keys k)
    {
        switch (k)
        {
            case Keys.NumPad0: case Keys.D0:
                return 0x0;
            case Keys.NumPad1: case Keys.D1:
                return 0x1;
            case Keys.NumPad2: case Keys.D2:
                return 0x2;
            case Keys.NumPad3: case Keys.D3:
                return 0x3;
            case Keys.NumPad4: case Keys.D4:
                return 0x4;
            case Keys.NumPad5: case Keys.D5:
                return 0x5;
            case Keys.NumPad6: case Keys.D6:
                return 0x6;
            case Keys.NumPad7: case Keys.D7:
                return 0x7;
            case Keys.NumPad8: case Keys.D8:
                return 0x8;
            case Keys.NumPad9: case Keys.D9:
                return 0x9;
            ///////Por encima de 9
            case Keys.A:
                return 0xA;
            case Keys.B:
                return 0xB;
            case Keys.C:
                return 0xC;
            case Keys.D:
                return 0xD;
            case Keys.E:
                return 0xE;
            case Keys.F:
                return 0xF;
            default:
                return not_a_chip8_key;
        }
    }
}