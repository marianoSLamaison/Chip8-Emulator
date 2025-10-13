using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chip8Emu;

class CHIP8_CPU
{
    private output.Chip8Screen _output_scr;

    public CHIP8_CPU(int scr_posx, int scr_posy, int scr_width, int scr_heigth)
    {
        _output_scr = new(scr_posx, scr_posy, scr_width, scr_heigth);
    }
    //TODO: 
    //1 Make it so the emulator loads with a load method like it should
    //2 Fix the constructor so you can adjust the aspec ratio of the pixels
    //3 Start implementing instructions handling logic
    public void Load(ContentManager c, GraphicsDevice g)
    {
        _output_scr.Load(g);

    }
    public void Draw(SpriteBatch s)
    {
        _output_scr.Draw(s);
    }
    public void Update(GameTime time)
    {

    }
    private void ExecuteInst(ushort raw_inst)
    {

    }
}