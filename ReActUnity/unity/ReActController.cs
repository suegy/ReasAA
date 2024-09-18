using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ReAct.sys;
using System.IO;




namespace ReAct.unity
{
    public abstract class ReActController : MonoBehaviour, IBehaviourConnector
    {
        public string engineLog;

        public ReActBehaviour[] behaviourPool;
        public TextAsset[] actionPlans;

        public string usedReActConfig;

        public TextAsset[] agentConfiguration;
        protected AgentBase[] agents;

        public bool stopAgents = false;

        //TODO: currently disabled because it would need further input to make a good UI for adding additional props 
        //public bool use_Agent_configuration;
        //public ReAct.sys.IBehaviourConnector.AgentParameter[] agentConfigurations;

        

        protected EmbeddedControl poshLink;

        protected Dictionary<string,string> plans;
        protected Dictionary<string, string> initFiles;

        protected bool started = false;


        protected virtual void InitReAct()
        {
#if LOG_ON
            string configFile = Application.dataPath + String.Format("{0}ReAct{0}lib{0}log4net.xml",Path.DirectorySeparatorChar);
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                configFile = Application.dataPath + "\\log4net.xml";
            }

            /**/
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(configFile);
            /**/
            log4net.Config.XmlConfigurator.ConfigureAndWatch(fileInfo);
            /**/
            log4net.LogManager.GetLogger(typeof(LogBase)).InfoFormat("tesat", configFile);
#endif 

            AssemblyControl.SetForUnityMode();
            poshLink = AssemblyControl.GetControl() as EmbeddedControl;
            poshLink.SetBehaviourConnector(this);

            plans = CreateReActDict(actionPlans);
            poshLink.SetActionPlans(plans);

            initFiles = CreateReActDict(agentConfiguration);
            poshLink.SetInitFiles(initFiles);
          
            engineLog = "init";
            

        }

        protected bool StopReAct()
        {
            foreach (AgentBase ag in agents)
                ag.StopLoop();
            Debug.Log("STOPPING REACT");
            started = false;
            return false;
        }

        protected bool PauseReAct()
        {
            Debug.Log("STOPPING REACT");

            foreach (AgentBase ag in agents)
                ag.PauseLoop();
            return false;
        }


        void OnApplicationPause(bool pauseStatus)
        {
            if (started)
                PauseReAct();
        }

        void OnApplicationQuit()
        {
            if (started)
                StopReAct();
        }

        /// <summary>
        /// Checks if at least one ReAct agent is still running
        /// </summary>
        /// <param name="checkStopped">If true the method checks if the agents are entirely stopped if false it will check if the agents are only paused.</param>
        /// <returns></returns>
        protected virtual bool AgentRunning(bool checkStopped)
        {
            foreach (AgentBase agent in agents)
                if (checkStopped)
                {
                    if (agent.LoopStatus().First)
                        return true;
                }
                else
                {
                    if (agent.LoopStatus().Second)
                        return true;
                }

            return false;
        }

        protected virtual bool RunReAct()
        {
            
            if (!started)
            {
                Debug.Log("init ReAct");

                List<Tuple<string, object>> agentInit = poshLink.InitAgents(true, "", usedReActConfig);
                agents = poshLink.CreateAgents(true, usedReActConfig, agentInit, new Tuple<World, bool>(null, false));
                poshLink.StartAgents(true, agents);
                Debug.Log("running ReAct");

            }
            poshLink.Running(true, agents, false);
            started = true;
            return started;
        }

        protected Dictionary<string, string> CreateReActDict(TextAsset [] files)
        {
            Dictionary<string, string> fileDict = new Dictionary<string, string>();

            foreach (TextAsset file in files)
            {
                fileDict.Add(file.name,file.text);
            }

            return fileDict;
        }

        
        public string GetBehaviourLibrary()
        {
            return usedReActConfig;
        }

        public sys.Behaviour[] GetBehaviours(AgentBase agent)
        {
            List<sys.Behaviour> result = new List<sys.Behaviour>();

            foreach (ReActBehaviour behave in this.behaviourPool)
            {
                sys.Behaviour poshBehaviour = behave.LinkReActBehaviour(agent);
                if (poshBehaviour != null)
                    result.Add(poshBehaviour);
            }
            return result.ToArray();
        }

        public virtual string GetPlanFileStream(string planName)
        {
            return plans[planName];
        }

        public virtual string GetInitFileStream(string libraryName)
        {
            return initFiles[libraryName];
        }

        public virtual bool Ready()
        {
            if (poshLink != null && behaviourPool.Count() > 0 && 
                actionPlans.Length > 0 && agentConfiguration.Count() > 0 && 
                usedReActConfig.Length > 1)
                return true;

            return false;
        }

    }
}
