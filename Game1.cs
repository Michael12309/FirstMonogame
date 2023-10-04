using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.ViewportAdapters;

namespace FirstGame;


public class Game1 : Game
{
    const int ScreenWidth = 1280;
    const int ScreenHeight = 720;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private OrthographicCamera _camera;

    Texture2D BackgroundLayerSix;
    Texture2D BackgroundLayerFive;
    Texture2D BackgroundLayerFour;
    Texture2D BackgroundLayerThree;
    Texture2D BackgroundLayerPlatform;
    Texture2D BackgroundLayerOne;

    Vector2 ScreenCenter;

    PlayerEntity PlayerEntity;

    private readonly List<IEntity> _entities = new List<IEntity>();
    private readonly CollisionComponent _collisionComponent;

    float PlayerSpeed;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _collisionComponent = new CollisionComponent(new RectangleF(0, 0, ScreenWidth * 2, ScreenHeight));
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.ApplyChanges();

        ScreenCenter = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                    _graphics.PreferredBackBufferHeight / 2);

        PlayerSpeed = 0.3f;

        PlayerEntity = new PlayerEntity(this, PlayerSpeed, new RectangleF(0, 0, 70, 35));
        _entities.Add(PlayerEntity);
        _entities.Add(new PlatformEntity(this, new RectangleF(ScreenCenter.X - 1000, ScreenCenter.Y + 100, 2000, 20)));
        foreach (IEntity entity in _entities)
        {
            _collisionComponent.Insert(entity);
        }

        var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        _camera = new OrthographicCamera(viewportAdapter);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        BackgroundLayerSix = Content.Load<Texture2D>("sixth-layer");
        BackgroundLayerFive = Content.Load<Texture2D>("fifth-layer");
        BackgroundLayerFour = Content.Load<Texture2D>("fourth-layer");
        BackgroundLayerThree = Content.Load<Texture2D>("third-layer");
        BackgroundLayerPlatform = Content.Load<Texture2D>("platform-layer");
        BackgroundLayerOne = Content.Load<Texture2D>("first-layer");
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kstate = Keyboard.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kstate.IsKeyDown(Keys.Escape))
            Exit();

        foreach (IEntity entity in _entities)
        {
            entity.Update(gameTime);
        }
        _collisionComponent.Update(gameTime);

        _camera.Position = new Vector2(PlayerEntity.Position.X - ScreenCenter.X, _camera.Position.Y);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        var transformMatrix = _camera.GetViewMatrix();

        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, transformMatrix: transformMatrix);

        _spriteBatch.Draw(BackgroundLayerSix, Vector2.Zero, null, Color.White, 0F, Vector2.Zero, 1F, SpriteEffects.None, 0.6F);
        _spriteBatch.Draw(BackgroundLayerFive, Vector2.Zero, null, Color.White, 0F, Vector2.Zero, 1F, SpriteEffects.None, 0.5F);
        _spriteBatch.Draw(BackgroundLayerFour, Vector2.Zero, null, Color.White, 0F, Vector2.Zero, 1F, SpriteEffects.None, 0.4F);
        _spriteBatch.Draw(BackgroundLayerThree, Vector2.Zero, null, Color.White, 0F, Vector2.Zero, 1F, SpriteEffects.None, 0.3F);
        _spriteBatch.Draw(BackgroundLayerPlatform, Vector2.Zero, null, Color.White, 0F, Vector2.Zero, 1F, SpriteEffects.None, 0.2F);
        _spriteBatch.Draw(BackgroundLayerOne, Vector2.Zero, null, Color.White, 0F, Vector2.Zero, 1F, SpriteEffects.None, 0.1F);

        foreach (IEntity entity in _entities)
        {
            entity.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}