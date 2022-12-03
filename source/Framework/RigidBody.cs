using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono2.Framework
{
    public interface IRigidBody
    {
        void applyForce(Vector2D force);

        public Vector2D Position
        {
            get;
            set;
        }

        public Vector2D Orientation
        {
            get;
        }

        public Vector2D Velocity
        {
            get;
            set;
        }
    }
    public class RigidBody : IRigidBody, GameObject
    {
        double mass = 1;
        public Parameter maxSpeed;
        public Parameter maxForce;

        Vector2D position;
        Vector2D orientation = new Vector2D(-1, 0);
        Vector2D velocity;
        Vector2D accaleration;

        public RigidBody(double mass, Parameter maxSpeed, Parameter maxForce, Vector2D position, Vector2D orientation, Vector2D velocity)
        {
            this.mass = mass;
            this.maxSpeed = maxSpeed;
            this.maxForce = maxForce;
            this.position = position;
            this.orientation = orientation;
            this.velocity = velocity;
            accaleration = new Vector2D();

            //GameObjectManager.addGameObject(this);
        }

        ~RigidBody()
        {
            GameObjectManager.removeGameObject(this);
        }


        public Vector2D Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        public Vector2D Orientation
        {
            get { return orientation; }
        }

        public Vector2D Velocity
        {
            get { return velocity; }
            set
            {
                velocity = value;
            }
        }

        public void applyForce(Vector2D force)
        {
            accaleration += force;
        }

        public bool getAlive()
        {
            return true;
        }

        public void Update(GameTime gameTime)
        {
            // update movement
            velocity += accaleration;
            velocity.limitMagnitude(maxSpeed.Value);
            if (velocity.Length() != 0)
            {
                orientation = new Vector2D(velocity.X, velocity.Y);
                orientation.Normalize();
            }


            position.X += velocity.X;
            position.Y += velocity.Y;
            accaleration = new Vector2D(0, 0);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
