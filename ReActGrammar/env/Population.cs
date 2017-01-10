using System;
using ReAct.Grammar.elements;

namespace ReAct.Grammar.env
{
    public class Population : IPopulation,ICloneable
    {
        public Configuration gpConfiguration { get; private set; }
        public IChromosomePool chromPool { get; private set; }
        public IProgramPool programmPool { get; private set; }


        private Population(Population oldPop)
            : this((Configuration) oldPop.gpConfiguration.Clone())
        {

        }

        public Population(Configuration conf)
        {
            this.gpConfiguration = conf;
        }

    
        public object  Clone()
        {
            return new Population(this);
        }
    }   
}
