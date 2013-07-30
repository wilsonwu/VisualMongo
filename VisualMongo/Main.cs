using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualMongo
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_Shown(object sender, EventArgs e)
        {
            var connections = new Connection.Connections();
            connections.ShowDialog();
            VisualMongo.Service.ConnectionService.Connect();
            VisualMongo.Service.ServerService.GetServer();
            treeView1.Nodes.Add("local");
            foreach (var db in VisualMongo.Service.DatabaseService.GetDatabases())
            {
                treeView1.Nodes[0].Nodes.Add(db);
            }
        }
    }
}
