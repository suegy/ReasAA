using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace posh.visu.model
{
    public class Documentation
    {

        // File Title
        public string strTitle { get; protected set;}

        // Author information
        public string strAuthor { protected set; get; }

        // Memo
        public  string strMemo { protected set; get; }

        /**
         * Initialize a blank documentation construct
         */
        public Documentation()
        {
            strTitle = "Your Title";
            strAuthor = "Your Name";
            strMemo = "Your file comments";
        }

        public Documentation(String title, String authorInfo, String memo) {
		    strTitle = title;
		    strAuthor = authorInfo;
		    strMemo = memo;
	    }
    }
}
