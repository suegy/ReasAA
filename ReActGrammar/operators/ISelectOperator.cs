using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReAct.Grammar.elements;
using ReAct.Grammar.env;

namespace ReAct.Grammar.operators
{
    public interface ISelectOperator
    {
        void AddPrograms(IProgram[] progs);

        void AddProgram(IProgram progs);

        void SetConfiguration(Configuration config);

        IProgram SelectIndividuum(GenoType genotype);

        bool Reset();

        void Empty();

    }
}
