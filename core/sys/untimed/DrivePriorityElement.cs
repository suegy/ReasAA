using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReAct.sys.parse;
using ReAct.sys.events;

namespace ReAct.sys.untimed
{
    /// <summary>
    /// A drive priority element, containing drive elements.
    /// </summary>
    public class DrivePriorityElement : ElementCollection
    {
        private List<DriveElement> elements;
        TimerBase timer;
        private Agent agent;
        private List<Behaviour> behaviours; 


        /// <summary>
        /// Initialises the drive priority element.
        /// 
        /// The log domain is set to [AgentName].DP.[drive_name]
        /// </summary>
        /// <param name="agent">The element's agent.</param>
        /// <param name="driveName">The name of the associated drive.</param>
        /// <param name="elements">The drive elements of the priority element.</param>
        public DrivePriorityElement(Agent agent, string driveName, DriveElement [] elements)
            : base(string.Format("DP.{0}", driveName), agent)
        {
            name = driveName;
            this.elements = new List<DriveElement>(elements);
            timer = agent.getTimer();
            this.agent = agent;

            log.Debug("Created");
        }

        /// <summary>
        /// Resets all drive elements in the priority element.
        /// </summary>
        public override void reset()
        {
            log.Debug("Reset");
            foreach (DriveElement elem in elements)
                elem.reset();
        }

        /// <summary>
        /// Fires the drive prority element.
        /// 
        /// This method fires the first ready drive element in its
        /// list and returns FireResult(False, None). If no
        /// drive element was ready, then None is returned.
        /// </summary>
        /// <returns>The result of firing the element.</returns>
        public override FireResult fire()
        {
            log.Debug("Fired");
            long timeStamp = timer.Time();
            elements = LAPParser.ShuffleList(elements);
            FireArgs args = new FireArgs();

            
            // for element in new_elements:
            foreach (DriveElement element in elements)
            {
                if (element.isReady(timeStamp))
                {
                    if (element != agent.dc.lastTriggeredElement)
                        if (agent.dc.lastTriggeredElement == null)
                        {
                            if (element.isLatched)
                                agent.dc.lastTriggeredElement = element;
                        }
                        else if (!agent.dc.lastTriggeredElement.isReady(timeStamp))
                            // event finished natually
                            agent.dc.lastTriggeredElement = null;
                        else
                        {
                            behaviours = agent.dc.lastTriggeredElement.behaviours;

                            // TODO: check this latching behaviour thing
                            foreach (Behaviour b in behaviours)
                                if ( b.GetType().IsSubclassOf(typeof(LatchedBehaviour)))
                                   ((LatchedBehaviour)b).signalInterrupt();

                            if (element.isLatched)
                                agent.dc.lastTriggeredElement = element;
                            else
                                agent.dc.lastTriggeredElement = null;
                        }
                    element.fire();
                    args.FireResult = false;
                    args.Time = DateTime.Now;
                    BroadCastFireEvent(args);
                    return new FireResult(false, null, ExecutionState.Finished);
                }
            }

            args.FireResult = false;
            args.Time = DateTime.Now;
            BroadCastFireEvent(args);
            return FireResult.Zero;
        }

        public override bool Contains(object elem)
        {
            if (elem is DriveElement)
                return elements.Contains(elem as DriveElement);

            return false;
        }

        public List<DriveElement> getSortedDrive()
        {
            // List<DriveElement> allElements;

            // TODO: if this is really needed it must be implemented
            throw new NotImplementedException();
        }

        /// <summary>
        /// Is never supposed to be called and raises an error.
        /// </summary>
        /// <returns>DrivePriorityElement.copy() is never supposed to be called</returns>
        public override ElementBase copy()
        {
            throw new NotImplementedException("DrivePriorityElement.copy() is never supposed to be called");
        }

        public override string ToSerialize(Dictionary<string, string> elements)
        {
            string plan = string.Empty;
            elements = (elements is Dictionary<string, string>) ? elements : new Dictionary<string, string>();

            // taking appart the senses and putting them into the right form


            foreach (DriveElement elem in this.elements)
            {
                plan += "\t(" + elem.ToSerialize(elements) + ")";
            }

            return plan;
        }
    }
}