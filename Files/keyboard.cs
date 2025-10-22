

using System;
using Microsoft.Xna.Framework.Input;

namespace Chip8Emu.IO;

class Chip8Keyboard
{
    uint _keysStates;
    byte _keyCode;

    public void Update()
    {
        KeyboardState _kstat = Keyboard.GetState();
        uint temp_state = 0;
        foreach (Keys k in _kstat.GetPressedKeys())
        {
            if (GetKeyCode(k) != 180)
            {
                temp_state |= (uint)0x1 << GetKeyCode(k);

            }
        }
        _keysStates = temp_state;
    }
    public bool IsKeyPressed(byte key) => (_keysStates & (0x1 << key)) != 0;
        
    public byte GetKeyPressed() => _keyCode;
    public bool AreKeysPressed() => _keysStates != 0;
    private byte GetKeyCode(Keys k)
    {
        switch (k)
        {
            case Keys.NumPad0:
                return 0x0;
            case Keys.NumPad1:
                return 0x1;
            case Keys.NumPad2:
                return 0x2;
            case Keys.NumPad3:
                return 0x3;
            case Keys.NumPad4:
                return 0x4;
            case Keys.NumPad5:
                return 0x5;
            case Keys.NumPad6:
                return 0x6;
            case Keys.NumPad7:
                return 0x7;
            case Keys.NumPad8:
                return 0x8;
            case Keys.NumPad9:
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
                return 180;
        }
    }
}