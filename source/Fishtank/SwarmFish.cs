using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mono2.Behaviours;
using mono2.Framework;
using mono2.source.Framework;
using mono2.source.SteeringBehaviours;

namespace mono2.source.Fishtank
{
    public class SwarmFish : Fish
    {


        public SwarmFish(Vector2D InitPos, Vector2D InitVelocity) : base(InitPos, InitVelocity, 6, 3, ParameterManager.getParameter("SwarmFish.MaxSpeed", 6), ParameterManager.getParameter("SwarmFish.MaxForce", 3), new AutonomousAgentPerception<SwarmFish>(100, Vector2D.Deg2Rad(90 + 45)))
        {
            setScale(2);
            defineBehaviour();
        }

        void defineBehaviour()
        {

            AutonomousAgentPerception<Predatorfish> predatorPerception = new AutonomousAgentPerception<Predatorfish>(100, 90 + 45);
            //FleeBehaviour fleeBehaviour = new FleeBehaviour(ParameterManager.getParameter("swarmFish.Flee.weight", 1), predatorPerception);
            AutonomousAgentBehaviour fleeBehaviour = new EvadeBehaviour(ParameterManager.getParameter("swarmFish.Flee.weight", 1), predatorPerception);


            AutonomousAgentBehaviour cohesionBehaviour = new CohesionBehaviour(ParameterManager.getParameter("swarmFish.cohesion.weight", 1), autonomousAgentPerception);
            AutonomousAgentBehaviour alignmentBehaviour = new AlignmentBehaviour(ParameterManager.getParameter("swarmFish.alignment.weight", 1), autonomousAgentPerception);
            AutonomousAgentBehaviour seperationBehaviour = new SeperationBehaviour(ParameterManager.getParameter("swarmFish.seperation.weight", 1), autonomousAgentPerception);
            AutonomousAgentBehaviour steerAwayBehaviour = new SteerAwayFromWallBehaviour(ParameterManager.getParameter("swarmFish.awayWall.weight", 1), autonomousAgentPerception);
            AutonomousAgentBehaviour wanderBehaviour = new WanderBehaviour(ParameterManager.getParameter("swarmFish.wander.weight", 1), autonomousAgentPerception);


            var nav = new Navigation();
            nav.AutonomousAgentBehaviours.Add(steerAwayBehaviour);
            nav.AutonomousAgentBehaviours.Add(fleeBehaviour);
            nav.AutonomousAgentBehaviours.Add(seperationBehaviour);
            nav.AutonomousAgentBehaviours.Add(alignmentBehaviour);
            nav.AutonomousAgentBehaviours.Add(cohesionBehaviour);
            nav.AutonomousAgentBehaviours.Add(wanderBehaviour);
            navigation = nav;

        }

    }
}
