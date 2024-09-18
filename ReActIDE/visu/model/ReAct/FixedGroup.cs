using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace posh.visu.model.ReAct
{
    class FixedGroup : AEditable
    {
    
        // Time interval/timeout (Ymir-like!)
        private TimeUnit tTimeOut = null;

	    private bool enabled = true;
	
	
	    /**
	     * Create an Action Pattern with a pre-loaded list of elements
	     *
	     * @param name Name of this element grouping
	     * @param time Time unit for our interval/timeout (Ymir-style)
	     **/
	    public FixedGroup(string id, string name, TimeUnit time, IEditable [] children = null, bool shouldBeEnabled = true) : base(id) {
            SetName(name);
            if (children is IEditable[])
                this.SetChildren(children);
		    tTimeOut = time;
		    this.SetEnabled(shouldBeEnabled);
	    }

	
	
	    /**
	     * Set whether or not this element is enabled
	     */
	    public override void SetEnabled(bool newValue) {
		    SetEnabledRecursive(newValue);
	    }

	    /**
	     * Get the timeout for this element grouping
	     *
	     * @return Time unit for our ymir like timeout/interval
	     **/
	    public TimeUnit getTimeUnit() {
		    return tTimeOut;
	    }

	    /**
	     * Set the timeout/interval for this element grouping
	     *
	     * @param unit The new timeout for this element grouping.
	     **/
	    public void setTimeUnit(TimeUnit unit) {
		    tTimeOut = unit;
	    }


        public override object Clone()
        {
            return ModelFactory.build().CloneActionPattern(this);
        }
    }
}
