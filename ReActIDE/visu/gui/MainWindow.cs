using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using posh.visu.control.io;
using posh.visu.model.ReAct;

namespace posh.visu
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
        }

        private void InitControllers(){

        }

        private void openRecentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /**
         * Opens lap files and uses the lappreader to read them into a useable format
         */
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Stream myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "lap files (*.lap)|*.lap" ;
            openFileDialog1.FilterIndex = 2 ;
            openFileDialog1.RestoreDirectory = true ;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if((myStream = openFileDialog1.OpenFile())!= null)
                {
                    DotLAPReader reader = new DotLAPReader();
                    
                    reader.Load(myStream);
                    sourceView.Text = reader.GetLastFile();
                    myStream.Close();
                }
            }
            TreeNode dc = new TreeNode("DriveCollection");
            dc.BackColor = Color.AliceBlue;
            dc.ContextMenu = new ContextMenu();
            TreeNode dc1 = new TreeNode("DriveCollection");
            int code1= dc.GetHashCode();
            int code2 = dc1.GetHashCode();
            overviewTree.Nodes.Add(dc);

            overviewTree.SelectedNode = dc;
            overviewTree.SelectedNode.Nodes.Add(dc1);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Open_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            redoToolStripMenuItem_Click(sender, e);
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // need to include the open undo and redo button which are accessible via the event
            Console.Out.WriteLine("");
        }

        private void newActionPatternToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

       

   }
}
