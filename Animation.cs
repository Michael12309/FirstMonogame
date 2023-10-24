using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FirstGame;

public class Animation
{
    private int _spriteCount;
    private int _spriteIndex;
    private int _spriteRow;
    private int _millisecondTimer;

    public Texture2D spriteSheet;
    public Rectangle FrameBoundingBox { get; set; }

    public int MillisecondTimeout { get; set; }

    public bool UpdateOverTime { get; set; }
    public bool Loop { get; set; }

    public Animation()
    {
        _millisecondTimer = 0;
        _spriteRow = 0;
        _spriteIndex = 0;
        UpdateOverTime = true;
        Loop = true;
    }

    public void LoadSpriteSheet(ContentManager content, string assetName, Rectangle spriteSize, int spriteCount)
    {
        spriteSheet = content.Load<Texture2D>(assetName);
        FrameBoundingBox = spriteSize;
        _spriteCount = spriteCount;
    }

    public void Update(GameTime gameTime)
    {
        if (UpdateOverTime)
        {
            _millisecondTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (_millisecondTimer < MillisecondTimeout)
            {
                return;
            }
            _millisecondTimer = 0;

            _spriteIndex++;
            if (_spriteIndex >= _spriteCount)
            {
                if (Loop == true)
                {
                    _spriteIndex = 0;
                }
                else
                {
                    _spriteIndex = _spriteCount;
                }
            }
        }
        FrameBoundingBox = new Rectangle(_spriteIndex * FrameBoundingBox.Width, _spriteRow * FrameBoundingBox.Height, FrameBoundingBox.Width, FrameBoundingBox.Height);
    }

    public void SetRow(int row, int spriteCount)
    {
        _spriteRow = row;
        _spriteCount = spriteCount;
    }

    public void SetIndex(int index)
    {
        if (index < _spriteCount)
        {
            _spriteIndex = index;
        }
    }

    public void Restart()
    {
        _spriteIndex = 0;
    }
}