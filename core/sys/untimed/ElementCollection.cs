﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReAct.sys.untimed
{
    /// <summary>
    /// A collection of POSH plan elements.
    /// 
    /// This collection provides the same functionality as POSH.strict.PlanElement.
    /// </summary>
    public class ElementCollection : PlanElement
    {
        /// <summary>
        /// A collection of POSH plan elements.
        /// 
        /// This collection provides the same functionality as POSH.strict.PlanElement.
        /// 
        /// Initialises the element collection.
        /// </summary>
        /// <param name="logDomain">The logging domain for the element collection.</param>
        /// <param name="agent">The agent that uses the element collection.</param>
        public ElementCollection(string logDomain, Agent agent)
            : base(logDomain, agent)
        {
        }

        public virtual bool Contains(object elem)
        {
            throw new NotImplementedException("ElementCollection needs to override Contains");
        }
    }
}
