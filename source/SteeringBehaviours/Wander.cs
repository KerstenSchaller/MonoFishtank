using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mono2.Framework;
using mono2.source.Fishtank;
using mono2.source.Framework;

namespace mono2.Behaviours
{
    public class WanderBehaviour : AutonomousAgentBehaviour
    {

        double wanderTheta = Vector2D.Deg2Rad(90);
        Parameter wanderDistance = ParameterManager.getParameter("wander.Distance", 150);
        Parameter wanderRadius = ParameterManager.getParameter("wander.Radius", 90);
        Parameter displacementRangeDegree = ParameterManager.getParameter("wander.maxDisplacement", 20);

        Parameter weight;

        public WanderBehaviour(Parameter _weight, IAutonomousAgentPerception _AutonomousAgentPerception) : base("Wander", _AutonomousAgentPerception)
        {
            weight = _weight;
        }


        public override Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {

            return weight.Value * wander(AutonomousAgent);
        }

        public Vector2D wander(AutonomousAgent AutonomousAgent)
        {
            var wanderPoint = new Vector2D(AutonomousAgent.Orientation.X, AutonomousAgent.Orientation.Y);
            if (wanderPoint.Length() != 0) { wanderPoint.Normalize(); }

            wanderPoint *= wanderDistance.Value;
            wanderPoint += AutonomousAgent.Position;

            bool debug = false;

            if (debug) GameObjectManager.addGameObject(new CircleObject(wanderPoint, wanderRadius.Value));


            var angle = wanderTheta + AutonomousAgent.Orientation.getAngle();
            wanderPoint += Vector2D.getVectorWithLengthAndAngle(wanderRadius.Value, angle);

            if (debug) GameObjectManager.addGameObject(new PointObject(wanderPoint));
            if (debug) GameObjectManager.addGameObject(new LineObject(AutonomousAgent.Position, wanderPoint));

            var steering = wanderPoint - AutonomousAgent.Position;
            steering.Normalize();
            steering *= AutonomousAgent.MaxForce;

            var randomScale = new Random().NextDouble() * 2 - 1;

            var displacement = randomScale * Vector2D.Deg2Rad(displacementRangeDegree.Value);
            wanderTheta += displacement;

            steering.Normalize();

            return steering;

        }


    }
}
