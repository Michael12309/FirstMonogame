using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public class Animation
{
    public Texture2D spriteSheet;
    private Rectangle frameBoundingBox;
    private int spriteCount;
    private int spriteIndex;

    public Animation()
    {

    }

    public void LoadSpriteSheet(ContentManager content, string assetName, Rectangle spriteSize, int spriteCount)
    {
        spriteSheet = content.Load<Texture2D>(assetName);
        frameBoundingBox = spriteSize;
        this.spriteCount = spriteCount;
        spriteIndex = 0;
    }

    public void Advance()
    {
        spriteIndex++;
        if (spriteIndex >= spriteCount)
        {
            spriteIndex = 0;
        }
        frameBoundingBox = new Rectangle(spriteIndex * frameBoundingBox.Width, frameBoundingBox.Y, frameBoundingBox.Width, frameBoundingBox.Height);
    }

    public Rectangle getFrameBoundingBox()
    {
        return frameBoundingBox;
    }
}