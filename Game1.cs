using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D backgroundTexture;
    Texture2D backgroundTexture2;

    Vector2 playerPosition;
    float playerSpeed;
    bool flipPlayer;

    Animation playerAnimation;

    float animationTimer;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                    (int)(_graphics.PreferredBackBufferHeight / 4 * 2.82));

        playerSpeed = .3F;
        flipPlayer = true;
        playerAnimation = new Animation();
        animationTimer = 0F;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        backgroundTexture = Content.Load<Texture2D>("00042");
        backgroundTexture2 = Content.Load<Texture2D>("00043");
        playerAnimation.LoadSpriteSheet(
            Content,
            "cat",
            new Rectangle(0, 0, 140, 87),
            9);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        animationTimer += gameTime.ElapsedGameTime.Milliseconds;
        if (animationTimer > 80)
        {
            animationTimer = 0;
        }

        // TODO: Add your update logic here
        KeyboardState kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.A))
        {
            playerPosition.X -= playerSpeed * (float)gameTime.ElapsedGameTime.Milliseconds;
            flipPlayer = false;

            if (animationTimer == 0)
            {
                playerAnimation.Advance();

            }
        }

        if (kstate.IsKeyDown(Keys.D))
        {
            playerPosition.X += playerSpeed * (float)gameTime.ElapsedGameTime.Milliseconds;
            flipPlayer = true;

            if (animationTimer == 0)
            {
                playerAnimation.Advance();

            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(backgroundTexture2, Vector2.Zero, Color.White);
        _spriteBatch.Draw(
            playerAnimation.spriteSheet,
            playerPosition,
            sourceRectangle: playerAnimation.getFrameBoundingBox(),
            Color.White,
            0f,
            new Vector2(playerAnimation.getFrameBoundingBox().Width / 2, playerAnimation.getFrameBoundingBox().Height / 2),
            Vector2.One,
            flipPlayer ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            0f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}