using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D playerTexture;
    Vector2 playerPosition;
    float playerSpeed;
    bool flipPlayer;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                    _graphics.PreferredBackBufferHeight / 2);

        playerSpeed = .5F;
        flipPlayer = false;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        playerTexture = Content.Load<Texture2D>("The_Knight_Idle");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        KeyboardState kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.A))
        {
            playerPosition.X -= playerSpeed * (float)gameTime.ElapsedGameTime.Milliseconds;
            flipPlayer = true;
        }

        if (kstate.IsKeyDown(Keys.D))
        {
            playerPosition.X += playerSpeed * (float)gameTime.ElapsedGameTime.Milliseconds;
            flipPlayer = false;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.ForestGreen);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(
            playerTexture,
            playerPosition,
            null,
            Color.White,
            0f,
            new Vector2(playerTexture.Width / 2, playerTexture.Height / 2),
            Vector2.One,
            flipPlayer ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            0f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
