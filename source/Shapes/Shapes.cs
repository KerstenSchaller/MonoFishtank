using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using mono2.Framework;

namespace mono2.Shapes
{
    public class AutonomousAgentShape : GameObject
    {
        // variables to define shape
        int mid_x = 1;
        int left_x = 0;
        int right_x = 2;
        int lower_y = 0;
        int mid_y = 1;
        int front_y = 4;
        Vector2D front, left, mid, right;

        Color color = Color.Black;

        // lists for filling structure with lines
        List<Vector2D> lowPairs = new List<Vector2D>();
        List<Vector2D> highPairs = new List<Vector2D>();

        List<Vector2D> lowPairsRotated = new List<Vector2D>();
        List<Vector2D> highPairsRotated = new List<Vector2D>();

        // rotation and movement
        Vector2D rotationCenter;
        double rotationAngle = 0;
        Vector2D position;

        public Vector2D Position => position;

        public void setColor(Color _color)
        {
            color = _color;
        }

        public AutonomousAgentShape(int scale)
        {
            // shift

            left_x -= mid_x;
            right_x -= mid_x;
            lower_y -= mid_y;
            front_y -= mid_y;


            mid_x -= mid_x;
            mid_y -= mid_y;

            // scale
            mid_x *= scale;
            left_x *= scale;
            right_x *= scale;
            lower_y *= scale;
            mid_y *= scale;
            front_y *= scale;


            // init points according to shape
            front = new Vector2D(mid_x, front_y);
            left = new Vector2D(left_x, lower_y);
            mid = new Vector2D(mid_x, mid_y);
            right = new Vector2D(right_x, lower_y);

            fillShape();
            rotationCenter = new Vector2D(mid_x, mid_y);
            //setRotation(0);

        }



        // function for getting a y value for an x on the shape(lower boundary)
        int getLowerY(int x)
        {
            double slope = 0;
            double c = 0;
            if (x == mid.X) return front_y;
            if (x < mid.X)
            {
                slope = (front_y - lower_y) / (mid_x - left_x);
                c = lower_y - left_x * slope;
            }
            else
            if (x > mid.X)
            {
                slope = (lower_y - front_y) / (right_x - mid_x);
                c = lower_y - right_x * slope;
            }
            var result = (int)(slope * x + c);
            return result;
        }

        // function for getting a y value for an x on the shape(upper boundary)
        int getUpperY(int x)
        {
            double slope = 0;
            double c = 0;
            if (x == mid.X) return mid_y;
            if (x < mid.X)
            {
                slope = (mid_y - lower_y) / (mid_x - left_x);
                c = lower_y - left_x * slope;
            }
            if (x > mid.X)
            {
                slope = (lower_y - mid_y) / (right_x - mid_x);
                c = lower_y - right_x * slope;
            }
            return (int)(slope * x + c);
        }

        // define points for lines which fill shape
        void fillShape()
        {
            lowPairs.Clear();
            highPairs.Clear();
            for (int x = left_x; x <= right_x; x++)
            {

                //lowPairs.Add(new Vector2D(x, getLowerY(x)));
                //highPairs.Add(new Vector2D(x, getUpperY(x)));
                highPairs.Add(new Vector2D(x, getLowerY(x)));
                lowPairs.Add(new Vector2D(x, getUpperY(x)));
            }
        }

        //rotate point with angle arround given point
        Vector2D rotatePoint(Vector2D point)
        {
            return point.rotateVector(rotationAngle, rotationCenter);
        }






        // do rotation for all points
        void rotateAll()
        {
            front = new Vector2D(mid_x, front_y);
            left = new Vector2D(left_x, lower_y);
            mid = new Vector2D(mid_x, mid_y);
            right = new Vector2D(right_x, lower_y);

            front = rotatePoint(front);
            left = rotatePoint(left);
            mid = rotatePoint(mid);
            right = rotatePoint(right);

            lowPairsRotated.Clear();
            highPairsRotated.Clear();
            for (int i = 0; i < highPairs.Count; i++)
            {
                lowPairsRotated.Add(rotatePoint(lowPairs[i]));
                highPairsRotated.Add(rotatePoint(highPairs[i]));
            }

        }

        // set new position
        public void moveTo(Vector2D newPos)
        {
            position = newPos;
        }

        public void setRotation(double angle)
        {
            rotationAngle = angle;
            rotateAll();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine((position + left).getVector2(), (position + front).getVector2(), Color.Black); // left to front
            spriteBatch.DrawLine((position + front).getVector2(), (position + right).getVector2(), Color.Black);// front to right
            spriteBatch.DrawLine((position + right).getVector2(), (position + mid).getVector2(), Color.Black);// right to mid
            spriteBatch.DrawLine((position + mid).getVector2(), (position + left).getVector2(), Color.Black);// mid to left

            for (int i = 0; i < lowPairs.Count; i++)
            {
                if (lowPairs[i].X != highPairs[i].X || lowPairs[i].Y != highPairs[i].Y)
                {
                    spriteBatch.DrawLine((position + lowPairsRotated[i]).getVector2(), (position + highPairsRotated[i]).getVector2(), color);
                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public bool getAlive()
        {
            return true;
        }
    }
}
