namespace CipherTools
{
    partial class AutokeySolver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutokeySolver));
            this.textBox = new System.Windows.Forms.TextBox();
            this.labelCurrentKey = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelStreams = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
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
            this.textBox.Size = new System.Drawing.Size(778, 158);
            this.textBox.TabIndex = 4;
            // 
            // labelCurrentKey
            // 
            this.labelCurrentKey.AutoSize = true;
            this.labelCurrentKey.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrentKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrentKey.ForeColor = System.Drawing.Color.White;
            this.labelCurrentKey.Location = new System.Drawing.Point(7, 210);
            this.labelCurrentKey.Name = "labelCurrentKey";
            this.labelCurrentKey.Size = new System.Drawing.Size(132, 25);
            this.labelCurrentKey.TabIndex = 5;
            this.labelCurrentKey.Text = "Current Key:";
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
            this.label1.TabIndex = 6;
            this.label1.Text = "Output";
            // 
            // flowLayoutPanelStreams
            // 
            this.flowLayoutPanelStreams.AutoScroll = true;
            this.flowLayoutPanelStreams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.flowLayoutPanelStreams.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelStreams.Location = new System.Drawing.Point(12, 243);
            this.flowLayoutPanelStreams.Name = "flowLayoutPanelStreams";
            this.flowLayoutPanelStreams.Size = new System.Drawing.Size(778, 298);
            this.flowLayoutPanelStreams.TabIndex = 3;
            this.flowLayoutPanelStreams.WrapContents = false;
            // 
            // AutokeySolver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = global::CipherTools.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(802, 553);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelCurrentKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanelStreams);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AutokeySolver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autokey Cipher Solver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label labelCurrentKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelStreams;
    }
}