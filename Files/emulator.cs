// Ref: https://multigesture.net/articles/how-to-write-an-emulator-chip-8-interpreter/

namespace Emulator
{//I love C# I don't like microsofth But I loooove C# 
    class CHIP_8
    {
        private static byte[] chip8_fontset = new byte[80]{
            0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
            0x20, 0x60, 0x20, 0x20, 0x70, // 1
            0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
            0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
            0x90, 0x90, 0xF0, 0x10, 0x10, // 4
            0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
            0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
            0xF0, 0x10, 0x20, 0x40, 0x40, // 7
            0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
            0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
            0xF0, 0x90, 0xF0, 0x90, 0x90, // A
            0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
            0xF0, 0x80, 0x80, 0x80, 0xF0, // C
            0xE0, 0x90, 0x90, 0x90, 0xE0, // D
            0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
            0xF0, 0x80, 0xF0, 0x80, 0x80  // F
            };

        private const int memCap = 4096;
        private const int hardwareCap = 16;//para stack, registros y keys
        private const int scrCap = 64 * 32;
        private ushort OpCode;
        private byte[] MemSpace;//hay 4096 o lo que es lo mismo 4kib de memoria ram para trabajar
        private byte[] Regs;//hay un total de 16 registros en la chip-8 todos de un byte de tamaño
        private ushort IndexReg;//registro de direccion ( usado para operaciones que implican memoria )
        private ushort pc;
        //la memoria tiene secciones ya establecidas de antemano
        /*
        0x000 -> 0x1FF es para el interprete de chip-8 osea el programa que va a leer nuestros scripts
        y va a proseguir a traducirlos en ordenes para la chip-8, tambien contiene el font set al parecer

        Ok en otro post sobre interpretes que lei nunca se menciona la distribucion de memoria, perooo
        ya se que la chip-8 esta pensada para maquinas muuy poco potentes a si que mi explicacion para esta
        sitacion de momento es que, como la virtual machine de chip correria como un programa de esas computadoras,
        la memoria fija no es solo por simulación, es tambien por que es lo que debia aclararse que el programa tomaria
        despues de que queda en claro que el programa tiene esto de memoria, se aclara que esos primeros bytes son
        para el verdadero codigo de la vm o inteprete, el resto es del usuario. (Aclaración, es interpretacion mia)
        si estaba mal pongame una issue en el repo.

        0x050 -> 0x0A0 Aqui es donde esta el font set
        0x200 -> 0xFFF aqui va la ROM del codigo del programa junto con su RAM

        Recordatorio que los graficos son hechos en base a hacer XOR logico con el valor anterior
        en pantalla, en caso de que un XOR resulte en un apagado, esto tornara un flag en el VF reg que 
        es lo que revisas para chequear colisiones ( si es muy vago para eso, pero si quieres algo mas preciso,
        vas a tener que hacerlo a mano y solo tenemos 4K para trabajar y yo tengo facultad a si que se trabajara
        con lo que hay)
        */
        private byte[] screen;//pantalla, segun la implementación, tiene una resolucion de 64 * 32 
                              //en el tutorial esta como unsigned char, pero creo que voy a compactarla


        private byte delay_timer;
        private byte sound_timer;
        /*la chip-8 no da soporte a interrupciones, pero tienes estos registros que 
        cada que son seteados a algo diferente de 0, empesaran a descontar al ritmo de 
        1 unidad cada 60 megahertz ( si la velocidad de los clocks esta ligada al clock )
        el buzzer suena cada que el timer llega a 0 el delay lo puedes usar para implementar 
        la version mas primitiva que hay de interrupciones.
        */
        ushort[] stack; //aqui se guarda la ultima instruccion, tiene una profundidad de hasta 16 
        ushort sp; // Stack Pointer osea lo que te recuerda en que posicion del stack estas

        ushort[] keys; //hay 16 keys
        public CHIP_8()
        {


            this.MemSpace = new byte[memCap];
            this.keys = new ushort[hardwareCap];
            this.stack = new ushort[hardwareCap];
            this.screen = new byte[scrCap];
            this.Initialize();
        }
        public void Initialize()
        {
            pc = 0x200;
            OpCode = 0x0;
            IndexReg = 0x0;
            sp = 0x0;

            //limpiar el display
            //limpiar el stack 
            //limpiar los registros
            //limpiar la memoria

            //cargar el font set
            for (int i = 0; i < 88; i++)
            {
                MemSpace[i] = chip8_fontset[i];
            }

            //reiniciar los timers

        }
        /// <summary>
        /// Funcion dedicada a emular un ciclo de CPU ( lo tenemos en una funcion aparte para poder 
        /// controlar mas comodamente la velocidad de ejecucion luego )
        /// </summary>
        public void EmulateCicle()
        {
            //NOTA: Algunos de los op codes son mas largos que un bye, y empiezan igual
            // a si que hay que revisarlos por partes o pensar en otra manera de revisarlos

        }
    }

}