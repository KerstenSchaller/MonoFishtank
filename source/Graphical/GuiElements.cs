using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using mono2.Framework;

namespace mono2.Graphical
{

    public class Optionsbox : GameObject
    {
        List<Parameter> ParameterList = new List<Parameter>();
        public Parameter parameterSetIndex;

        int index = 0;
        Vector2D position;
        SpriteFont font;
        int maxIndex;

        bool alternateControl = false;


        public Vector2D Position => position;
        public bool AlternateControl
        {
            get { return alternateControl; }
            set { alternateControl = value; }
        }


        public Optionsbox(Vector2D Position)
        {
            ParameterList.AddRange(ParameterManager.AllParameters);

            font = Game1.gameContent.Load<SpriteFont>("Font1");
            position = Position;

            maxIndex = ParameterList.Count;
            parameterSetIndex = new Parameter("parameterSetIndex", 0, 0, Parameter.AllParameterLists.Count - 1, true);
            ParameterList.Insert(0, parameterSetIndex);

        }

        public void setDownSignal()
        {
            if (index < maxIndex) index++;
        }

        public void setUpSignal()
        {
            if (index > 0) index--;
        }
        public void setValue(double value)
        {
            Parameter parameter = ParameterList[index];
            //parameter.Value = value;
            ParameterManager.setParameter(parameter, value);
        }

        public void setLeftSignal()
        {
            double diff = 0.1f;
            if (alternateControl) diff *= 100;

            var parameter = ParameterList[index];
            double value = parameter.Value - diff;
            value = Math.Max(value, 0);
            parameter.Value = value;

            if (index == 0)
            {
                if (Parameter.AllParameterLists.Count > (int)parameter.Value)
                {
                    Parameter.load((int)parameter.Value);

                }
            }

        }



        public void setRightSignal()
        {
            double diff = 0.1f;
            if (alternateControl) diff *= 100;
            var parameter = ParameterList[index];
            parameter.Value =  parameter.Value + diff;
            if (index == 0)
            {
                if (Parameter.AllParameterLists.Count > (int)parameter.Value)
                {
                    Parameter.load((int)parameter.Value);
                }
            }

        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            int i = 0;
            foreach (var parameter in ParameterList)
            {
                if (i == 0)
                {
                    spriteBatch.DrawString(font, (index == i ? "* " : "") + parameterSetIndex.Name + ": " + parameterSetIndex.Value + "/" + parameterSetIndex.maxValue, new Vector2D(position.X, position.Y + i * 15).getVector2(), Color.White); i++;
                }
                else
                {
                    spriteBatch.DrawString(font, (index == i ? "* " : "") + parameter.Name + ": " + parameter.Value, new Vector2D(position.X, position.Y + i * 15).getVector2(), Color.White); i++;
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
