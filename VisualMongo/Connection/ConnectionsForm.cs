using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualMongo.Entity;

namespace VisualMongo.Connection
{
    public partial class ConnectionsForm : Form
    {
        public ConnectionsForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ConnectionForm connection = new ConnectionForm(false);
            if (connection.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BindConnections();
            }
        }

        private void BindConnections()
        {
            lvConnections.Items.Clear();
            List<ConnectionEntity> connections = new List<ConnectionEntity>();
            connections = VisualMongo.Service.ConnectionService.GetAllConnections();
            if (connections != null)
            {
                lvConnections.Items.AddRange(connections.Select(c => new ListViewItem
                {
                    Text = c.ConnectionName,
                }).ToArray());
            }
        }

        private void ConnectionsForm_Load(object sender, EventArgs e)
        {
            BindConnections();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectionEntity currentConnection = VisualMongo.Service.ConnectionService.GetConnection(lvConnections.SelectedItems[0].Text);
            VisualMongo.Service.ConnectionService.Connect(currentConnection);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvConnections.SelectedItems.Count > 0)
            {
                VisualMongo.Service.ConnectionService.DeleteConnection(lvConnections.SelectedItems[0].Text);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (lvConnections.SelectedItems.Count > 0)
            {
                ConnectionForm connection = new ConnectionForm(true, lvConnections.SelectedItems[0].Text);
                if (connection.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BindConnections();
                }
            }
        }
    }
}
