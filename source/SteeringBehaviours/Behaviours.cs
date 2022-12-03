using System;
using System.Collections.Generic;
using mono2.Framework;
using mono2.source.Fishtank;
using mono2.source.Framework;

namespace mono2.Behaviours
{

    public abstract class AutonomousAgentBehaviour 
    {

        public string name;
        public IAutonomousAgentPerception autonomousAgentPerception;
        public abstract Vector2D getSteering(AutonomousAgent AutonomousAgent, List<AutonomousAgent> otherAutonomousAgents);
        public virtual Vector2D getSteering(AutonomousAgent AutonomousAgent)
        {
            var otherAutonomousAgents = autonomousAgentPerception.getAutonomousAgentsInSight(AutonomousAgent);
            return getSteering(AutonomousAgent, otherAutonomousAgents);
        }

        public AutonomousAgentBehaviour(string _name, IAutonomousAgentPerception _AutonomousAgentPerception)
        {
            name = _name;
            autonomousAgentPerception = _AutonomousAgentPerception;

        }

        public string Name
        {
            get { return name; }
        }


    }









}
