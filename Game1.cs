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
    Texture2D BackgroundLayerOne;

    Vector2 ScreenCenter;

    PlayerEntity PlayerEntity;

    private readonly List<IEntity> _entities = new List<IEntity>();
    private readonly CollisionComponent _collisionComponent;

    private TileMap _tileMap;

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
        _graphics.SynchronizeWithVerticalRetrace = true;
        _graphics.ApplyChanges();

        ScreenCenter = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                    _graphics.PreferredBackBufferHeight / 2);

        //                                                   x, y, bounding width, bounding height
        PlayerEntity = new PlayerEntity(this, new RectangleF(200, 0, 70, 35));
        _entities.Add(PlayerEntity);
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

        _tileMap = new TileMap();
        _tileMap.LoadMap(this, _collisionComponent, "platforms.tsj", "platforms.tmj");

        BackgroundLayerSix = Content.Load<Texture2D>("sixth-layer");
        BackgroundLayerFive = Content.Load<Texture2D>("fifth-layer");
        BackgroundLayerFour = Content.Load<Texture2D>("fourth-layer");
        BackgroundLayerThree = Content.Load<Texture2D>("third-layer");
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

        // sampleerState: PointClamp == liner upscale
        _spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);

        Vector3 paralax = new Vector3(-1f, 0f, 0f) * _camera.GetViewMatrix().Translation;
        _spriteBatch.Draw(BackgroundLayerSix, new Vector2(paralax.X, paralax.Y), null, Color.White, 0F, Vector2.Zero, Constants.GameScale, SpriteEffects.None, 0.6F);
        paralax = new Vector3(0.3f, 0f, 0f) * _camera.GetViewMatrix().Translation;
        _spriteBatch.Draw(BackgroundLayerFive, new Vector2(paralax.X, paralax.Y), null, Color.White, 0F, Vector2.Zero, Constants.GameScale, SpriteEffects.None, 0.5F);
        paralax = new Vector3(0.2f, 0f, 0f) * _camera.GetViewMatrix().Translation;
        _spriteBatch.Draw(BackgroundLayerFour, new Vector2(paralax.X, paralax.Y), null, Color.White, 0F, Vector2.Zero, Constants.GameScale, SpriteEffects.None, 0.4F);
        paralax = new Vector3(0.1f, 0f, 0f) * _camera.GetViewMatrix().Translation;
        _spriteBatch.Draw(BackgroundLayerThree, new Vector2(paralax.X, paralax.Y), null, Color.White, 0F, Vector2.Zero, Constants.GameScale, SpriteEffects.None, 0.3F);
        paralax = new Vector3(-0.1f, 0f, 0f) * _camera.GetViewMatrix().Translation;
        _spriteBatch.Draw(BackgroundLayerOne, new Vector2(paralax.X, paralax.Y), null, Color.White, 0F, Vector2.Zero, Constants.GameScale, SpriteEffects.None, 0.1F);

        _tileMap.Draw(_spriteBatch);

        foreach (IEntity entity in _entities)
        {
            entity.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}