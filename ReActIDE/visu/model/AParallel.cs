using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace posh.visu.model
{
    abstract class AParallel : AEditable
    {
        private List<List<AEditable>> parallelChildren;
       

        protected AParallel(string id) : base(id)
        {
            parallelChildren = new List<List<AEditable>>();
        }

        protected internal override void SetEnabledRecursive(bool newValue) 
        {
            base.SetEnabled(newValue);
            if (parallelChildren is List<List<AEditable>> && parallelChildren.Count > 0)
			    foreach (List<AEditable> parallel in parallelChildren)
                    foreach (AEditable child in parallel)
			            child.SetEnabledRecursive(newValue);
	    }

        internal void SetParallelChildren(List<IEditable[]> list)
        {
            this.parallelChildren.Clear();
            AddParallelChilden(list);
        }

        public void AddParallelChilden(List<IEditable[]> list)
        {
            foreach (IEditable[] elem in list)
                AddParallelChild(elem,false);
        }

        

        public bool AddParallelChild(IEditable[] child, bool parallel, int pos = -1,int depth = 0 ){
		    if (child is AEditable[]){
                List<AEditable> t = new List<AEditable>(child as AEditable[]);

                if (parallel && pos > 0 && parallelChildren.Count < pos - 1 && depth >= 0)
                {
                    parallelChildren[pos].InsertRange(depth,t);
                }
                else if (!parallel && pos > 0 && parallelChildren.Count < pos - 1)
                {
                    List<List<AEditable>> b = new List<List<AEditable>>();
                    b.Add(t);
                    parallelChildren.InsertRange(pos, b);
                }
                else
                {
                    parallelChildren.Add(t);
                }
                
			    foreach (AEditable pChild in t)
			        pChild.SetParent(this);
			    return true;
		    }
		    return false;
	    }

        public override IEditable GetDirectChild(string name){
		    foreach (List<AEditable> child in parallelChildren)
                foreach (AEditable pChild in child)
			        if (pChild.GetName().Equals(name))
				        return pChild;
		
		    return null;
	    }

        /**
         * Searches for the ID Breadth First
         * @param childID
         * @return
         */
        internal override IEditable GetChildRecursive(string childID){
		
		    if (parallelChildren == null || parallelChildren.Count < 1)
			    return null;
		    if (ContainsChild(childID))
			    return GetDirectChild(childID);
            foreach (List<AEditable> child in parallelChildren)
                foreach (AEditable pChild in child)
                {
			    IEditable result;
			    if ( ( result = pChild.GetChildRecursive(childID)) != null)
				     return result;
		        }
		    return null;
	    }

        /**
         * Searches all sub nodes of the current child for a specific node type. 
         * If a node is of a specific type it is added to the return result.
         * @param elements a temporary return list of elements
         * @param searchType type of class to search for
         * @return List\<IEditable\> of all elements of the search type or empty arraylist if none is found
         */
        protected internal override List<IEditable> GetChildRecursive(List<IEditable> elements, Type searchType)
        {
		    if (!(elements is List<IEditable>))
			    elements = new List<IEditable>();
		    if (this.parallelChildren == null || this.parallelChildren.Count < 1)
			    return elements;

            foreach (List<AEditable> child in this.parallelChildren)
                foreach (AEditable pChild in child)
                {
			        if ( pChild.GetType().IsSubclassOf(searchType))
				            elements.Add(pChild);
			        elements = ((AEditable)pChild).GetChildRecursive(elements, searchType);
		        }

		    return elements;
	    }


        protected internal override int RemoveChildRecursive(string childID){
		    int result;
		
		    if (parallelChildren == null || this.parallelChildren.Count < 1)
			    return -1;
		    if (ContainsChild(childID)){
                foreach (List<AEditable> child in parallelChildren)
                    foreach (AEditable pChild in child)
				        if (pChild.GetName().Equals(childID)){
					        result = parallelChildren.IndexOf(child);
					        child.Remove(pChild);
					        return result;
				        }
		    }

            foreach (List<AEditable> elem in parallelChildren)
                foreach (AEditable pChild in elem)
                {
			        if (( result = pChild.RemoveChildRecursive(childID)) != -1)
				         return result;
		        }
		    return -1;
	    }

        protected string PrintParallelChildRecursive(string prefix, string childID){
		string result = "";
		
		if (parallelChildren == null || this.parallelChildren.Count < 1)
			return "";
		
		if (ContainsChild(childID))
			return prefix +" "+ childID+"\n";

        foreach (List<AEditable> elem in parallelChildren)
            foreach (AEditable pChild in elem)
            {
			    string answer = "";
			    result += prefix +" "+ pChild.GetID()+"\n";
			    if (!(answer = pChild.PrintChildRecursive(prefix+prefix,childID)).Equals(string.Empty))
				    return result += answer;
            }
		return result;
	}


        protected internal List<IEditable[]> GetParallelChildren(){
		    if (!(this.parallelChildren is List<List<AEditable>>))
                return null;
            List<IEditable[]> result = new List<IEditable[]>();
            foreach(List<AEditable> subList in parallelChildren)
                result.Add(subList.ToArray());
			
		    return result;
	    }

        public override bool ContainsChild(string name){
            foreach (List<AEditable> elem in parallelChildren)
                foreach (AEditable pChild in elem)
			        if (pChild.GetName().Equals(name))
				        return true;
		
		    return false;
	    }

        public override bool ContainsChildRecursive(string name){
		if (ContainsChild(name))
			return true;
        foreach (List<AEditable> elem in parallelChildren)
            foreach (AEditable pChild in elem)
			     if (pChild.ContainsChildRecursive(name))
				     return true;
		
		return false;
    	}
        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
