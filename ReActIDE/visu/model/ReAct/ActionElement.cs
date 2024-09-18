using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace posh.visu.model.ReAct
{
    class ActionElement : AEditable
    {
        // Is this a sense?
	    private bool isSense = false;

	    // Value input for the sense for comparison
	    private string strValue = null;

	    // Comparator / Predicate
	    private string strComparator = null;
	
	    /**
	     * Create an action element with a sense name, a value and a predicate
	     * @param strSense      Name of the sense we're constructing
	     * @param strVal        Value comparing against
	     * @param pred          Predicate used for comparison (i.e. < or >)
	     **/
        internal  ActionElement(string id, string strSense, bool isSense = true, string strVal = "", string pred = "")
            : base(id)
        {
            SetIsSense(isSense);
            SetName(strSense);
            strValue = strVal;
		    strComparator = pred;
	    }
	
	    /**
	     * Return whether or not we represent a sense
	     *
	     * @return  True if we're a sense, false if we're an action or composite.
	     **/
	    public bool IsSense() {
		    return isSense;
	    }

	    /**
	     * Get the predicate we're using
	     *
	     * @return  The predicate used for comparisons or null if none is specified.
	     **/
	    public string GetPredicate() {
		    return strComparator;
	    }

	    /**
	     * Get the value of the argument of this element
	     * 
	     * @return  Value we're being compared against
	     **/
	    public string GetValue() {
		    return strValue;
	    }

	    /**
	     * Set whether or not we're a sense
	     **/
	    public void SetIsSense(bool isSense) {
		    this.isSense = isSense;
	    }

	    /**
	     * Set the predicate this action element uses
	     **/
	    public void SetPredicate(string pred) {
		    strComparator = pred;
	    }

	    /**
	     * Set the value to compare against
	     **/
	    public void SetValue(string val) {
		    strValue = val;
	    }



        /** Clone interface implementation,
	     * used for duplicating ActionElements.
	     */
	    public override object Clone()
        {
            return ModelFactory.build().CloneActionElement(this);
        }
    }
}
