﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReAct.Grammar.env;
using ReAct.sys;
using ReAct.Grammar.elements.ReAct;
using ReAct.sys.events;

namespace ReAct.Grammar.elements
{
    class ReActProgram : IProgram
    {
        public Configuration m_gpConfig { get; protected set; }
        
        public IChromosome m_chomosome { get; protected set; }

        public AgentBase m_agent { get; protected set; }

        public DriveCollection m_drive { get; protected set; }

        public IListener m_listen { get; protected set; }

        private double fitness;

        public ReActProgram(Configuration config, AgentBase agent, GPPlanBuilder builder)
        {
            m_gpConfig = config;
            m_agent = agent;
            m_listen = new EventListener();
            m_agent.RegisterListener(m_listen);

            m_chomosome = new Chromosome(config);
            m_drive = builder.Build(m_chomosome, config);
        }

        public IChromosome  GetChromosome()
        {
            return m_chomosome;
        }

        public AgentBase GetAgent()
        {
            return m_agent;
        }


        public double GetFitnessValue()
        {
            // TODO: needs to be implemented
            return fitness;
        }
    }
}
