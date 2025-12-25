using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EmuUi;

class TextBlock : ui_toolkit.UiBlock{
    private String _text;
    private String[] _displayable_text;
    private Rectangle _rect;
    private SpriteFont _font;
    private int _numb_lines;
    private int _chars_per_line;

    public TextBlock (Rectangle rect, String text)
    {
        _rect = rect;
        _text = text;
    }
    public void Load(ContentManager c)
    {
        _font = c.Load<SpriteFont>("Fonts/standarFont");
        Vector2 textSize = _font.MeasureString(_text);
        _numb_lines = (int)(textSize.X) / _rect.Width+ 1;
        _chars_per_line = (int)(( (float)_text.Length / textSize.X ) * _rect.Width);
        _displayable_text = new String[_numb_lines];
        for (int i=0; i<_displayable_text.Length; i++)
        {
            _displayable_text[i] = _text.Substring(
                _chars_per_line * i, 
                MathHelper.Min(
                    _chars_per_line, 
                    MathHelper.Max(//para que no de calores negativos
                        0, 
                        _text.Length - _chars_per_line * i)
                    )
                );
        }
    }
    public void Draw(SpriteBatch s)
    {
        for (int i=0; i<_displayable_text.Length; i++)
            s.DrawString(_font, _displayable_text[i], Vector2.Zero + Vector2.UnitY * 32 * i, Color.Yellow);
    }

    public Rectangle GetUsedRect() => _rect;

    public void SetPosition(Point pos)
    {
        _rect.Location = pos;
    }
}