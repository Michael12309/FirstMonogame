using System;
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

    Texture2D BackgroundTexture;
    Vector2 ScreenCenter;

    PlayerEntity PlayerEntity;

    private readonly List<IEntity> _entities = new List<IEntity>();
    private readonly CollisionComponent _collisionComponent;

    float PlayerSpeed;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _collisionComponent = new CollisionComponent(new RectangleF(0, 0, ScreenWidth, ScreenHeight));
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        _graphics.PreferredBackBufferWidth = ScreenWidth;
        _graphics.PreferredBackBufferHeight = ScreenHeight;
        _graphics.ApplyChanges();

        ScreenCenter = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                    _graphics.PreferredBackBufferHeight / 2);

        PlayerSpeed = 0.3f;

        PlayerEntity = new PlayerEntity(this, PlayerSpeed, new RectangleF(ScreenCenter.X, ScreenCenter.Y, 40, 30));
        _entities.Add(PlayerEntity);
        _entities.Add(new PlatformEntity(this, new RectangleF(ScreenCenter.X - 100, ScreenCenter.Y + 100, 200, 20)));
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

        BackgroundTexture = Content.Load<Texture2D>("00042");
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

        _camera.Position = PlayerEntity.Position - ScreenCenter;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        var transformMatrix = _camera.GetViewMatrix();

        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin(transformMatrix: transformMatrix);
        _spriteBatch.Draw(BackgroundTexture, Vector2.Zero, Color.White);

        foreach (IEntity entity in _entities)
        {
            entity.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}