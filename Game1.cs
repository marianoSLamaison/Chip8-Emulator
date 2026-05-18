using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Chip8Emu;

namespace Emulator;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    //private Chip8Emu.cpu.Cpu chip8;
    private TestButton _test_button;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        //chip8 = new(GraphicsDevice.Viewport, new(100,100));
        _test_button = new(Vector2.Zero);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData<Color>(new[] {Color.White});
        _test_button.Load(pixel);
        //chip8.Load(Content, GraphicsDevice);
        ///asd
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        //chip8.Update(gameTime);
        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        //chip8.Draw(_spriteBatch);
        _test_button.Draw(_spriteBatch);
        _spriteBatch.End();
        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
