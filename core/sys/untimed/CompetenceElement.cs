﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReAct.sys.events;

namespace ReAct.sys.untimed
{
    /// <summary>
    /// A competence element.
    /// </summary>
    public class CompetenceElement : PlanElement
    {
        private Trigger trigger;
        private PlanElement element;
        private int maxRetries;
        private int retries;
        /// <summary>
        /// Initialises the competence element.
        /// 
        /// The log domain is set to [AgentName].CE.[element_name].
        /// </summary>
        /// <param name="agent">The competence element's agent.</param>
        /// <param name="elementName">The name of the competence element.</param>
        /// <param name="trigger">The element's trigger</param>
        /// <param name="element">The element to fire (Action,Competence or ActionPattern).</param>
        /// <param name="maxRetries">The maximum number of retires. If this is set
        ///         to a negative number, it is ignored.</param>
        public CompetenceElement(Agent agent, string elementName, Trigger trigger, PlanElement element, int maxRetries)
            :base(string.Format("CE.{0}",elementName),agent)
        {
            this.name = elementName;
            this.trigger = trigger;
            this.element = element;
            this.maxRetries = maxRetries;
            this.retries = 0;

            log.Debug("Created");
        }

        /// <summary>
        /// Resets the retry count.
        /// </summary>
        public override void  reset()
        {
 	         retries = 0;
        }

        /// <summary>
        /// Returns if the element is ready to be fired.
       /// 
       /// The element is ready to be fired if its trigger is
       /// satisfied and it was not fired more than maxRetries.
       /// Note that timestamp is ignored in this method. It is only
       /// there because isReady is defined like that in the
       /// POSH.strict.Element interface.
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns>If the element is ready to be fired.</returns>
        public override bool  isReady(long timeStamp)
        {
            
            bool success = false;
            if (trigger.fire())
                 if (maxRetries == 0 || retries < maxRetries )
                 {
                     retries +=1;
                     success = true;
                 } else
                    log.Debug("Retry limit exceeded");
            
            return success;
        }

        /// <summary>
        /// Fires the competence element.
        /// 
        /// If the competence element's element is an Action, then this
        /// action is executed and FireResult(False, None) is returned.
        /// Otherwise, FireResult(True, element) is returned,
        /// indicating that at the next execution step that element has
        /// to be fired.
        /// </summary>
        /// <returns>Result of firing the competence element.</returns>
        public override FireResult  fire()
        {
            FireArgs args = new FireArgs();
            

 	        log.Debug("Fired");
            
            FireResult res = element.fire();

            args.FireResult = res.Result;
            args.Time = DateTime.Now;
            BroadCastFireEvent(args);
            
            if (res.State != ExecutionState.Running && res.State != ExecutionState.Started)    
                return new FireResult(res.Result, null, res.State);
            
            args.FireResult = true;
            args.Time = DateTime.Now;
            BroadCastFireEvent(args);
            return new FireResult(res.Result, element, res.State);
        }

        /// <summary>
        /// Returns a reset copy of itsself.
        /// 
        /// This method creates a copy of itsself that links to the
        /// same element, but has a reset retry counter.
        /// </summary>
        /// <returns>A reset copy of itself.</returns>
        public override ElementBase  copy()
        {
            CompetenceElement newObj = (CompetenceElement) this.MemberwiseClone();
            newObj.reset();
            
            return newObj;
        }

        public override string ToSerialize(Dictionary<string, string> elements)
        {
            string plan = string.Empty;
            elements = (elements is Dictionary<string, string>) ? elements : new Dictionary<string, string>();

            // taking appart the senses and putting them into the right form


            plan = String.Format("({0} (trigger {1}) {2} {3})",name,trigger.ToSerialize(elements),element.ToSerialize(elements),this.maxRetries);
            

            return plan;
        }
    }
}
