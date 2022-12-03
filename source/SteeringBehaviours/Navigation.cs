using System.Collections.Generic;
using Microsoft.Xna.Framework;
using mono2.Behaviours;
using mono2.Framework;

namespace mono2.source.SteeringBehaviours
{
    public class Navigation
    {
        public List<AutonomousAgentBehaviour> AutonomousAgentBehaviours = new List<AutonomousAgentBehaviour>();
        public virtual Vector2D getSteering(AutonomousAgent autonomousAgent)
        {
            List<Vector2D> forces = new  List<Vector2D>();
            foreach (AutonomousAgentBehaviour behaviour in AutonomousAgentBehaviours)
            {
                forces.Add( behaviour.getSteering(autonomousAgent));
            }

            return arbitrateForces1(forces, autonomousAgent.MaxForce);
        }

        private Vector2D arbitrateForces1(List<Vector2D> forces, double maxForce) 
        {
            // Prioritized acceleration allocation
            Vector2D force = new Vector2D();

            double magnitude = 0;

    
            foreach (var f in forces)
            {
                var mag = f.Length();
                f.Normalize();
                if ((magnitude + mag) < maxForce)
                {
                    magnitude += mag;
                    force += f*maxForce;
                }
                else
                { 
                    force += f * (maxForce - magnitude);
                }
            }

            return force;
        }

        private Vector2D arbitrateForces2(List<Vector2D> forces, double maxForce)
        {
            Vector2D force = new Vector2D();
            foreach (var f in forces)
            {
                force += f*maxForce;
            }
            return force;
        }

    }

}
