using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class TestButton
{
    Vector2 _position;
    Texture2D _texture;
    public TestButton(Vector2 _position)
    {
        this._position = _position;
    }
    public void Load(Texture2D _texture)
    {
        this._texture = _texture;
    }
    public void Draw(SpriteBatch s)
    {
        s.Draw(_texture, new Rectangle(_position.ToPoint(), new(100,100)), Color.Red);
    }
    public void Update()
    {

    }
}