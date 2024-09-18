using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReAct.sys.timed
{
    /// <summary>
    /// Implementation of an FixedGroup.
    /// </summary>
    public class FixedGroup : ElementCollection
    {
        List<CopiableElement> elements;
        private int elementIdx;

        /// <summary>
        /// Initialises the element grouping.
        /// 
        /// The log domain is set to [AgentId].AP.[patternName]
        /// </summary>
        /// <param name="agent">The corresponding agent.</param>
        /// <param name="patternName">The name of the element grouping.</param>
        /// <param name="elements">The sequence of actions or senses and 
        ///         an optional competence as the final element.</param>
        /// </param>
        public FixedGroup(Agent agent, string patternName, CopiableElement []elements)
            : base(string.Format("AP.{0}", patternName),agent)
        {
            name = patternName;
            this.elements = (elements.Length > 0) ? new List<CopiableElement>(elements) : new List<CopiableElement>();
            this.elementIdx = 0;
            log.Debug("Created");
        }

        /// <summary>
        /// Resets the element grouping.
        /// 
        /// This method sets the element grouping to fire the
        /// first action of the pattern upon the next call to L{fire}.
        /// </summary>
        public override void  reset()
        {
 	         log.Debug("Reset");
            this.elementIdx = 0;
        }

        /// <summary>
        /// Fires the element grouping.
        /// 
        /// This method fires the current action / sense / sense-act or
        /// competence of the pattern. In case of firing an action / sense
        /// / sense-act, the method points to the next element in the
        /// pattern and returns FireResult(True, None) if the current
        /// action / sense / sense-act was successful (i.e. evaluated to
        /// True) and not the last action in the sequence, in which case
        /// it returns FireResult(False, None) and resets the action
        /// pattern.
        /// 
        /// If the current element is a competence, then competence is
        /// returned as the next element by returning
        /// FireResult(True, competence), and the element grouping is
        /// reset.
        /// </summary>
        /// <returns>The result of firing the element grouping.</returns>
        public override FireResult  fire()
        {
 	        log.Debug("Fired");
            CopiableElement element = elements[elementIdx];
            if (element is ReAction || element is ReSense)
            {
                bool result;
                if (element is ReAction)
                    result = ((ReAction)element).fire();
                else
                    result = ((ReSense)element).fire();

                if (!result)
                {
                    log.Debug(string.Format("Action/Sense {0} failed", element.getName()));
                    elementIdx = 0;
                    return new FireResult(false, null);
                }

                // check if we've just fired the last action
                elementIdx += 1;
                if (elementIdx >= elements.Count)
                {
                    elementIdx = 0;
                    return new FireResult(false, null);
                }
                return new FireResult(true, null);
            }
            else if (elements[elementIdx] is Competence)
            {
                // we have a competence
                elementIdx = 0;
                return new FireResult(true, element);
            }

            return null;
        }

        /// <summary>
        /// Returns a reset copy of itsself.
        ///
        /// This method returns a copy of itsself, and calls L{reset}
        /// on it.
        /// </summary>
        /// <returns>A reset copy of itsself.</returns>
        public override CopiableElement  copy()
        {
 	         FixedGroup newObj = (FixedGroup)this.MemberwiseClone();
            newObj.reset();
            
            return newObj;
        }

        /// <summary>
        /// Sets the elements of an element grouping.
        /// 
        /// Calling this method also resets the element grouping.
        /// </summary>
        /// <param name="elements">The list of elements of the element groupings. 
        ///         A sequence of Actions. An additional Competence can be the
        ///         last Element of the FixedGroup.</param>
        public void setElements(CopiableElement [] elements)
        {
            this.elements = new List<CopiableElement>(elements);
            
            reset();
        }

    }
}