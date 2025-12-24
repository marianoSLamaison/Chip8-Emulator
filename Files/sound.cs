using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Chip8Emu.IO;

class Chip8SoundManager
{
    SoundEffect beep;
    SoundEffectInstance beep_instance;

    public void Load(ContentManager c)
    {
        beep = c.Load<SoundEffect>("audio/Sfx_01");
        beep_instance = beep.CreateInstance();
        beep_instance.IsLooped = false;
    }
    public void Play()
    {
        if (beep_instance.State != SoundState.Playing)
        {
            beep_instance.Play();
        }
    }
}