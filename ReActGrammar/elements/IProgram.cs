using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReAct.sys;

namespace ReAct.Grammar.elements
{
    public interface IProgram
    {
        IChromosome GetChromosome();

        AgentBase GetAgent();

        double GetFitnessValue();

    }
}
