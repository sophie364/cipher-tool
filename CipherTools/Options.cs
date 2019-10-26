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
using System.IO;

namespace CipherTools
{
    public partial class Options : Form
    {
        public static string keywordsFile = "keywords.txt"; // text file storing the keywords
        private static int keywordIndex = 0; // stores the row index of the selected word in the dgv

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;
        
        public Options()
        {
            InitializeComponent();

            byte[] fontData = Properties.Resources.RobotoCondensed_Light;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.RobotoCondensed_Light.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.RobotoCondensed_Light.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 12.5F);
            buttonBack.Font = myFont;
            buttonDelete.Font = myFont;
            myFont = new Font(fonts.Families[0], 14.0F);
            dgvWords.DefaultCellStyle.Font = myFont;
            textBoxWord.Font = myFont;
            myFont = new Font(fonts.Families[0], 16.0F);
            dgvWords.ColumnHeadersDefaultCellStyle.Font = myFont;
            label1.Font = myFont;

            ButtonHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent; // stops the grey highlight
            ButtonHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            ButtonHelp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);
        }

        private void Options_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true; // prevents flickering of help label

            dgvWords.ClearSelection(); // clears any selections made in the DGV
            dgvWords.CurrentCell = null;

            if (!File.Exists(keywordsFile))
            {
                File.WriteAllText(keywordsFile, ""); // if no keyword file currently exists, a new one is created
            }
            else
            {
                FillDGV(); // if a keyword file already exists, the dgv is filled with the words from the text file
            }
        }

        private void FillDGV()
        {
            using (FileStream file = new FileStream(keywordsFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string fields = ""; // this is where each line of the text file is stored temporarily

                    while (!reader.EndOfStream)
                    {
                        fields = reader.ReadLine();
                        dgvWords.Rows.Add(fields); // displays each keyword from the file in the grid view
                    }
                    reader.Close();
                }
                file.Close();
            }

            if (dgvWords.Rows.Count != 0) // if the dgv isn't empty
            {
                dgvWords.CurrentCell = dgvWords.Rows[0].Cells[0]; // selects the first keyword type by default
                dgvWords_CellClick(this.dgvWords, new DataGridViewCellEventArgs(0, 0));
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (BackButtonClicked == false)
            {
                e.Cancel = Program.ExitProgram();
            }
        }

        private bool BackButtonClicked = false;

        private void buttonBack_Click(object sender, EventArgs e)
        {
            BackButtonClicked = true;
            MainMenu start = new MainMenu();
            start.Show();
            this.Close();
        }

        private bool isHovering = false;
        
        private void ButtonHelp_Click(object sender, EventArgs e) // tells the user more information
        {
            MessageBox.Show("When brute force is used, there may be many solutions to check through. " +
                "However, the program may suggest realistic solutions by searching each solution for " +
                "the keywords given and displaying them in order of how often the keywords appear. " +
                "To add a new keyword, enter it into the text box and press 'Add'. To delete a keyword," +
                " select it in the table and press 'Delete'.", "Help");
        }

        private void ButtonHelp_MouseClick(object sender, MouseEventArgs e)
        {
            ButtonHelp.BackgroundImage = Properties.Resources.helpClick;
        }

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
            ButtonHelp.BackgroundImage = Properties.Resources.helpHover;
        }

        private void helpTimer_Tick(object sender, EventArgs e)
        {
            int step = 15; // move label by 15 pixels per tick
            if (isHovering == true && (labelHelp.Location.X) > (305))
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dgvWords.SelectedCells.Count != 0) // if there is actually a keyword selected for deletion
            {
                List<string> linesList = File.ReadAllLines(keywordsFile).ToList(); // puts all the lines in the text file into a list
                linesList.RemoveAt(keywordIndex); // removes the selected keyword from the list of keywords
                File.WriteAllLines(keywordsFile, linesList.ToArray()); // the keyword file is rewritten without the deleted word
                dgvWords.Rows.Clear(); // all of the words in the dgv are removed
                FillDGV(); // the words are added back into the dgv - this is so that it is updated & does not show the deleted word any longer
            }
            else
            {
                MessageBox.Show("No keyword has been selected.", "Message");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxWord.Text)) // if a keyword has been entered into the textbox
            {
                bool alreadyExists = false; // for checking whether the keyword entered is already recognised
                string newWord = ""; // will store the user's input as it will be written to file

                using (FileStream file = new FileStream(keywordsFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string fields = ""; // will temporarily store each line in the keyword file
                        
                        do
                        {
                            fields = reader.ReadLine(); // the current line in the text file is stored in 'fields'
                            newWord = textBoxWord.Text.ToUpper(); // the user's input is converted to uppercase
                            newWord = newWord.Replace(" ", String.Empty); // removes any spaces that may have been entered

                            if (fields == newWord)
                            {
                                alreadyExists = true; // if any of the lines in the text file match the user's input,
                            }                         // the keyword entered won't be added - prevents redundant data
                        } while (!reader.EndOfStream); // reads the whole file

                        reader.Close();
                    }
                    file.Close();
                }

                
                if (alreadyExists == false) // if the user's input is a new word
                {
                    using (StreamWriter writer = File.AppendText(keywordsFile))
                    {
                        writer.WriteLine(newWord); // the new keyword is added to the text file
                        writer.Close();
                    }

                    dgvWords.Rows.Add(newWord); // the new keyword is shown in the datagridview
                    dgvWords.CurrentCell = dgvWords.Rows[dgvWords.Rows.Count - 1].Cells[0]; // the new word is selected
                    keywordIndex = dgvWords.CurrentCell.RowIndex;
                    textBoxWord.Clear();
                }
                else
                {
                    MessageBox.Show("The keyword you entered already exists.", "Message");
                }
            }
            else
            {
                MessageBox.Show("You haven't entered a keyword.", "Message");
            }
        }
        
        private void dgvWords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            keywordIndex = dgvWords.CurrentCell.RowIndex;
        }
    }
}
