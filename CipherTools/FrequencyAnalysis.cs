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
using System.Windows.Forms.DataVisualization.Charting;

namespace CipherTools
{
    public partial class FrequencyAnalysis : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;

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

            myFont = new Font(fonts.Families[0], 15.0F);
            textBox.Font = myFont;

            myFont = new Font(fonts.Families[0], 14.0F);
            radioButtonLC.Font = myFont;
            radioButtonPrcnt.Font = myFont;
            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    if (c.Name == "textBox")
                    {
                        c.Font = myFont;
                    }
                    else
                    {
                        myFont = new Font(fonts.Families[0], 13.7F);
                        c.Font = myFont;
                        myFont = new Font(fonts.Families[0], 14.0F);
                        c.BackColor = Color.White;
                        c.ForeColor = Color.DimGray;
                        c.Enabled = false;
                    }
                }
            }

            myFont = new Font(fonts.Families[0], 12.5F);
            buttonTables.Font = myFont;
            buttonRS.Font = myFont;
            buttonInsert.Font = myFont;
            buttonColour.Font = myFont;
            buttonGraph.Font = myFont;
        }

        public FrequencyAnalysis()
        {
            InitializeComponent();

            btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent; // stops the grey highlight
            btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);

            SetFont();
        }
        
        private void buttonTables_Click(object sender, EventArgs e)
        {
            SeeTables freq = new SeeTables();
            freq.Show();
        }

        private bool colourOn = true; // colour codes the frequencies by default
        private int[] letterCount = new int[26];

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            letterCount = new int[26];
            double totalLetters = 0;
            double percentage = 0;

            int highestLetterCount = 0;

            string onlyLetters = new String(textBox.Text.Where(Char.IsLetter).ToArray());
            totalLetters = onlyLetters.Length;
            
            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" && c.Name != "textBox")
                {
                    c.ResetText();
                    c.BackColor = Color.White;
                }
            }
            
            for (int i = 0; i < 26; i++)
            {
                char letter = (char)(i + 97); // lowercase a = 97
                letterCount[i] = Regex.Matches(textBox.Text.ToLower(), letter.ToString()).Count;

                string textboxName = "textBox" + i;

                if (radioButtonLC.Checked == true)
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.Name == textboxName && letterCount[i] != 0)
                        {
                            c.Enabled = true;
                            c.Text = letterCount[i].ToString();
                            if (letterCount[i] > highestLetterCount)
                            {
                                highestLetterCount = letterCount[i];
                            }
                        }
                    }
                }
                else if (radioButtonPrcnt.Checked == true)
                {
                    percentage = ((double)letterCount[i] / totalLetters) * 100;
                    
                    foreach (Control c in this.Controls)
                    {
                        if (c.Name == textboxName && letterCount[i] != 0)
                        {
                            c.Enabled = true;
                            c.Text = percentage.ToString("0.#");
                            if (letterCount[i] > highestLetterCount)
                            {
                                highestLetterCount = letterCount[i];
                            }
                        }
                    }
                }
            }

            if (colourOn == true)
            {
                for (int i = 0; i < 26; i++)
                {
                    string textboxName = "textBox" + i;

                    foreach (Control c in this.Controls)
                    {
                        if (c.Name == textboxName)
                        {
                            if (letterCount[i] != 0)
                            {
                                double coefficient = 1 - ((double)letterCount[i] / highestLetterCount);
                                Color c1 = Color.Orange;
                                c.BackColor = Color.FromArgb(c1.A, (int)(c1.R * coefficient), 235, (int)(c1.B * coefficient));
                            }
                        }
                    }
                }
            }

            if (buttonGraph.Text == "HIDE GRAPH") // if a chart has been made and is shown
            {
                chartFA.Dispose();
                putDataInGraph();
            }


        }

        private void radioButtonPrcnt_CheckedChanged(object sender, EventArgs e)
        {
            textBox_TextChanged(this, EventArgs.Empty);
        }

        private void radioButtonLC_CheckedChanged(object sender, EventArgs e)
        {
            textBox_TextChanged(this, EventArgs.Empty);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) // Ctrl + A shortcut for select all
            {
                textBox.SelectAll();

                // prevents 'ding' noise when Ctrl + A is pressed
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void buttonRS_Click(object sender, EventArgs e) // remove spaces
        {
            string newText = textBox.Text;
            newText = newText.Replace(" ", "");
            newText = Regex.Replace(newText, @"\r\n?|\n", "");
            textBox.Text = newText;
        }

        private void buttonColour_Click(object sender, EventArgs e)
        {
            colourOn = !colourOn;
            textBox_TextChanged(this, EventArgs.Empty);
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            textBox.Text = Regex.Replace(textBox.Text, ".{5}", "$0 "); // inserts a space after every 5 characters
        }

        private bool btnGraphClickedAlready = false;

        private void buttonGraph_Click(object sender, EventArgs e)
        {
            string text = new String(textBox.Text.Where(Char.IsLetter).ToArray());

            if (btnGraphClickedAlready == true)
            {
                this.Size = new Size(535, 484);
                this.CenterToScreen();
                btnGraphClickedAlready = false;
                buttonGraph.Text = "SHOW GRAPH";
                chartFA.Dispose();
            }
            else
            {
                buttonGraph.Text = "HIDE GRAPH";
                this.Size = new Size(900, 484);
                this.CenterToScreen();
                putDataInGraph();
                btnGraphClickedAlready = true;
            }
            
        }

        private void putDataInGraph()
        {
            SetupBarChart();
            string[] letterLabels = createAlphabetArray();
            int[] frequencies = new int[26];

            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" && c.Name != "textBox" && c.Text != "")
                {
                    int index = 0;
                    if (c.Name.Length == 8)
                    {
                        index = int.Parse(c.Name.Substring(7, 1));
                    }
                    else
                    {
                        index = int.Parse(c.Name.Substring(7, 2));
                    }

                    frequencies[c.TabIndex - 1] = letterCount[index];
                }
            }
            
            Stack<string> stack = new Stack<string>();
            letterLabels = stack.ReverseArray(letterLabels);

            Stack<int> stack2 = new Stack<int>();
            frequencies = stack2.ReverseArray(frequencies);
            chartFA.Series[0].Points.DataBindXY(letterLabels, frequencies);
        }

        private string[] createAlphabetArray()
        {
            string[] letterLabels = new string[26];
            for (int i = 0; i < 26; i++)
            {
                letterLabels[i] = (char)(65 + i) + "";
            }
            return letterLabels;
        }

        private Chart chartFA;

        private void SetupBarChart()
        {
            chartFA = new Chart();
            chartFA.Size = new Size(352, 421);
            chartFA.Location = new Point(520, 12);
            chartFA.BackColor = Color.FromArgb(64, 64, 64);

            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = Color.FromArgb(64,64,64);
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.MajorGrid.LineWidth = 0;
            chartArea.AxisY.MajorGrid.LineWidth = 1;
            chartArea.AxisY.MajorGrid.LineColor = Color.DimGray;
            chartArea.AxisX.MajorGrid.LineColor = Color.White;
            chartArea.AxisX.LineColor = Color.DimGray;
            chartArea.AxisY.LineColor = Color.DimGray;
            chartArea.AxisX.LabelStyle.ForeColor = Color.White;
            chartArea.AxisY.LabelStyle.ForeColor = Color.White;
            chartArea.AxisX.MajorTickMark.LineColor = Color.DimGray;
            chartArea.AxisY.MajorTickMark.LineColor = Color.DimGray;

            chartFA.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.Color = Color.FromArgb(243,74,21);
            series.LabelForeColor = Color.LightGray;
            series.ChartType = SeriesChartType.Bar;
            series.XValueType = ChartValueType.String;
            series.YValueType = ChartValueType.Int32;
            series.IsVisibleInLegend = false;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;

            chartFA.Series.Add(series);
            Controls.Add(chartFA);
            chartFA.BringToFront();
        }

        private bool isHovering = false;

        private void timerHelp_Tick(object sender, EventArgs e)
        {
            int step = 15; // move label by 15 pixels per tick
            if (isHovering == true && (labelHelp.Location.X) > (390))
            {
                //Move from right to left by decreasing x
                labelHelp.Location = new Point(labelHelp.Location.X - step, labelHelp.Location.Y);
            }

            else if (isHovering == false)
            {
                if ((labelHelp.Location.X) < (480))
                {
                    //Move from left to right by decreasing x
                    labelHelp.Location = new Point(labelHelp.Location.X + step, labelHelp.Location.Y);
                }
                else
                {
                    timerHelp.Enabled = false; //Stop timer
                }
            }
        }

        private void btnHelp_MouseHover(object sender, EventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpHover;

            timerHelp.Enabled = true;
            isHovering = true;
        }

        private void btnHelp_MouseClick(object sender, MouseEventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpClick;

            MessageBox.Show("Frequency analysis allows text to be decrypted by comparing letter frequencies in normal " +
                "English text with letter frequencies in the ciphertext. This method is particularly effective " +
                "against monoalphabetic ciphers such as the Caesar cipher as although the letters change "+
            "in the ciphertext, the characteristic frequencies do not.\nTo use this tool, enter the ciphertext into " +
            "the main text box. The count of each letter should appear in the grid. You can view a graph of the results "+
            "by pressing the 'Show Graph' button. The 'Tables' button will open a new screen which shows the "+
            "frequencies of letters in normal English text. You can use these tables to compare the frequencies in the "+
            "ciphertext to normal frequencies. Using this information you can form a substitution alphabet using the "+
            "'Map Letters' feature found on the main menu.", "Message");
        }

        private void btnHelp_MouseDown(object sender, MouseEventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpClick;
        }

        private void btnHelp_MouseLeave(object sender, EventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpNormal;
            isHovering = false;
        }

        private void btnHelp_MouseUp(object sender, MouseEventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpNormal;
        }
    }
}
