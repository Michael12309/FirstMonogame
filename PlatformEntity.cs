using FirstGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

public class PlatformEntity : IEntity
{
    private readonly Game1 _game;
    public IShapeF Bounds { get; }

    public PlatformEntity(Game1 game, RectangleF rectangleF)
    {
        _game = game;
        Bounds = rectangleF;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);
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