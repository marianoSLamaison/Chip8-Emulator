using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chip8Emu.IO;

class Chip8Screen
{
    Texture2D pixel;
    Rectangle pixel_rect;
    Point position;
    const byte bits_in_byte = 8;
    const byte scr_width = 64;
    const byte scr_heigth = 32;
    const byte scr_w_bytes = scr_width / bits_in_byte;
    public Chip8Screen(int x, int y, int _scr_width, int _scr_heigth)
    {
        position = new(x, y);
        int min_dim = MathHelper.Min(_scr_width / scr_width, _scr_heigth / scr_heigth);
        pixel_rect = new(position, new(min_dim));
    }
    public void Clear(Span<byte> scr)
    {//set every byte to 0
        foreach (ref byte reg in scr)
            reg = 0;
    }
    public void Load(GraphicsDevice g)
    {
        pixel = new(g, 1, 1);
        pixel.SetData(new Color[] { Color.White });
    }

    public void Draw(SpriteBatch g, Span<byte> scr_state)
    {
        int current_pixel;
        for (int i=0; i<scr_state.Length; i++ )
        {
            for (byte j=0; j<bits_in_byte; j++)
            {
                if ((scr_state[i] & (1<<j) )!= 0)
                {
                    current_pixel = i * bits_in_byte + j;
                    pixel_rect.X = (current_pixel % scr_width) * pixel_rect.Width + position.X;
                    pixel_rect.Y = (current_pixel / scr_width) * pixel_rect.Height + position.Y;
                    g.Draw(pixel, pixel_rect, Color.MonoGameOrange);
                }
                
            }
        }
    }
}