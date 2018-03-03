using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReAct.sys.timed
{
    /// <summary>
    /// A basic POSH element.
    ///
    /// A basic POSH element is any plan / behaviour element, like a drive,
    /// a drive element, an action pattern, a sense, ...
    ///
    /// Each such an element has a unique numeric id, that is
    /// assigned to the element upon creating it.
    ///
    /// This element is not used directly, but is inherited
    /// by L{POSH.scheduled.Sense}, L{POSH.scheduled.Action}, and
    /// L{POSH.scheduled.PlanElement}.
    /// </summary>
    public class ElementBase : LogBase
    {
        private static long currentId = 0;
        protected long id;
        protected string name;
        private Agent agent;

        /// <summary>
        /// Returns a unique element id.
        /// This function returns an id for plan elements. At every call, 
        /// the internal id counter is increased by 1.
        /// </summary>
        /// <returns>A unique element id.</returns>
        static long getNextId()
        {
            return currentId += 1;
        }


        /// <summary>
        /// Initialises the element, and assigns it a unique id.
        /// </summary>
        /// <param name="logDomain">The logging domain for the element.</param>
        /// <param name="agent">The agent that uses the element.</param>
        public ElementBase(string logDomain, Agent agent)
            : base(logDomain, agent)
        {
            id = getNextId();
            this.agent = agent;
            name = "NoName";

        }

        /// <summary>
        /// Returns the name of the element.
        /// 
        /// The name has to be set by overriding classes by setting
        /// the object variable name
        /// </summary>
        /// <returns> The element's name.</returns>
        public string getName()
        {
            return name;
        }

        /// <summary>
        /// Returns the string representation of the element.
        /// </summary>
        /// <returns>[Classname] [Elementname]</returns>
        public override string ToString()
        {

            return string.Format("{0} {1}", this.GetType().Name, name);
        }

        /// <summary>
        /// Returns the element's id.
        /// </summary>
        /// <returns>The element's id.</returns>
        public long getId()
        {
            return id;
        }

        /// <summary>
        /// Returns a reset copy of itself.
        /// 
        /// This method returns a copy of itself, by creating a new
        /// instance of itsself and replicating all state-dependent object
        /// variables. If the object variables are not state-dependent,
        /// they can be copied as references rather than real copies.
        /// 
        /// This method needs to be overriddent by inheriting classes.
        /// In its current implementation it raises NotImplementedError
        /// </summary>
        /// <returns>A copy of itsself.</returns>
        public virtual ElementBase copy()
        {
            throw new NotImplementedException("ElementBase.copy() needs to be overridden");
        }

    }
}
