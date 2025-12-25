using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ui_toolkit;
/// <summary>
/// This is a basic block of UI, since the idea of resizing it brings some trouble
/// I decided to let that to an specific interfase later. 
/// you use this to have a better hold of a set of ui objects and how to adjust them later
/// </summary>
interface UiBlock
{
    /// <summary>
    /// Returns the required rectangle to hold the block
    /// </summary>
    /// <returns></returns>
    public Rectangle GetUsedRect();
    /// <summary>
    /// changes the position of the block
    /// </summary>
    /// <param name="pos">a position in screen cords</param>
    public void SetPosition(Point pos);
    public void Draw(SpriteBatch s);
    public void Load(ContentManager c);
}