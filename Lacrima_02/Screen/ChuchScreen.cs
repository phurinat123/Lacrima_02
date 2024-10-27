#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Timers;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;

#endregion

namespace Lacrima_02
{
    public class ChuchScreen : screen
    {

        Game1 game;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;

        private readonly List<IEntity> _entities = new List<IEntity>();
        public readonly CollisionComponent _collisionComponent;

        TiledMapObjectLayer _wallTiledObj, _doorTiledObj;



        public ChuchScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, game.MapWidth, game.MapHeight));

            //Load tilemap 
            _tiledMap = game.Content.Load<TiledMap>("Resources\\mapchuch");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);

            //Get object layers 
            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "Wall_Object")
                {
                    _wallTiledObj = layer;
                }

                if (layer.Name == "Door_Object")
                {
                    _doorTiledObj = layer;
                }
            }

            //Create entities from map 
            foreach (TiledMapObject obj in _wallTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new WallEntity(this.game, new RectangleF(position, obj.Size)));
            }
            foreach (TiledMapObject obj in _doorTiledObj.Objects)
            {
                Point2 position = new Point2(obj.Position.X, obj.Position.Y);
                _entities.Add(new DoorEntity(this.game, new RectangleF(position, obj.Size)));
            }

            //Setup player 
            SpriteSheet playerSheet = game.Content.Load<SpriteSheet>("Resources\\Marcus.sf", new JsonContentLoader());
            _entities.Add(new PlayerEntity(this.game, new RectangleF(new Point2(32, 470), new Size2(48, 48)), new AnimatedSprite(playerSheet)));

            foreach (IEntity entity in _entities)
            {
                _collisionComponent.Insert(entity);
            }

            this.game = game;
        }
        public override void Update(GameTime theTime)
        {
            if (game.hall1 == true)
            {
                ScreenEvent.Invoke(game.mHall1Screen, new EventArgs());
                return;
            }


            foreach (IEntity entity in _entities)
            {
                entity.Update(theTime);
            }
            _collisionComponent.Update(theTime);
            _tiledMapRenderer.Update(theTime);
            base.Update(theTime);
        }
        public override void Draw(SpriteBatch theBatch)
        {
            _tiledMapRenderer.Draw();
           
            foreach (IEntity entity in _entities)
            {
                entity.Draw(theBatch);
            }
            base.Draw(theBatch);
        }


    }
}
