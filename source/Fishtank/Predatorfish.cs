using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using mono2.Behaviours;
using mono2.Framework;
using mono2.source.Framework;
using mono2.source.SteeringBehaviours;

namespace mono2.source.Fishtank
{
    public class Predatorfish : Fish
    {
        public Predatorfish(Vector2D InitPos, Vector2D InitVelocity) : base(InitPos, InitVelocity, 0, 8, ParameterManager.getParameter("PredatorFish.MaxSpeed", 6), ParameterManager.getParameter("PredatorFish.MaxForce", 3), new AutonomousAgentPerception<SwarmFish>(700, Vector2D.Deg2Rad(90 + 45)))
        {
            setScale(5);
            defineBehaviour();
        }


        void defineBehaviour()
        {

            FlockHunting flockHunting = new FlockHunting( autonomousAgentPerception);
            

            SteerAwayFromWallBehaviour steerAwayBehaviour = new SteerAwayFromWallBehaviour(ParameterManager.getParameter("PredatorFish.awayWall.weight", 1), autonomousAgentPerception);
            WanderBehaviour wanderBehaviour = new WanderBehaviour(ParameterManager.getParameter("PredatorFish.wander.weight", 1), autonomousAgentPerception);

            var nav = new Navigation();
            nav.AutonomousAgentBehaviours.Add(flockHunting);
            nav.AutonomousAgentBehaviours.Add(wanderBehaviour);
            nav.AutonomousAgentBehaviours.Add(steerAwayBehaviour);
            

            navigation = nav;

        }

        public override void Update(GameTime gameTime)
        {
            CircleObject circle = new CircleObject(this.Position + this.Velocity * 10, 5);
            GameObjectManager.addGameObject(circle);
            base.Update(gameTime);
        }
    }
}
