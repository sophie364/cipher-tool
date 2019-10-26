namespace CipherTools
{
    partial class MainMenu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.radioButtonE = new System.Windows.Forms.RadioButton();
            this.radioButtonD = new System.Windows.Forms.RadioButton();
            this.dgvCipherType = new System.Windows.Forms.DataGridView();
            this.CipherType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.labelInstruction = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.textBoxA = new System.Windows.Forms.TextBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.timerHelp = new System.Windows.Forms.Timer(this.components);
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnFreq = new System.Windows.Forms.Button();
            this.btnMap = new System.Windows.Forms.Button();
            this.btnReverse = new System.Windows.Forms.Button();
            this.labelExtra = new System.Windows.Forms.Label();
            this.btnLettersOnly = new System.Windows.Forms.Button();
            this.btnIofC = new System.Windows.Forms.Button();
            this.btnFindReplace = new System.Windows.Forms.Button();
            this.lblCharCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCipherType)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(259, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text";
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.DimGray;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.ForeColor = System.Drawing.Color.White;
            this.textBox.Location = new System.Drawing.Point(264, 45);
            this.textBox.MinimumSize = new System.Drawing.Size(488, 124);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(488, 124);
            this.textBox.TabIndex = 0;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // radioButtonE
            // 
            this.radioButtonE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonE.AutoSize = true;
            this.radioButtonE.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonE.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonE.ForeColor = System.Drawing.Color.White;
            this.radioButtonE.Location = new System.Drawing.Point(614, 268);
            this.radioButtonE.Name = "radioButtonE";
            this.radioButtonE.Size = new System.Drawing.Size(117, 28);
            this.radioButtonE.TabIndex = 3;
            this.radioButtonE.Text = "ENCRYPT";
            this.radioButtonE.UseVisualStyleBackColor = false;
            this.radioButtonE.CheckedChanged += new System.EventHandler(this.radioButtonE_CheckedChanged);
            // 
            // radioButtonD
            // 
            this.radioButtonD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonD.AutoSize = true;
            this.radioButtonD.BackColor = System.Drawing.Color.Transparent;
            this.radioButtonD.Checked = true;
            this.radioButtonD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonD.ForeColor = System.Drawing.Color.White;
            this.radioButtonD.Location = new System.Drawing.Point(614, 312);
            this.radioButtonD.Name = "radioButtonD";
            this.radioButtonD.Size = new System.Drawing.Size(116, 28);
            this.radioButtonD.TabIndex = 2;
            this.radioButtonD.TabStop = true;
            this.radioButtonD.Text = "DECRYPT";
            this.radioButtonD.UseVisualStyleBackColor = false;
            this.radioButtonD.CheckedChanged += new System.EventHandler(this.radioButtonD_CheckedChanged);
            // 
            // dgvCipherType
            // 
            this.dgvCipherType.AllowUserToAddRows = false;
            this.dgvCipherType.AllowUserToDeleteRows = false;
            this.dgvCipherType.AllowUserToResizeColumns = false;
            this.dgvCipherType.AllowUserToResizeRows = false;
            this.dgvCipherType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvCipherType.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvCipherType.BackgroundColor = System.Drawing.Color.White;
            this.dgvCipherType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCipherType.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvCipherType.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCipherType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCipherType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCipherType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CipherType});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCipherType.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCipherType.EnableHeadersVisualStyles = false;
            this.dgvCipherType.GridColor = System.Drawing.Color.DimGray;
            this.dgvCipherType.Location = new System.Drawing.Point(12, 12);
            this.dgvCipherType.MultiSelect = false;
            this.dgvCipherType.Name = "dgvCipherType";
            this.dgvCipherType.ReadOnly = true;
            this.dgvCipherType.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCipherType.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCipherType.RowHeadersWidth = 20;
            this.dgvCipherType.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCipherType.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvCipherType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCipherType.Size = new System.Drawing.Size(240, 373);
            this.dgvCipherType.TabIndex = 4;
            this.dgvCipherType.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCipherType_CellClick);
            // 
            // CipherType
            // 
            this.CipherType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CipherType.HeaderText = "Cipher Type";
            this.CipherType.Name = "CipherType";
            this.CipherType.ReadOnly = true;
            this.CipherType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // textBoxKey
            // 
            this.textBoxKey.BackColor = System.Drawing.Color.DimGray;
            this.textBoxKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxKey.Enabled = false;
            this.textBoxKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxKey.ForeColor = System.Drawing.Color.White;
            this.textBoxKey.Location = new System.Drawing.Point(264, 221);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(488, 27);
            this.textBoxKey.TabIndex = 1;
            this.textBoxKey.Visible = false;
            this.textBoxKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxKey_KeyDown);
            // 
            // labelInstruction
            // 
            this.labelInstruction.AutoSize = true;
            this.labelInstruction.BackColor = System.Drawing.Color.Transparent;
            this.labelInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInstruction.ForeColor = System.Drawing.Color.White;
            this.labelInstruction.Location = new System.Drawing.Point(259, 189);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Size = new System.Drawing.Size(204, 24);
            this.labelInstruction.TabIndex = 0;
            this.labelInstruction.Text = "Choose a cipher type...";
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.ForeColor = System.Drawing.Color.White;
            this.btnGo.Location = new System.Drawing.Point(705, 353);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(47, 32);
            this.btnGo.TabIndex = 5;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // textBoxB
            // 
            this.textBoxB.BackColor = System.Drawing.Color.DimGray;
            this.textBoxB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxB.Enabled = false;
            this.textBoxB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxB.ForeColor = System.Drawing.Color.White;
            this.textBoxB.Location = new System.Drawing.Point(361, 221);
            this.textBoxB.MaxLength = 2;
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(78, 27);
            this.textBoxB.TabIndex = 1;
            this.textBoxB.Visible = false;
            // 
            // textBoxA
            // 
            this.textBoxA.BackColor = System.Drawing.Color.DimGray;
            this.textBoxA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxA.Enabled = false;
            this.textBoxA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxA.ForeColor = System.Drawing.Color.White;
            this.textBoxA.Location = new System.Drawing.Point(264, 221);
            this.textBoxA.MaxLength = 2;
            this.textBoxA.Name = "textBoxA";
            this.textBoxA.Size = new System.Drawing.Size(78, 27);
            this.textBoxA.TabIndex = 1;
            this.textBoxA.Visible = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ForeColor = System.Drawing.Color.Transparent;
            this.btnHelp.Location = new System.Drawing.Point(726, 9);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(0);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(30, 30);
            this.btnHelp.TabIndex = 14;
            this.btnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ButtonHelp_MouseClick);
            this.btnHelp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonHelp_MouseDown);
            this.btnHelp.MouseEnter += new System.EventHandler(this.ButtonHelp_MouseHover);
            this.btnHelp.MouseLeave += new System.EventHandler(this.ButtonHelp_MouseLeave);
            this.btnHelp.MouseHover += new System.EventHandler(this.ButtonHelp_MouseHover);
            this.btnHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonHelp_MouseUp);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(744, 12);
            this.label5.MaximumSize = new System.Drawing.Size(790, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 21);
            this.label5.TabIndex = 12;
            this.label5.Text = "                            ";
            // 
            // labelHelp
            // 
            this.labelHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHelp.AutoSize = true;
            this.labelHelp.BackColor = System.Drawing.Color.Transparent;
            this.labelHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelHelp.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHelp.ForeColor = System.Drawing.Color.White;
            this.labelHelp.Location = new System.Drawing.Point(757, 12);
            this.labelHelp.MaximumSize = new System.Drawing.Size(790, 0);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(192, 21);
            this.labelHelp.TabIndex = 13;
            this.labelHelp.Text = "How does this cipher work?";
            // 
            // timerHelp
            // 
            this.timerHelp.Interval = 20;
            this.timerHelp.Tick += new System.EventHandler(this.helpTimer_Tick);
            // 
            // btnOptions
            // 
            this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOptions.BackColor = System.Drawing.Color.White;
            this.btnOptions.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOptions.ForeColor = System.Drawing.Color.DimGray;
            this.btnOptions.Location = new System.Drawing.Point(614, 353);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(79, 32);
            this.btnOptions.TabIndex = 7;
            this.btnOptions.Text = "OPTIONS";
            this.btnOptions.UseVisualStyleBackColor = false;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnFreq
            // 
            this.btnFreq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFreq.BackColor = System.Drawing.Color.DarkOrange;
            this.btnFreq.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrange;
            this.btnFreq.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFreq.ForeColor = System.Drawing.Color.White;
            this.btnFreq.Location = new System.Drawing.Point(264, 265);
            this.btnFreq.Name = "btnFreq";
            this.btnFreq.Size = new System.Drawing.Size(177, 32);
            this.btnFreq.TabIndex = 6;
            this.btnFreq.Text = "FREQUENCY ANALYSIS";
            this.btnFreq.UseVisualStyleBackColor = false;
            this.btnFreq.Click += new System.EventHandler(this.btnFreq_Click);
            // 
            // btnMap
            // 
            this.btnMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMap.BackColor = System.Drawing.Color.LimeGreen;
            this.btnMap.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMap.ForeColor = System.Drawing.Color.White;
            this.btnMap.Location = new System.Drawing.Point(264, 309);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(177, 32);
            this.btnMap.TabIndex = 7;
            this.btnMap.Text = "MAP LETTERS";
            this.btnMap.UseVisualStyleBackColor = false;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // btnReverse
            // 
            this.btnReverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReverse.BackColor = System.Drawing.Color.DarkOrchid;
            this.btnReverse.FlatAppearance.BorderColor = System.Drawing.Color.DarkOrchid;
            this.btnReverse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReverse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReverse.ForeColor = System.Drawing.Color.White;
            this.btnReverse.Location = new System.Drawing.Point(453, 265);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(149, 32);
            this.btnReverse.TabIndex = 7;
            this.btnReverse.Text = "REVERSE TEXT";
            this.btnReverse.UseVisualStyleBackColor = false;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // labelExtra
            // 
            this.labelExtra.AutoSize = true;
            this.labelExtra.BackColor = System.Drawing.Color.Transparent;
            this.labelExtra.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExtra.ForeColor = System.Drawing.Color.White;
            this.labelExtra.Location = new System.Drawing.Point(454, 221);
            this.labelExtra.Name = "labelExtra";
            this.labelExtra.Size = new System.Drawing.Size(104, 24);
            this.labelExtra.TabIndex = 0;
            this.labelExtra.Text = "Extra Label";
            this.labelExtra.Visible = false;
            // 
            // btnLettersOnly
            // 
            this.btnLettersOnly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLettersOnly.BackColor = System.Drawing.Color.Crimson;
            this.btnLettersOnly.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
            this.btnLettersOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLettersOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLettersOnly.ForeColor = System.Drawing.Color.White;
            this.btnLettersOnly.Location = new System.Drawing.Point(453, 309);
            this.btnLettersOnly.Name = "btnLettersOnly";
            this.btnLettersOnly.Size = new System.Drawing.Size(149, 32);
            this.btnLettersOnly.TabIndex = 7;
            this.btnLettersOnly.Text = "LETTERS ONLY";
            this.btnLettersOnly.UseVisualStyleBackColor = false;
            this.btnLettersOnly.Click += new System.EventHandler(this.btnLettersOnly_Click);
            // 
            // btnIofC
            // 
            this.btnIofC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIofC.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnIofC.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnIofC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIofC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIofC.ForeColor = System.Drawing.Color.White;
            this.btnIofC.Location = new System.Drawing.Point(264, 353);
            this.btnIofC.Name = "btnIofC";
            this.btnIofC.Size = new System.Drawing.Size(177, 32);
            this.btnIofC.TabIndex = 7;
            this.btnIofC.Text = "INDEX OF COINCIDENCE";
            this.btnIofC.UseVisualStyleBackColor = false;
            this.btnIofC.Click += new System.EventHandler(this.btnIofC_Click);
            // 
            // btnFindReplace
            // 
            this.btnFindReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFindReplace.BackColor = System.Drawing.Color.HotPink;
            this.btnFindReplace.FlatAppearance.BorderColor = System.Drawing.Color.HotPink;
            this.btnFindReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindReplace.ForeColor = System.Drawing.Color.White;
            this.btnFindReplace.Location = new System.Drawing.Point(453, 353);
            this.btnFindReplace.Name = "btnFindReplace";
            this.btnFindReplace.Size = new System.Drawing.Size(149, 32);
            this.btnFindReplace.TabIndex = 7;
            this.btnFindReplace.Text = "FIND AND REPLACE";
            this.btnFindReplace.UseVisualStyleBackColor = false;
            this.btnFindReplace.Click += new System.EventHandler(this.btnFindReplace_Click);
            // 
            // lblCharCount
            // 
            this.lblCharCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCharCount.Font = new System.Drawing.Font("Roboto Condensed Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCharCount.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCharCount.Location = new System.Drawing.Point(453, 171);
            this.lblCharCount.Name = "lblCharCount";
            this.lblCharCount.Size = new System.Drawing.Size(305, 16);
            this.lblCharCount.TabIndex = 0;
            this.lblCharCount.Text = "Character Count: 0     Letter Count: 0";
            this.lblCharCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CipherTools.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(764, 397);
            this.Controls.Add(this.labelExtra);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnFreq);
            this.Controls.Add(this.btnIofC);
            this.Controls.Add(this.btnFindReplace);
            this.Controls.Add(this.btnLettersOnly);
            this.Controls.Add(this.btnReverse);
            this.Controls.Add(this.btnMap);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.radioButtonD);
            this.Controls.Add(this.radioButtonE);
            this.Controls.Add(this.textBoxA);
            this.Controls.Add(this.textBoxB);
            this.Controls.Add(this.textBoxKey);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelInstruction);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCipherType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelHelp);
            this.Controls.Add(this.lblCharCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cipher Tool";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCipherType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.RadioButton radioButtonE;
        private System.Windows.Forms.RadioButton radioButtonD;
        private System.Windows.Forms.DataGridView dgvCipherType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CipherType;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.TextBox textBoxA;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelHelp;
        private System.Windows.Forms.Timer timerHelp;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnFreq;
        private System.Windows.Forms.Button btnMap;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Label labelExtra;
        private System.Windows.Forms.Button btnLettersOnly;
        private System.Windows.Forms.Button btnIofC;
        private System.Windows.Forms.Button btnFindReplace;
        private System.Windows.Forms.Label lblCharCount;
    }
}

