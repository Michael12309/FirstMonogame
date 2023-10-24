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
    private bool _flipHorizontally;
    private Vector2 _boundsOffet;
    private Vector2 _velocity;
    private bool _canJump;

    public Vector2 Position;
    public Animation Animation;

    public IShapeF Bounds { get; }

    public PlayerEntity(Game1 game, RectangleF rectangleF)
    {
        _game = game;
        _flipHorizontally = true;
        _boundsOffet = new Vector2(36, 32);
        _velocity = Vector2.Zero;

        Bounds = rectangleF;
        Position = Bounds.Position;
        Bounds.Position += _boundsOffet;

        Animation = new Animation();

        Animation.LoadSpriteSheet(
            game.Content,
            "cat-spritesheet",
            new Rectangle(0, 0, 140, 87),
            14);

        Animation.MillisecondTimeout = 70;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Animation.spriteSheet,
            Position,
            sourceRectangle: Animation.FrameBoundingBox,
            Color.White,
            0f,
            new Vector2(0, 0),
            Vector2.One,
            _flipHorizontally ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            0.15F);

        //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.A))
        {
            _velocity.X = -0.3f;
            _flipHorizontally = false;
        }
        if (kstate.IsKeyDown(Keys.D))
        {
            _velocity.X = 0.3f;
            _flipHorizontally = true;
        }
        if (kstate.IsKeyUp(Keys.A) && kstate.IsKeyUp(Keys.D))
        {
            _velocity.X = 0;
            Animation.SetRow(1, 9);
        }
        else
        {
            Animation.SetRow(0, 9);
        }

        if (Math.Abs(_velocity.Y) > 0f)
        {
            Animation.SetRow(2, 14);
            int jumpProgression = (int)Math.Clamp(Math.Round(Helper.Map(_velocity.Y, -0.4f, 0.1f, 1f, 14f), 0), 1, 14);
            Animation.UpdateOverTime = false;
            Animation.SetIndex(jumpProgression);
        }
        else
        {
            Animation.UpdateOverTime = true;
        }

        if (kstate.IsKeyDown(Keys.Space) && _canJump)
        {
            _velocity.Y -= 0.5f;
        }
        else
        {
            _velocity.Y += 0.013f;
        }


        Console.WriteLine("velocity " + _velocity);

        Position += _velocity * (float)gameTime.ElapsedGameTime.Milliseconds;
        Bounds.Position = Position + _boundsOffet;

        _canJump = false;

        Animation.Update(gameTime);
    }

    public void OnCollision(CollisionEventArgs collisionInfo)
    {
        Position -= collisionInfo.PenetrationVector;
        Bounds.Position = Position + _boundsOffet;
        _velocity.Y = 0;
        _canJump = true;
    }
}