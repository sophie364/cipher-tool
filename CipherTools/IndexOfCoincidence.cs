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

namespace CipherTools
{
    public partial class IndexOfCoincidence : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;

        public IndexOfCoincidence()
        {
            InitializeComponent();

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

            myFont = new Font(fonts.Families[0], 13.0F);
            radioButtonGetAvgIC.Font = myFont;
            radioButtonGetIC.Font = myFont;

            myFont = new Font(fonts.Families[0], 12.5F);
            label2.Font = myFont;
            textBoxStart.Font = myFont;
            textBoxEnd.Font = myFont;
            buttonGo.Font = myFont;

            this.Size = new Size(473, 305);
            this.DoubleBuffered = true;
            ButtonHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            ButtonHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ButtonHelp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
            
        }

        private System.Windows.Forms.ToolTip myToolTip = new System.Windows.Forms.ToolTip();

        private void radioButtonGetAvgIC_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonGetAvgIC.Checked == true)
            {
                this.Size = new Size(473, 337);
                label2.Visible = true;
                textBoxStart.Visible = true;
                textBoxEnd.Visible = true;
                buttonGo.Location = new Point(389, 254);
            }
            else if (radioButtonGetAvgIC.Checked == false)
            {
                this.Size = new Size(473, 305);
                label2.Visible = false;
                textBoxStart.Clear();
                textBoxEnd.Clear();
                textBoxStart.Visible = false;
                textBoxEnd.Visible = false;
                buttonGo.Location = new Point(389, 222);
            }
        }

        private bool isHovering = false;

        private void ButtonHelp_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonHelp.BackgroundImage = Properties.Resources.helpClick;
        }

        private void ButtonHelp_MouseHover(object sender, EventArgs e)
        {
            ButtonHelp.BackgroundImage = Properties.Resources.helpHover;

            helpTimer.Enabled = true;
            isHovering = true;
        }

        private void ButtonHelp_MouseLeave(object sender, EventArgs e)
        {
            ButtonHelp.BackgroundImage = Properties.Resources.helpNormal;
            isHovering = false;
        }

        private void ButtonHelp_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonHelp.BackgroundImage = Properties.Resources.helpNormal;
        }

        private void ButtonHelp_MouseClick(object sender, MouseEventArgs e)
        {
            ButtonHelp.BackgroundImage = Properties.Resources.helpClick;

            MessageBox.Show("The index of coincidence is a measure of how similar a frequency distribution is to the uniform distribution. " +
                "The I.C. of a piece of text does not change if the text is enciphered with a substitution cipher and it can be used to " +
                "determine the size of a key. For the English language, the expected I.C. value without normalization is equal to 0.067.\n" +
                "\nUseful links:\nhttps://en.wikipedia.org/wiki/Index_of_coincidence\nhttp://practicalcryptography.com/cryptanalysis/text-characterisation/index-coincidence/" +
                "\nhttp://practicalcryptography.com/cryptanalysis/stochastic-searching/cryptanalysis-vigenere-cipher/", "Message");
        }

        private void helpTimer_Tick(object sender, EventArgs e)
        {
            int step = 15; // move label by 15 pixels per tick
            if (isHovering == true && (labelHelp.Location.X) > (335))
            {
                //Move from right to left by decreasing x
                labelHelp.Location = new Point(labelHelp.Location.X - step, labelHelp.Location.Y);
            }

            else if (isHovering == false)
            {
                if ((labelHelp.Location.X) < (450))
                {
                    //Move from left to right by decreasing x
                    labelHelp.Location = new Point(labelHelp.Location.X + step, labelHelp.Location.Y);
                }
                else
                {
                    helpTimer.Enabled = false; //Stop timer
                }
            }
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

        private int startingPeriod;
        private int endingPeriod;

        private bool InputGiven()
        {
            if (radioButtonGetIC.Checked == true)
            {
                if (textBox.Text != "")
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("You haven't entered any text.", "Message");
                    return false;
                }
            }
            else
            {
                if (textBox.Text != "")
                {
                    if (textBoxStart.Text == "" || textBoxEnd.Text == "")
                    {
                        MessageBox.Show("You need to enter a range of period values to test.", "Message");
                        return false;
                    }
                    else
                    {
                        int start;
                        bool startIsInt = int.TryParse(textBoxStart.Text, out start);
                        int end;
                        bool endIsInt = int.TryParse(textBoxEnd.Text, out end);

                        if (startIsInt == false || endIsInt == false)
                        {
                            MessageBox.Show("The period values must be integers.", "Message");
                            return false;
                        }
                        else
                        {
                            if (start > end) // if for some reason the user enters the numbers the wrong way round
                            {                // they are switched so that the lowest number is the startingPeriod
                                endingPeriod = start;
                                startingPeriod = end;
                            }
                            else
                            {
                                startingPeriod = start;
                                endingPeriod = end;
                            }

                            return true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You haven't entered any text.", "Message");
                    return false;
                }
            }
        }

        private double GetIC(string text)
        {
            double numerator = 0;
            double denominator = text.Length * (text.Length - 1);
            double IC;
            int[] intArray = new int[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                intArray[i] = (int)(text[i] - 65); // stores the int values of each character in the text (A = 0)
            }

            for (int i = 0; i < 25; i++)
            {
                int count = 0;

                foreach (int c in intArray)
                {
                    if (c == i)
                    {
                        count += 1;
                    }
                }

                numerator += count * (count - 1);
            }

            IC = numerator / denominator;
            return IC;
        }

        private double GetAvgIC(string text, int period)
        {
            List<string> streams = new List<string>();
            List<double> ICs = new List<double>();
            int offset = 0;

            for (int i = 0; i < period; i++)
            {
                streams.Add(""); // adds N amount of streams where N is the period number

                for (int j = 0; j < text.Length; j += period)
                {
                    if ((j+offset) < text.Length)
                    {
                        streams[i] += text[j + offset];
                    }
                }

                offset += 1;
            }

            for (int i = 0; i < streams.Count; i++)
            {
                ICs.Add(GetIC(streams[i]));
            }

            double sum = ICs.Sum();
            double average = sum / (double)streams.Count;
            return average;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            bool inputGiven = InputGiven();

            if (inputGiven == true)
            {
                string text = textBox.Text.ToUpper();
                text = new string(text.Where(Char.IsLetter).ToArray());

                if (radioButtonGetIC.Checked == true)
                {
                    double IC = GetIC(text);

                    MessageBox.Show("The index of coincidence is: " + IC.ToString("0.000000000000000"), "Message");
                }
                else if (radioButtonGetAvgIC.Checked == true)
                {
                    string outputByPeriod = "Period\tAverage I.C.\r\n--------------------------------------------\r\n";
                    string outputByAvg = "Period\tAverage I.C.\r\n--------------------------------------------\r\n";
                    double[] avgICs = new double[endingPeriod - startingPeriod + 1];
                    double[] periods = new double[endingPeriod - startingPeriod + 1];
                    int counter = 0;

                    for (int i = startingPeriod; i <= endingPeriod; i++)
                    {
                        periods[counter] = i;
                        avgICs[counter] = GetAvgIC(text, i);
                        
                        outputByPeriod += i + ".\t" + avgICs[counter].ToString("0.000000000000000") + "\r\n";

                        counter += 1;
                    }
                    
                    Array.Sort(avgICs, periods);

                    Stack<double> stack = new Stack<double>();
                    avgICs = stack.ReverseArray(avgICs);
                    periods = stack.ReverseArray(periods);
                    
                    for (int i = 0; i < periods.Length; i++)
                    {
                        outputByAvg += periods[i] + ".\t" + avgICs[i].ToString("0.000000000000000") + "\r\n";
                    }

                    IofCOutput output = new IofCOutput(outputByPeriod, outputByAvg);
                    output.Show();
                }
            }
        }

        private void textBoxEnd_MouseHover(object sender, EventArgs e)
        {
            myToolTip.Show("Enter a period value to end at.", textBoxEnd, 3000);
        }

        private void textBoxStart_MouseHover(object sender, EventArgs e)
        {
            myToolTip.Show("Enter a period value to start from.", textBoxStart, 3000);
        }
    }
}
