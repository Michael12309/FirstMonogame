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
    public float Speed { get; }
    public Vector2 Velocity;
    public Vector2 Position;

    public Animation Animation;

    public IShapeF Bounds { get; }

    public PlayerEntity(Game1 game, float speed, RectangleF rectangleF)
    {
        _game = game;
        _flipHorizontally = true;
        _boundsOffet = new Vector2(36, 32);

        Speed = speed;
        Bounds = rectangleF;
        Position = Bounds.Position;
        Bounds.Position += _boundsOffet;

        Animation = new Animation();

        Animation.LoadSpriteSheet(
            game.Content,
            "cat-spritesheet",
            new Rectangle(0, 0, 140, 87),
            9);

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

        spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState kstate = Keyboard.GetState();

        Vector2 lastVelocity = Velocity;
        if (kstate.IsKeyDown(Keys.A))
        {
            Velocity.X = -1;
            _flipHorizontally = false;
        }
        if (kstate.IsKeyDown(Keys.D))
        {
            Velocity.X = 1;
            _flipHorizontally = true;
        }
        if (kstate.IsKeyUp(Keys.A) && kstate.IsKeyUp(Keys.D))
        {
            Velocity.X = 0;
            Animation.SetRow(1);
        }
        else
        {
            Animation.SetRow(0);
        }
        if (kstate.IsKeyDown(Keys.Space))
        {
            Velocity.Y = -1;
        }

        Velocity += new Vector2(0, 0.03f);

        if (lastVelocity.X < 0 && Velocity.X > 0 || lastVelocity.X > 0 && Velocity.X < 0)
        {
            Animation.Restart();
        }

        Position += Velocity * Speed * (float)gameTime.ElapsedGameTime.Milliseconds;
        Bounds.Position = Position + _boundsOffet;

        Animation.Update(gameTime);
    }

    public void OnCollision(CollisionEventArgs collisionInfo)
    {
        Position -= collisionInfo.PenetrationVector;
        Bounds.Position = Position + _boundsOffet;
        Velocity = Vector2.Zero;
    }
}