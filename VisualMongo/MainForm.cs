using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualMongo.Service;
using MongoDB.Bson;
using MongoDB.Driver;

namespace VisualMongo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void tvServer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
            {
                e.Node.Nodes.Clear();
                DatabaseService.ConnectDatabase(e.Node.Text);

                var collections = CollectionService.GetCollectionNames();
                if (collections.Count() >= 2)
                {
                    foreach (var collection in collections)
                    {
                        if (collection.StartsWith("system.") == false && collection.StartsWith("fs.") == false)
                        {
                            e.Node.Nodes.Add(new TreeNode(collection, 2, 2));
                        }
                    }
                    e.Node.Expand();
                }
                DatabaseStatsResult stats = DatabaseService.GetDatabaseStats(e.Node.Text);
                if (stats != null)
                {
                    textBox2.Text = stats.Response.ToBsonDocument().ToString();
                }
            }

            if (e.Node.Level == 2)
            {
                e.Node.Nodes.Clear();
                DatabaseService.ConnectDatabase(e.Node.Parent.Text);
                var bsons = EntityService.GetEntities(e.Node.Text, 1, 100);
                FillData(bsons);
            }
        }

        private void FillData(List<BsonDocument> bsons)
        {
            treeView2.Nodes.Clear();
            treeView2.Nodes.AddRange(bsons.Select(b => new TreeNode
            {
                Text = b.ToString(),
            }).ToArray());

            dataGridView1.DataSource = EntityService.ConvertToDataTable(bsons);
            textBox1.Text = EntityService.ConvertToText(bsons).ToString();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Connection.ConnectionsForm connections = new Connection.ConnectionsForm();
            if (connections.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tvServer.Nodes.Add(new TreeNode(ServerService.GetServer(), 0, 0));
                foreach (var db in DatabaseService.GetDatabaseNames())
                {
                    tvServer.Nodes[0].Nodes.Add(new TreeNode(db, 1, 1));
                }
            }
        }
    }
}
