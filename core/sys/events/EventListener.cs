using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReAct.sys.untimed;

namespace ReAct.sys.events
{
    /// <summary>
    /// A EventListener listens for specific events to ocure and tracks them in the background. The Listener is used for analysing the traversal of the plan and for debugging. 
    /// </summary>
    public class EventListener : IListener
    {

        public Stack<Tuple<EventType, object, EventArgs>> eventStack;

        /// <summary>
        /// A ReActListner listens for specific events to ocure and tracks them in the background. The Listener is used for analysing the traversal of the plan and for debugging. 
        /// </summary>
        public EventListener()
        {
            eventStack = new Stack<Tuple<EventType, object, EventArgs>>();
        }

        public void Subscribe(object p)
        {
            if (p is PlanElement)
            {
                PlanElement pE = p as PlanElement;
                pE.FireEvent += new FireHandler(Listen);
            }
        }

        public bool ListensFor(EventType evType)
        {
            return (evType == EventType.Fire) ? true : false;
        }

        private void Listen(EventType t, object p, EventArgs f)
        {
            eventStack.Push(new Tuple<EventType, object, EventArgs>(t, p, f));
        }
    }
}
