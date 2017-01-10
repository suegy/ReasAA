using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace posh.visu.model.POSH
{
    /**
    *  A Competence Element is is a named trigger and an action, along with
    *  an optional number of retries.
    **/
    class CompetenceElement :AEditable
    {
        
	    // Arraylist containing ActionElement triggers
	    private List<ActionElement> alTrigger = null;

	    // Name of action to invoke
	    private string strAction = null;

	    // How many times to retry?
	    private int iRetries = 0;
	
	    // Is this element collapsed / hidden
	    private bool collapsed = false;
	
	    /**
	     * Initialize this Competence Element with a name, a list of triggers, an action
	     * and the optional number of retries IS NOT being specified.
	     *
	     * @param name Name of this competence
	     * @param triggerList List of actionelements comprising the trigger
	     * @param action Action to invoke
	     **/
        internal CompetenceElement(string id, string name, IEditable[] triggerList = null, string action = "", int retries = 0)
            : base(id)
        {
		    SetName(name);
            if (triggerList is IEditable[])
                SetTrigger(triggerList);
            else
                alTrigger = new List<ActionElement>();
		    SetAction(action);
            SetRetries(retries);
	    }

        /**
	     * Get the action
	     *
	     * @return Name of our invoked action
	     **/
	    public string GetAction() {
		    return strAction;
	    }

	    /**
	     * Get the number of retries for this element
	     *
	     * @return Number of retries
	     **/
	    public int GetRetries() {
		    return iRetries;
	    }

	    /**
	     * Get the arraylist of action elements that comprises our trigger.
	     *
	     * @return Arraylist of action eleemtns comprising the trigger
	     **/
	    public IEditable[] GetTrigger() {
		    return alTrigger.ToArray();
	    }

	    /**
	     * Set the name of the action to invoke
	     *
	     * @param action Name of new action to invoke
	     **/
	    public void SetAction(string action) {
		    strAction = action;
	    }

	    /**
	     * Set the number of retries for this element.
	     *
	     * @param val New number of retries.
	     **/
	    public void SetRetries(int val) {
		    iRetries = val;
	    }

	    /**
	     * Set our list of triggers
	     **/
	    public void SetTrigger(IEditable[] list) {
            
            List<ActionElement> triggerList = new List<ActionElement>();
            foreach (IEditable trigger in list)
            {
                ActionElement ae = trigger as ActionElement;
                if (ae != null && ae.IsSense())
                {
                    triggerList.Add(ae);
                    ae.SetParent(this);
                }

            }
		    alTrigger = triggerList;
	    }

        	    /**
	     * Set our list of triggers
	     **/
        public void AddTrigger(IEditable trigger)
        {
            ActionElement ae = trigger as ActionElement;

            if (ae != null && ae.IsSense())
            {
                alTrigger.Add(ae);
                ae.SetParent(this);
            }
        }
            
		    

        public override object Clone()
        {
            return ModelFactory.build().CloneCompetenceElement(this);
        }
    }
}
