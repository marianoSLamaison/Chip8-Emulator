using System;
using Chip8Emu.cpu.Operations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chip8Emu.cpu;

partial class Cpu
{
    private IO.Chip8Screen _screen;
    private IO.Chip8Keyboard _keyboard;
    private Memory _mem;
    private FunctionRunner _function_runner;
    //Registers//////////////////////////////////////////////
    private ushort _ir;
    private byte[] _v;//registrys there are 16
    private ushort _mar;//is supsed to be 12 bits long it's also the I in the docs, but
    //I'm implementing the thing so I called as it's called in my text book for computer
    //architecture
    private byte _sp; //stack pointer
    public byte _delay_timer;
    public byte _sound_timer;
    ///////////////////////////////////////////////////////////
    private float _time_between_frames = 0.0f;
    private bool _automatically_increment = true;

    public Cpu(Viewport view, Point position)
    {
        _mem = new();
        _screen = new(position.X, position.Y, view.Width / 2, view.Height / 2);
        _keyboard = new();
        _function_runner = new();
        ///Registers  
        _ir = 0x200;
        _v = new byte[0x10];
        _mar = 0;
        _sp = 0;
        _delay_timer = 0;
        _sound_timer = 0;
    }
    public void Instruction_cicle()
    {
        ushort raw_inst = fetch();
        //Console.WriteLine("Instruccion = {0:x}", raw_inst);
        Chip8DecodedInst inst = decode(raw_inst);
        execute(inst);
        if (_automatically_increment)
        {
            _ir += _inst_size;
            _ir = (ushort)(_ir >= 4096 - _inst_size ? 0x200 : _ir);
        }
        else
            _automatically_increment = true;
    }
    private ushort fetch() => _mem.Read(_ir);
    private Chip8DecodedInst decode(ushort inst)
    {
        byte op_family = (byte)((inst & 0xF000) >> 0x0C);
        ushort args = (ushort)(inst & 0x0FFF);
        return new(op_family, args);
    }
    /// <summary>
    /// Look, I know this is kinda dificult to follow,
    /// but Every instruction familie has it's how conditions to run X or Y method,
    /// And way of getting it's data from it, so I decided that life is easier if you have
    /// one guy whos job is to decode all that, whe here just get the instruction family 
    /// (because I like to stick to the instruction cicle) and tell it to do it's magic
    /// </summary>
    /// <param name="inst"></param>
    private void execute(Chip8DecodedInst inst)
    {
        _function_runner.ExecuteInstruction(this , inst);
    }
    public void Update(GameTime t)
    {
        if (_time_between_frames > 0.05)
        {
            _time_between_frames = 0.0f;
            Instruction_cicle();
        }
        _time_between_frames += (float)t.ElapsedGameTime.TotalSeconds;
    }

    public void Load(ContentManager c, GraphicsDevice g)
    {
        _mem.Load("tests/4-flags.ch8", c);
        _screen.Load(g);
    }
    
    public void Draw(SpriteBatch s)
    {
        _screen.Draw(s, _mem.GetScreenState());
    }

}