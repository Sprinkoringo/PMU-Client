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


namespace Client.Logic.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// </summary>
    public partial class ErrorBox : Form
    {
        #region Fields

        bool errorShown;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorBox"/> class.
        /// </summary>
        public ErrorBox()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Shows the dialog box
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="message">The message.</param>
        /// <param name="details">The details.</param>
        /// <returns></returns>
        public static DialogResult ShowDialog(string caption, string message, string details)
        {
            ErrorBox error = new ErrorBox();
            error.lblError.Text = message;
            error.Text = caption;
            error.txtDetails.Text = details;

            try {
                Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("clienterror", details));
            } catch { }

            return error.ShowDialog();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (errorShown == false) {
                this.Size = new Size(499, 292);
                btnDetails.Text = "Details ^";
                errorShown = true;
            } else {
                this.Size = new Size(499, 193);
                btnDetails.Text = "Details v";
                errorShown = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion Methods

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ErrorBox
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "ErrorBox";
            this.Load += new System.EventHandler(this.ErrorBox_Load);
            this.ResumeLayout(false);

        }

        private void ErrorBox_Load(object sender, EventArgs e)
        {

        }
    }
}