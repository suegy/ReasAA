using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using posh.visu.model;
using posh.visu.model.POSH;
using System.Drawing;

namespace posh.visu.visual
{
    class VActionElement : ATreeNode
    {
        public VActionElement(IEditable model)
            : base(model)
        {
            ActionElement ae = model as ActionElement;
            if (ae is ActionElement)
                SetNode(ae);
            
            this.Tag = model;
        }

        private void SetNode(ActionElement ae)
        {

            if (ae.IsSense())
            {
                this.BackColor = Color.SkyBlue;
                this.ToolTipText = String.Format("Sense: $1 $2", ae.GetPredicate(), ae.GetValue());
                return;
            }
            else
            {
                this.BackColor = Color.LawnGreen;
                this.ToolTipText = "Action";
                return;
            }
        }

        internal override ATreeNode AddNode(model.IEditable model)
        {
            return null;
        }
    }
}
