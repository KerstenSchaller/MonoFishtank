using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono2.Framework
{
    public interface GameObject
    {
        public bool getAlive();
        public void Update(GameTime gameTime);
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public Vector2D Position
        {
            get;
        }

    }

    public interface DrawAble
    {
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }





}
