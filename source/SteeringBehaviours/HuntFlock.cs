using System.Collections.Generic;
using mono2.Behaviours;
using mono2.Framework;
using mono2.source.Fishtank;
using mono2.source.Framework;

namespace mono2.source.SteeringBehaviours
{
    public class FlockHunting : AutonomousAgentBehaviour
    {
        Parameter weight;
        Parameter neighBourThreshold;

        public FlockHunting( IAutonomousAgentPerception _AutonomousAgentPerception) : base("FlockHunting", _AutonomousAgentPerception)
        {
            weight = ParameterManager.getParameter(base.Name + ".weight",1);
            neighBourThreshold = ParameterManager.getParameter("FlockHunting.neighbourCount", 15, true);
        }

        public override Vector2D getSteering(AutonomousAgent autonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {

            var target = getTarget(otherAutonomousAgents);
            if (target is null) return new Vector2D();
            Vector2D desired = target - autonomousAgent.Position;

            if (desired.Length() != 0) desired.Normalize();
            desired *= autonomousAgent.MaxSpeed;

            Vector2D steering = desired - autonomousAgent.Velocity;

            steering.limitMagnitude(autonomousAgent.MaxForce);
            steering.Normalize();

            LineObject line = new LineObject(autonomousAgent.Position, target);
            //GameObjectManager.addGameObject(line);

            return weight.Value * steering;
        }

        private Vector2D getTarget(List<AutonomousAgent> fishes)
        {

            foreach (var fish in fishes)
            {
                var neighbours = fish.getNeighbours();
                if (neighbours.Count > neighBourThreshold.Value) 
                {
                    Vector2D center = new Vector2D();
                    foreach (var neightbour in neighbours) 
                    {
                        center += neightbour.Position;
                    }
                    center = center/neighbours.Count;
                    return center;
                }
            }
            return null;
        }
    }
}
