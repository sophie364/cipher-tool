using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace CipherTools
{
    public partial class AutokeySolver : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;
        private int key;
        private string originalText;
        private List<Label> streamLabels = new List<Label>();
        private List<Label> shiftLabels = new List<Label>();
        private List<TrackBar> trackbars = new List<TrackBar>();
        private List<DataGridView> dgvs = new List<DataGridView>();
        private int[] trackbarValueBefore; // keeps track of the values of the trackbars
        private char[] currentKey; // stores each character in the key

        private void SetFont()
        {
            byte[] fontData = Properties.Resources.RobotoCondensed_Light;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.RobotoCondensed_Light.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.RobotoCondensed_Light.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 16.0F);
            label1.Font = myFont;
            myFont = new Font(fonts.Families[0], 14.0F);
            textBox.Font = myFont;
            labelCurrentKey.Font = myFont;
        }

        public AutokeySolver(string o_Text, int keyNum)
        {
            InitializeComponent();
            SetFont();

            key = keyNum;
            originalText = new String(o_Text.ToUpper().Where(Char.IsLetter).ToArray()); 
            trackbarValueBefore = new int[key];
            currentKey = new char[key];

            for (int i = 0; i < key; i++)
            {
                string labelName = "labelSFA" + i; // SFA = stream frequency analysis
                string label2Name = "labelShift" + i;
                string textboxName = "textBoxSFA" + i;
                string trackbarName = "trackBarSFA" + i;

                string dgvName = "dgvSFA" + i;

                Label streamLabel = new Label();
                streamLabel.Name = labelName;
                streamLabel.Text = "Letter " + (i + 1);
                streamLabel.AutoSize = true;
                myFont = new Font(fonts.Families[0], 13.0F);
                streamLabel.Font = myFont;
                streamLabel.ForeColor = Color.White;
                streamLabel.BackColor = Color.Transparent;
                streamLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                streamLabels.Add(streamLabel);
                flowLayoutPanelStreams.Controls.Add(streamLabel);

                myFont = new Font(fonts.Families[0], 12.5F);
                DataGridView streamDGV = new DataGridView();
                streamDGV.Name = dgvName;
                streamDGV.AllowUserToAddRows = false;
                streamDGV.AllowUserToDeleteRows = false;
                streamDGV.AllowUserToResizeRows = false;
                streamDGV.AllowUserToResizeColumns = false;
                streamDGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                streamDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                streamDGV.BackgroundColor = Color.White;
                streamDGV.BorderStyle = BorderStyle.None;
                streamDGV.CellBorderStyle = DataGridViewCellBorderStyle.None;
                streamDGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                DataGridViewCellStyle style = streamDGV.ColumnHeadersDefaultCellStyle;
                style.BackColor = Color.DimGray;
                style.Font = myFont;
                style.ForeColor = Color.White;
                style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                style.WrapMode = DataGridViewTriState.True;
                DataGridViewCellStyle defaultCellStyle = streamDGV.DefaultCellStyle;
                defaultCellStyle.BackColor = Color.White;
                defaultCellStyle.Font = myFont;
                defaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                defaultCellStyle.SelectionBackColor = Color.White;
                defaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);
                defaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewCellStyle rowHeadersStyle = streamDGV.RowHeadersDefaultCellStyle;
                rowHeadersStyle.BackColor = Color.White;
                streamDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                streamDGV.Columns.Add("A", "A"); streamDGV.Columns.Add("B", "B"); streamDGV.Columns.Add("C", "C"); streamDGV.Columns.Add("D", "D"); streamDGV.Columns.Add("E", "E");
                streamDGV.Columns.Add("F", "F"); streamDGV.Columns.Add("G", "G"); streamDGV.Columns.Add("H", "H"); streamDGV.Columns.Add("I", "I"); streamDGV.Columns.Add("J", "J"); streamDGV.Columns.Add("K", "K");
                streamDGV.Columns.Add("L", "L"); streamDGV.Columns.Add("M", "M"); streamDGV.Columns.Add("N", "N"); streamDGV.Columns.Add("O", "O"); streamDGV.Columns.Add("P", "P"); streamDGV.Columns.Add("Q", "Q");
                streamDGV.Columns.Add("R", "R"); streamDGV.Columns.Add("S", "S"); streamDGV.Columns.Add("T", "T"); streamDGV.Columns.Add("U", "U"); streamDGV.Columns.Add("V", "V"); streamDGV.Columns.Add("W", "W");
                streamDGV.Columns.Add("X", "X"); streamDGV.Columns.Add("Y", "Y"); streamDGV.Columns.Add("Z", "Z");
                streamDGV.RowHeadersWidth = 4;
                streamDGV.EnableHeadersVisualStyles = false;
                streamDGV.GridColor = Color.DimGray;
                streamDGV.ReadOnly = true;
                streamDGV.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                streamDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                streamDGV.Size = new Size(753, 27);
                streamDGV.Enabled = false;
                dgvs.Add(streamDGV);
                flowLayoutPanelStreams.Controls.Add(streamDGV);
                
                TrackBar trackbar = new TrackBar();
                trackbar.Name = trackbarName;
                trackbar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                trackbar.BackColor = Color.FromArgb(64, 64, 64);
                trackbar.Maximum = 25;
                trackbar.Size = new Size(753, 45);
                trackbar.ValueChanged += new EventHandler(trackbar_ValueChanged);
                trackbar.LargeChange = 1;
                trackbars.Add(trackbar);
                flowLayoutPanelStreams.Controls.Add(trackbar);

                Label gap = new Label();
                gap.BackColor = Color.Transparent;
                gap.MaximumSize = new Size(10, 6);
                flowLayoutPanelStreams.Controls.Add(gap);
            }
            
            textBox.Text = originalText;

            for (int i = 0; i < trackbars.Count; i++)
            {
                trackbar_ValueChanged(trackbars[i], EventArgs.Empty);
            }
        }
        
        private void trackbar_ValueChanged(object sender, EventArgs e)
        {
            TrackBar trackbar = sender as TrackBar; // the trackbar that caused this event to take place can be referred to as 'trackbar'

            for (int i = 0; i < trackbars.Count; i++) // searches through all the trackbars to find the matching one; this is so the index of the trackbar can be used
            {
                if (trackbars[i] == trackbar)
                {
                    currentKey[i] = (char)(trackbar.Value + 65); // the letter will be shown in the key label
                }
            }

            labelCurrentKey.Text = "Current Key: " + new String(currentKey);

            //char[] longKey = new char[originalText.Length];
            //char[] plaintext = new char[longKey.Length]; // will store the decrypted message

            //for (int i = 0; i < key; i++)
            //{
            //    longKey[i] = currentKey[i]; // puts the keyword given by the user into the array
            //}

            //for (int i = 0; i < longKey.Length; i++)
            //{
            //    if (Char.IsLetter(originalText[i]))
            //    {
            //        int originalLetterValue = (int)originalText[i] - 65; // the -65 is to make 'A' correspond to 0
            //        int keyLetterValue = (int)longKey[i] - 65;

            //        int subtractKey = originalLetterValue - keyLetterValue;
            //        int findLetter = ((subtractKey % 26) + 26) % 26; // +26 prevents wrong mod answers when the value being modded is negative
            //        plaintext[i] = (char)(findLetter + 65);

            //        if (i < longKey.Length - key)
            //        {
            //            longKey[i + key] = (char)(findLetter + 65);
            //        } 
            //    }
            //    else
            //    {
            //        plaintext[i] = originalText[i];
            //    }
            //}

            char[] longKey = new char[originalText.Length];
            char[] plaintext = new char[longKey.Length]; // will store the decrypted message

            for (int i = 0; i < currentKey.Length; i++)
            {
                longKey[i] = currentKey[i]; // puts the keyword given by the user into the array
            }

            for (int i = 0; i < longKey.Length; i++)
            {
                int originalLetterValue = (int)originalText[i] - 65; // the -65 is to make 'A' correspond to 0
                int keyLetterValue = (int)longKey[i] - 65;

                int subtractKey = originalLetterValue - keyLetterValue;
                int findLetter = ((subtractKey % 26) + 26) % 26;
                plaintext[i] = (char)(findLetter + 65);

                if (i < longKey.Length - currentKey.Length) // if letters still need to be added to the key so that the next plaintext letters can be decrypted
                {
                    longKey[i + currentKey.Length] = (char)(findLetter + 65);
                }
            }


            textBox.Text = new String(plaintext);
        }
    }
}
