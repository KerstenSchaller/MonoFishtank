using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using mono2.Framework;
using mono2.source.Framework;

namespace mono2.source.Fishtank
{
    public class Fish : AutonomousAgent
    {
        public Texture2D textureLeft;
        public Texture2D textureRight;

        public Vector2 scale;

        public void setScale(double _scale)
        {
            scale = new Vector2(1, 1);
            scale = (float)_scale * scale;
        }

        public Fish(Vector2D InitPos, Vector2D InitVelocity, int _row, int _column, Parameter maxSpeed, Parameter maxForce, IAutonomousAgentPerception autonomousAgentPerception) : base(InitPos, InitVelocity, maxSpeed, maxForce, autonomousAgentPerception)
        {
            var column = _column;
            var row = _row;

            Rectangle sourceRectangle = new Rectangle(column * 16, row * 16, 16, 16);
            Texture2D texture = Game1.gameContent.Load<Texture2D>("fish_all");
            textureLeft = Helper.cropTexture(texture, Game1.graphics.GraphicsDevice, sourceRectangle);

            column = 9 - column;
            sourceRectangle = new Rectangle(column * 16, row * 16, 16, 16);
            texture = Game1.gameContent.Load<Texture2D>("fish_all_flipped");
            textureRight = Helper.cropTexture(texture, Game1.graphics.GraphicsDevice, sourceRectangle);

        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var origin = new Vector2(textureLeft.Width / 2, textureLeft.Height / 2);

            // decide which version of the texture to draw(fish looking left or right)
            if (rigidBody.Orientation.X < 0)
            {
                spriteBatch.Draw(textureLeft, rigidBody.Position.CameraRelativePosition.getVector2(), new Rectangle(0, 0, textureLeft.Width, textureLeft.Height), Color.White, (float)rigidBody.Orientation.getAngle() - (float)Vector2D.Deg2Rad(180 + 45), origin, scale, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(textureRight, rigidBody.Position.CameraRelativePosition.getVector2(), new Rectangle(0, 0, textureRight.Width, textureRight.Height), Color.White, (float)rigidBody.Orientation.getAngle() - (float)Vector2D.Deg2Rad(-45), origin, scale, SpriteEffects.None, 0);
            }

            base.Draw(gameTime, spriteBatch);

            if (debugActive)
            {
                // Draw Debug Line and Trace
                var end = rigidBody.Position + rigidBody.Orientation * 300;
                spriteBatch.DrawLine(rigidBody.Position.CameraRelativePosition.getVector2(), end.CameraRelativePosition.getVector2(), Color.Red);

            }

        }

    }
}
