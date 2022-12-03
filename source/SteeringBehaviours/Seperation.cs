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
    public class SeperationBehaviour : AutonomousAgentBehaviour
    {
        Parameter weight;

        public SeperationBehaviour(Parameter _weight, IAutonomousAgentPerception _AutonomousAgentPerception) : base("seperation", _AutonomousAgentPerception)
        {
            weight = _weight;
        }



        public override Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {
            return weight.Value * seperation(AutonomousAgent, otherAutonomousAgents);
        }
        private Vector2D seperation(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
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
                    var modifier = autonomousAgentPerception.SightRadius / (AutonomousAgent.Position - otherAutonomousAgent.Position).Length();

                    desired += (AutonomousAgent.Position - otherAutonomousAgent.Position) * modifier;
                }
                desired /= otherAutonomousAgents.Count;
                if (desired.Length() != 0) desired.Normalize();
                desired *= AutonomousAgent.MaxSpeed;


                Vector2D steering = desired - AutonomousAgent.Velocity;

                steering.Normalize();

                return steering;
            }
        }
    }

}
