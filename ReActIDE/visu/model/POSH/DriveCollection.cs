using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace posh.visu.model.POSH
{
     /**
      * A (RealTime/Discrete Time) drive collection is a named goal and a set of
      * drive elements that work to achieve that goal.
      * 
      */
    class DriveCollection : AParallel
    {
       
        // Are we a realtime drive collection (False->Discrete Time)
	    private bool bIsRealTime = false;
	
	
	    private bool bIsStrict = false;

	    // Our goal (ArrayList<Object>of actionelements)
	    private List<IEditable>alGoal = null;

	
	    /**
	     * Initialize this drive collection
	     * 
	     * @param name
	     *            Name of the collection
	     * @param realTime
	     *            Is this a real-time drive collection?
	     * @param elements
	     *            ArrayList\<Object\> of drive elements (or lists thereof, to be more
	     *            precise)
	     */
	    internal  DriveCollection(string id, string name, bool realTime, bool strict, IEditable [] goal,
			    List<IEditable []> elements, bool shouldBeEnabled) : base(id) {
		    strName = name;
		    bIsRealTime = realTime;
		    bIsStrict = strict;
		    alGoal = (goal is IEditable[])? new List<IEditable>(goal) : new List<IEditable>() ;
            if (elements is List<IEditable[]>)
                SetParallelChildren(elements);
            this.SetEnabled(shouldBeEnabled);
	    }

	    

	    public override void SetEnabled(bool newValue) {
		    SetEnabledRecursive(newValue);
		}



	    /**
	     * Get our list of drive elements
	     * 
	     * @return Arraylist of lists of drive elements
	     */
	    public List<IEditable []> GetDriveElements() {
		    return GetParallelChildren();
	    }

	    /**
	     * Get the arraylist of actionelements that comprise our goal
	     * 
	     * @return Arraylist of action elements comprising our goal
	     */
	    public IEditable[] GetGoal() {
		    return alGoal.ToArray();
	    }

	    /**
	     * Get whether or not this is a real-time drive collection
	     * 
	     * @return True if real time drive collection, false otherwise
	     */
	    public bool IsRealTime() {
		    return bIsRealTime;
	    }

	    /**
	     * Set our list of drive elements to be some new list
	     * 
	     * @param drive
	     *            Drive element lists .
	     */
	    public void SetDriveElements(List<IEditable[]> driveElements) {
            this.SetParallelChildren(driveElements);
		}

	    /**
	     * Set our goal list
	     */
	    public void SetGoal( IEditable[] goal) {
            alGoal = (goal is IEditable[]) ? new List<IEditable>(goal) : new List<IEditable>(); ;
	    }

	    /**
	     * Set whether or not this is a real-time drive collection
	     * 
	     * @param real
	     *            Real time if true, discrete time if not
	     */
	    public void SetRealTime(bool real) {
		    bIsRealTime = real;
	    }
	
	
	    public bool IsStrictMode() {
		    return bIsStrict;
	    }
	
	    public void SetStrictMode(bool isStrict) {
		    bIsStrict = isStrict;
	    }


        public override object Clone()
        {
            return ModelFactory.build().CloneDriveCollection(this);
        }
    }
}
