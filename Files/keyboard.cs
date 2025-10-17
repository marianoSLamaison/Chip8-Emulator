

using Microsoft.Xna.Framework.Input;

namespace Chip8Emu.IO;

class Chip8Keyboard
{
    KeyboardState kstate;

    
    public bool IsKeyPadPressed(byte key_code)
    {
        //I know this could be all done in a more data driven way with config files and that,
        //But the pain from dealing with seting that up now will aoutweigth the pain of doing this.
        //I fix it in version 3.0
        kstate = Keyboard.GetState();        
        switch(key_code)
        {
            case 0x0:
                return kstate.IsKeyDown(Keys.D0);
            case 0x1:
                return kstate.IsKeyDown(Keys.D1);
            case 0x2:
                return kstate.IsKeyDown(Keys.D2);
            case 0x3:
                return kstate.IsKeyDown(Keys.D3);
            case 0x4:
                return kstate.IsKeyDown(Keys.D4);
            case 0x5:
                return kstate.IsKeyDown(Keys.D5);
            case 0x6:
                return kstate.IsKeyDown(Keys.D6);
            case 0x7:
                return kstate.IsKeyDown(Keys.D7);
            case 0x8:
                return kstate.IsKeyDown(Keys.D8);
            case 0x9:
                return kstate.IsKeyDown(Keys.D9);
            case 0xA:
                return kstate.IsKeyDown(Keys.Q);
            case 0xB:
                return kstate.IsKeyDown(Keys.W);
            case 0xC:
                return kstate.IsKeyDown(Keys.A);
            case 0xD:
                return kstate.IsKeyDown(Keys.S);
            case 0xE:
                return kstate.IsKeyDown(Keys.Z);
            case 0xF:
                return kstate.IsKeyDown(Keys.X);
            default:
                return false;
        }
    }
}