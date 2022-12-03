using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using mono2.Framework;
using mono2.Graphical;

namespace mono2.source.Fishtank
{
    public class Scenario
    {


        public AutonomousAgentOptions AutonomousAgentOptions;
        public Optionsbox optionsBox;

        public static Point worldsize;
        public Background backGround;
        public Scenario()
        {
            resetGameState();

        }

        public void resetGameState()
        {
            GameObjectManager.clear();

            // load background texture
            var backgroundTexture = Game1.gameContent.Load<Texture2D>("underWaterBackground");
            backGround = new Background(backgroundTexture);
            GameObjectManager.addGameObject(backGround);

            worldsize = backGround.getSize();
            BackgroundBorders borders = new BackgroundBorders(worldsize);
            GameObjectManager.addGameObject(borders);

            int numberOfAdditionalAutonomousAgents = 150;
            for (int i = 0; i < numberOfAdditionalAutonomousAgents; i++)
            {
                var randAngle = new Random().NextDouble() * 2 * Math.PI;
                var randX = new Random().NextDouble() * worldsize.X;
                var randY = new Random().NextDouble() * worldsize.Y;

                AutonomousAgent b = new SwarmFish(new Vector2D(randX, randY), Vector2D.getVectorWithLengthAndAngle(1, (float)randAngle));
                GameObjectManager.addGameObject(b);
            }

            numberOfAdditionalAutonomousAgents = 2;
            for (int i = 0; i < numberOfAdditionalAutonomousAgents; i++)
            {
                var randAngle = new Random().NextDouble() * 2 * Math.PI;
                var randX = new Random().NextDouble() * worldsize.X;
                var randY = new Random().NextDouble() * worldsize.Y;

                AutonomousAgent b = new Predatorfish(new Vector2D(randX, randY), Vector2D.getVectorWithLengthAndAngle(1, (float)randAngle));
                GameObjectManager.addGameObject(b);
            }
            
            optionsBox = new Optionsbox(new Vector2D(10, 10));
            GameObjectManager.addGameObject(optionsBox);

            
            //AutonomousAgent bDebug = new Predatorfish(new Vector2D(50, 50), Vector2D.getVectorWithLengthAndAngle(1, (float)0));
            //GameObjectManager.addGameObject(bDebug);
            //Game1.camera.followGameObject(bDebug);
        }
    }

    public class BackgroundBorders : GameObject
    {
        public Vector2D Position => new Vector2D(0, 0);
        Point size;

        public BackgroundBorders(Point _size)
        {
            size = _size;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(new Rectangle((int)Position.CameraRelativePosition.X, (int)Position.CameraRelativePosition.Y, size.X, size.Y), Color.Black);
        }

        public bool getAlive()
        {
            return true;
        }

        public void Update(GameTime gameTime)
        {
        }
    }

    public class Background : GameObject
    {
        public Vector2D position;
        Point size;
        Texture2D background;
        double textureToScreenRatio;
        Point screenSize;

        public Vector2D Position => position;

        public Background(Texture2D background)
        {
            this.background = background;
            resize();
        }

        public void resize()
        { 
            position = new Vector2D(0, 0);

            screenSize = Game1.getSize();
            textureToScreenRatio = background.Height / (double)screenSize.Y;
            var width = background.Width / textureToScreenRatio;
            var height = screenSize.Y;
            size = new Point((int)width, height);
        
        }

        public Point getSize()
        {
            return size;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var destRect = new Rectangle((int)position.CameraRelativePosition.X, (int)position.CameraRelativePosition.Y, size.X, size.Y);
            spriteBatch.Draw(background, destRect, new Rectangle(0, 0, background.Width, background.Height), Color.AliceBlue);
        }

        public bool getAlive()
        {
            return true;
        }

        public void Update(GameTime gameTime)
        {
        }
    }

}
