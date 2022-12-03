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

    public class SeekBehaviour : AutonomousAgentBehaviour
    {
        Parameter weight;

        public SeekBehaviour(Parameter _weight, IAutonomousAgentPerception _AutonomousAgentPerception) : base("seek", _AutonomousAgentPerception)
        {
            weight = _weight;
        }



        public override Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {
            if (otherAutonomousAgents.Count == 0) return new Vector2D();
            return weight.Value * seek(AutonomousAgent, otherAutonomousAgents[0]);
        }

        public static Vector2D seek(AutonomousAgent autonomousAgent, AutonomousAgent target)
        {

            Vector2D desired = target.Position - autonomousAgent.Position;

            if (desired.Length() != 0) desired.Normalize();
            desired *= autonomousAgent.MaxSpeed;

            Vector2D steering = desired - autonomousAgent.Velocity;

            steering.Normalize();

            return steering;

        }

    }



    public class FleeBehaviour : AutonomousAgentBehaviour
    {
        Parameter weight;

        public FleeBehaviour(Parameter _weight, IAutonomousAgentPerception _AutonomousAgentPerception) : base("flee", _AutonomousAgentPerception)
        {
            weight = _weight;
        }

        public override Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {
            if (otherAutonomousAgents.Count == 0) return new Vector2D();
            return -1 * weight.Value * SeekBehaviour.seek(AutonomousAgent, otherAutonomousAgents[0]);
        }
    }

    }
