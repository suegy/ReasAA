using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace posh.visu.model.POSH
{
    /**
     * A Drive Element is a name, a trigger list of action elements and a named
     * action, and an optional timeout for scheduling frequency
     * the action which is triggered is kept as a child and can be set and reset using GetChild/SetChild
     * 
    */
    class DriveElement :  AEditable
    {
        // Our list of ActionElements that comprises the trigger
	    private List<IEditable> alTrigger = null;

	    // Frequency for scheduling
	    private TimeUnit tFrequency = null;
	

	    /**
	     * Create this drive element, with a name, trigger (list of action elements)
	     * and a corresponding name of action to invoke, as well as a scheduling
	     * frequency.
	     * 
	     * @param name
	     *            Name of the drive element
	     * @param trigger
	     *            Arraylist of action elements comprising our trigger
	     * @param action
	     *            Posh root to trigger
	     * @param freq
	     *            Frequency specification for this drive element
	     */
	    public DriveElement(string id, string name, IEditable[] trigger = null, IEditable action = null, TimeUnit freq = null) : base(id){
		    SetName(name);
		    alTrigger = new List<IEditable>();
		    if (trigger != null)
			    foreach (IEditable iEditable in trigger) {
				    alTrigger.Add(iEditable);
			    }
            if (action is IEditable)
		       AddChild(action);
		    if (freq is TimeUnit)
                tFrequency = freq;
	    }
	

	    /**
	     * Sets whether the element is enabled
	     * @param Boolean indicating whether or not element should be enabled
	     */
	    public override void SetEnabled(bool value) {
		    this.SetEnabledRecursive(value);
		    //Disable children
		    foreach (IEditable iterable in this.alTrigger) {
			    iterable.SetEnabled(value);
		    }
	    }
	
        /**
	     * Get the scheduling frequency of this drive element
	     * 
	     * @return Frequency time unit
	     */
	
	    public TimeUnit GetFrequency() {
		    return tFrequency;
	    }

	    /**
	     * Get our trigger list
	     * 
	     * @return Trigger in form of arraylist
	     */
	    public IEditable[] GetTrigger() {
		    return alTrigger.ToArray();
	    }

	    /**
	     * Set our scheduling frequency
	     * 
	     * @param time
	     *            New frequency
	     */
	    public void SetFrequency(TimeUnit time) {
		    tFrequency = time;
	    }

	    /**
	     * Set our trigger (Should be arraylist of actionelement objects)
	     * 
	     * @param trigger
	     *            Arraylist of action elements comprising the trigger
	     */
	    public void SetTrigger(IEditable[] trigger) {
            List<IEditable> triggers = new List<IEditable>();
            foreach (IEditable elem in trigger)
            {
                ActionElement ae = elem as ActionElement;
                if (ae == null)
                    continue;

                ae.SetParent(this);
                triggers.Add(ae);
            }

            this.alTrigger = triggers;
		}

        public override object Clone()
        {
            return ModelFactory.build().CloneDriveElement(this);
        }
    }
}
