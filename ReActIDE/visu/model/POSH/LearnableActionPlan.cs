using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReAct.sys;

namespace posh.visu.model.POSH
{
    /**
     * A LearnableActionPattern encapsulates the various constructs within the LAP
     * file as an arraylist of elements which are drive collections, action patterns
     * and competances
     */
    class LearnableActionPlan : AEditable
    {
        

        private Documentation lapDocumentation;

	
	    /**
	     * Initialize this BOD file with a pre-loaded arraylist of elements and some
	     * documentation
	     */
	    internal LearnableActionPlan(string id, string name, IEditable [] elements = null,Documentation d = null) : base(id)
        {
		    
            strName = name;
		    AddChilden(elements);

            if (d is Documentation)
                this.lapDocumentation = d;
	    }


        public void SetLapDocumentation(Documentation doc)
        {
            if (doc is Documentation)
                this.lapDocumentation = doc;
        }

        public Documentation GetLapDocumentation()
        {
            return this.lapDocumentation;
        }

	    /**
	     * Do we contain an element with the name given?
	     */
	    public bool ContainsElementNamed(string name) {
		    return ContainsChildRecursive(name);
	    }

	    public void PrintElements() {
		    Console.Out.WriteLine("-------------");
		    Console.Out.WriteLine(PrintChildRecursive("--", "ae---"));
		    Console.Out.WriteLine("-------------");
	    }
	
	    /**
	     * Get an element with the name given
	     */
	    public IEditable GetElementNamed(string name) {
		    this.PrintElements();
		
		    return GetChildRecursive(name);
	    }





        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
