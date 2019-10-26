namespace CipherTools
{
    partial class VigSolver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VigSolver));
            this.flowLayoutPanelStreams = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCurrentKey = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanelStreams
            // 
            this.flowLayoutPanelStreams.AutoScroll = true;
            this.flowLayoutPanelStreams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.flowLayoutPanelStreams.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelStreams.Location = new System.Drawing.Point(12, 341);
            this.flowLayoutPanelStreams.Name = "flowLayoutPanelStreams";
            this.flowLayoutPanelStreams.Size = new System.Drawing.Size(848, 283);
            this.flowLayoutPanelStreams.TabIndex = 0;
            this.flowLayoutPanelStreams.WrapContents = false;
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.DimGray;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(12, 42);
            this.textBox.MinimumSize = new System.Drawing.Size(428, 120);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(848, 260);
            this.textBox.TabIndex = 1;
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output";
            // 
            // labelCurrentKey
            // 
            this.labelCurrentKey.AutoSize = true;
            this.labelCurrentKey.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentKey.ForeColor = System.Drawing.Color.White;
            this.labelCurrentKey.Location = new System.Drawing.Point(8, 311);
            this.labelCurrentKey.Name = "labelCurrentKey";
            this.labelCurrentKey.Size = new System.Drawing.Size(132, 25);
            this.labelCurrentKey.TabIndex = 2;
            this.labelCurrentKey.Text = "Current Key:";
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopy.BackColor = System.Drawing.Color.LimeGreen;
            this.btnCopy.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopy.ForeColor = System.Drawing.Color.White;
            this.btnCopy.Location = new System.Drawing.Point(762, 307);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(98, 29);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "COPY KEY";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // VigSolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = global::CipherTools.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(872, 636);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelCurrentKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanelStreams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "VigSolver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vigenère Cipher Solver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelStreams;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCurrentKey;
        private System.Windows.Forms.Button btnCopy;
    }
}