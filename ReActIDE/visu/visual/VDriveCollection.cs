using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using posh.visu.model;

namespace posh.visu.visual
{
    class VDriveCollection : ATreeNode
    {
        public VDriveCollection(IEditable model)
            : base(model)
        {
            this.BackColor = Color.Sienna;
            this.SelectedImageIndex = 0;
            this.Tag = model;
            this.ToolTipText = "DriveCollection";
        }

        public void SetChildren(IEditable[] children)
        {

        }

        internal override ATreeNode AddNode(IEditable model)
        {
            throw new NotImplementedException();
        }
    }
}
