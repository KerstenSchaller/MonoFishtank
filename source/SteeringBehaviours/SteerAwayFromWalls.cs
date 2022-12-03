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
    public class SteerAwayFromWallBehaviour : AutonomousAgentBehaviour
    {
        Parameter weight;
        public SteerAwayFromWallBehaviour(Parameter _weight, IAutonomousAgentPerception _AutonomousAgentPerception) : base("SteerAway", _AutonomousAgentPerception)
        {
            weight = _weight;
        }

        public override Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents)
        {
            return weight.Value * steerAwayFromWalls(AutonomousAgent);
        }

        public Vector2D steerAwayFromWalls(AutonomousAgent AutonomousAgent)
        {
            var wallSightRadius = 190;

            var desired = new Vector2D(AutonomousAgent.Velocity.X, AutonomousAgent.Velocity.Y);

            if (AutonomousAgent.Position.X < wallSightRadius)
            {
                desired.X = AutonomousAgent.MaxSpeed;
            }
            else if (Scenario.worldsize.X - AutonomousAgent.Position.X < wallSightRadius)
            {
                desired.X = -AutonomousAgent.MaxSpeed;
            }

            if (AutonomousAgent.Position.Y < wallSightRadius)
            {
                desired.Y = AutonomousAgent.MaxSpeed;
            }
            else if (Scenario.worldsize.Y - AutonomousAgent.Position.Y < wallSightRadius)
            {
                desired.Y = -AutonomousAgent.MaxSpeed;
            }

            //var desired = AutonomousAgent.Position - wallPosition;
            // if (desired.Length() != 0) desired.Normalize();
            // desired *= AutonomousAgent.MaxSpeed;
            Vector2D steering = desired - AutonomousAgent.Velocity;
            steering.Normalize();
            return steering;

        }

    }
}
