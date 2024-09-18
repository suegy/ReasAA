using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using posh.visu.model.ReAct;
using System.IO;

namespace posh.visu.control.io
{
    /**
     * Interface for LAP Readers
     */
    interface ILAPReader
    {
         bool IsLAPFile(String fileName);

         bool IsDebugMode();

         void SetDebugMode(bool mode);

         LearnableActionPlan Load(String fileName);

         LearnableActionPlan Load(Stream file);
    }
}
