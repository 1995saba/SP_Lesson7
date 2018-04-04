using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string currentUserPath = @"HKEY_CURRENT_USER\";
        string localMachinePath= @"HKEY_LOCAL_MACHINE\";
        string regKeyName = "";
        RegistryKey rootKey;
        public Form1()
        {
            InitializeComponent();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            if (pathTextBox.Text.Contains(currentUserPath))
            {
                regKeyName = pathTextBox.Text.Replace(currentUserPath,"");
                rootKey = Registry.CurrentUser;
            }

            if (pathTextBox.Text.Contains(localMachinePath))
            {
                regKeyName = pathTextBox.Text.Replace(localMachinePath, "");
                rootKey = Registry.LocalMachine.OpenSubKey(regKeyName);
            }

            if (rootKey == null)
            {
                MessageBox.Show("Error");
                return;
            }

            keysListView.Clear();
            keysListView.Columns.Add("N");
            keysListView.Columns.Add("Keys");
            valuesListView.Clear();
            valuesListView.Columns.Add("N");
            valuesListView.Columns.Add("Values");
            valuesListView.Columns.Add("Type");

            try
            {
                RegistryKey regSubKey = rootKey.OpenSubKey(regKeyName);
                string[] keysStr = regSubKey.GetSubKeyNames();
                int i = 0;
                foreach(string str in keysStr)
                {
                    keysListView.Items.Add(i++.ToString()).SubItems.Add(str);
                }
                string[] valuesStr = regSubKey.GetValueNames();
                i = 0;
                foreach (string str in valuesStr)
                {
                    RegistryValueKind kind = regSubKey.GetValueKind(str);
                    ListViewItem item = valuesListView.Items.Add(i++.ToString());
                    item.SubItems.Add(str);
                    item.SubItems.Add(kind.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            keysListView.Columns[0].AutoResize(
                ColumnHeaderAutoResizeStyle.ColumnContent);
            keysListView.Columns[1].AutoResize(
                ColumnHeaderAutoResizeStyle.ColumnContent);
            valuesListView.Columns[0].AutoResize(
                ColumnHeaderAutoResizeStyle.ColumnContent);
            valuesListView.Columns[1].AutoResize(
                ColumnHeaderAutoResizeStyle.ColumnContent);
            valuesListView.Columns[2].AutoResize(
                ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
