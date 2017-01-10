using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReAct.Grammar.operators
{
    public interface IFitnessEvaluator
    {
        bool IsFitter(object one, object two);


    }
}
