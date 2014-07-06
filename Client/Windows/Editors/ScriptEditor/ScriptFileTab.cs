using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Client.Logic.Windows.Editors.ScriptEditor
{
    class ScriptFileTab
    {
        Alsing.Windows.Forms.SyntaxBoxControl syntaxBox;
        string file;

        public string File {
            get {
                return file;
            }
        }

        public Alsing.Windows.Forms.SyntaxBoxControl SyntaxBox {
            get { return syntaxBox; }
        }

        public ScriptFileTab() {
            Load();
        }

        private delegate void LoadDelegate();
        private void Load() {
            if (Windows.Editors.EditorManager.ScriptEditor.InvokeRequired) {
                Windows.Editors.EditorManager.ScriptEditor.Invoke(new LoadDelegate(Load));
            } else {
                syntaxBox = new Alsing.Windows.Forms.SyntaxBoxControl();
                syntaxBox.AllowDrop = false;
                syntaxBox.Location = new System.Drawing.Point(0, 0);
                syntaxBox.Dock = System.Windows.Forms.DockStyle.Fill;
            }
        }

        public void AddToTabPage(TabPage tabPage) {
            tabPage.Controls.Add(syntaxBox);
            tabPage.Tag = this;
        }

        public void SetDocumentFile(string file) {
            this.file = file;
        }

        private delegate void SetDocumentDelegate(string text);
        public void SetDocumentText(string text) {
            if (Windows.Editors.EditorManager.ScriptEditor.InvokeRequired) {
                Windows.Editors.EditorManager.ScriptEditor.Invoke(new SetDocumentDelegate(SetDocumentText), text);
            } else {
                syntaxBox.Document.SyntaxFile = IO.Paths.StartupPath + "Script/CSharp.syn";
                syntaxBox.Document.Text = text;
                syntaxBox.Document.ReParse();
            }
        }
    }
}
