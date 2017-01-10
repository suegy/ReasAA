using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using posh.visu.model.POSH;

namespace posh.visu.model
{
    /**
     * A TimeUnit is a measurement of time with two attributes, the interval and the
     * unit of measurement. For example, 10 seconds, 5 min, and so forth.
     * 
    */
    class TimeUnit : AEditable
    {
        // Frequency, delay or Interval this time unit represents
	    private double dInterval = 0;
        
        /**
	    * Initialize this unit time
	    */
        protected internal TimeUnit(string id, string unit, double value, IEditable parent = null) : base(id) {
		    SetParent(parent);
		    SetName(unit);
		    dInterval = value;
	    }

        /**
    	 * Get our interval value for this unit time
	     */
    	public double GetUnitValue() {
    		return dInterval;
	    }

        /**
         * Set the value of this unit of time
         */
        public void SetUnitValue(double value)
        {
            dInterval = value;
        }

        public override object  Clone()
        {
            return ModelFactory.build().CloneTimeUnit(this);
        }
}

    
	

	
	
	



	



	
	
	
}
