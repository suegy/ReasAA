using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using posh.visu.model.POSH;
using System.IO;
using ReAct.sys;
using ReAct.sys.parse;

namespace posh.visu.control.io
{
    class DotLAPReader : ILAPReader
    {
        private bool debug;
        private string lastFile;

        public bool IsLAPFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileStream data = File.OpenRead(fileName);
                if (data.CanRead)
                {
                    data.Close();
                    return true;
                }
            }

            return false;
        }

        public bool IsDebugMode()
        {
            return debug;
        }

        public void SetDebugMode(bool mode)
        {
            this.debug = mode;
        }

        public LearnableActionPlan Load(Stream file)
        {
            ModelFactory.build().CreateLearnableActionPlan(GetFileData(file));
            return null;
        }


        private PlanBuilder GetFileData(Stream file)
        {
            lastFile = new StreamReader(file).ReadToEnd();
            LAPParser parser = new LAPParser();
            PlanBuilder builder = parser.Parse(lastFile);
            
            return builder;

        }


        public LearnableActionPlan Load(string fileName)
        {
            throw new NotImplementedException();
        }

        public string GetLastFile()
        {
            return lastFile;
        }

    }
}
