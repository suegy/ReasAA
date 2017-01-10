using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using posh.visu.model.POSH;

namespace posh.visu.model
{
    public interface IEditable
    {
               
        /**
	    * Sets enabled
	    */
        void SetEnabled(bool newValue);

        /**
         * Sets enabled
         */
        bool IsEnabled();

        string GetID();

        IEditable GetParent();

        bool ContainsChild(string name);

        bool ContainsChildRecursive(string name);

        /**
         * Sets the element documentation
         */
        void SetDocumentation(string newDocumentation);

        /**
         * Returns the documentation String
         * @return doc String for the specific element
         */
        string GetDocumentation();

        string GetName();

        void Dispose();

        /**
         * Sets the element do
         */

        /**
         * When we click this Action Element in the GUI populate the properties
         * panel with the various attributes and setup listeners to catch
         * modifications that are made.
         * 
         * @param mainGui
         *            The reference to the outer GUI
         * @param subGui
         *            The internal frame we're referring to
         * @param diagram
         *            The diagram we're being select on.
         */
        //void OnSelect(JAbode mainGui, JEditorWindow subGui, JDiagram diagram);

        /**
         * Build the tree structure of the file
         * 
         * @root Root node this tree attatches to
         * @lap File we're mapping to
         * @detailed Show detailing nodes (i.e. trigger lists)
         * @expanded Recursive expansion of sub-nodes
         * 
         * @return Tree node representing this node and the relevent sub-tree for
         *         the specified diagram rendering settings
         */
        //TreeNode BuildTree(TreeNode root, LearnableActionPattern lap, bool detailed, bool expanded);

        /**
         * Produce and show a context menu for this object
         * 
         * @param showOn
         *            The tree node invoking us
         * @param lap
         *            The file we're a part of
         * @param window
         *            The window we're being dispalyed in
         * @param diagram
         *            The diagram in the window we'return being shown on
         */
        //void ShowContextMenu(TreeNode showOn, LearnableActionPattern lap, JEditorWindow window, JDiagram diagram);
    }
}
