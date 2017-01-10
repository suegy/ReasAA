using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using posh.visu.model.POSH;

namespace posh.visu.visual
{
    class TreeBuilder
    {
        private TreeView overviewTree;
        private TreeView logicviewTree;
        private TreeView actionPatternsTree;
        private TreeView competencesTree;
        private TreeView driveCollectionsTree;
        private TreeView printViewTree;
        private List<LearnableActionPlan> plans;
        
        /**
         * builds the different treeviews for the editor and stores links to them
         * @trees the given object references to the 6 tree representations in order:
         * overviewTree, logicviewTree, actionPatternsTree, competencesTree, driveCollectionsTree printViewTree
         */ 
        public TreeBuilder(TreeView [] trees)
        {
            overviewTree = trees[0];
            logicviewTree= trees[1];
            actionPatternsTree= trees[2];
            competencesTree= trees[3];
            driveCollectionsTree= trees[4];
            printViewTree= trees[5];
            plans = new List<LearnableActionPlan>();
            

        }

        public bool AddActionPlan(LearnableActionPlan lap)
        {
            if (plans is List<LearnableActionPlan> && !plans.Contains(lap))
            {
                plans.Add(lap);
                return true;
            }

            return false;
        }

        public bool SetActionPlans(LearnableActionPlan [] laps)
        {
            //@TODO(swen) need to implement some clever means to combine multiple laps into one visual representation
            throw new NotImplementedException();
        }

        /**
	     * Produce an overview tree
	     */
	    public void SetOverviewTree() {
            //if (overviewTree is TreeView && overviewTree.GetNodeCount(false) > 0)
            //    overviewTree.Nodes.Clear();
            //overviewTree.Nodes.
            //JTreeNode root = new JTreeNode("Drive Collections", "", Configuration.getRGB("colours/rootNode"), this, null);
            //root.setRendered(false);

            //for (IEditable child : getChildren())
            //    if (child instanceof DriveCollection)
            //        ((DriveCollection) child).buildTree(root, this, false, true);
			
				
            //return root;
	    }

	    /**
	     * Produce a logic tree
	     */
	    public void SetLogicTree() {
            //JTreeNode root = new JTreeNode("Drive Collections", "", Configuration.getRGB("colours/rootNode"), this, null);
            //root.setRendered(false);

            //for (IEditable child : getChildren())
            //    if (child instanceof DriveCollection)
            //        ((DriveCollection) child).buildTree(root, this, true, true);
		
            //return root;
	    }

	    /**
	     * Add the specified node summary beneath the current node (i.e in overview
	     * mode, use to add the sub-tree for the action pattern or competence in
	     * question)
	     */
        //public void scanActionTree(JTreeNode deNode, string action, bool detailed, bool expanded) {
        //        Scan the file structure again looking for action patterns or
        //        competences
        //        that have the same name as our action, then add them to the tree.
		
        //    List<IEditable> nodes=new ArrayList<IEditable>();
        //    nodes = getChildRecursive(nodes, INamedElement.class);
		
        //    for ( IEditable node : nodes) {
        //        if (((INamedElement)node).getName().equals(action))
        //            node.buildTree(deNode, this, detailed, expanded);
        //    }
		
        //}

	    /**
	     * Produce a tree hierarchy of competence elements
	     */
        //public JTreeNode toCompetenceTree() {
        //    JTreeNode root = new JTreeNode("Competences", "", Configuration.getRGB("colours/rootNode"), this, null);
        //    // root.setRendered(false);

        //    List<IEditable> nodes=new ArrayList<IEditable>();
        //    nodes = getChildRecursive(nodes, Competence.class);
		
        //    for ( IEditable node : nodes) {
        //            node.buildTree(root, this, true, false);
        //    }
		
        //    return root;
        //}

	    /**
	     * Produce a diagram tree hierarchy of action pattern elements
	     */
        //public JTreeNode toActionTree() {
        //    JTreeNode root = new JTreeNode("Action Patterns", "", Configuration.getRGB("colours/rootNode"), this, null);
        //    // root.setRendered(false);

        //    List<IEditable> nodes=new ArrayList<IEditable>();
        //    nodes = getChildRecursive(nodes, ActionPattern.class);
		
        //    for ( IEditable node : nodes) {
        //            node.buildTree(root, this, true, false);
        //    }
		
        //    return root;
        //}

	    /**
	     * Produce a tree hierarchy of drive elements
	     */
        //public JTreeNode toDriveTree() {
        //    JTreeNode root = new JTreeNode("Drive Collections", "", Configuration.getRGB("colours/rootNode"), this, null);
        //     root.setRendered(false);

        //    for (IEditable child : getChildren())
        //        if (child instanceof DriveCollection)
        //            ((DriveCollection) child).buildTree(root, this, true, false);
		
        //    return root;
        //}


    }
}
