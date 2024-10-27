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
    public class Hall1Screen : screen
    {
        Game1 game;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;

        public Hall1Screen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Load tilemap 
            _tiledMap = game.Content.Load<TiledMap>("Resources\\hall1");
            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);

            this.game = game;
        }
        public override void Update(GameTime theTime)
        {
            _tiledMapRenderer.Update(theTime);
            base.Update(theTime);
        }

        public override void Draw(SpriteBatch theBatch)
        {
            _tiledMapRenderer.Draw();
            base.Draw(theBatch);
        }
    }
}
