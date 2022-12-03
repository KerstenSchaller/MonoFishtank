using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mono2.Framework;
using mono2.source.Fishtank;

namespace mono2.source.Framework
{
    public interface IAutonomousAgentPerception 
    {
        public List<AutonomousAgent> getAutonomousAgentsInSight(AutonomousAgent AutonomousAgent);
        public double SightRadius { get; }
    }

    public class AutonomousAgentPerception<T> : IAutonomousAgentPerception
    {
        double sightRadius, sightAngle;

        public AutonomousAgentPerception(double _sightRadius, double _sightAngle)
        {
            sightRadius = _sightRadius;
            sightAngle = _sightAngle;
        }

        public double SightRadius { get { return sightRadius; } }


        public List<AutonomousAgent> getAutonomousAgentsInSight(AutonomousAgent AutonomousAgent)
        {

            List<AutonomousAgent> AutonomousAgents = new List<AutonomousAgent>();

            // check distance
            List<GameObject> fishes = GameObjectManager.getGameObjects<T>();

            foreach (Fish otherAutonomousAgent in fishes)
            {
                var distance = (otherAutonomousAgent.Position - AutonomousAgent.Position).Length();
                var diffVector = otherAutonomousAgent.Position - AutonomousAgent.Position;
                var angle = diffVector.getAngle(AutonomousAgent.Orientation);
                if (distance == 0) continue;
                if (Math.Abs(distance) < sightRadius && Math.Abs(angle) < sightAngle) AutonomousAgents.Add(otherAutonomousAgent);
            }

            return AutonomousAgents;
        }


    }
}
