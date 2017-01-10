using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using posh.visu.model;

namespace posh.visu.visual
{
    class VActionPattern : ATreeNode
    {
        public VActionPattern(IEditable model)
            : base(model)
        {
        }

        internal override ATreeNode AddNode(model.IEditable model)
        {
            throw new NotImplementedException();
        }
    }
}
