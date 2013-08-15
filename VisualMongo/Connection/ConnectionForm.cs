using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualMongo.Entity;
using VisualMongo.Service;

namespace VisualMongo.Connection
{
    public partial class ConnectionForm : Form
    {
        private bool IsModify = false;
        private string ConnectionNameOld = string.Empty;

        public ConnectionForm(bool isModify, string connectionName = "")
        {
            InitializeComponent();
            IsModify = isModify;
            if (IsModify == true)
            {
                ConnectionEntity currentConnection = VisualMongo.Service.ConnectionService.GetConnection(connectionName);
                if (currentConnection != null)
                {
                    txtConnectionName.Text = currentConnection.ConnectionName;
                    txtServerName.Text = currentConnection.ServerName;
                    txtServerPort.Text = currentConnection.ServerPort.ToString();
                    txtUserName.Text = currentConnection.UserName;
                    txtPassword.Text = currentConnection.Password;
                    txtDatabases.Text = currentConnection.Databases;
                    ConnectionNameOld = txtConnectionName.Text;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ConnectionEntity newConnection = null;
            if (IsModify == false)
            {
                newConnection = ConnectionService.AddConnection(txtConnectionName.Text, txtServerName.Text, Convert.ToInt32(txtServerPort.Text), txtUserName.Text, txtPassword.Text, txtDatabases.Text);
            }
            else
            {
                newConnection = ConnectionService.UpdateConnection(ConnectionNameOld, txtConnectionName.Text, txtServerName.Text, Convert.ToInt32(txtServerPort.Text), txtUserName.Text, txtPassword.Text, txtDatabases.Text);
            }

            if (newConnection == null)
            {
                MessageBox.Show("Bad name!");
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (ConnectionService.TryConnect(txtServerName.Text, Convert.ToInt32(txtServerPort.Text), txtUserName.Text, txtPassword.Text, txtDatabases.Text) == true)
            {
                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }
    }
}
