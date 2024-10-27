using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Screens;
using Microsoft.Xna.Framework.Content;
using System;

namespace Lacrima_02
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public ChuchScreen mChuchScreen;
        public TitleScreen mTitleScreen;
        public LevelScreen mLevelScreen;
        public Hall1Screen mHall1Screen;
        public screen mCurrentScreen;

        public int MapWidth = 1280;
        public int MapHeight = 720;

        public bool level_2 = false;
        public bool level_3 = false;
        public bool hall1 = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = MapWidth;
            graphics.PreferredBackBufferHeight = MapHeight;
            graphics.ApplyChanges();
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mChuchScreen = new ChuchScreen(this, new EventHandler(GameplayScreenEvent));
            mTitleScreen = new TitleScreen(this, new EventHandler(GameplayScreenEvent));
            mLevelScreen = new LevelScreen(this, new EventHandler(GameplayScreenEvent));
            mHall1Screen = new Hall1Screen(this, new EventHandler(GameplayScreenEvent));
            mCurrentScreen = mTitleScreen;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mCurrentScreen.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            mCurrentScreen.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void GameplayScreenEvent(object obj, EventArgs e)
        {
            mCurrentScreen = (screen)obj;

            if (mCurrentScreen is TitleScreen)
            {
                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 720;
            }
            else if (mCurrentScreen is ChuchScreen)
            {
                graphics.PreferredBackBufferWidth = 640;
                graphics.PreferredBackBufferHeight = 896;
            }
            else if (mCurrentScreen is LevelScreen)
            {
                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 720;
            }

            graphics.ApplyChanges();
        }

        public int GetMapWidth()
        {
            return MapWidth;
        }

        public int GetMapHeight()
        {
            return MapHeight;
        }
    }
}
