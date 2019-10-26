namespace CipherTools
{
    partial class FindReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindReplace));
            this.mainRTB = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblMatches = new System.Windows.Forms.Label();
            this.lblOutOf = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbReplace = new System.Windows.Forms.TextBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.cbCaseSensitive = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainRTB
            // 
            this.mainRTB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mainRTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainRTB.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainRTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.mainRTB.Location = new System.Drawing.Point(220, 12);
            this.mainRTB.Name = "mainRTB";
            this.mainRTB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.mainRTB.Size = new System.Drawing.Size(512, 417);
            this.mainRTB.TabIndex = 0;
            this.mainRTB.Text = "Paste text here...";
            this.mainRTB.TextChanged += new System.EventHandler(this.mainRTB_TextChanged);
            this.mainRTB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mainRTB_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Roboto Condensed Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find";
            // 
            // tbFind
            // 
            this.tbFind.BackColor = System.Drawing.Color.DimGray;
            this.tbFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFind.Font = new System.Drawing.Font("Roboto Condensed Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFind.ForeColor = System.Drawing.Color.White;
            this.tbFind.Location = new System.Drawing.Point(12, 41);
            this.tbFind.Margin = new System.Windows.Forms.Padding(9);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(196, 29);
            this.tbFind.TabIndex = 2;
            this.tbFind.TextChanged += new System.EventHandler(this.tbFind_TextChanged);
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnPrev.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.ForeColor = System.Drawing.Color.White;
            this.btnPrev.Location = new System.Drawing.Point(12, 100);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(92, 32);
            this.btnPrev.TabIndex = 8;
            this.btnPrev.Text = "←";
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnNext.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(116, 100);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(92, 32);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "→";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblMatches
            // 
            this.lblMatches.AutoSize = true;
            this.lblMatches.BackColor = System.Drawing.Color.Transparent;
            this.lblMatches.Font = new System.Drawing.Font("Roboto Condensed Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatches.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblMatches.Location = new System.Drawing.Point(8, 71);
            this.lblMatches.Name = "lblMatches";
            this.lblMatches.Size = new System.Drawing.Size(137, 20);
            this.lblMatches.TabIndex = 1;
            this.lblMatches.Text = "Enter a string to locate.";
            // 
            // lblOutOf
            // 
            this.lblOutOf.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblOutOf.BackColor = System.Drawing.Color.Transparent;
            this.lblOutOf.Font = new System.Drawing.Font("Roboto Condensed Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutOf.ForeColor = System.Drawing.Color.White;
            this.lblOutOf.Location = new System.Drawing.Point(134, 71);
            this.lblOutOf.Name = "lblOutOf";
            this.lblOutOf.Size = new System.Drawing.Size(80, 20);
            this.lblOutOf.TabIndex = 1;
            this.lblOutOf.Text = "(---/---)";
            this.lblOutOf.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOutOf.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Roboto Condensed Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Replace";
            // 
            // tbReplace
            // 
            this.tbReplace.BackColor = System.Drawing.Color.DimGray;
            this.tbReplace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbReplace.Font = new System.Drawing.Font("Roboto Condensed Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReplace.ForeColor = System.Drawing.Color.White;
            this.tbReplace.Location = new System.Drawing.Point(12, 238);
            this.tbReplace.Margin = new System.Windows.Forms.Padding(9);
            this.tbReplace.Name = "tbReplace";
            this.tbReplace.Size = new System.Drawing.Size(196, 29);
            this.tbReplace.TabIndex = 2;
            // 
            // btnReplace
            // 
            this.btnReplace.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReplace.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReplace.Font = new System.Drawing.Font("Roboto Condensed Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplace.ForeColor = System.Drawing.Color.White;
            this.btnReplace.Location = new System.Drawing.Point(12, 279);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(196, 32);
            this.btnReplace.TabIndex = 8;
            this.btnReplace.Text = "REPLACE";
            this.btnReplace.UseVisualStyleBackColor = false;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // cbCaseSensitive
            // 
            this.cbCaseSensitive.AutoSize = true;
            this.cbCaseSensitive.BackColor = System.Drawing.Color.Transparent;
            this.cbCaseSensitive.Font = new System.Drawing.Font("Roboto Condensed Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCaseSensitive.ForeColor = System.Drawing.Color.White;
            this.cbCaseSensitive.Location = new System.Drawing.Point(12, 140);
            this.cbCaseSensitive.Name = "cbCaseSensitive";
            this.cbCaseSensitive.Size = new System.Drawing.Size(116, 25);
            this.cbCaseSensitive.TabIndex = 9;
            this.cbCaseSensitive.Text = "Case Sensitive";
            this.cbCaseSensitive.UseVisualStyleBackColor = false;
            this.cbCaseSensitive.CheckedChanged += new System.EventHandler(this.cbCaseSensitive_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Crimson;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Roboto Condensed Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(12, 397);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(196, 32);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "CLEAR TEXTBOX";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.BackColor = System.Drawing.Color.DarkOrchid;
            this.btnSelectAll.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrchid;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Font = new System.Drawing.Font("Roboto Condensed Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.ForeColor = System.Drawing.Color.White;
            this.btnSelectAll.Location = new System.Drawing.Point(12, 353);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(196, 32);
            this.btnSelectAll.TabIndex = 8;
            this.btnSelectAll.Text = "SELECT ALL";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // FindReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CipherTools.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(744, 441);
            this.Controls.Add(this.cbCaseSensitive);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.tbReplace);
            this.Controls.Add(this.tbFind);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMatches);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainRTB);
            this.Controls.Add(this.lblOutOf);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FindReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find & Replace";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox mainRTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblMatches;
        private System.Windows.Forms.Label lblOutOf;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbReplace;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.CheckBox cbCaseSensitive;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSelectAll;
    }
}