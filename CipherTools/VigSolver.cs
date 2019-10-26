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
    public partial class VigSolver : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;
        private List<Label> streamLabels = new List<Label>();
        private List<Label> shiftLabels = new List<Label>();
        private List<TrackBar> trackbars = new List<TrackBar>();
        private List<DataGridView> dgvs = new List<DataGridView>();

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
            myFont = new Font(fonts.Families[0], 11.0F);
            textBox.Font = myFont;
            labelCurrentKey.Font = myFont;
            btnCopy.Font = myFont;
        }

        private int key;
        private string originalText;

        private int[] trackbarValueBefore; // keeps track of the values of the trackbars
        private int[] shiftValues; // when the form starts it automatically guesses each shift by moving the most common letter to 'E' and the amount it has to shift to do this is stored here
        private char[] currentKey; // stores each character in the key

        public VigSolver(string o_Text, int keyNum)
        {
            InitializeComponent();

            key = keyNum;
            originalText = o_Text;
            trackbarValueBefore = new int[key];
            shiftValues = new int[key];
            currentKey = new char[key];

            this.DoubleBuffered = true;
            SetFont();

            for (int i = 0; i < key; i++)
            {
                string labelName = "labelSFA" + i; // SFA = stream frequency analysis
                string label2Name = "labelShift" + i;
                string textboxName = "textBoxSFA" + i;
                string trackbarName = "trackBarSFA" + i;

                string dgvName = "dgvSFA" + i;

                Label streamLabel = new Label();
                streamLabel.Name = labelName;
                streamLabel.Text = "Stream " + (i+1) + " Frequency Analysis";
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
                streamDGV.Columns.Add("A","A"); streamDGV.Columns.Add("B", "B"); streamDGV.Columns.Add("C", "C"); streamDGV.Columns.Add("D", "D"); streamDGV.Columns.Add("E", "E");
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
                streamDGV.Size = new Size(823, 55);
                streamDGV.Rows.Add();
                streamDGV.Enabled = false;
                dgvs.Add(streamDGV);
                flowLayoutPanelStreams.Controls.Add(streamDGV);
                
                Label shiftLabel = new Label();
                shiftLabel.Name = label2Name;
                shiftLabel.Text = "Shift Value: 0/A";
                shiftLabel.AutoSize = true;
                shiftLabel.Font = myFont;
                shiftLabel.ForeColor = Color.White;
                shiftLabel.BackColor = Color.FromArgb(64,64,64);
                shiftLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                shiftLabels.Add(shiftLabel);
                flowLayoutPanelStreams.Controls.Add(shiftLabel);

                TrackBar trackbar = new TrackBar();
                trackbar.Name = trackbarName;
                trackbar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                trackbar.BackColor = Color.FromArgb(64, 64, 64);
                trackbar.Maximum = 25;
                trackbar.Size = new Size(823, 45);
                trackbar.ValueChanged += new EventHandler(trackbar_ValueChanged);
                trackbar.LargeChange = 1;
                trackbars.Add(trackbar);
                flowLayoutPanelStreams.Controls.Add(trackbar);

                Label gap = new Label();
                gap.BackColor = Color.Transparent;
                gap.MaximumSize = new Size(10, 6);
                flowLayoutPanelStreams.Controls.Add(gap);
            }

            string text = new string(originalText.Where(Char.IsLetter).ToArray());
            textBox.Text = text.ToUpper();
            List<string> streams = new List<string>();
            int offset = 0;

            for (int i = 0; i < key; i++)
            {
                streams.Add(""); // adds N amount of streams where N is the period number

                for (int j = 0; j < text.Length; j += key)
                {
                    if ((j + offset) < text.Length)
                    {
                        streams[i] += text[j + offset]; // each stream is filled with text
                    }
                }

                offset += 1; // changes the starting point of each stream
            }

            int[,] letterCount = new int[streams.Count ,26]; // stores [stream number, count of each letter]

            for (int i = 0; i < streams.Count; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    char letter = (char)(j + 97); // lowercase a = 97
                    letterCount[i,j] = Regex.Matches(streams[i].ToLower(), letter.ToString()).Count; // counts the number of times each letter appears
                }
            }
            
            for (int i = 0; i < dgvs.Count; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    dgvs[i].Rows[0].Cells[j].Value = letterCount[i,j]; // the letter counts for each letter in each stream is stored in the right cells
                }
            }

            for (int i = 0; i < trackbars.Count; i++)
            {
                trackbar_ValueChanged(trackbars[i], EventArgs.Empty); // colour changes
                trackbars[i].Value = ((shiftValues[i] - 4)+26) % 26; // the most frequent letter is aligned to 'E' as a guess
            }
        }
        
        private void trackbar_ValueChanged(object sender, EventArgs e) // whenever any of the trackbars are slid, this event is triggered
        {
            TrackBar trackbar = sender as TrackBar; // the trackbar that caused this event to take place can be referred to as 'trackbar'
            
            for (int i = 0; i < trackbars.Count; i++) // searches through all the trackbars to find the matching one; this is so the index of the trackbar can be used
            {
                int highestValue = 0; // cells that match the highest value will have green text
                
                if (trackbars[i] == trackbar)
                {
                    shiftLabels[i].Text = "Shift Value: " + trackbar.Value + "/" + (char)(trackbar.Value + 65); // +65 allows the ASCII capital letters to be displayed
                    currentKey[i] = (char)(trackbar.Value + 65); // the letter that the stream has been shifted to will be shown in the key label
                    int[] beforeShift = new int[26]; // stores the values inside the dgv before they are moved

                    for (int j = 0; j < 26; j++)
                    {
                        beforeShift[j] = (int)dgvs[i].Rows[0].Cells[j].Value;

                        if (beforeShift[j] > highestValue)
                        {
                            highestValue = beforeShift[j]; // the highest value stored in the dgv will be stored
                        }
                    }

                    for (int k = 0; k < 26; k++)
                    {
                        int difference = trackbar.Value - trackbarValueBefore[i]; // calculates the number that everything should be shifted by
                        if (difference < 0)
                        {
                            difference += 26; // can't have negative indexes; this prevents the mod of negative numbers being taken in the next line
                        }
                        dgvs[i].Rows[0].Cells[k].Value = beforeShift[(k + difference) % 26]; // mod 26 wraps the indexes around so they don't exceed 25
                        
                        if ((int)dgvs[i].Rows[0].Cells[k].Value == 0) // any 0 values will be displayed in red
                        {
                            dgvs[i].Rows[0].Cells[k].Style.ForeColor = Color.Red;
                            dgvs[i].Rows[0].Cells[k].Style.SelectionForeColor = Color.Red;
                        }
                        else if((int)dgvs[i].Rows[0].Cells[k].Value == highestValue) // any cells that contain the highest value are displayed in green
                        {
                            dgvs[i].Rows[0].Cells[k].Style.ForeColor = Color.FromArgb(0,204,0);
                            dgvs[i].Rows[0].Cells[k].Style.SelectionForeColor = Color.FromArgb(0, 204, 0);

                            shiftValues[i] = k;
                        }
                        else // other cell values are displayed in dark grey
                        {
                            dgvs[i].Rows[0].Cells[k].Style.ForeColor = Color.FromArgb(64, 64, 64);
                            dgvs[i].Rows[0].Cells[k].Style.SelectionForeColor = Color.FromArgb(64, 64, 64);
                        }

                    }
                    
                    string text = new String(originalText.ToUpper().Where(Char.IsLetter).ToArray()); // stores only letters from the input text
                    char[] textArray = new char[text.Length]; // will store any letters that have been changed by the trackbar being slid

                    for (int m = 0; m < textArray.Length; m++)
                    {
                        textArray[m] = ' '; // each array position is first filled with a space so any that remain unfilled after changed letters are stored
                    }                       // can then be told to store the same letter as before

                    for (int j = i; j < textBox.Text.Length; j += key)
                    {
                        int index = (int)text[j] - 65; // every Nth letter is changed; where N is the key size
                        textArray[j] = (char)((((index - trackbar.Value)+26) % 26) + 65);
                    }

                    for (int l = 0; l < textArray.Length; l++)
                    {
                        if (textArray[l] == ' ')
                        {
                            textArray[l] = textBox.Text[l]; // other letters are kept the same
                        }
                    }

                    text = new string(textArray);
                    textBox.Text = text; // the output text changes in realtime

                    trackbarValueBefore[i] = trackbar.Value; // allows the difference to be calculated next time the trackbar is changed
                }
            }

            labelCurrentKey.Text = "Current Key: " + new string(currentKey); // displays what the keyword would be if the current slider positions were correct
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.A) // allows Ctrl + A to select all text
            {
                textBox.SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string key = new string(currentKey);
            Clipboard.SetText(key);
        }
    }
}
