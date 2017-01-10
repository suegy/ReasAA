using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace posh.visu.model.POSH
{
    /**
    * A competence is a named list of lists of competence elements with a goal and an interval
    */
    class Competence : AParallel
    {
        

	    // The goal is an arraylist of ActionElements
	    private List<ActionElement> alGoal = null;
        private List<IEditable[]> competenceElements;

	    // Timeout (Ymir like, Ymir want breadsticks, me likey breadsticks!)
	    private TimeUnit tTimeout = null;

	    private bool collapsed = false;

	    /**
	     * Create this competence with a name, ymir-like timeout and a goal (Arraylist of actionelements)
	     * as well as an arraylist of arraylists of CompetenceElements comprising the competence itself.
	     *
	     * @param name Name of the competence
	     * @param timeUnit Timeout
	     * @param goal Goal to try and obtain
	     * @param elementLists Lists of lists of competence elements that comprise the actions etc.
	     **/
	    internal  Competence(string id, string name, TimeUnit timeUnit, IEditable [] goal = null, List<IEditable []> elementLists = null, bool shouldBeEnabled = true) : base(id){
            competenceElements = new List<IEditable[]>();

            SetName(name);
            SetTimeout(timeUnit);
            if (goal is IEditable[])
                SetGoal(goal);
            if (elementLists is List<IEditable[]>)
                SetParallelChildren(elementLists);
		    SetEnabled(shouldBeEnabled);
	    }
	
	    /**
	     * Sets whether this element is enabled
	     */
	    public override void SetEnabled(bool newValue) {
		    SetEnabledRecursive(newValue);
	    }

	    /**
	     * Get the goal list
	     *
	     * @return Arraylist of actionelements that comprise the goal
	     **/
	    public IEditable[] GetGoal() {
		    return alGoal.ToArray();
	    }


	    /**
	     * Get the timeout of this competence
	     *
	     * @return Timeout of this competence
	     **/
	    public TimeUnit GetTimeout() {
		    return tTimeout;
	    }
	
	    /**
	     * Set the goal
	     *
	     * @param goal Array of action elements that comprises the goal
	     **/
	    public void SetGoal(IEditable[] goal) {
            if (goal == null)
                return;
            List<ActionElement> succ = new List<ActionElement>();
            foreach (IEditable elem in goal)
            {
                ActionElement ae = elem as ActionElement;
                if (ae == null)
                    continue;

                ae.SetParent(this);
                succ.Add(ae);
            }

            alGoal = succ;
	    }

        /**
         * Add element to the goal definition
         *
         * @param goal action elements that contributes to the goal
         **/
        public void AddGoal(IEditable goal)
        {
            ActionElement ae = goal as ActionElement;
            if (ae == null)
                return;

            ae.SetParent(this);
            alGoal.Add(ae);
        }


	    /**
	     * Set the timeout of this competence.
	     *
	     * @param timeout Timeout value
	     **/
	    public void SetTimeout(TimeUnit timeout) {
            timeout.SetParent(this);
		    tTimeout = timeout;
	    }

        public override object Clone()
        {
            return ModelFactory.build().CloneCompetence(this);
        }
    }
}
