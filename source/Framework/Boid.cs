using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using mono2.Behaviours;
using mono2.source.Fishtank;
using mono2.source.Framework;
using mono2.source.SteeringBehaviours;

namespace mono2.Framework
{

    public class AutonomousAgentOptions
    {
        public List<AutonomousAgentBehaviour> behaviours = new List<AutonomousAgentBehaviour>();
        

        public AutonomousAgentOptions()
        {
        }
        
    }

    public class AutonomousAgent : GameObject, IRigidBody
    {

        public IAutonomousAgentPerception autonomousAgentPerception;
        public Navigation navigation;

        public bool debugActive = false;
        static public AutonomousAgentOptions AutonomousAgentOptions;
        public RigidBody rigidBody;

        public double MaxSpeed { get { return rigidBody.maxSpeed.Value; } }
        public double MaxForce { get { return rigidBody.maxForce.Value; } }

        public Vector2D Position
        {
            get { return rigidBody.Position; }
            set
            {
                rigidBody.Position = value;
            }
        }

        public Vector2D Orientation
        {
            get { return rigidBody.Orientation; }
        }

        public Vector2D Velocity
        {
            get { return rigidBody.Velocity; }
            set
            {
                rigidBody.Velocity = value;
            }
        }

        public AutonomousAgent(Vector2D InitPos, Vector2D InitVelocity, Parameter maxSpeed, Parameter maxForce, IAutonomousAgentPerception _autonomousAgentPerception,  bool debugActive = false)
        {
            var orienation = new Vector2D(InitVelocity);
            orienation.Normalize();
            rigidBody = new RigidBody(1, maxSpeed, maxForce, InitPos, orienation, InitVelocity);
            autonomousAgentPerception = _autonomousAgentPerception;
            this.debugActive = debugActive;
        }

        public List<AutonomousAgent> getNeighbours() 
        {
            return autonomousAgentPerception.getAutonomousAgentsInSight(this);
        }
        public void applyForce(Vector2D force)
        {
            rigidBody.applyForce(force);
        }

        virtual public void Update(GameTime gameTime)
        {
            double deltaTime = (double)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;


            var force = navigation.getSteering(this);
            applyForce(force * deltaTime);

            this.rigidBody.Update(gameTime);
        }


        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

        public bool getAlive()
        {
            return true;
        }

     
    }






}
