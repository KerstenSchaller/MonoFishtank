using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono2.Framework
{
    public class CircleObject : GameObject
    {
        Vector2D center;
        double radius;
        Color color = Color.White;
        bool alive = true;

        public Vector2D Position => center;

        public CircleObject(Vector2D center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public void setColor(Color c) { color = c; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(center.CameraRelativePosition.getVector2(), (float)radius, 50, color, 3);
            alive = false;
        }

        public void Update(GameTime gameTime)
        {
        }

        public bool getAlive()
        {
            return alive;
        }
    }

    public class ArcObject : GameObject
    {
        Vector2D center;
        Vector2D orientation;
        double radius;
        double angle;
        Color color = Color.White;
        bool alive = true;

        public Vector2D Position => center;



        public ArcObject(Vector2D center, double radius, Vector2D Orientation, double Angle)
        {
            this.center = center;
            this.radius = radius;
            orientation = Orientation;
            angle = Angle;
        }

        public void setColor(Color c) { color = c; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawArc(center.CameraRelativePosition.getVector2(), (float)radius, 50, (float)orientation.getAngle(), (float)angle, color);
            alive = false;
        }

        public void Update(GameTime gameTime)
        {
        }

        public bool getAlive()
        {
            return alive;
        }
    }

    public class PointObject : CircleObject
    {
        public PointObject(Vector2D _position) : base(_position, 5)
        {
            setColor(Color.Purple);
        }
    }

    public class LineObject : GameObject
    {
        Vector2D start;
        Vector2D end;
        Color color = Color.White;
        bool alive = true;

        public Vector2D Position => null;



        public LineObject(Vector2D Start, Vector2D End)
        {
            start = Start;
            end = End;
        }

        public void setColor(Color c) { color = c; }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Primitives2D.DrawPoints(spriteBatch, new Vector2(0, 0), new List<Vector2>() { start.CameraRelativePosition.getVector2(), end.CameraRelativePosition.getVector2() }, color, 3);
            alive = false;
        }

        public void Update(GameTime gameTime)
        {
        }

        public bool getAlive()
        {
            return alive;
        }
    }

    public class Trace : GameObject
    {
        List<Vector2D> vertices = new List<Vector2D>();


        // simple circular buffer behaviour for vertices
        int currentStep = 0;
        int devider;
        int maxVertices;

        public Vector2D Position => null;
        public Trace(int updateRate, int MaxVerices)
        {
            devider = updateRate;
            maxVertices = MaxVerices;
        }
        public void addPosition(Vector2D position)
        {
            if (currentStep == devider)
            {
                if (vertices.Count == maxVertices)
                {
                    vertices.Remove(vertices[0]);
                }
                vertices.Add(position);

                currentStep = 0;
            }
            else
            {
                currentStep++;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 1; i < vertices.Count; i++)
            {
                spriteBatch.DrawLine(vertices[i - 1].CameraRelativePosition.getVector2(), vertices[i].CameraRelativePosition.getVector2(), Color.Green);
            }

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
