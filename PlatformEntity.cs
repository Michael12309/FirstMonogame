using FirstGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

public class PlatformEntity : IEntity
{
    private readonly Game1 _game;

    // TODO: make staic?
    private Texture2D _spriteSheet;
    private Rectangle _boundingBox;
    public IShapeF Bounds { get; }

    public bool IsCollidable { get; set; }

    public PlatformEntity(Game1 game, RectangleF rectangleF, Texture2D spriteSheet, Rectangle boundingBox)
    {
        _game = game;
        _spriteSheet = spriteSheet;
        _boundingBox = boundingBox;
        Bounds = rectangleF;
        IsCollidable = true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _spriteSheet,
            Bounds.Position,
            sourceRectangle: _boundingBox,
            Color.White,
            0f,
            new Vector2(0, 0),
            Vector2.One,
            SpriteEffects.None,
            0.0F);

        if (IsCollidable)
        {
            //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);
        }
    }

    public void Update(GameTime gameTime)
    {
        // do nothing
    }

    public void OnCollision(CollisionEventArgs collisionInfo)
    {
        // do nothing
    }
}