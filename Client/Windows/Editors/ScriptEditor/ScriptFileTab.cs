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
