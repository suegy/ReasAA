using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using posh.visu.model;

namespace posh.visu.visual
{
    abstract class ATreeNode : TreeNode
    {
        public IEditable model { private set; get; }

        public ATreeNode(IEditable model)
            : base(model.GetName())
        {
            this.model = model;
        }

        public virtual void AddChildren(bool recursiveAdd)
        {
            if (!recursiveAdd)
            {
                foreach (AEditable child in ((AEditable)model).GetChildren())
                    this.Nodes.Add(AddNode(child));
                return;
            }

            foreach (AEditable child in ((AEditable)model).GetChildren())
            {
                ATreeNode cNode = AddNode(child);
                this.Nodes.Add(cNode);
                cNode.AddChildren(recursiveAdd);
            }

        }

        internal abstract ATreeNode AddNode(IEditable model);
    }
}
