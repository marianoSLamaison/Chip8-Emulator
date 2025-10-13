namespace Chip8Emu.cpu;

struct CPUContext
{
    public ushort ir;
    public  ushort[] v;//registrys there are 16
    public  byte[] memory;//4096 size memory programs read from 512 onwards
    public  ushort mar;//is supsed to be 12 bits long 
    public byte[] stack;//48 in length and up to 12 levels of nesting
    public byte sp; //stack pointer
    public ushort delay_timer;
    public ushort sound_timer;

    public CPUContext()
    {
        ir = 0;
        v = new ushort[0xF];
        mar = 0;
        memory = new byte[0x1000];
        stack = new byte[0x30];//48 en exa
        sp = 0;
        delay_timer = 0;
        sound_timer = 0;
    }
}