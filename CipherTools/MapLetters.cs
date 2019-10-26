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
    public partial class MapLetters : Form
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

            myFont = new Font(fonts.Families[0], 13.08F);
            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox")
                {
                    if (c.Name != "textBoxNewAlphabet")
                    {
                        c.Font = myFont;
                        c.BackColor = Color.White;
                        c.ForeColor = Color.DimGray;
                    }
                    else
                    {
                        myFont = new Font(fonts.Families[0], 14.7F);
                        c.Font = myFont;
                        myFont = new Font(fonts.Families[0], 13.08F);
                    }
                }
            }

            myFont = new Font(fonts.Families[0], 16.0F);
            label1.Font = myFont;
            label2.Font = myFont;

            myFont = new Font(fonts.Families[0], 12.5F);
            buttonGo.Font = myFont;
        }

        public MapLetters()
        {
            InitializeComponent();

            SetFont();

            ButtonHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent; // stops the grey highlight
            ButtonHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ButtonHelp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
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

            MessageBox.Show("This tool is particularly useful for the simple substitution cipher. It allows" +
                " a new alphabet/key to be made by directly mapping each letter in the normal alphabet to " +
                "a new letter.", "Message");
        }

        private void helpTimer_Tick(object sender, EventArgs e)
        {
            int step = 15; // move label by 15 pixels per tick
            if (isHovering == true && (labelHelp.Location.X) > (470))
            {
                //Move from right to left by decreasing x
                labelHelp.Location = new Point(labelHelp.Location.X - step, labelHelp.Location.Y);
            }

            else if (isHovering == false)
            {
                if ((labelHelp.Location.X) < (580))
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

        private void buttonGo_Click(object sender, EventArgs e)
        {
            char[] letters = new char[26]; // will store the new alphabet
            int counter = 0;

                label2.Visible = true;
                textBoxNewAlphabet.Visible = true;
                textBoxNewAlphabet.Enabled = true;

                foreach (Control c in this.Controls.Cast<Control>().OrderBy(c => c.TabIndex))
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.TextBox")
                    {
                        if (c.Name != "textBoxNewAlphabet")
                        {
                            if (c.Text != "")
                            {
                                letters[counter] = char.Parse(c.Text);
                            }
                            else
                            {
                                letters[counter] = '-';
                            }
                            counter += 1;
                        }
                    }
                }
                textBoxNewAlphabet.Text = new string(letters);
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar)) // all the "control" characters like the backspace key and clipboard keyboard shortcuts are exempted.
            {
                SendKeys.Send("{TAB}"); // automatically moves to next textbox once a letter is entered
                TextBox focusedTxt = Controls.OfType<TextBox>().FirstOrDefault(x => x.Focused);
                focusedTxt.SelectAll();
            }
        }
    }
}
