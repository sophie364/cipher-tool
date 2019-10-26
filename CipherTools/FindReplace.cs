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
    public partial class FindReplace : Form
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
            label2.Font = myFont;

            myFont = new Font(fonts.Families[0], 12.5F);
            btnReplace.Font = myFont;
            btnSelectAll.Font = myFont;
            btnClear.Font = myFont;

            myFont = new Font(fonts.Families[0], 12.0F);
            cbCaseSensitive.Font = myFont;

            myFont = new Font(fonts.Families[0], 11.25F);
            lblMatches.Font = myFont;
            lblOutOf.Font = myFont;
        } // makes text in certain controls a certain font

        public FindReplace()
        {
            InitializeComponent();
            SetFont();
            mainRTB.SelectAll();
            DisableButtons();
            textBefore = mainRTB.Text;
        }

        private void ResetHighlight() // removes highlight from text
        {
            mainRTB.SelectAll();
            mainRTB.SelectionColor = Color.FromArgb(64, 64, 64);
            mainRTB.SelectionBackColor = Color.WhiteSmoke;
        }

        private void EnableButtons()
        {
            currentSelection = 0;
            btnNext.BackColor = Color.DeepSkyBlue;
            btnNext.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            btnPrev.BackColor = Color.DeepSkyBlue;
            btnPrev.FlatAppearance.BorderColor = Color.DeepSkyBlue;
            btnReplace.BackColor = Color.DodgerBlue;
            btnReplace.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnNext.Enabled = true;
            btnPrev.Enabled = true;
            btnReplace.Enabled = true;
            tbReplace.Enabled = true;
            lblOutOf.Visible = true;
        }

        private void DisableButtons()
        {
            currentSelection = -1;
            btnNext.BackColor = Color.FromArgb(78,78,78);
            btnNext.FlatAppearance.BorderColor = Color.FromArgb(78, 78, 78);
            btnPrev.BackColor = Color.FromArgb(78, 78, 78);
            btnPrev.FlatAppearance.BorderColor = Color.FromArgb(78, 78, 78);
            btnReplace.BackColor = Color.FromArgb(78, 78, 78);
            btnReplace.FlatAppearance.BorderColor = Color.FromArgb(78, 78, 78);
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            btnReplace.Enabled = false;
            tbReplace.Clear();
            tbReplace.Enabled = false;
            lblOutOf.Visible = false;
        }

        MatchCollection matches = Regex.Matches("", "");
        int currentSelection = -1;

        private void tbFind_TextChanged(object sender, EventArgs e)
        {
            currentSelection = -1;
            string escaped = Regex.Escape(tbFind.Text); // allows special characters to be used without errors

            if (cbCaseSensitive.Checked == true) // if the user wants the search to be case sensitive
            {
                matches = Regex.Matches(mainRTB.Text, escaped, RegexOptions.None);
            }
            else
            {
                matches = Regex.Matches(mainRTB.Text, escaped, RegexOptions.IgnoreCase);
            }

            if (matches.Count > 0 && tbFind.Text.Length > 0) // if matches have been found
            {
                lblMatches.Text = matches.Count + " Matches";
                lblMatches.ForeColor = System.Drawing.Color.LimeGreen;
                EnableButtons();
                lblOutOf.Text = "(1/" + matches.Count + ")";
            }
            else if (tbFind.Text.Length == 0)
            {
                lblMatches.Text = "Enter a string to locate.";
                lblMatches.ForeColor = Color.FromArgb(255, 128, 0);
                DisableButtons();
            }
            else
            {
                lblMatches.Text = "No Matches";
                lblMatches.ForeColor = System.Drawing.Color.Red;
                DisableButtons();
            }

            ResetHighlight();
            
            foreach (Match match in matches)
            {
                mainRTB.SelectionStart = match.Index;
                mainRTB.SelectionLength = match.Length;
                if (match == matches[0])
                {
                    mainRTB.SelectionColor = Color.FromArgb(64,64,64);
                    mainRTB.SelectionBackColor = Color.SpringGreen;
                }
                else
                {
                    mainRTB.SelectionColor = Color.FromArgb(64, 64, 64);
                    mainRTB.SelectionBackColor = Color.Aquamarine;
                }
            }

            if (matches.Count > 0)
            {
                mainRTB.Select(matches[0].Index, 0);
                mainRTB.ScrollToCaret();
            }
            
        }

        private void cbCaseSensitive_CheckedChanged(object sender, EventArgs e)
        {
            tbFind_TextChanged(tbFind, EventArgs.Empty); // when the check box is changed the matches might be different so the text is re-checked
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentSelection++;
            lblMatches.Text = matches.Count + " Matches";
            lblOutOf.Text = "(" + (currentSelection + 1) + "/" + matches.Count + ")";
            if (currentSelection == matches.Count)
            {
                currentSelection = 0;
                lblMatches.Text = matches.Count + " Matches";
                lblOutOf.Text = "(1/" + matches.Count + ")";
            }

            int currentMatch = 0;
            foreach (Match match in matches)
            {
                mainRTB.SelectionStart = match.Index;
                mainRTB.SelectionLength = match.Length;
                if (currentMatch == currentSelection)
                {
                    mainRTB.ScrollToCaret();
                    mainRTB.SelectionColor = Color.FromArgb(64, 64, 64);
                    mainRTB.SelectionBackColor = Color.SpringGreen;
                }
                else
                {
                    mainRTB.SelectionColor = Color.FromArgb(64, 64, 64);
                    mainRTB.SelectionBackColor = Color.Aquamarine;
                }
                currentMatch++;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            currentSelection--;
            lblMatches.Text = matches.Count + " Matches";
            lblOutOf.Text = "(" + (currentSelection + 1) + "/" + matches.Count + ")";
            if (currentSelection <= -1)
            {
                currentSelection = matches.Count - 1;
                lblMatches.Text = matches.Count + " Matches";
                lblOutOf.Text = "("+ matches.Count +"/" + matches.Count + ")";
            }
            
            int currentMatch = 0;
            foreach (Match match in matches)
            {
                mainRTB.SelectionStart = match.Index;
                mainRTB.SelectionLength = match.Length;
                if (currentMatch == currentSelection)
                {
                    mainRTB.ScrollToCaret();
                    mainRTB.SelectionColor = Color.FromArgb(64, 64, 64);
                    mainRTB.SelectionBackColor = Color.SpringGreen;
                }
                else
                {
                    mainRTB.SelectionColor = Color.FromArgb(64, 64, 64);
                    mainRTB.SelectionBackColor = Color.Aquamarine;
                }
                currentMatch++;
            }
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            int oldNoOfMatches = 0;

            if (matches.Count > 0 && tbFind.Text.Length > 0)
            {
                string escaped = Regex.Escape(tbFind.Text); // allows special characters to be used without errors
                oldNoOfMatches = matches.Count;

                if (cbCaseSensitive.Checked == true)
                {
                    mainRTB.Text = Regex.Replace(mainRTB.Text, escaped, tbReplace.Text, RegexOptions.None);
                }
                else
                {
                    mainRTB.Text = Regex.Replace(mainRTB.Text, escaped, tbReplace.Text, RegexOptions.IgnoreCase);
                }
            }

            ResetHighlight();

            if (matches.Count == 1)
            {
                MessageBox.Show(oldNoOfMatches + " occurence has been replaced.", "Message");
            }
            else
            {
                MessageBox.Show(oldNoOfMatches + " occurences have been replaced.", "Message");
            }

            tbFind.Text = tbReplace.Text;
            tbReplace.Text = "";
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            mainRTB.Focus();
            mainRTB.SelectAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            mainRTB.Text = "";
            mainRTB.Focus();
            tbFind_TextChanged(tbFind, EventArgs.Empty);
        }
        
        private void mainRTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                mainRTB.SelectAll();

                // prevents 'ding' noise when Ctrl + A is pressed
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private string textBefore;

        private void mainRTB_TextChanged(object sender, EventArgs e)
        {
            if (mainRTB.Text != textBefore)
            {
                textBefore = mainRTB.Text;
                DisableButtons();
                int index = mainRTB.SelectionStart;
                tbFind.Clear();
                mainRTB.SelectionStart = index;
            }
        }
    }
}
