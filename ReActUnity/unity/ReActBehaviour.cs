using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ReAct.sys;

namespace ReAct.unity
{
    public abstract class ReActBehaviour : MonoBehaviour
    {
        protected ReActInnerBehaviour poshBehaviour;
        protected ReActController controller;
        protected AgentBase agent = null;
        

        /// <summary>
        /// Links the actual gameObject and its Component the ReActBehaviour to the ReActController.
        /// </summary>
        /// <param name="agent">The agent controlling the gameObject</param>
        /// <returns>A ReActBehaviour which is connected to the gameObject so that ReAct is able to control Unity objects</returns>
        public sys.Behaviour LinkReActBehaviour(AgentBase agent)
        {
            if (this.agent == null || this.agent == agent)
            {
                Dictionary<string, object> parameters = agent.GetAttributes();
                poshBehaviour = InstantiateInnerBehaviour(agent);
                if (!poshBehaviour.IsSuitedForAgent(agent))
                    return null;
                this.agent = agent;
            }
            else
            {
                if (poshBehaviour == null)
                    poshBehaviour = InstantiateInnerBehaviour(agent);
                if (!poshBehaviour.IsSuitedForAgent(agent))
                    return null;
                GameObject clone = (GameObject) Instantiate(this.gameObject);
                poshBehaviour = clone.GetComponent<ReActBehaviour>().LinkReActBehaviour(agent) as ReActInnerBehaviour;
            }
            return poshBehaviour;
        }

        /// <summary>
        /// Needs tyo be implemented by an actual behaviour to allow the behaviour to specifically instantiate the ReActInnerBehaviour and set additional things in motion.
        /// Is called only from LinkReActBehaviour.
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        protected abstract ReActInnerBehaviour InstantiateInnerBehaviour(AgentBase agent);

        protected internal abstract void ConfigureParameters(Dictionary<string,object> parameters);

        protected internal abstract void ConfigureParameter(string parameter, object value);

        

        public void ConnectReActUnity(ReActController control)
        {
            controller = control;
        }

        public ReActInnerBehaviour GetInnerBehaviour()
        {
            return poshBehaviour;
        }




    }
}