using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Chip8Emu.IO;

class Chip8SoundManager
{
    SoundEffect beep;

    public void Load(ContentManager c)
    {
        beep = c.Load<SoundEffect>("audio/sfx_1");
        
    }
}