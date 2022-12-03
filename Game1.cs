using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using mono2.Framework;
using mono2.source.Fishtank;
using System;

namespace mono2
{

    static class Helper 
    {
        static public Texture2D cropTexture(Texture2D src, GraphicsDevice graphics, Rectangle rect)
        {
            Texture2D tex = new Texture2D(graphics, rect.Width, rect.Height);
            int count = rect.Width * rect.Height;
            Color[] data = new Color[count];
            src.GetData(0, rect, data, 0, count);
            tex.SetData(data);
            return tex;
        }
    }
    public class Game1 : Game
    {
#pragma warning disable IDE0052 // Remove unread private members
        public static GraphicsDeviceManager graphics;
#pragma warning restore IDE0052 // Remove unread private members
        SpriteBatch spriteBatch;
        public static ContentManager gameContent;

        Scenario scenario;
        public static Camera camera = new Camera();



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameContent = Content;
           

        }



        protected override void Initialize()
        {
            base.Initialize();
            initScreen();
            scenario = new Scenario();
            GameObjectManager.addGameObject(camera);

            

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }

        bool rightKeyDown = false;
        bool leftKeyDown = false;
        bool upKeyDown = false;
        bool downtKeyDown = false;
        bool fKeyDown = false;
        bool bKeyDown = false;
        bool sKeyDown = false;
        private bool _isFullscreen = false;
        private bool _isBorderless = false;
        private static int _width;
        private static int _height;
        public static int ScreenWidth;
        public static int ScreenHeight;

        public static Point getSize() { return new Point(ScreenWidth, ScreenHeight); }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
                {
                    camera.position += new Vector2D(10, 0);
                    camera.position.X = Math.Min(camera.position.X, scenario.backGround.getSize().X - ScreenWidth);
                }
                else
                {
                    if (rightKeyDown == false)
                    {
                        scenario.optionsBox.setRightSignal();
                        rightKeyDown = true;
                    }
                }
            }
            else 
            {
                rightKeyDown = false;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt))
                {
                    camera.position += new Vector2D(-10, 0);
                    camera.position.X = Math.Max(camera.position.X, 0);
                }
                else 
                {
                    if (leftKeyDown == false)
                    {
                        scenario.optionsBox.setLeftSignal();
                        leftKeyDown = true;
                    }
                }
            }
            else 
            {
                leftKeyDown = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (upKeyDown == false)
                {
                    scenario.optionsBox.setUpSignal();
                    upKeyDown = true;
                }
            }
            else
            {
                upKeyDown = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (downtKeyDown == false)
                {
                    scenario.optionsBox.setDownSignal();
                    downtKeyDown = true;
                }
            }
            else
            {
                downtKeyDown = false;
            }

            // Reset game
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                scenario.resetGameState();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {

            }

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                if (fKeyDown == false)
                {
                    ToggleFullscreen();
                    this.scenario.backGround.resize();
                    fKeyDown = true;
                }
            }
            else
            {
                fKeyDown = false;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.B))
            {
                if (bKeyDown == false)
                {
                    camera.toggleFollowCamAllowed();
                    bKeyDown = true;
                }
            }
            else
            {
                bKeyDown = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (sKeyDown == false)
                {
                    Parameter.save();
                    scenario.optionsBox.parameterSetIndex.maxValue += 1;
                    sKeyDown = true;
                }
            }
            else
            {
                sKeyDown = false;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.D0)) scenario.optionsBox.setValue(0);
            if (Keyboard.GetState().IsKeyDown(Keys.D1)) scenario.optionsBox.setValue(1);
            if (Keyboard.GetState().IsKeyDown(Keys.D2)) scenario.optionsBox.setValue(2);
            if (Keyboard.GetState().IsKeyDown(Keys.D3)) scenario.optionsBox.setValue(3);
            if (Keyboard.GetState().IsKeyDown(Keys.D4)) scenario.optionsBox.setValue(4);
            if (Keyboard.GetState().IsKeyDown(Keys.D5)) scenario.optionsBox.setValue(5);
            if (Keyboard.GetState().IsKeyDown(Keys.D6)) scenario.optionsBox.setValue(6);
            if (Keyboard.GetState().IsKeyDown(Keys.D7)) scenario.optionsBox.setValue(7);
            if (Keyboard.GetState().IsKeyDown(Keys.D8)) scenario.optionsBox.setValue(8);
            if (Keyboard.GetState().IsKeyDown(Keys.D9)) scenario.optionsBox.setValue(9);

            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                scenario.optionsBox.AlternateControl = true;
            }
            else
            {
                scenario.optionsBox.AlternateControl = false;
            }

            GameObjectManager.Update(gameTime);
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            GameObjectManager.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ToggleFullscreen()
        {
            bool oldIsFullscreen = _isFullscreen;

            if (_isBorderless)
            {
                _isBorderless = false;
            }
            else
            {
                _isFullscreen = !_isFullscreen;
            }

            ApplyFullscreenChange(oldIsFullscreen);
        }
        public void ToggleBorderless()
        {
            bool oldIsFullscreen = _isFullscreen;

            _isBorderless = !_isBorderless;
            _isFullscreen = _isBorderless;

            ApplyFullscreenChange(oldIsFullscreen);
        }

        private void ApplyFullscreenChange(bool oldIsFullscreen)
        {
            if (_isFullscreen)
            {
                if (oldIsFullscreen)
                {
                    ApplyHardwareMode();
                }
                else
                {
                    SetFullscreen();
                }
            }
            else
            {
                UnsetFullscreen();
            }
        }
        private void ApplyHardwareMode()
        {
            graphics.HardwareModeSwitch = !_isBorderless;
            graphics.ApplyChanges();
        }
        private void SetFullscreen()
        {
            _width = Window.ClientBounds.Width;
            _height = Window.ClientBounds.Height;

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;
            graphics.HardwareModeSwitch = !_isBorderless;

            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }
        private void UnsetFullscreen()
        {
            graphics.PreferredBackBufferWidth = _width;
            graphics.PreferredBackBufferHeight = _height;
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
        }

        private void initScreen() 
        {
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 1200;
            ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }

    }



}
