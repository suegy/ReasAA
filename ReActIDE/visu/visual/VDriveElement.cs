using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using posh.visu.model;

namespace posh.visu.visual
{
    class VDriveElement : TreeNode
    {
        public VDriveElement(string name, IEditable model)
            : base(name)
        {
            this.BackColor = Color.Sienna;
            this.SelectedImageIndex = 0;
            this.Tag = model;
            this.ToolTipText = "DriveElement";
        }
    }
}
