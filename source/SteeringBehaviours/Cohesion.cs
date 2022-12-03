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
    public class CohesionBehaviour : AutonomousAgentBehaviour
    {
        Parameter weight;

        public CohesionBehaviour(Parameter _weight, IAutonomousAgentPerception _AutonomousAgentPerception) : base("Cohesion", _AutonomousAgentPerception)
        {
            weight = _weight;
        }

        public override Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {
            return weight.Value * cohesion(AutonomousAgent, otherAutonomousAgents);
        }

        public Vector2D cohesion(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {
            if (otherAutonomousAgents.Count == 0)
            {
                return new Vector2D(0, 0); ;
            }
            else
            {
                Vector2D desired = new Vector2D();
                foreach (var otherAutonomousAgent in otherAutonomousAgents)
                {
                    desired += otherAutonomousAgent.Position - AutonomousAgent.Position;
                }


                desired /= otherAutonomousAgents.Count;
                if (desired.Length() != 0) desired.Normalize();
                desired *= AutonomousAgent.MaxSpeed;
                Vector2D steering = desired - AutonomousAgent.Velocity;
                steering.limitMagnitude(AutonomousAgent.MaxForce);
                steering.Normalize();

                return steering;
            }
        }
    }

}
