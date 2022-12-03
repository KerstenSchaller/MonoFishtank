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

    public class PursueBehaviour : AutonomousAgentBehaviour
    {
        Parameter weight;

        public PursueBehaviour(Parameter _weight, IAutonomousAgentPerception _AutonomousAgentPerception) : base("pursue", _AutonomousAgentPerception)
        {
            weight = _weight;
        }



        public override Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {
            if (otherAutonomousAgents.Count == 0) return new Vector2D();
            return weight.Value * Pursue(AutonomousAgent, otherAutonomousAgents[0]);
        }

        public static Vector2D Pursue(AutonomousAgent autonomousAgent, AutonomousAgent target)
        {

            Vector2D desired = target.Position + target.Velocity*10 - autonomousAgent.Position;

            

            if (desired.Length() != 0) desired.Normalize();
            desired *= autonomousAgent.MaxSpeed;

            Vector2D steering = desired - autonomousAgent.Velocity;

            steering.limitMagnitude(autonomousAgent.MaxForce);
            steering.Normalize();

            return steering;

        }

    }



    public class EvadeBehaviour : AutonomousAgentBehaviour
    {
        Parameter weight;

        public EvadeBehaviour(Parameter _weight, IAutonomousAgentPerception _AutonomousAgentPerception) : base("evade", _AutonomousAgentPerception)
        {
            weight = _weight;
        }

        public override Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {
            if (otherAutonomousAgents.Count == 0) return new Vector2D();
            return -1 * weight.Value * PursueBehaviour.Pursue(AutonomousAgent, otherAutonomousAgents[0]);
        }
    }

    }
