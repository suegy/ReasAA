using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReAct.sys;

namespace posh.visu.model.ReAct
{
    class ModelFactory
    {
        private Dictionary<string, LearnableActionPlan> learnableActionPatterns;
        private Dictionary<string, AEditable> createdElements;
        private long elementCounter;
        private static ModelFactory instance;

        private ModelFactory()
        {
            learnableActionPatterns = new Dictionary<string, LearnableActionPlan>();
            createdElements = new Dictionary<string, AEditable>();
            elementCounter = 0;

        }

        public static ModelFactory build()
        {
            if (instance is ModelFactory)
                return instance;
           
            instance = new ModelFactory();
            return instance;
        }

        /*
        * Removes an entity from the factory pool
        */
        internal bool Remove(AEditable aEditable)
        {
            if (aEditable.GetParent() is AEditable)
                ((AEditable)aEditable.GetParent()).RemoveChildRecursive(aEditable.GetID());
            return createdElements.Remove(aEditable.GetID());
        }

        private List<AEditable> DeepCloneChildren(AEditable parent,List<AEditable> result,IEditable[] children)
        {
            foreach (AEditable child in children)
            {
                AEditable clonedChild = (AEditable)child.Clone();
                clonedChild.SetParent(parent);
                result.Add(clonedChild);
            }
            return result;
        }

        public IEditable CreateActionElement(string strSense, bool isSense = true, string strVal = "", string pred = "")
        {
            string id = "ae-"+elementCounter++;
            createdElements[id]=new ActionElement(id,strSense,isSense,strVal,pred);

            return createdElements[id];
        }
        internal IEditable CloneActionElement(ActionElement actionElement)
        {
            ActionElement clone = (ActionElement) CreateActionElement(actionElement.GetName());
            clone.SetIsSense(actionElement.IsSense());
            clone.SetValue(actionElement.GetValue());
            clone.SetDocumentation(actionElement.GetDocumentation());
            clone.SetEnabled(actionElement.IsEnabled());
            clone.SetPredicate(actionElement.GetPredicate());
            clone.SetParent(actionElement.GetParent());
            
            return clone;
        }

        public IEditable CreateTimeUnit(string unit, double value, IEditable parent = null)
        {
            string id = "tu-"+elementCounter++;
            TimeUnit tu = new TimeUnit(id, unit, value, parent);
            this.createdElements[id] = tu;

            return tu;
        }

        internal IEditable CloneTimeUnit(TimeUnit timeUnit)
        {
            TimeUnit clone = (TimeUnit)CreateTimeUnit(timeUnit.GetName(),timeUnit.GetUnitValue());
            if (timeUnit.GetParent() is IEditable)
                clone.SetParent(timeUnit.GetParent());

            return clone;
        }

                                
        public IEditable CreateActionPattern(string name, TimeUnit timeUnit, IEditable[] children = null, bool isEnabled = true)
        {
            string id = "ap-"+elementCounter++;
            FixedGroup ap = new FixedGroup(id, name, timeUnit);
            if (children is IEditable[])
                ap.SetChildren(children);
            ap.SetEnabled(isEnabled);

            this.createdElements[id] = ap;
            return ap;
        }

        internal IEditable CloneActionPattern(FixedGroup actionPattern)
        {
            FixedGroup clone = (FixedGroup)CreateActionPattern(actionPattern.GetName(),actionPattern.getTimeUnit());
            clone.SetEnabled(actionPattern.IsEnabled());
            clone.SetParent(actionPattern.GetParent());
            clone.SetChildren(DeepCloneChildren(clone,new List<AEditable>(), actionPattern.GetChildren()).ToArray());
            return clone;
        }

        public IEditable CreateCompetenceElement(string name, IEditable[] triggerList = null, string action = "", int retries = 0)
        {
            string id = "ce-"+elementCounter++;
            CompetenceElement c = new CompetenceElement(id, name, triggerList, action, retries);
            this.createdElements[id] = c;

            return c;
        }


        internal object CloneCompetenceElement(CompetenceElement competenceElement)
        {
            CompetenceElement clone = (CompetenceElement)CreateCompetenceElement(competenceElement.GetName(),competenceElement.GetTrigger(),competenceElement.GetAction(),competenceElement.GetRetries());
            clone.SetParent(competenceElement.GetParent());

            return clone;
        }

        public IEditable CreateCompetence(string name, TimeUnit timeUnit, IEditable[] goal = null, List<IEditable[]> elementLists = null, bool shouldBeEnabled = true)
        {
            string id = "c-" + elementCounter++;
            Competence c = (Competence) new Competence(id, name, timeUnit, goal, elementLists, shouldBeEnabled);
            this.createdElements[id] = c;

            return c;
        }

        /**
         * creates a deep clone of a competence tree (super costly operation as the competence trees can be quite large)
         */
        internal object CloneCompetence(Competence competence)
        {
            Competence clone = (Competence) CreateCompetence(competence.GetName(), CloneTimeUnit(competence.GetTimeout()) as TimeUnit);
            clone.SetChildren(DeepCloneChildren(clone, new List<AEditable>(), competence.GetChildren()).ToArray());
            clone.SetGoal(DeepCloneChildren(clone, new List<AEditable>(), competence.GetGoal()).ToArray());

            return clone;
        }

        public IEditable CreateDriveElement(string name, IEditable[] trigger = null, IEditable action = null, TimeUnit freq = null)
        {
            string id = "de-" + elementCounter++;
            DriveElement de = new DriveElement(id,name,trigger,action,freq);
            this.createdElements[id] = de;

            return de;
        }

        internal object CloneDriveElement(DriveElement de)
        {
            DriveElement clone = (DriveElement)CreateDriveElement(de.GetName());
            clone.SetTrigger(DeepCloneChildren(de, new List<AEditable>(), de.GetTrigger()).ToArray());
            clone.AddChilden(DeepCloneChildren(de,new List<AEditable>(),de.GetChildren()).ToArray());

            return clone;
        }

        /**
         * The drive collection as 
        ///    (id, name, goal, [priority1, priority2, ...])
         */
        public IEditable CreateDriveCollection(String name, bool realTime=true, bool strict=true, IEditable [] goal=null,
			    List<IEditable[]> elements=null, bool shouldBeEnable=true)
        {
            string id = "dc-" + elementCounter++;
            
            DriveCollection dc = new DriveCollection(id, name, realTime,strict,goal,elements,shouldBeEnable);
            this.createdElements[id] = dc;
            return dc;
        }
        
        internal object CloneDriveCollection(DriveCollection dc)
        {
            DriveCollection clone = (DriveCollection) CreateDriveCollection(dc.GetName(),dc.IsRealTime(),dc.IsStrictMode());
            clone.SetGoal(dc.GetGoal());
            clone.SetDriveElements(dc.GetDriveElements());
            clone.SetEnabled(dc.IsEnabled());

            return clone;
        }
        
        public IEditable CreateLearnableActionPlan(string name, IEditable[] elements = null, Documentation d = null)
        {
            string id = "lap-" + elementCounter++;

            LearnableActionPlan lap = new LearnableActionPlan(id, name);
            lap.SetLapDocumentation(d);
            lap.SetChildren(elements);
            this.createdElements[id] = lap;

            return lap;
        }

        /**
         * The drive collection as imported by the Lexer
         * @builder Planbuilder providing structural information about the Driveollection (<type>, <name>, Tuple<string,string,string> [] \<goal\>, [priority1, priority2, ...])
         */
        internal IEditable CreateLearnableActionPlan(PlanBuilder builder)
        {
            //@TODO(swen): current lap files only contain a single drivecollection whereas mutiple can be loaded for parallel execution 
            LearnableActionPlan plan = (LearnableActionPlan)CreateLearnableActionPlan("agent");
            string name = builder.driveCollection.Second;
            string planType = builder.driveCollection.First;
            DriveCollection dc  = (DriveCollection) CreateDriveCollection( name, planType.Contains('R') ? true:false, planType.Contains('S') ? true:false );
            
            //constructing the goals of the drivecollection 
            List<IEditable> goals = ConstructSenses(builder.driveCollection.Third);

            dc.SetGoal(goals.ToArray());

            //constructing the drive elements of the drivecollection
            List<IEditable[]> parallelDEs = new List<IEditable[]>();
            foreach (Tuple<string, List<object>, string, long>[] ta in builder.driveCollection.Forth)
            {
                List<DriveElement> des = new List<DriveElement>();
                foreach (Tuple<string, List<object>, string, long> t in ta)
                {
                    string tname = t.First;
                    // triggers are senses which are AND connected
                    List<IEditable> triggers = ConstructSenses(t.Second);
                    // timeout is in milliseconds
                    long timeout = t.Forth;
                    IEditable action = ConstructRecursiveAction(t.Third, builder.competences, builder.actionPatterns);

                }
                parallelDEs.Add(des.ToArray());
            }

            dc.SetParallelChildren(parallelDEs);
            plan.AddChild(dc);
            return plan;
        }

        private Competence ConstructCompetence(Tuple<string, long, List<object>, List<Tuple<string, List<object>, string, int>[]>> rawC, Dictionary<string, Tuple<string, long, List<object>, List<Tuple<string, List<object>, string, int>[]>>> rawComps, Dictionary<string, Tuple<string, long, List<object>>> rawAPs)
        {
            Competence c = (Competence)CreateCompetence(rawC.First, (TimeUnit)CreateTimeUnit("ms", rawC.Second));
            List<IEditable> triggers = ConstructSenses(rawC.Third);
            List<IEditable[]> ces = ConstructCompetenceElement(rawC.Forth,rawComps,rawAPs);
            c.SetGoal(triggers.ToArray());
            c.SetParallelChildren(ces);
            
            return c;
        }

        

        private FixedGroup ConstructActionPattern(Tuple<string, long, List<object>> rawAP, Dictionary<string, Tuple<string, long, List<object>, List<Tuple<string, List<object>, string, int>[]>>> rawComps, Dictionary<string, Tuple<string, long, List<object>>> rawAPs)
        {
            FixedGroup ap = (FixedGroup)CreateActionPattern(rawAP.First, (TimeUnit)CreateTimeUnit("ms", rawAP.Second));
            List<IEditable> nodes = new List<IEditable>();
            if (rawAP.Third is List<object>)
                foreach(object node in rawAP.Third)
                    nodes.Add(ConstructRecursiveAction(node as string, rawComps, rawAPs));
            ap.SetChildren(nodes.ToArray());

            return ap;
        }

        private List<IEditable[]> ConstructCompetenceElement(List<Tuple<string, List<object>, string, int>[]> rawCEs, Dictionary<string, Tuple<string, long, List<object>, List<Tuple<string, List<object>, string, int>[]>>> rawCs, Dictionary<string, Tuple<string, long, List<object>>> rawAPs)
        {
            List<IEditable[]> parallelCEs = new List<IEditable[]>();
            foreach (Tuple<string, List<object>, string, int>[] ta in rawCEs)
            {
                List<IEditable> ces = new List<IEditable>();
                foreach (Tuple<string, List<object>, string, int> rawCE in ta)
                {
                    string name = rawCE.First;
                    List<IEditable> triggers = ConstructSenses(rawCE.Second);
                    IEditable action = ConstructRecursiveAction(rawCE.Third, rawCs, rawAPs);
                    int retries = rawCE.Forth;
                    CompetenceElement ce = (CompetenceElement)CreateCompetenceElement(name, triggers.ToArray());
                    ce.AddChild(action);
                    ce.SetRetries(retries);

                    ces.Add(ce);
                }
                parallelCEs.Add(ces.ToArray());
            }
            return parallelCEs;
        }

        private List<IEditable> ConstructSenses(List<object> lst)
        {
            List<IEditable> senses = new List<IEditable>();
            foreach (object obj in lst)
            {
                Tuple<string, string, string> t = obj as Tuple<string, string, string>;
                if (t != null && t.First.Length > 1)
                    senses.Add(CreateActionElement(t.First, true, t.Second, t.Third));
            }

            return senses;
        }

        private IEditable ConstructRecursiveAction(string actionToken, Dictionary<string, Tuple<string, long, List<object>, List<Tuple<string, List<object>, string, int>[]>>> rawComps, Dictionary<string, Tuple<string, long, List<object>>> rawAPs)
        {
            IEditable action = null;
            // checking if the action of the competence is either another competence an element grouping or an action element
            if (rawComps.ContainsKey(actionToken))
            {
                action = ConstructCompetence(rawComps[actionToken], rawComps, rawAPs);
            }
            else if (rawAPs.ContainsKey(actionToken))
            {
                action = ConstructActionPattern(rawAPs[actionToken], rawComps, rawAPs);
            }
            else
            {
                action = CreateActionElement(actionToken, false);
            }

            return action;
        }

        internal IEditable CloneLearnableActionPattern(LearnableActionPlan lap)
        {
            LearnableActionPlan clone = (LearnableActionPlan) CreateLearnableActionPlan(lap.GetName());
            clone.SetLapDocumentation(lap.GetLapDocumentation());
            clone.AddChilden(DeepCloneChildren(lap, new List<AEditable>(), lap.GetChildren()).ToArray());
                        
            return clone;
        }
        
    }
}
