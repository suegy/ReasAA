using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReAct.Grammar.elements
{
    public interface IGenePool : ICloneable
    {
        bool AddGene(AGene gene);

        bool RemoveGene(AGene gene);

        bool RemoveGene(decimal id);

        bool Contains(AGene gene);

        AGene GetGene(decimal geneID);

        AGene[] GetAllGenes(ReAct.Grammar.elements.AGene.GeneType gType);
    }
}
