using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using posh.visu.model.ReAct;

namespace posh.visu.model
{
    abstract class AEditable : IEditable,ICloneable
    {

        protected IEditable parent;
        protected string id;
        protected string strName;
        private List<IEditable> children;
        private string doc;
        private bool enabled;
        public bool active { get; protected set; }


        protected AEditable(string id)
        {
            this.enabled = true;
            this.id = id;
            this.children = new List<IEditable>();
        }


        public virtual void SetEnabled(bool newValue)
        {
            this.enabled = newValue;
        }

        public bool IsEnabled()
        {
            return this.enabled;
        }

        protected internal virtual void SetEnabledRecursive(bool newValue)
        {
		    this.enabled = newValue;
		    if (children is List<IEditable> && children.Count > 0)
			    foreach (AEditable child in children)
			        child.SetEnabled(newValue);
	    }

        public string GetID()
        {
            return this.id;
        }

        public void SetName(string name)
        {
            this.strName = name;
        }

        public string GetName()
        {
            return this.strName;
        }

        public IEditable GetParent()
        {
            return this.parent;
        }

        internal void SetParent(IEditable parent)
        {
            this.parent = parent;
        }

        
        public void SetDocumentation(string newDocumentation)
        {
            this.doc = newDocumentation;
        }

        public string GetDocumentation() 
        {
            return this.doc;
        }

        internal void SetChildren(IEditable[] list)
        {
            this.children.Clear();
            AddChilden(list);
        }

        public void AddChilden(IEditable[] list){
		    foreach (IEditable elem in list) 
			    AddChild(elem);
		}

        public bool AddChild(IEditable child){
		if (child is AEditable){
			this.children.Add((AEditable) child);
			((AEditable) child).SetParent(this);
			return true;
		}
		return false;
	}

        public bool AddChild(IEditable child, int pos){
		if (child is AEditable){
			this.children.Insert(pos, (AEditable) child);
			((AEditable) child).SetParent(this);
			return true;
		}
		return false;
	}

        public virtual IEditable GetDirectChild(string name){
		foreach (AEditable child in children) 
			if (child.GetName().Equals(name))
				return child;
		
		return null;
	}

        /**
         * Searches for the ID Breadth First
         * @param childID
         * @return
         */
        internal virtual IEditable GetChildRecursive(string childID){
		
		if (children == null || children.Count < 1)
			return null;
		if (ContainsChild(childID))
			return GetDirectChild(childID);
		foreach (AEditable child in children){
			IEditable result;
			if ( ( result = child.GetChildRecursive(childID)) != null)
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
        protected internal virtual List<IEditable> GetChildRecursive(List<IEditable> elements, Type searchType){
		if (!(elements is List<IEditable>))
			elements = new List<IEditable>();
		if (this.children == null || this.children.Count < 1)
			return elements;
		
		foreach (IEditable child in this.children) {
			if ( child.GetType().IsSubclassOf(searchType))
				elements.Add(child);
			elements = ((AEditable)child).GetChildRecursive(elements, searchType);
		}

		return elements;
	}


        protected internal virtual int RemoveChildRecursive(string childID){
		int result;
		
		if (children == null || this.children.Count < 1)
			return -1;
		if (ContainsChild(childID)){
			foreach (AEditable elem in children) 
				if (elem.GetName().Equals(childID)){
					result = children.IndexOf(elem);
					children.Remove(elem);
					return result;
				}
		}

		foreach (AEditable elem in children){
			if (( result = elem.RemoveChildRecursive(childID)) != -1)
				 return result;
		}
		return -1;
	}

        protected internal string PrintChildRecursive(string prefix, string childID){
		string result = "";
		
		if (children == null || this.children.Count < 1)
			return "";
		
		if (ContainsChild(childID))
			return prefix +" "+ childID+"\n";
		
		foreach (AEditable child in children){
			string answer = "";
			result += prefix +" "+ child.id+"\n";
			if (!(answer = child.PrintChildRecursive(prefix+prefix,childID)).Equals(string.Empty))
				return result += answer;
				 
		}
		return result;
	}


        protected internal IEditable[] GetChildren(){
		    if (this.children is List<IEditable>){
			    return this.children.ToArray();
		    }
		    return null;
	    }

        public virtual bool ContainsChild(string name){
		    foreach (AEditable child in children) 
			    if (child.GetName().Equals(name))
				    return true;
		
		    return false;
	    }

        public virtual bool ContainsChildRecursive(string name){
		    if (ContainsChild(name))
			    return true;
		    foreach (AEditable child in children) 
			     if (child.ContainsChildRecursive(name))
				     return true;
		
		    return false;
	    }

        public abstract object Clone();
        

        public void Dispose()
        {
            ModelFactory.build().Remove(this);
        }
    }
}
