

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace FirstGame;

public class TileMap
{
    const string CONTENT_DIRECTORY = "Content/";

    private readonly List<IEntity> _entities = new List<IEntity>();

    private TSJMetadataModel _tileset;
    private TMJMetadataModel _tilemap;

    public TileMap()
    {

    }

    // Must be a json file
    public void LoadMap(Game1 game, CollisionComponent collisionComponent, string tilesetFile, string tilemapFile)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        _tileset = JsonSerializer.Deserialize<TSJMetadataModel>(File.ReadAllText(CONTENT_DIRECTORY + tilesetFile), options);
        _tilemap = JsonSerializer.Deserialize<TMJMetadataModel>(File.ReadAllText(CONTENT_DIRECTORY + tilemapFile), options);

        Texture2D spriteSheet = game.Content.Load<Texture2D>(_tileset.Image);

        for (int i = 0; i < _tilemap.Height; i++)
        {
            for (int j = 0; j < _tilemap.Width; j++)
            {
                int tileId = _tilemap.Layers.First().Data[j + i * _tilemap.Width];

                if (tileId == 0)
                {
                    continue;
                }


                PlatformEntity platform = new PlatformEntity(
                    game,
                    new RectangleF(j * 16 * 4, i * 16 * 4, 16 * 4, 16 * 4),
                    spriteSheet,
                    GetBoundingBoxFromTileId(tileId));

                _entities.Add(platform);

                // The Tiled sprite (tsj) file ids are off by one for some reason
                // compared to the sprite map (tmj) file
                TSJTileModel tile = _tileset.Tiles.Where(p => p.Id == tileId - 1).FirstOrDefault();
                if (tile != null)
                {
                    collisionComponent.Insert(platform);
                }
                else
                {
                    platform.IsCollidable = false;
                }
            }
        }
    }

    private Rectangle GetBoundingBoxFromTileId(int tileId)
    {
        return new Rectangle(
            (tileId % _tileset.Columns - 1) * 16 * 4,
            tileId / _tileset.Columns * 16 * 4,
            16 * 4,
            16 * 4);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (IEntity entity in _entities)
        {
            entity.Draw(spriteBatch);
        }
    }

}