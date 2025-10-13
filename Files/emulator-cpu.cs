using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chip8Emu;

class CHIP8_CPU
{
    private output.Chip8Screen _output_scr;
    private Interpreter _interpreter;
    private ushort _ir;
    private ushort[] _v;//registrys there are 16
    private byte[] _memory;//4096 size memory programs read from 512 onwards
    private ushort _mar;//is supsed to be 12 bits long 
    private byte[] _stack;//48 in length and up to 12 levels of nesting
    private byte _sp; //stack pointer
    private ushort _delay_timer;
    private ushort _sound_timer;
    public CHIP8_CPU(ContentManager c, GraphicsDevice g, int scr_posx, int scr_posy, int scr_width, int scr_heigth)
    {
        _interpreter = new(c, "tests/1-chip8-logo.ch8");
        _output_scr = new(g, scr_posx, scr_posy, scr_width, scr_heigth);
        _ir = 0;
        _v = new ushort[0xF];
        _mar = 0;
        _memory = new byte[0x1000];
        _stack = new byte[0x30];//48 en exa
        _sp = 0;
        _delay_timer = 0;
        _sound_timer = 0;
        for (int i=0; i<10; i++)
        {
            Console.WriteLine("El codigo de la instruccion "+ (i)+ " es "+ _interpreter.GetInstruction(i*2));
        }
    }
    public void Draw(SpriteBatch s)
    {
        _output_scr.Draw(s);
    }
}