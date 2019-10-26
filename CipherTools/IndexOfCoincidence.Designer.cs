namespace CipherTools
{
    partial class IndexOfCoincidence
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndexOfCoincidence));
            this.textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonGetIC = new System.Windows.Forms.RadioButton();
            this.radioButtonGetAvgIC = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxStart = new System.Windows.Forms.TextBox();
            this.textBoxEnd = new System.Windows.Forms.TextBox();
            this.buttonGo = new System.Windows.Forms.Button();
            this.ButtonHelp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.helpTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.DimGray;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(12, 42);
            this.textBox.MinimumSize = new System.Drawing.Size(388, 120);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(433, 142);
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
            this.label1.Size = new System.Drawing.Size(54, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text";
            // 
            // radioButtonGetIC
            // 
            this.radioButtonGetIC.AutoSize = true;
            this.radioButtonGetIC.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonGetIC.Checked = true;
            this.radioButtonGetIC.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonGetIC.ForeColor = System.Drawing.Color.White;
            this.radioButtonGetIC.Location = new System.Drawing.Point(12, 192);
            this.radioButtonGetIC.Name = "radioButtonGetIC";
            this.radioButtonGetIC.Size = new System.Drawing.Size(187, 28);
            this.radioButtonGetIC.TabIndex = 4;
            this.radioButtonGetIC.TabStop = true;
            this.radioButtonGetIC.Text = "GET I.C. OF TEXT";
            this.radioButtonGetIC.UseVisualStyleBackColor = false;
            // 
            // radioButtonGetAvgIC
            // 
            this.radioButtonGetAvgIC.AutoSize = true;
            this.radioButtonGetAvgIC.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonGetAvgIC.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonGetAvgIC.ForeColor = System.Drawing.Color.White;
            this.radioButtonGetAvgIC.Location = new System.Drawing.Point(12, 224);
            this.radioButtonGetAvgIC.Name = "radioButtonGetAvgIC";
            this.radioButtonGetAvgIC.Size = new System.Drawing.Size(481, 28);
            this.radioButtonGetAvgIC.TabIndex = 5;
            this.radioButtonGetAvgIC.Text = "GET AVERAGE I.C. FOR DIFFERENT KEY PERIODS";
            this.radioButtonGetAvgIC.UseVisualStyleBackColor = false;
            this.radioButtonGetAvgIC.CheckedChanged += new System.EventHandler(this.radioButtonGetAvgIC_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Roboto Condensed Light", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(29, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Test periods between        and";
            this.label2.Visible = false;
            // 
            // textBoxStart
            // 
            this.textBoxStart.BackColor = System.Drawing.Color.DimGray;
            this.textBoxStart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStart.ForeColor = System.Drawing.Color.White;
            this.textBoxStart.Location = new System.Drawing.Point(169, 260);
            this.textBoxStart.MaxLength = 3;
            this.textBoxStart.MinimumSize = new System.Drawing.Size(2, 20);
            this.textBoxStart.Name = "textBoxStart";
            this.textBoxStart.Size = new System.Drawing.Size(28, 20);
            this.textBoxStart.TabIndex = 6;
            this.textBoxStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxStart.Visible = false;
            this.textBoxStart.MouseHover += new System.EventHandler(this.textBoxStart_MouseHover);
            // 
            // textBoxEnd
            // 
            this.textBoxEnd.BackColor = System.Drawing.Color.DimGray;
            this.textBoxEnd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEnd.ForeColor = System.Drawing.Color.White;
            this.textBoxEnd.Location = new System.Drawing.Point(225, 260);
            this.textBoxEnd.MaxLength = 3;
            this.textBoxEnd.MinimumSize = new System.Drawing.Size(2, 20);
            this.textBoxEnd.Name = "textBoxEnd";
            this.textBoxEnd.Size = new System.Drawing.Size(28, 20);
            this.textBoxEnd.TabIndex = 6;
            this.textBoxEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxEnd.Visible = false;
            this.textBoxEnd.MouseHover += new System.EventHandler(this.textBoxEnd_MouseHover);
            // 
            // buttonGo
            // 
            this.buttonGo.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonGo.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGo.ForeColor = System.Drawing.Color.White;
            this.buttonGo.Location = new System.Drawing.Point(389, 222);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(56, 32);
            this.buttonGo.TabIndex = 7;
            this.buttonGo.Text = "GO";
            this.buttonGo.UseVisualStyleBackColor = false;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // ButtonHelp
            // 
            this.ButtonHelp.BackColor = System.Drawing.Color.Transparent;
            this.ButtonHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonHelp.BackgroundImage")));
            this.ButtonHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonHelp.FlatAppearance.BorderSize = 0;
            this.ButtonHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonHelp.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonHelp.ForeColor = System.Drawing.Color.Transparent;
            this.ButtonHelp.Location = new System.Drawing.Point(418, 9);
            this.ButtonHelp.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonHelp.Name = "ButtonHelp";
            this.ButtonHelp.Size = new System.Drawing.Size(30, 30);
            this.ButtonHelp.TabIndex = 31;
            this.ButtonHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ButtonHelp.UseVisualStyleBackColor = false;
            this.ButtonHelp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonHelp_MouseClick);
            this.ButtonHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonHelp_MouseDown);
            this.ButtonHelp.MouseEnter += new System.EventHandler(this.ButtonHelp_MouseHover);
            this.ButtonHelp.MouseLeave += new System.EventHandler(this.ButtonHelp_MouseLeave);
            this.ButtonHelp.MouseHover += new System.EventHandler(this.ButtonHelp_MouseHover);
            this.ButtonHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonHelp_MouseUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(434, 13);
            this.label5.MaximumSize = new System.Drawing.Size(790, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 21);
            this.label5.TabIndex = 32;
            this.label5.Text = "                            ";
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.BackColor = System.Drawing.Color.Transparent;
            this.labelHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelHelp.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelp.ForeColor = System.Drawing.Color.White;
            this.labelHelp.Location = new System.Drawing.Point(432, 12);
            this.labelHelp.MaximumSize = new System.Drawing.Size(790, 0);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(93, 21);
            this.labelHelp.TabIndex = 33;
            this.labelHelp.Text = "What is this?";
            this.labelHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // helpTimer
            // 
            this.helpTimer.Interval = 20;
            this.helpTimer.Tick += new System.EventHandler(this.helpTimer_Tick);
            // 
            // IndexOfCoincidence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CipherTools.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(457, 298);
            this.Controls.Add(this.ButtonHelp);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.textBoxEnd);
            this.Controls.Add(this.textBoxStart);
            this.Controls.Add(this.radioButtonGetIC);
            this.Controls.Add(this.radioButtonGetAvgIC);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelHelp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "IndexOfCoincidence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Index Of Coincidence";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonGetIC;
        private System.Windows.Forms.RadioButton radioButtonGetAvgIC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxStart;
        private System.Windows.Forms.TextBox textBoxEnd;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Button ButtonHelp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Timer helpTimer;
    }
}