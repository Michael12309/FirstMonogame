using System;
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
    private Rectangle _spriteBoundingBox;
    private RectangleF? _collider;
    public Vector2 Position { get; set; }
    public IShapeF Bounds { get; }

    public PlatformEntity(Game1 game, Vector2 position, RectangleF? collider, Texture2D spriteSheet, Rectangle spriteBoundingBox)
    {
        _game = game;
        Position = position;
        _collider = collider;
        if (_collider.HasValue)
        {
            _collider = new RectangleF(
                Position.X + _collider.Value.X,
                Position.Y + _collider.Value.Y,
                _collider.Value.Width,
                _collider.Value.Height);
            Bounds = _collider.Value;
        }
        _spriteSheet = spriteSheet;
        _spriteBoundingBox = spriteBoundingBox;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            _spriteSheet,
            Position,
            sourceRectangle: _spriteBoundingBox,
            Color.White,
            0f,
            new Vector2(0, 0),
            Constants.GameScale,
            SpriteEffects.None,
            0.14F);

        if (_collider.HasValue)
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