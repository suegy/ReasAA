﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReAct.Grammar.elements;
using ReAct.sys;
using ReAct.Grammar.elements.ReAct;

namespace ReAct.Grammar.env
{
    public class GenoType 
    {

        private Configuration m_config;

        private Dictionary<string, ReActProgram> m_programs;

        private IGenePool m_pool;

        private int m_maxNodes;

        public bool verbose { get; protected set; }

        private GenoType() 
        {
        }

        public static GenoType RandomGenotype(Configuration config, AGene[] genes)
        {
            return new GenoType();
        }

        public static GenoType LoadReActGenotype(Configuration config, GPPlanBuilder [] poshPlans, AgentBase[] agents, AGene[] genes)
        {
            IGenePool pool = new GenePool();
            foreach (AGene gene in genes)
               pool.AddGene(gene);

            return LoadReActGenotype(config,poshPlans,agents,pool);
        }

        public static GenoType LoadReActGenotype(Configuration config, GPPlanBuilder[] poshPlans, AgentBase[] agents, IGenePool pool)
        {
            GenoType geno = new GenoType();
            geno.m_programs = new Dictionary<string, ReActProgram>();
            geno.m_config = config;
            geno.m_pool = (pool  is IGenePool) ? pool : new GenePool();

            if (agents.Length == poshPlans.Length)
                for (int i = 0;i< agents.Length;i++) 
                {
                    geno.AddNewProgram(config, agents[i], poshPlans[i]);
                 }

            geno.verbose = config.verbose;
            geno.m_maxNodes = config.maxNodes;

            return geno;
        }


        private bool AddNewProgram(Configuration config, AgentBase agent, GPPlanBuilder builder)
        {

            if (m_programs.ContainsKey(agent.id))
                return false;

            ReActProgram prog = new ReActProgram(config,agent,builder);
            m_programs[agent.id] = prog;

            return true;
        }

        public bool Evolve()
        {
            throw new NotImplementedException();
        }

        public Configuration GetConfiguration()
        {
            return m_config;
        }


        internal IProgram[] GetAllPrograms()
        {
            return m_programs.Values.ToArray();
        }
    }
}
