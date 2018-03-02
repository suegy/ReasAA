using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Reflection;
using System.IO;
using ReAct.sys.error;

#if LOG_ON
    using log4net;
#else
    using ReAct.sys;
#endif

namespace ReAct.sys
{
    public class EmbeddedControl : AssemblyControl
    {
        protected IBehaviourConnector connector;

        protected BehaviourDict behaviours;

        /// <summary>
        /// Contains the different lap files used for possible agents.
        /// They key is the planID.
        /// </summary>
        protected Dictionary<string, string> actionPlans;
        

        /// <summary>
        /// The dictionary contains the agents and their internal parameters which will be used. Key of the outer dict is the planID. 
        /// The second dict contains parameters and their values in string representation.
        /// </summary>
        protected Dictionary<string, Dictionary<string, string>> initParameters;
        protected Dictionary<string, string> initFile;
                

        internal EmbeddedControl() : base()
        {
            actionPlans = new Dictionary<string, string>();
            initParameters = new Dictionary<string, Dictionary<string, string>>();

        }



        public override BehaviourDict GetBehaviours(string lib,AgentBase agent)
		{
			return GetBehaviours (lib, null,agent);
		}

        /// <summary>
        /// Returns a sequence of classes, containing all behaviour classes that
        /// are available in a particular library.
        /// 
        /// The method searches the behaviour subclasses by attempting to import
        /// all file in the library ending in .dll, except for the WORLDSCRIPT, and
        /// search through all classes they contain to see if they are derived from
        /// any behaviour class.
        /// 
        /// If a log object is given, then logging output at the debug level is
        /// produced.
        /// </summary>
        /// <param name="lib">Name of the library to find the classes for</param>
        /// <param name="log">A log object</param>
        /// <returns>The dictionary containing the Assembly dll name and the included Behaviour classes</returns>
#if LOG_ON
        public override BehaviourDict GetBehaviours(string lib, log4net.ILog log, AgentBase agent)
#else
        public override BehaviourDict GetBehaviours(string lib, ILog log,AgentBase agent)
#endif
        {
            BehaviourDict dict = new BehaviourDict();
            Behaviour[] behaviours = null;
            if (log is ILog)
                log.Debug("Scanning library " + lib + " for behaviour classes");

            if (this.connector is IBehaviourConnector)
                behaviours = this.connector.GetBehaviours(agent);

            if (behaviours != null)
                foreach (Behaviour behave in behaviours)
                    if (behave != null && behave.GetType().IsSubclassOf(typeof(Behaviour)))
                        dict.RegisterBehaviour(behave);



            return dict;
        }
        public void SetActionPlans(Dictionary<string,string> plans) {
            actionPlans = plans;
        }

        public void AddActionPlan(string planName, string plan)
        {
            actionPlans[planName] = plan;
        }

        public void SetBehaviourConnector(IBehaviourConnector connector)
        {
            this.connector = connector;
        }

        public void SetInitFiles(Dictionary<string, string> initFiles)
        {
            initFile = initFiles;
        }



        /// <summary>
        /// Returns the plan file name for the given library and plan
        /// </summary>
        /// <param name="lib">The library that the plan is from</param>
        /// <param name="plan">The name of the plan (without the .lap ending)</param>
        /// <returns>The filename with full path of the plan</returns>
        override internal string GetPlanFile(string lib, string plan)
        {
            string planStream;

            if (this.actionPlans.ContainsKey(plan))
                planStream = this.actionPlans[plan];
            else
                planStream = string.Empty;

            return planStream;
        }

        /// <summary>
        /// Running is checking if the agents are still active. 
        /// The method should be called only once when all agents are started to either iteratively check if they are all active(loopsRunning = true). In this case, 
        /// the method will only return when all agents stoped running.
        /// If you want externally check if the agents are active use loopsRunning = false to see if at least one agent is alive.
        /// </summary>
        /// <param name="verbose">If true, we try to write an info stream to the Console.</param>
        /// <param name="agents">The list of Agents we want to check.</param>
        /// <param name="iterate">If true, we iterativly check all agents and return once none is active. 
        /// If false, we just execute one cycle and return if at least one agent is active.</param>
        /// <returns> Returns the status of the system based on at least one active agent.</returns>
        public override bool Running(bool verbose, AgentBase[] agents, bool iterate)
        {
            // check all 0.1 seconds if the loops are still running, and exit otherwise

              DateTime time = System.DateTime.UtcNow;
            if (time.Subtract(currentTime).TotalMilliseconds > 100)
            {
                currentTime = time;
                iterate = false;
                foreach (AgentBase agent in agents)
                {
                    if (!agent.IsThreaded())
                        agent.LoopThread();
                    if (agent.LoopStatus().First)
                        iterate = true;
                }
            }
            if (verbose)
            {
                if (!iterate)
                    Console.Out.WriteLine("- all agents stopped");
                else
                    Console.Out.WriteLine("- agents are running");
            }
            return iterate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="verbose"></param>
        /// <param name="assembly"></param>
        /// <param name="agentLibrary"></param>
        /// <returns>returns a dictionary containing agentnames and a dictionary containing attributes for the agent</returns>
        public override List<Tuple<string, object>> InitAgents(bool verbose, string assembly, string agentLibrary)
        {

            if (connector == null || !connector.Ready())
                return null;

            List<Tuple<string, object>> agentsInit = null;
            string agentsInitFile = string.Format("{0}_{1}", agentLibrary, "init.txt");

            // check if the agent init file exists
            //if (connector.GetInitFileStream(agentsInitFile) == null)
            //    throw new UsageException(string.Format("cannot find specified agent init file in for library '{0}' in the resources",
            //            agentLibrary));

            if (verbose)
                Console.Out.WriteLine(string.Format("reading initialisation file '{0}'", agentsInitFile));
            try
            {
                agentsInit = AgentInitParser.initAgentFile(connector.GetInitFileStream(agentsInitFile));
            }
            catch (Exception )
            {
                try
                {
                    agentsInit = AgentInitParser.initAgentFile(connector.GetInitFileStream(agentLibrary));
                }
                catch (Exception )
                {
                    //TODO: meaningfull error message regarding the agentinit file which seems to be either corrupt or not linked
                }
            }

            return agentsInit;
        }

    }




}
