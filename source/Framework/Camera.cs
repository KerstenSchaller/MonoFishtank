using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using mono2.source.Fishtank;

namespace mono2.Framework
{
    public class Camera : GameObject
    {
        public Vector2D position = new Vector2D();
        GameObject target;
        bool followCamAllowed = false;

        bool FollowCamAllowed
        {
            get { return followCamAllowed; }
            set
            {
                if (value == false) position = new Vector2D(0, 0);
                followCamAllowed = value;
            }
        }

        public void toggleFollowCamAllowed()
        {
            FollowCamAllowed = !followCamAllowed;
        }
        public Vector2D Position => position;

        public void followGameObject(GameObject obj)
        {
            target = obj;

        }

        public void unfollowGameObject()
        {
            target = null;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // nothing to draw
        }

        public bool getAlive()
        {
            return true;
        }

        public void Update(GameTime gameTime)
        {
            if (target != null && followCamAllowed)
            {
                var width = Game1.ScreenWidth;
                var height = Game1.ScreenHeight;
                var shift = new Vector2D(width, height) * 0.5;

                position = target.Position - shift;
                position.X = Math.Max(position.X, 0);
                position.X = Math.Min(position.X, Scenario.worldsize.X - Game1.ScreenWidth);

                position.Y = 0;
            }
        }
    }


}
