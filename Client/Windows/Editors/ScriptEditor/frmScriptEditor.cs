/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Client.Logic.Network;
using PMU.Sockets;

namespace Client.Logic.Windows.Editors.ScriptEditor
{
    partial class frmScriptEditor : Form
    {
        public frmScriptEditor() {
            InitializeComponent();

        }

        private delegate void AddDocumentTabDelegate(ScriptFileTab tab);
        public void AddDocumentTab(ScriptFileTab tab) {
            if (InvokeRequired) {
                Invoke(new AddDocumentTabDelegate(AddDocumentTab), tab);
            } else {
                TabPage page = new TabPage(tab.File);
                tab.AddToTabPage(page);
                tabControl1.TabPages.Add(page);
            }

        }

        private void button1_Click(object sender, EventArgs e) {
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditscriptfile", (string)comboBox1.SelectedItem));
        }

        private delegate void SetFileListDelegate(List<string> files);
        public void SetFileList(List<String> files) {
            if (InvokeRequired) {
                Invoke(new SetFileListDelegate(SetFileList), files);
            } else {
                comboBox1.Items.Clear();
                for (int i = 0; i < files.Count; i++) {
                    comboBox1.Items.Add(files[i]);
                }
            }
        }

        private delegate void SetClassesListDelegate(List<string> files);
        public void SetClassesList(List<string> files) {
            if (InvokeRequired) {
                Invoke(new SetClassesListDelegate(SetClassesList), files);
            } else {
                comboBox2.Items.Clear();
                for (int i = 0; i < files.Count; i++) {
                    comboBox2.Items.Add(files[i]);
                }
            }
        }

        private delegate void SetMethodsListDelegate(List<string> files);
        public void SetMethodsList(List<string> files) {
            if (InvokeRequired) {
                Invoke(new SetMethodsListDelegate(SetMethodsList), files);
            } else {
                comboBox3.Items.Clear();
                for (int i = 0; i < files.Count; i++) {
                    comboBox3.Items.Add(files[i]);
                }
            }
        }

        private delegate void SetErrorsListDelegate(List<string> errors);
        public void SetErrorsList(List<string> errors) {
            if (InvokeRequired) {
                Invoke(new SetErrorsListDelegate(SetErrorsList), errors);
            } else {
                listBox1.Items.Clear();
                for (int i = 0; i < errors.Count; i++) {
                    listBox1.Items.Add(errors[i]);
                }
            }
        }

        private delegate void SetScriptParameterInfoDelegate(string info);
        public void SetScriptParameterInfo(string info) {
            if (InvokeRequired) {
                Invoke(new SetScriptParameterInfoDelegate(SetScriptParameterInfo), info);
            } else {
                lblParameters.Text = "Parameters: " + info;
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e) {
            ScriptFileTab tab = tabControl1.SelectedTab.Tag as ScriptFileTab;
            if (tab != null) {
                Messenger.SendPacket(TcpPacket.CreatePacket("savescript", tab.File.Replace(".cs", ""),
                    tab.SyntaxBox.Document.Text.Replace(TcpPacket.SEP_CHAR, '/')));
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e) {
            Messenger.SendPacket(TcpPacket.CreatePacket("reloadscripts"));
        }

        private void finalizeScriptToolStripMenuItem_Click(object sender, EventArgs e) {
            Messenger.SendPacket(TcpPacket.CreatePacket("finalizescript"));
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            Messenger.SendPacket(TcpPacket.CreatePacket("getscriptmethods", (string)comboBox2.SelectedItem));
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) {
            Messenger.SendPacket(TcpPacket.CreatePacket("getscriptparam", (string)comboBox3.Items[0], (string)comboBox3.SelectedItem, numericUpDown1.Value.ToString()));
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
            Messenger.SendPacket(TcpPacket.CreatePacket("getscriptparam", (string)comboBox3.Items[0], (string)comboBox3.SelectedItem, numericUpDown1.Value.ToString()));
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e) {
            Messenger.SendPacket(TcpPacket.CreatePacket("addnewclass", toolStripTextBox1.Text));
        }

        private void listBox1_MouseLeave(object sender, EventArgs e) {
            listBox1.Hide();
            pnlOptions.Show();
            panel2.Size = new Size(968, 42);
        }

        private void button2_Click(object sender, EventArgs e) {
            pnlOptions.Hide();
            listBox1.Show();
            panel2.Size = new Size(968, 186);
        }

        private void frmScriptEditor_FormClosing(object sender, FormClosingEventArgs e) {
            Messenger.SendPacket(TcpPacket.CreatePacket("scripteditexit"));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void lblParameters_Click(object sender, EventArgs e)
        {

        }


    }
}
