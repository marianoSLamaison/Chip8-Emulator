using System;
using System.Net.Http.Headers;
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
        scr.Clear();
    }
    public bool SetPixel(Span<byte> buffer, Span<byte> sprite, byte x_pos, byte y_pos)
    {
        const byte _num_blocks_per_row = 8;
        const byte _scr_heigth_bytes = 32;
        const byte _scr_width_bytes = 64; 
        bool _pixels_have_been_set = false;
        byte _curret_pixel_pos;
        for (int i=0; i<sprite.Length; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //sacamos en donde deberia de estar posicionado en teoria
                //esto se hace aqui por que una celda puede cambiar en medio de su dibujado,
                //imagina si tienes que dibujar pero la poscion es justo en el final de la celda
                _curret_pixel_pos = (byte)(((y_pos + i) % _scr_heigth_bytes * _num_blocks_per_row) + (  (x_pos + j % _scr_width_bytes) / _num_blocks_per_row));
                //hacemos un xor con el valor que halla en sprite en esa region en particular
                buffer[_curret_pixel_pos] ^= (byte)(sprite[i] & (0x1 << j));
            }
            
            //_pixels_have_been_set = (buffer[_scr_start_pos + i * _num_blocks_per_row] & sprite[i]) != 0 ? true : _pixels_have_been_set;
            //buffer[_scr_start_pos + i * _num_blocks_per_row] ^= sprite[i];
        }
        return _pixels_have_been_set;
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
                    current_pixel = i * bits_in_byte + 8-j;
                    pixel_rect.X = (current_pixel % scr_width) * pixel_rect.Width + position.X;
                    pixel_rect.Y = (current_pixel / scr_width) * pixel_rect.Height + position.Y;
                    g.Draw(pixel, pixel_rect, Color.MonoGameOrange);
                }
                
            }
        }
    }
}