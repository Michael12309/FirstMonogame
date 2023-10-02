using System;
using FirstGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

public class PlayerEntity : IEntity
{
    private readonly Game1 _game;
    public float Speed { get; }
    public Vector2 Velocity;
    public Vector2 Position;

    public IShapeF Bounds { get; }

    public PlayerEntity(Game1 game, float speed, RectangleF rectangleF)
    {
        _game = game;
        Speed = speed;
        Bounds = rectangleF;
        Position = Bounds.Position;

        // playerAnimation.LoadSpriteSheet(
        //     Content,
        //     "cat",
        //     new Rectangle(0, 0, 140, 87),
        //     9);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);

        // _spriteBatch.Draw(
        // playerAnimation.spriteSheet,
        // playerPosition,
        // sourceRectangle: playerAnimation.getFrameBoundingBox(),
        // Color.White,
        // 0f,
        // new Vector2(playerAnimation.getFrameBoundingBox().Width / 2, playerAnimation.getFrameBoundingBox().Height / 2),
        // Vector2.One,
        // flipPlayer ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
        // 0f);
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.A))
        {
            Velocity.X = -1;
        }
        if (kstate.IsKeyDown(Keys.D))
        {
            Velocity.X = 1;
        }
        if (kstate.IsKeyDown(Keys.Space))
        {
            Velocity.Y = -1;
        }

        Velocity += new Vector2(0, 0.03f);

        Position += Velocity * Speed * (float)gameTime.ElapsedGameTime.Milliseconds;
        Bounds.Position = Position;
    }

    public void OnCollision(CollisionEventArgs collisionInfo)
    {
        Position -= collisionInfo.PenetrationVector;
        Bounds.Position = Position;
        Velocity = Vector2.Zero;
    }
}