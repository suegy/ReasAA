using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReAct.sys.untimed
{
    public enum ExecutionState
    {
        Error = -1, NONE = 0,Started = 1, Running = 2, Finished = 3, Abort = 4
    }


    /// <summary>
    /// The result of firing a plan element.
    /// 
    /// This result determines three things:
    ///     - where in the execution of the subtree we are
    /// 
    ///     - if we want to continue executing the next element of the plan
    ///     
    ///     - the plan element to execute in the next step, given that we are
    ///       continuing to execute the current plan of the step.
    /// 
    /// Continuing the execution means to either to fire the
    /// same element in the next execution step, or to descend further
    /// in the plan tree and fire the next element in that tree.
    /// The next element to execute also needs to be given. 
    /// 
    /// If we are not continuing the execution of the current part of the
    /// plan, the currently fired drive element returns to the root of the plan.
    /// </summary>
    public struct FireResult
    {
        private PlanElement next;
        private ExecutionState state;
        private bool result;
        private static FireResult zero = new FireResult(false, null, ExecutionState.NONE);
        /// <summary>
        /// Initialises the result of firing an element.
        /// 
        /// </summary>
        /// <param name="continueExecution">If we want to continue executing the current
        /// part of the plan.</param>
        /// <param name="nextElement">The next plan element to fire.</param>
        public FireResult(bool result, PlanElement executioningNext, ExecutionState state)
        {
            this.state = state;
            this.result = result;
            if ( state != ExecutionState.Abort && state != ExecutionState.Error  && executioningNext is ElementBase)
                // copy the next element, if there is one
                // FIX: @swen: I do not see the need for copying loads of elements when they can be referenced instead.
                // FIXME: @ check if this still works when not cloned
                next = executioningNext;
            else
                next = null;
        }

        /// <summary>
        /// Returns the element to fire at the next step.
        /// </summary>
        /// <returns></returns>
        public PlanElement NextElement
        {
            get
            {
                return next;
            }
        }

        public bool Result
        {
            get
            {
                return result;
            }
        }

        /// <summary>
        /// Returns the element to fire at the next step.
        /// </summary>
        /// <returns></returns>
        public ExecutionState State
        {
            get
            {
                return state;
            }
        }

        public static FireResult Zero
        {
            get
            {
                return zero; 
            }
        }

    }
}
