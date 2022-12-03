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
    public class AlignmentBehaviour : AutonomousAgentBehaviour
    {
        Parameter weight;
        public AlignmentBehaviour(Parameter _weight, IAutonomousAgentPerception _AutonomousAgentPerception) : base("alignment", _AutonomousAgentPerception)
        {
            weight = _weight;
        }

        public override Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {
            return weight.Value * align(AutonomousAgent, otherAutonomousAgents);
        }

        private Vector2D align(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
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
                    desired += otherAutonomousAgent.Velocity;
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
