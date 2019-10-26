using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace CipherTools
{
    public partial class MainMenu : Form
    {
        // allows the font I want to be used
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        private PrivateFontCollection fonts = new PrivateFontCollection();
        Font myFont;

        private void SetFont()  // makes text in certain controls a certain font
        {
            byte[] fontData = Properties.Resources.RobotoCondensed_Light;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.RobotoCondensed_Light.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.RobotoCondensed_Light.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 16.0F);
            labelExtra.Font = myFont;
            label1.Font = myFont;
            labelInstruction.Font = myFont;
            dgvCipherType.ColumnHeadersDefaultCellStyle.Font = myFont;

            myFont = new Font(fonts.Families[0], 14.0F);
            dgvCipherType.DefaultCellStyle.Font = myFont;
            radioButtonD.Font = myFont;
            radioButtonE.Font = myFont;
            textBoxKey.Font = myFont;
            textBoxA.Font = myFont;
            textBoxB.Font = myFont;

            myFont = new Font(fonts.Families[0], 12.5F);
            btnGo.Font = myFont;
            btnOptions.Font = myFont;
            btnFreq.Font = myFont;
            btnMap.Font = myFont;
            btnReverse.Font = myFont;
            btnLettersOnly.Font = myFont;
            btnIofC.Font = myFont;
            btnFindReplace.Font = myFont;
            textBox.Font = myFont;

            myFont = new Font(fonts.Families[0], 8.5F);
            lblCharCount.Font = myFont;
        }

        Button btnZigzagDirection = new Button(); // an additional button is needed for the Rail Cipher
        Button btnOrder = new Button(); // button needed for Polybius cipher - determines whether to read row, then column or other way round

        private void CreateZigzagBtn() // makes a button for the zigzag setting to be modified in the rail fence cipher
        {
            btnZigzagDirection.Location = new Point(612, 221);
            btnZigzagDirection.Size = new Size(140, 32);
            btnZigzagDirection.Text = "ZIGZAG DOWN";
            myFont = new Font(fonts.Families[0], 12.5F);
            btnZigzagDirection.Font = myFont;
            btnZigzagDirection.ForeColor = Color.White;
            btnZigzagDirection.FlatStyle = FlatStyle.Flat;
            btnZigzagDirection.TextAlign = ContentAlignment.MiddleCenter;
            btnZigzagDirection.BackColor = Color.RoyalBlue;
            btnZigzagDirection.FlatAppearance.BorderColor = Color.RoyalBlue;
            this.Controls.Add(btnZigzagDirection);
            btnZigzagDirection.BringToFront();
            btnZigzagDirection.Visible = false;
            btnZigzagDirection.Click += new EventHandler(btnZigzagDirection_Click);
        } 

        private void CreateOrderBtn() // button is made which will allow certain ciphers to be read row first, then column or the other way round
        {
            btnOrder.Location = new Point(612, 221);
            btnOrder.Size = new Size(140, 32);
            btnOrder.Text = "(ROW, COL)";
            myFont = new Font(fonts.Families[0], 12.5F);
            btnOrder.Font = myFont;
            btnOrder.ForeColor = Color.White;
            btnOrder.FlatStyle = FlatStyle.Flat;
            btnOrder.TextAlign = ContentAlignment.MiddleCenter;
            btnOrder.BackColor = Color.DeepPink;
            btnOrder.FlatAppearance.BorderColor = Color.DeepPink;
            this.Controls.Add(btnOrder);
            btnOrder.BringToFront();
            btnOrder.Visible = false;
            btnOrder.Click += new EventHandler(btnOrder_Click);
        } 

        private void btnZigzagDirection_Click(object sender, EventArgs e) // event handler which is fired when the zigzag button is pressed
        {
            if (btnZigzagDirection.Text == "ZIGZAG UP") // if the user clicks the button while it says 'ZIGZAG UP' it will change to 'ZIGZAG DOWN'
            {
                btnZigzagDirection.Text = "ZIGZAG DOWN";
                btnZigzagDirection.BackColor = Color.RoyalBlue;
                btnZigzagDirection.FlatAppearance.BorderColor = Color.RoyalBlue;
            }
            else
            {
                btnZigzagDirection.Text = "ZIGZAG UP";
                btnZigzagDirection.BackColor = Color.DeepPink;
                btnZigzagDirection.FlatAppearance.BorderColor = Color.DeepPink;
            }
        } 

        private void btnOrder_Click(object sender, EventArgs e) // event handler which is fired when the order button is clicked
        {
            if (btnOrder.Text == "(ROW, COL)")
            {
                btnOrder.Text = "(COL, ROW)";
                btnOrder.BackColor = Color.RoyalBlue;
                btnOrder.FlatAppearance.BorderColor = Color.RoyalBlue;
            }
            else
            {
                btnOrder.Text = "(ROW, COL)";
                btnOrder.BackColor = Color.DeepPink;
                btnOrder.FlatAppearance.BorderColor = Color.DeepPink;
            }
        }

        public MainMenu()
        {
            InitializeComponent();

            SetFont(); // sets the font of certain controls

            btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent; // stops the grey highlight
            btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(51, 51, 51);

            // these buttons are shown for certain ciphers that have additional settings
            CreateZigzagBtn();
            CreateOrderBtn();

            this.DoubleBuffered = true; // reduces flashing due to lag during animation of the help button
        }

        protected override void OnFormClosing(FormClosingEventArgs e) // when the form is about to close
        {
            e.Cancel = Program.ExitProgram();
        }

        private string cipherType = ""; // stores the cipher type that the user has selected
        private char enOrDe; // stores either 'e' or 'd' depending on whether it is set to encrypt or decrypt

        private void FillDGV() // fills the datagridview with the cipher types
        {
            // a row for each type of cipher is added to the data grid view
            dgvCipherType.Rows.Add(" Caesar Cipher");
            dgvCipherType.Rows.Add(" Atbash Cipher");
            dgvCipherType.Rows.Add(" Substitution Cipher");
            dgvCipherType.Rows.Add(" Affine Cipher");
            dgvCipherType.Rows.Add(" Rail Fence Cipher");
            dgvCipherType.Rows.Add(" Vigenère Cipher");
            dgvCipherType.Rows.Add(" Beaufort Cipher");
            dgvCipherType.Rows.Add(" Columnar Transposition");
            dgvCipherType.Rows.Add(" Playfair Cipher");
            dgvCipherType.Rows.Add(" Polybius Square");
            dgvCipherType.Rows.Add(" Autokey Cipher");
            dgvCipherType.Rows.Add(" ADFGX Cipher");
            dgvCipherType.Rows.Add(" ADFGVX Cipher");
            dgvCipherType.Rows.Add(" Hill Cipher");
            dgvCipherType.Rows.Add(" Bifid Cipher");

            dgvCipherType.ClearSelection(); // clears any selections made in the DGV
            dgvCipherType.CurrentCell = null;
            cipherType = ""; // this variable will store the selected type of cipher
            enOrDe = 'd'; // initially, the program will be set to decrypt the message
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            FillDGV(); // the datagridview is filled with the cipher types

            dgvCipherType.CurrentCell = dgvCipherType.Rows[0].Cells[0]; // selects the first cipher type by default
            dgvCipherType_CellClick(this.dgvCipherType, new DataGridViewCellEventArgs(0, 0));

            HideKeyBoxes(); // initally all the key textboxes are hidden
            textBoxKey.Visible = true; // only the appropriate key textbox for the first cipher type is shown upon loading the form
            labelInstruction.Visible = true; // this label explains what data needs to be provided in the key textbox
        }


        // procedures for changing the visibility of the text boxes
        private void HideKeyBoxes()
        {
            // all key textboxes are hidden and/or disabled
            textBoxKey.Visible = false;
            textBoxB.Visible = false;
            textBoxB.Enabled = false;
            textBoxA.Visible = false;
            textBoxA.Enabled = false;

            // all key textboxes are cleared of any input data
            textBoxKey.Clear();
            textBoxA.Clear();
            textBoxB.Clear();

            // all instruction labels are made invisible
            labelExtra.Visible = false;
            labelInstruction.Visible = false;
            btnZigzagDirection.Visible = false;
            btnOrder.Visible = false;

            // resets the size of the form if the current cipher is not one that requires a larger form size
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                if (dgvCipherType.CurrentCell.Value.ToString() != " ADFGX Cipher" && dgvCipherType.CurrentCell.Value.ToString() != " ADFGVX Cipher")
                {
                    this.Size = new Size(780, 436);
                    dgvCipherType.Size = new Size(240, 373);
                }
            }

            // if the textbox has already been made for the ADFGVX cipher
            if (makeBox == false)
            {
                lblKeyword.Visible = false;
                tbKeyword.Visible = false;
                tbKeyword.Clear();
            }

        }

        private void EnableKeyTextBox()
        {
            textBoxKey.Enabled = true;
            textBoxKey.Visible = true;
            labelInstruction.Visible = true;
        }

        private void EnableABTextBoxes()
        {
            textBoxA.Enabled = true;
            textBoxA.Visible = true;
            textBoxB.Enabled = true;
            textBoxB.Visible = true;
            labelInstruction.Visible = true;
        }

        private bool makeBox = true; // will change to false once the new textbox for part of the ADFGVX cipher has already been made

        private Label lblKeyword; // will display instructions for what to input in the textbox
        private TextBox tbKeyword; // keyword textbox

        private void BoxForADFGVX()
        {
            // making the label for displaying the instruction
            lblKeyword = new Label();
            myFont = new Font(fonts.Families[0], 16.0F);
            lblKeyword.Font = myFont;
            lblKeyword.BackColor = Color.Transparent;
            lblKeyword.AutoSize = true;
            lblKeyword.Text = "Enter the keyword.";
            lblKeyword.ForeColor = Color.White;
            lblKeyword.Location = new Point(259, 260);
            this.Controls.Add(lblKeyword);

            // making the textbox
            tbKeyword = new TextBox();
            myFont = new Font(fonts.Families[0], 14.0F);
            tbKeyword.Font = myFont;
            tbKeyword.BackColor = Color.DimGray;
            tbKeyword.BorderStyle = BorderStyle.FixedSingle;
            tbKeyword.ForeColor = Color.White;
            tbKeyword.Size = new Size(488, 27);
            tbKeyword.Location = new Point(264, 291);
            tbKeyword.MaxLength = 50;
            this.Controls.Add(tbKeyword);
        }

        // functions used by the ciphers
        private int PositiveModulus(int dividend, int divisor) // the normal mod function sometimes returns negative values when positive values are needed
        {
            int a = dividend % divisor;
            int b = a + divisor; // ensures the number is positive before it is modded on the next line
            int c = b % divisor;

            return c;
        }

        private int GetGCDByModulus(int number, int modValue) // GCD = greatest common divisor
        {
            if (modValue == 0) // base case
            {
                return number;
            }

            int remainder = number % modValue;

            return GetGCDByModulus(modValue, remainder); // recursion
        }

        private int ModularInverse(int valueToFindInverseOf, int modNumber)
        {
            // returns (d,s,t) such that d = gcd(a,b) and d == a*s + b*t
            // the GCD must be 1 i.e. the numbers must be coprime for an inverse to exist.
            // if this is satisfied:        a*s + b*t == 1
            // the MODULAR inverse is a number [s] that multiplies by the other number [a] to give 1 (mod [b])
            // so from the equation above:  a*s + b*t == 1 (mod b)
            // b*t (mod b) = 0 so:          a*s == 1 (mod b)
            // so 's' is the modular inverse              
            int[] dst = ExtendedEuclidean(valueToFindInverseOf, modNumber);
            int inverse = PositiveModulus(dst[1], 26); // the inverse is the 's' value
            return inverse;
        }

        private int[] ExtendedEuclidean(int a, int b) // used to help find modular inverse
        {
            // returns (d,s,t) such that d = gcd(a,b) and d == a*s + b*t
            if (b == 0) // base case
            {
                int[] returnArray = { a, 1, 0 }; // gcd(a,b) = a*1 + b*0 = a
                return returnArray;
            }

            int[] dst = ExtendedEuclidean(b, a % b); // recursion
            int d = dst[0];
            int s = dst[2];
            int t = dst[1] - (a / b) * dst[2];
            int[] dst1 = { d, s, t };

            return dst1;
        }

        private string CalculateInverseMatrix(ref int[,] keyMatrix)
        {
            // using a formula to find the inverse of 2 x 2 matrices
            int size = keyMatrix.GetLength(0);
            int a;
            int b;
            int c;
            int d;

            if (size == 2)
            {
                a = keyMatrix[0, 0];
                b = keyMatrix[0, 1];
                c = keyMatrix[1, 0];
                d = keyMatrix[1, 1];

                int denominator = (a * d) - (b * c); // the denominator is also the determinant
                if (denominator == 0 || GetGCDByModulus(denominator, 26) != 1) // dividing by 0 is undefined; the determinant has to be coprime with 26 (GCD = 1) for an inverse to be found
                {
                    return "Inverse matrix does not exist."; // not all 2 x 2 matrices have an inverse
                }
                else
                {
                    int[,] tempMatrix = new int[2, 2];
                    tempMatrix[0, 0] = d;
                    tempMatrix[0, 1] = -b;
                    tempMatrix[1, 0] = -c;
                    tempMatrix[1, 1] = a;

                    int modularInverse = ModularInverse(denominator, 26);

                    if (modularInverse == 0)
                    {
                        return "The inverse matrix calculated cannot be used.";
                    }
                    else
                    {
                        int[,] inverseMatrix = new int[2, 2];

                        for (int i = 0; i < 2; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                inverseMatrix[i, j] = (int)(PositiveModulus((modularInverse * tempMatrix[i, j]), 26));
                            }
                        }
                        keyMatrix = inverseMatrix;
                        return "";
                    }

                }
            }
            else if (size == 3)
            {
                // step 1: find matrix of minors
                int[,] matrixOfMinors = new int[size, size];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // for each number in the matrix
                        int[] abcd = new int[4];
                        int counter = 0;
                        for (int k = 0; k < 3; k++)
                        {
                            for (int l = 0; l < 3; l++)
                            {
                                if (k != i && l != j) // if number is not in same row and column as current number
                                {
                                    abcd[counter] = keyMatrix[l, k];
                                    counter++;
                                }
                            }
                        }

                        a = abcd[0];
                        b = abcd[1];
                        c = abcd[2];
                        d = abcd[3];

                        matrixOfMinors[j, i] = (a * d) - (b * c);
                    }
                }

                // step 2: matrix of cofactors
                int[,] matrixOfCofactors = new int[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        matrixOfCofactors[i, j] = matrixOfMinors[i, j];
                    }
                }
                matrixOfCofactors[0, 1] = matrixOfMinors[0, 1] * -1;
                matrixOfCofactors[1, 0] = matrixOfMinors[1, 0] * -1;
                matrixOfCofactors[1, 2] = matrixOfMinors[1, 2] * -1;
                matrixOfCofactors[2, 1] = matrixOfMinors[2, 1] * -1;

                // step 3: adjoint/adjugate
                int[,] adjointMatrix = new int[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        adjointMatrix[i, j] = matrixOfCofactors[i, j];
                    }
                }
                adjointMatrix[0, 1] = matrixOfCofactors[1, 0];
                adjointMatrix[0, 2] = matrixOfCofactors[2, 0];
                adjointMatrix[1, 0] = matrixOfCofactors[0, 1];
                adjointMatrix[1, 2] = matrixOfCofactors[2, 1];
                adjointMatrix[2, 0] = matrixOfCofactors[0, 2];
                adjointMatrix[2, 1] = matrixOfCofactors[1, 2];

                // step 4: determinant
                int temp1 = keyMatrix[0, 0] * matrixOfCofactors[0, 0];
                int temp2 = keyMatrix[0, 1] * matrixOfCofactors[0, 1];
                int temp3 = keyMatrix[0, 2] * matrixOfCofactors[0, 2];
                int determinant = temp1 + temp2 + temp3;

                int modularInverse = 0;
                for (int i = 0; i < 26; i++)
                {
                    int repeatTil1 = (determinant * i) % 26;

                    if (repeatTil1 == 1)
                    {
                        modularInverse = i;
                    }
                }

                if (modularInverse == 0)
                {
                    return "The inverse matrix calculated cannot be used.";
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            keyMatrix[i, j] = (int)(PositiveModulus((modularInverse * adjointMatrix[i, j]), 26));
                        }
                    }
                    return "";
                }
            }
            else
            {
                return "Inverse matrix too complex to calculate.";
            }
        }
        
        private char[,] MakeGrid(string input, int size) // puts a string into an n x n 'grid'
        {
            int counter = 0; // increments for each letter in the key
            char[,] grid = new char[size, size]; // will store the letters in the grid

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = input[counter]; // the grid is filled with the key
                    counter++;
                }
            }

            return grid;
        }

        private string[] Digrams(string input) // turns input text into an array of digrams
        {
            string[] digrams = new string[input.Length / 2];
            int index = 0;

            for (int i = 0; i < input.Length / 2; i++)
            {
                digrams[i] = input.Substring(index, 2);
                index += 2;
            }

            return digrams;
        }

        private string FormatOutput(string text) // reinserts punctuation & modifies upper/lowercase letters as inputted
        {
            string finalMessage = "";
            int counter = 0;

            for (int i = 0; i < textBox.Text.Length; i++)
            {
                if (Char.IsLetter(textBox.Text[i]))
                {
                    if (Char.IsUpper(textBox.Text[i]))
                    {
                        finalMessage += Char.ToUpper(text[counter]);
                    }
                    else if (Char.IsLower(textBox.Text[i]))
                    {
                        finalMessage += Char.ToLower(text[counter]);
                    }
                    counter++;
                }
                else
                {
                    finalMessage += textBox.Text[i];
                }
            }

            return finalMessage;
        }
        

        // procedures & functions for carrying out encryption/decryption
        private void Caesar(string originalText, string key, bool brute) // if brute = false, a shift number has been given
        {
            originalText = new String(originalText.Where(Char.IsLetter).ToArray()).ToUpper(); // stores the uppercase letters that will be enciphered

            if (brute == false) // if brute force isn't being used
            {
                int shift = int.Parse(key); // turns the shift value into an int
                string finalMessage = CaesarEncryptDecrypt(originalText, shift);

                // the resulting text is outputted in a new form
                Output output = new Output(finalMessage);
                output.Show();
            }
            else if (brute == true) // if brute force is being used
            {
                string[] solutions = new string[25]; // will store the brute force solutions

                for (int shift = 1; shift < 26; shift++) // for every possible shift value
                {
                    string finalMessage = CaesarEncryptDecrypt(originalText, shift);
                    solutions[shift - 1] = finalMessage; // the for loop starts with shift = 1; arrays are 0 based hence 'shift - 1' in the square brackets
                }

                Brute output = new Brute(solutions, FormatOutput(originalText));
                output.Show();
            }

        }

        private string CaesarEncryptDecrypt(string originalText, int shift)
        {
            // if the user wants to decrypt, the negative value of the key is needed to revert the message
            if (enOrDe == 'd')
            {
                shift = -shift;
            }

            char[] characters = new char[originalText.Length]; // all the characters in the ciphertext will be stored in an array
            for (int i = 0; i < characters.Length; i++)
            {
                char letter = originalText[i]; // stores the current letter that will be manipulated

                int index = (int)letter; // stores the ASCII value of the letter

                index = index - 65 + shift; // sets A to 0, B to 1 etc. then applies the shift
                index = PositiveModulus(index, 26);

                letter = (char)(65 + index); // turns the index back into an ASCII value so that a letter is shown

                characters[i] = letter; // adds the plaintext/ciphertext letter to the array 
            }

            return FormatOutput(new string(characters)); // puts all of the translated letters into a string & formats the output
        }

        private void Atbash(string originalText)
        {
            Substitution(originalText, "zyxwvutsrqponmlkjihgfedcba"); // the Atbash cipher is the same as the substitution cipher using a reversed alphabet
        }

        private void Substitution(string originalText, string key)
        {
            originalText = new String(originalText.Where(Char.IsLetter).ToArray()).ToUpper();
            string finalMessage = ""; // will store the message that has been created after the original message has been encrypted/decrypted

            if (enOrDe == 'e') // if the user has chosen to encrypt a message
            {
                char[] characters = new char[originalText.Length]; // this array will store each character in the translated message

                for (int i = 0; i < originalText.Length; i++) // for each character in the original message
                {
                    int currentIndex = originalText[i]; // the index of the current letter is found

                    currentIndex -= 65; // 'A' = 65 in ASCII; doing this makes 'A' = 0

                    characters[i] = char.ToUpper(key[currentIndex]); // the index is used to find its corresponding new letter in the key
                                                                     // and this new letter is added to the new message

                }

                finalMessage = new string(characters); // the character array is converted into a string
            }
            else if (enOrDe == 'd') // if the user chooses to decrypt
            {
                char[] characters = new char[originalText.Length]; // this char array will store the translated message characters

                for (int i = 0; i < originalText.Length; i++)
                {
                    int index = key.IndexOf(char.ToLower(originalText[i]));
                    if (index != -1) // if letter is found in the key
                    {
                        index += 65;
                        characters[i] = (char)index; // the key is used to find the right letters to swap 
                    }
                    else
                    {
                        characters[i] = '-';
                    }
                }

                finalMessage = new string(characters); // the character array is converted to a string
            }

            Output output = new Output(FormatOutput(finalMessage));
            output.Show();
        }

        private void Affine(string originalText, int A, int B, bool brute)
        {
            originalText = new String(originalText.Where(Char.IsLetter).ToArray()).ToUpper();
            string finalMessage = "";

            if (enOrDe == 'e') // if encrypting
            {
                if (brute == false)
                {
                    finalMessage = FormatOutput(AffineEncryption(originalText, A, B));

                    Output output = new Output(finalMessage);
                    output.Show();
                }
                else if (brute == true)
                {
                    int arrayCounter = 0; // tracks the array positions where the possible affine solutions are stored
                    string[] solutions = new string[312]; // will store the brute force solutions
                    int[] possibleAValues = { 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25 }; // the possible A values are every odd number under 26 apart from 13

                    for (int a = 0; a < possibleAValues.Length; a++)
                    {
                        for (int b = 0; b < 26; b++)
                        {
                            solutions[arrayCounter] = "\tA = " + possibleAValues[a] + "\t\tB = " + b + "\r\n" + FormatOutput(AffineEncryption(originalText, possibleAValues[a], b));
                            arrayCounter += 1;
                        }
                    }

                    Brute newForm = new Brute(solutions, textBox.Text);
                    newForm.Show();
                }
            }
            else if (enOrDe == 'd')
            {
                if (brute == false)
                {
                    finalMessage = FormatOutput(AffineDecryption(originalText, A, B));

                    Output output = new Output(finalMessage);
                    output.Show();
                }
                else if (brute == true)
                {
                    int arrayCounter = 0;
                    string[] solutions = new string[312]; // will store the brute force solutions; there are 312 possible solutions
                    int[] possibleAValues = { 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25 }; // the possible A values are every odd number under 26 apart from 13
                    
                    for (int a = 0; a < possibleAValues.Length; a ++)
                    {
                        for (int b = 0; b < 26; b++)
                        {
                            solutions[arrayCounter] = "\tA = " + possibleAValues[a] + "\t\tB = " + b + "\r\n" + FormatOutput(AffineDecryption(originalText, possibleAValues[a], b));
                            arrayCounter += 1;
                        }
                    }

                    Brute newForm = new Brute(solutions, textBox.Text);
                    newForm.Show();
                }
            }
        }

        private string AffineEncryption(string originalText, int A, int B)
        {
            char[] charArray = new char[originalText.Length];
            for (int i = 0; i < originalText.Length; i++)
            {
                int currentCharCode = (int)originalText[i] - 65; // converts letters to numbers where A=0
                charArray[i] = (char)(((A * currentCharCode + B) % 26) + 65); // applies mathematical encryption function
            }

            return new string(charArray);
        }

        private string AffineDecryption(string originalText, int A, int B)
        {
            int modularInverse = ModularInverse(A,26);
            //int modularInverse = ModularInverse(A);

            char[] charArray = new char[originalText.Length];
            for (int i = 0; i < originalText.Length; i++)
            {
                int currentCharCode = (int)originalText[i] - 65; // converts letters to numbers where A=0
                charArray[i] = (char)(PositiveModulus((modularInverse * (currentCharCode - B)),26) + 65);
            }

            return new string(charArray);
        }
        
        private void RailFence(string originalText, int key, bool brute)
        {
            if (brute == false)
            {
                if (enOrDe == 'e') // if encrypting a message without brute force
                {
                    Output output = new Output(RailFenceEncryption(originalText, key));
                    output.Show();
                }
                else if (enOrDe == 'd') // if decrypting
                {
                    Output output = new Output(RailFenceDecryption(originalText, key));
                    output.Show();
                }
            }
            else if (brute == true)
            {
                List<string> bruteSolutionsZDown = new List<string>();
                List<string> bruteSolutionsZUp = new List<string>();

                btnZigzagDirection.Text = "ZIGZAG DOWN";

                for (int i = 2; i < key; i++) // tries keys from 2 to N where N is the text length
                {
                    if (enOrDe == 'e')
                    {
                        bruteSolutionsZDown.Add("Rail Setting: DOWN\tKey: " + i + "\r\n" + RailFenceEncryption(originalText, i));// solutions for downwards zigzag directon are found
                    }
                    else if (enOrDe == 'd')
                    {
                        bruteSolutionsZDown.Add("Rail Setting: DOWN\tKey: " + i + "\r\n" + RailFenceDecryption(originalText, i));// solutions for downwards zigzag directon are found
                    }
                }

                btnZigzagDirection.Text = "ZIGZAG UP"; // causes upward zigzag solutions to be found

                for (int i = 2; i < key; i++)
                {
                    if (enOrDe == 'e')
                    {
                        bruteSolutionsZUp.Add("Rail Setting: UP\t\tKey: " + i + "\r\n" + RailFenceEncryption(originalText, i)); // solutions for upwards zigzag directon are found
                    }
                    else if (enOrDe == 'd')
                    {
                        bruteSolutionsZUp.Add("Rail Setting: UP\t\tKey: " + i + "\r\n" + RailFenceDecryption(originalText, i)); // solutions for upwards zigzag directon are found
                    }
                }

                string[] solutions = bruteSolutionsZDown.Concat(bruteSolutionsZUp).ToArray(); // merges the lists & stores all solutions in one array

                Brute output = new Brute(solutions, originalText);
                output.Show();

                // resets the button
                btnZigzagDirection_Click(this, EventArgs.Empty);
                btnZigzagDirection_Click(this, EventArgs.Empty);
            }
        }

        private string RailFenceEncryption(string originalText, int key)
        {
            List<string> railFence = new List<string>();
            for (int i = 0; i < key; i++)
            {
                railFence.Add(""); // inserts the same number of blank strings to the list as the key number
            }                      // these strings will be the rails

            int railLine = 0; // the first rail index is 0 because the starting position of an array is 0 
            int increment = 1; // this is the amount of positions for the current rail to move to the next rail

            if (btnZigzagDirection.Text == "ZIGZAG UP") // if the zigzag direction is upwards instead of downwards
            {
                railLine = key - 1; // the initial rail will be the last rail; in an array the index will be the number of array positions - 1
                increment = -1; // initially the letters will move up the rail instead of down so the next array position will be 1 less than the previous one
            }

            foreach (char c in originalText)
            {
                if (railLine + increment == key) // if the bottom rail has been reached
                {
                    increment = -1; // the increment changes to -1 so that it goes back up the rails
                }
                else if (railLine + increment == -1) // if the top rail has been reached
                {
                    increment = 1; // the rail line numbers will increase (goes back down the rails)
                }
                railFence[railLine] += c; // the character is put onto the right line
                railLine += increment; // the rail line that gets added to next changes to either the rail
            }                          // above (if increment = -1) or the rail below (if increment = 1)

            string finalMessage = "";
            foreach (string s in railFence)
            {
                finalMessage += s; // puts all the letters from all the rails into one string
            }

            return finalMessage;
        }

        private string RailFenceDecryption(string originalText, int key)
        {
            int cipherLength = originalText.Length;
            List<List<int>> railFence = new List<List<int>>(); // a list of lists is made - will store each rail line, and each rail line will have
            for (int i = 0; i < key; i++)                      // its own list containing the positions in the final message where the ciphertext
            {                                                  // letters should move to
                railFence.Add(new List<int>());
            }

            int railLine = 0;
            int increment = 1;

            if (btnZigzagDirection.Text == "ZIGZAG UP")
            {
                railLine = key - 1;
                increment = -1;
            }

            for (int i = 0; i < cipherLength; i++)
            {
                if (railLine + increment == key)
                {
                    increment = -1; // moves back up the rail
                }
                else if (railLine + increment == -1)
                {
                    increment = 1; // moves down the rail
                }
                railFence[railLine].Add(i); // for each line, an array is made which contains the position values of letters that lie on that line
                railLine += increment;
            }

            int counter = 0;
            char[] outputArray = new char[cipherLength]; // this array will store the final message
            for (int i = 0; i < key; i++) // for each line that there is
            {
                for (int j = 0; j < railFence[i].Count; j++) // for each letter that lies on the current rail line
                {
                    outputArray[railFence[i][j]] = originalText[counter]; // each letter from the original (cipher) text is put in the right place
                    counter++;                                            // in the output array
                }
            }

            return new string(outputArray); // the final message is stored in a string instead of an array
        }

        private void Vigenère(string originalText, string key)
        {
            string inputText = new String(originalText.Where(Char.IsLetter).ToArray()).ToUpper(); // removes anything that is not a letter
            char[] originalArray = inputText.ToArray(); // puts all the letters of the original message into an array

            char[] keyArray = new char[inputText.Length]; // this array will store the key repeated to the same length as the message
            for (int i = 0; i < keyArray.Length; i++)
            {
                keyArray[i] = key[i % key.Length]; // the mod allows the word to repeat over and over again
            }

            char[] newMessageArray = new char[originalArray.Length]; // this array will store the new message

            for (int i = 0; i < originalArray.Length; i++)
            {
                int originalLetterValue = (int)originalArray[i] - 65; // the -65 is to make 'A' correspond to 0
                int keyLetterValue = (int)keyArray[i] - 65;

                if (enOrDe == 'e') // encrypt
                {
                    newMessageArray[i] = (char)(((originalLetterValue + keyLetterValue) % 26) + 65); // mod 26 makes the alphabet wrap around
                                                                                                     // and 65 is added so that letters are displayed not random symbols
                }
                else if (enOrDe == 'd') // decrypt
                {
                    int subtractKey = originalLetterValue - keyLetterValue;
                    int findLetter = PositiveModulus(subtractKey, 26);
                    newMessageArray[i] = (char)(findLetter + 65); // adding 65 makes the letters actually display as letters
                }
            }

            Output output = new Output(FormatOutput(new string(newMessageArray)));
            output.Show();
        }

        private void Beaufort(string originalText, string key)
        {
            string text = new String(originalText.Where(Char.IsLetter).ToArray()).ToUpper(); // removes anything that is not a letter
            char[] originalArray = text.ToArray(); // puts all the letters of the original message into an array

            char[] keyArray = new char[text.Length]; // this array will store the key repeated to the same length as the message
            for (int i = 0; i < keyArray.Length; i++)
            {
                keyArray[i] = key[i % key.Length]; // the mod allows the word to repeat over and over again
            }

            char[] newMessageArray = new char[originalArray.Length]; // this array will store the new message

            for (int i = 0; i < originalArray.Length; i++)
            {
                int originalLetterValue = (int)originalArray[i] - 65; // the -65 is to make 'A' correspond to 0
                int keyLetterValue = (int)keyArray[i] - 65;

                newMessageArray[i] = (char)(PositiveModulus((keyLetterValue - originalLetterValue),26) + 65); // encryption and decryption follow the same algorithm
            }

            Output output = new Output(FormatOutput(new string(newMessageArray)));
            output.Show();
        }

        private void Columnar(string originalText, string key)
        {
            int noOfRows = originalText.Length / key.Length;
            bool roundedUp = false;

            if (originalText.Length % key.Length != 0)
            {
                noOfRows++; // rounds up because even if the final row will only be partially filled there must be enough rows to store the text
                roundedUp = true;
            }

            if (enOrDe == 'e') // encrypt
            {
                int letterCounter = 0; // ensures all the letters are added to the grid and starts adding 'X' in any extra gaps

                char[,] grid = new char[noOfRows, key.Length]; // stores the text in grid form

                for (int i = 0; i < noOfRows; i++)
                {
                    for (int j = 0; j < key.Length; j++)
                    {
                        if (letterCounter < originalText.Length) // if there are still letters to be put into the grid
                        {
                            grid[i, j] = originalText[letterCounter]; // the letters are put in the right places
                            letterCounter++;
                        }
                        else
                        {
                            grid[i, j] = 'X'; // any extra spaces at the end are filled
                        }
                    }
                }

                string[] columns = new string[key.Length]; // will store the text in each column

                for (int i = 0; i < key.Length; i++)
                {
                    char[] columnArray = new char[noOfRows]; // stores the characters that make up a column

                    for (int j = 0; j < noOfRows; j++)
                    {
                        columnArray[j] = grid[j, i];
                    }

                    columns[i] = new string(columnArray); // each column is converted to a string
                }

                char[] keyChars = key.ToArray();
                Array.Sort(keyChars, columns); // the characters in the key are sorted alphabetically and this sorts the columns at the same time

                StringBuilder finalMessage = new StringBuilder();
                foreach (string column in columns)
                {
                    finalMessage.Append(column);
                }

                Output output = new Output(finalMessage.ToString());
                output.Show();
            }
            else if (enOrDe == 'd')
            {
                int noOfFullCols = 0; // will be needed if the cipher is irregular and there are no nulls

                if (roundedUp == true)
                {
                    int temp = noOfRows * key.Length;
                    int noOfPlaceholders = temp - originalText.Length;
                    noOfFullCols = key.Length - noOfPlaceholders;
                }

                char[] sortedKey = key.ToArray();
                int[] indexes = new int[sortedKey.Length];

                for (int i = 0; i < sortedKey.Length; i++)
                {
                    indexes[i] = i; // stores numbers from 0 to keyLength
                }

                Array.Sort(sortedKey, indexes); // the index numbers are re-ordered in conjunction with the alphabetical order of the key
                
                string[] columns = new string[key.Length]; // makes a column for every character in the key
                int start = 0; // this value will increase by the number of rows so that each column contains the no. of rows amount of characters

                for (int i = 0; i < columns.Length; i++)
                {
                    if (roundedUp == true)
                    {
                        if (indexes[i] < noOfFullCols)
                        {
                            columns[i] = originalText.Substring(start, noOfRows); // each column is filled with the same number of characters
                            start += noOfRows;
                        }
                        else
                        {
                            columns[i] = originalText.Substring(start, noOfRows - 1); // -1 because there is a placeholder
                            columns[i] += "X";
                            start += noOfRows - 1;
                        }
                    }
                    else
                    {
                        columns[i] = originalText.Substring(start, noOfRows); // each column is filled with the same number of characters
                        start += noOfRows;
                    }
                }
                
                char[,] grid = new char[noOfRows, key.Length]; // stores the text in grid form

                for (int i = 0; i < key.Length; i++)
                {
                    char[] columnChars = columns[i].ToArray(); // stores each character in the current column

                    for (int j = 0; j < noOfRows; j++)
                    {
                        grid[j, indexes[i]] = columnChars[j]; // goes through each row one at a time and places the current column char in the right column using the index
                    }
                }

                string finalMessage = "";

                foreach (char c in grid)
                {
                    finalMessage += c; // all the characters in the grid array are put into one string to be output
                }

                if (cipherType == "columnar")
                {
                    Output output = new Output(FormatOutput(finalMessage));
                    output.Show();
                }
                else // procedure is used in ADFGVX cipher
                {
                    columnarOutput = finalMessage;
                }
            }
        }

        private string columnarOutput; // the ADFGVX cipher uses the Columnar procedure as part of the algorithm; the output needs to be stored

        private void Playfair(string originalText, string key)
        {
            if (originalText.Length % 2 == 1)
            {
                originalText += "X"; // completes final digram if the text length is odd
            }

            string[] digrams = Digrams(originalText);

            for (int i = 0; i < digrams.Length; i++)
            {
                char[] charsInTheDigram = digrams[i].ToArray();
                if (charsInTheDigram[0] == charsInTheDigram[1]) // if both letters in the digram are the same
                {
                    charsInTheDigram[1] = 'X'; // the second letter is changed to an X
                    digrams[i] = new string(charsInTheDigram);
                }
            }

            // making the key table
            string keyWithoutRepeats = new string(key.ToCharArray().Distinct().ToArray()); // removes any repeated letters in the key
            char[,] keyTable = new char[5, 5]; // makes the 5x5 grid
            int counter = 0;
            char[] alphabet = new char[25]; // J is excluded

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (counter < keyWithoutRepeats.Length)
                    {
                        alphabet[counter] = keyWithoutRepeats[counter]; // puts each of the key letters into the list of letters first
                        counter++;
                    }
                    else
                    {
                        for (int k = 0; k < 26; k++)
                        {
                            string currentLetters = new string(alphabet);
                            if (!currentLetters.Contains((char)(k + 65))) // goes through the alphabet looking for characters that aren't already in the alphabet array
                            {
                                if ((k + 65) != 74) // J = 74 but we want to exclude J so characters are only added if they aren't J
                                {
                                    alphabet[counter] = (char)(k + 65);
                                    counter++;
                                }
                            }
                        }
                    }
                }
            }

            counter = 0; // counter is reset and reused
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    keyTable[i, j] = alphabet[counter]; // the alphabet array has the letters in the right order; now they just need to be put into the 2D array
                    counter++;
                }
            }


            string[] newDigrams = new string[originalText.Length / 2];
            counter = 0;
            bool newDigramFound = false;
            int rowIndex1 = 0;
            int colIndex1 = 0;
            foreach (string digram in digrams)
            {
                char[] lettersInDigram = digram.ToArray();
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {
                        // finds keyTable row and column index of the second letter in the digram
                        if (lettersInDigram[1] == keyTable[row, col])
                        {
                            rowIndex1 = row;
                            colIndex1 = col;
                        }
                    }
                }
                for (int row = 0; row < 5; row++)
                {
                    for (int col = 0; col < 5; col++)
                    {

                        if (lettersInDigram[0] == keyTable[row, col])
                        {
                            newDigramFound = false;

                            for (int k = 0; k < 5; k++)
                            {
                                if (k != col)
                                {
                                    if (lettersInDigram[1] == keyTable[row, k])
                                    {
                                        if (enOrDe == 'e') // encrypt
                                        {
                                            // same row - replace with letters on immediate right on grid
                                            char[] newLetters = { keyTable[row, (col + 1) % 5], keyTable[row, (k + 1) % 5] };
                                            newDigrams[counter] = new string(newLetters);
                                        }
                                        else // decrypt
                                        {
                                            // same row - replace with letters on immediate left on grid
                                            char[] newLetters = { keyTable[row, PositiveModulus((col - 1), 5)], keyTable[row, PositiveModulus((k - 1), 5)] };
                                            newDigrams[counter] = new string(newLetters);
                                        }

                                        counter++;
                                        newDigramFound = true;
                                    }
                                }

                                if (k != row)
                                {
                                    if (lettersInDigram[1] == keyTable[k, col])
                                    {
                                        if (enOrDe == 'e') // encrypt
                                        {
                                            // same column - replace with letters immediately below
                                            char[] newLetters = { keyTable[(row + 1) % 5, col], keyTable[(k + 1) % 5, col] };
                                            newDigrams[counter] = new string(newLetters);
                                        }
                                        else // decrypt
                                        {
                                            // same column - replace with letters immediately above
                                            char[] newLetters = { keyTable[PositiveModulus((row - 1), 5), col], keyTable[PositiveModulus((k - 1), 5), col] };
                                            newDigrams[counter] = new string(newLetters);
                                        }

                                        counter++;
                                        newDigramFound = true;
                                    }
                                }
                            }

                            if (newDigramFound == false) // if the second letter in the digram wasn't in the same row or column as the first
                            {
                                char[] newLetters = { keyTable[row, colIndex1], keyTable[rowIndex1, col] };
                                newDigrams[counter] = new string(newLetters);
                                counter++;
                            }
                        }
                    }
                }
            }

            StringBuilder builder = new StringBuilder();
            foreach (string digram in newDigrams)
            {
                builder.Append(digram); // puts all the digrams into one big string
            }

            string tempString = builder.ToString();
            string finalMessage = tempString;

            if (enOrDe == 'd')
            {
                if (tempString.Contains("X"))
                {
                    string tempMessage = tempString;
                    foreach (Match match in Regex.Matches(tempString, "X"))
                    {
                        char letter = tempMessage[match.Index - 1];
                        char[] temp = tempMessage.ToArray();
                        temp[match.Index] = letter;
                        tempMessage = new string(temp);
                    }

                    if (tempMessage[tempMessage.Length - 1] == tempMessage[tempMessage.Length - 2])
                    {
                        tempMessage = tempMessage.Remove(tempMessage.Length - 1);
                    }

                    finalMessage = tempString + "\r\n\r\nAssuming all 'X's were double letters and/or appended during encryption:\r\n" + tempMessage;
                }
            }

            Output output = new Output(finalMessage);
            output.Show();
        }

        private void Polybius(string originalText, string key)
        {
            char[,] grid = MakeGrid(key, 5);

            if (enOrDe == 'e')
            {
                originalText = new String(originalText.Where(Char.IsLetter).ToArray());
                string ciphertext = ""; // will have numbers appended to it

                for (int i = 0; i < originalText.Length; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (originalText[i] == grid[j, k]) // if the plaintext letter matches the grid letter
                            {
                                if (btnOrder.Text == "(ROW, COL)")
                                {
                                    ciphertext += (j + 1).ToString() + (k + 1).ToString() + " "; // the coordinates are written (row, column)
                                }
                                else
                                {
                                    ciphertext += (k + 1).ToString() + (j + 1).ToString() + " "; // the coordinates are written (column, row)
                                }
                            }
                        }
                    }
                }

                Output output = new Output(ciphertext);
                output.Show();
            }
            else if (enOrDe == 'd')
            {
                originalText = new String(originalText.Where(Char.IsDigit).ToArray());

                if (originalText.Length % 2 != 0) // if there aren't an even number of digits
                {
                    MessageBox.Show("There must be an even number of digits in the ciphertext.", "Message");
                }
                else
                {
                    string plaintext = ""; // plaintext characters will be appended to this
                    string[] digitPairs = new string[originalText.Length / 2];
                    int index = 0;

                    // stores the digits in pairs
                    for (int i = 0; i < originalText.Length / 2; i++)
                    {
                        digitPairs[i] = originalText.Substring(index, 2);
                        index += 2;
                    }

                    // each pair is used as coordinates to locate the correct letter in the grid
                    foreach (string pair in digitPairs)
                    {
                        if (btnOrder.Text == "(ROW, COL)")
                        {
                            plaintext += grid[int.Parse(pair[0].ToString()) - 1, int.Parse(pair[1].ToString()) - 1];
                        }
                        else
                        {
                            plaintext += grid[int.Parse(pair[1].ToString()) - 1, int.Parse(pair[0].ToString()) - 1];
                        }
                    }

                    Output output = new Output(plaintext);
                    output.Show();
                }
            }
        }
        
        private void Autokey(string originalText, string key)
        {
            int lettersFromTextToAppend = originalText.Length - key.Length;

            if (enOrDe == 'e')
            {
                string longKey = key + originalText.Substring(0, lettersFromTextToAppend);
                char[] newMessageArray = new char[originalText.Length]; // this array will store the new message

                for (int i = 0; i < originalText.Length; i++)
                {
                    int originalLetterValue = (int)originalText[i] - 65; // the -65 is to make 'A' correspond to 0
                    int keyLetterValue = (int)longKey[i] - 65;

                    newMessageArray[i] = (char)(((originalLetterValue + keyLetterValue) % 26) + 65); // mod 26 makes the alphabet wrap around
                                                                                                     // and 65 is added so that letters are displayed not random symbols
                }

                Output output = new Output(FormatOutput(new string(newMessageArray)));
                output.Show();
            }
            else if (enOrDe == 'd')
            {
                char[] longKey = new char[originalText.Length];
                char[] plaintext = new char[longKey.Length]; // will store the decrypted message

                for (int i = 0; i < key.Length; i++)
                {
                    longKey[i] = key[i]; // puts the keyword given by the user into the array
                }

                for (int i = 0; i < longKey.Length; i++)
                {
                    int originalLetterValue = (int)originalText[i] - 65; // the -65 is to make 'A' correspond to 0
                    int keyLetterValue = (int)longKey[i] - 65;

                    int subtractKey = originalLetterValue - keyLetterValue;
                    int findLetter = PositiveModulus(subtractKey,26);
                    plaintext[i] = (char)(findLetter + 65);

                    if (i < longKey.Length - key.Length) // if letters still need to be added to the key so that the next plaintext letters can be decrypted
                    {
                        longKey[i + key.Length] = (char)(findLetter + 65);
                    }
                }

                Output output = new Output(FormatOutput(new string(plaintext)));
                output.Show();
            }

        }

        private void ADFGX(string originalText, string keyGrid, string keyword)
        {
            char[,] grid = MakeGrid(keyGrid, 5);
            originalText = Regex.Replace(originalText, @"\s", ""); // removes whitespace

            if (enOrDe == 'e')
            {
                string numbers = ""; // will have numbers appended to it
                string ciphertext = ""; // ciphertext will be appended

                for (int i = 0; i < originalText.Length; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (originalText[i] == grid[j, k]) // if the plaintext letter matches the grid letter
                            {
                                numbers += (j).ToString() + (k).ToString(); // the coordinates are written (row, column)
                            }
                        }
                    }
                }

                foreach (char c in numbers)
                {
                    switch (c)
                    {
                        case '0':
                            { ciphertext += 'A'; break; }
                        case '1':
                            { ciphertext += 'D'; break; }
                        case '2':
                            { ciphertext += 'F'; break; }
                        case '3':
                            { ciphertext += 'G'; break; }
                        case '4':
                            { ciphertext += 'X'; break; }
                    }
                }

                Columnar(ciphertext, keyword);
            }
            else if (enOrDe == 'd')
            {
                Columnar(originalText, keyword);
                string numbers = "";

                foreach (char c in columnarOutput)
                {
                    switch (c)
                    {
                        case 'A':
                            { numbers += '0'; break; }
                        case 'D':
                            { numbers += '1'; break; }
                        case 'F':
                            { numbers += '2'; break; }
                        case 'G':
                            { numbers += '3'; break; }
                        case 'X':
                            { numbers += '4'; break; }
                    }
                }

                string plaintext = ""; // plaintext characters will be appended to this
                string[] digitPairs = Digrams(numbers);

                foreach (string pair in digitPairs)
                {
                    plaintext += grid[int.Parse(pair[0].ToString()), int.Parse(pair[1].ToString())];
                }

                Output output = new Output(plaintext);
                output.Show();
            }

        }
        
        private void ADFGVX(string originalText, string keyGrid, string keyword)
        {
            char[,] grid = MakeGrid(keyGrid, 6);
            originalText = Regex.Replace(originalText, @"\s", ""); // removes whitespace

            if (enOrDe == 'e')
            {
                string numbers = ""; // will have numbers appended to it
                string ciphertext = ""; // ciphertext will be appended

                for (int i = 0; i < originalText.Length; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        for (int k = 0; k < 6; k++)
                        {
                            if (originalText[i] == grid[j, k]) // if the plaintext letter matches the grid letter
                            {
                                numbers += (j).ToString() + (k).ToString(); // the coordinates are written (row, column)
                            }
                        }
                    }
                }

                foreach (char c in numbers)
                {
                    switch (c)
                    {
                        case '0':
                            { ciphertext += 'A'; break; }
                        case '1':
                            { ciphertext += 'D'; break; }
                        case '2':
                            { ciphertext += 'F'; break; }
                        case '3':
                            { ciphertext += 'G'; break; }
                        case '4':
                            { ciphertext += 'V'; break; }
                        case '5':
                            { ciphertext += 'X'; break; }
                    }
                }

                Columnar(ciphertext, keyword);
            }
            else if (enOrDe == 'd')
            {
                Columnar(originalText, keyword);
                string numbers = "";

                foreach (char c in columnarOutput)
                {
                    switch (c)
                    {
                        case 'A':
                            { numbers += '0'; break; }
                        case 'D':
                            { numbers += '1'; break; }
                        case 'F':
                            { numbers += '2'; break; }
                        case 'G':
                            { numbers += '3'; break; }
                        case 'V':
                            { numbers += '4'; break; }
                        case 'X':
                            { numbers += '5'; break; }
                    }
                }

                string plaintext = ""; // plaintext characters will be appended to this
                string[] digitPairs = Digrams(numbers);

                foreach (string pair in digitPairs)
                {
                    plaintext += grid[int.Parse(pair[0].ToString()), int.Parse(pair[1].ToString())];
                }

                Output output = new Output(plaintext);
                output.Show();
            }
        }

        private void Hill(string originalText, int size, List<int> matrixValues)
        {
            int[,] keyMatrix = new int[size, size];
            int counter = 0;

            // the key matrix is set up
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    keyMatrix[row, col] = matrixValues[counter];
                    counter++;
                }
            }

            string outputText = HillEncryption(originalText, size, keyMatrix);

            if (enOrDe == 'e')
            {
                Output output = new Output(outputText);
                output.Show();
            }
            else if (enOrDe == 'd')
            {
                string finalMessage = "Assuming the key given is the decryption key:\r\n\t" + FormatOutput(outputText) + "\r\n\r\n----------------------------------------------------------------------------------------------------------------------------------------------------------" +
                    "\r\nAssuming the key given is the encryption key:\r\n";

                string temp = CalculateInverseMatrix(ref keyMatrix); // the inverse matrix (if found) will be stored in keyMatrix
                
                if (temp != "") // if an inverse matrix was found, "" will be returned to temp & the code contained in the 'else' will execute. Otherwise, a message saying an inverse doesn't exist will be returned
                {
                    finalMessage += temp;
                }
                else
                {
                    outputText = HillEncryption(originalText, size, keyMatrix); // the new keyMatrix is the decryption matrix; performing the encryption algorithm with the decryption matrix should revert the ciphertext to plaintext
                    string decryptionKey = "The inverse matrix of the matrix provided is: ";

                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            decryptionKey += keyMatrix[i, j] + " ";
                        }
                    }

                    finalMessage += decryptionKey + "\r\n\t" + FormatOutput(outputText);
                }

                Output output = new Output(finalMessage);
                output.Show();
            }
        }

        private string HillEncryption(string originalText, int size, int[,] keyMatrix)
        {
            int remainder = originalText.Length % size;
            if (remainder != 0)
            {
                for (int i = 0; i < size - remainder; i++)
                {
                    originalText += "X"; // appends extra letters to make text divisible by n
                }
            }

            int[,] ptChunks = new int[originalText.Length / size, size];
            int counter = 0;
            for (int i = 0; i < originalText.Length / size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    ptChunks[i, j] = (int)originalText[counter] - 65; // stores the int values of the letters in chunks of n
                    counter++;
                }
            }

            int[,] tempMatrix = new int[size, size];
            char[,] ctChunks = new char[originalText.Length / size, size];
            string ciphertext = "";

            for (int i = 0; i < originalText.Length / size; i++)
            {
                for (int tempRow = 0; tempRow < size; tempRow++)
                {
                    for (int tempCol = 0; tempCol < size; tempCol++)
                    {
                        tempMatrix[tempRow, tempCol] = keyMatrix[tempRow, tempCol] * ptChunks[i, tempCol]; // makes a matrix of numbers that will be added together (each row) to form the final matrix
                    }
                }

                for (int row = 0; row < size; row++)
                {
                    int sum = 0;

                    for (int col = 0; col < size; col++)
                    {
                        sum += tempMatrix[row, col]; // the values in each row are totalled
                    }

                    ciphertext += (char)((sum % 26) + 65); // each letter is appended to the ciphertext string
                }
            }

            return ciphertext;
        }
        
        private void Bifid(string originalText, string keysquare, int period)
        {
            char[,] grid = MakeGrid(keysquare, 5);

            originalText = new String(originalText.Where(Char.IsLetter).ToArray());

            int numberOfGroups = originalText.Length / period;
            if (originalText.Length % period != 0)
            {
                numberOfGroups += 1; // rounds the number of groups up so that there is enough groups to fit all the characters
            }

            if (enOrDe == 'e') // if encrypting
            {
                int[] rowCoordinates = new int[originalText.Length];
                int[] colCoordinates = new int[originalText.Length];

                for (int i = 0; i < originalText.Length; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (originalText[i] == grid[j, k]) // if the plaintext letter matches the grid letter
                            {
                                // the coordinates of the letters are stored
                                if (btnOrder.Text == "(ROW, COL)")
                                {
                                    rowCoordinates[i] = j + 1;
                                    colCoordinates[i] = k + 1;
                                }
                                else
                                {
                                    rowCoordinates[i] = k + 1;
                                    colCoordinates[i] = j + 1;
                                }
                            }
                        }
                    }
                }

                int[,,] groups = new int[numberOfGroups, 2, period]; // [group number, row/col, position in the group]

                int rowCharCounter = 0;
                int colCharCounter = 0;

                // the coordinates are put into the appropriate groups
                for (int i = 0; i < numberOfGroups; i++)
                {
                    for (int j = 0; j < period; j++)
                    {
                        if (rowCharCounter < originalText.Length)
                        {
                            groups[i, 0, j] = rowCoordinates[rowCharCounter];
                            rowCharCounter++;
                        }
                        if (colCharCounter < originalText.Length)
                        {
                            groups[i, 1, j] = colCoordinates[colCharCounter];
                            colCharCounter++;
                        }

                    }
                }

                string numbers = "";

                // the groups are ordered and their contents are stored in one string
                for (int i = 0; i < numberOfGroups; i++)
                {
                    for (int j = 0; j < period; j++)
                    {
                        if (groups[i, 0, j] != 0)
                        {
                            numbers += groups[i, 0, j];
                        }
                    }
                    for (int k = 0; k < period; k++)
                    {
                        if (groups[i, 0, k] != 0)
                        {
                            numbers += groups[i, 1, k];
                        }
                    }
                }

                string ciphertext = ""; // ciphertext characters will be appended to this
                string[] digitPairs = Digrams(numbers);

                foreach (string pair in digitPairs)
                {
                    // the pairs are used as coordinates to locate the ciphertext letters
                    if (btnOrder.Text == "(ROW, COL)")
                    {
                        ciphertext += grid[int.Parse(pair[0].ToString()) - 1, int.Parse(pair[1].ToString()) - 1];
                    }
                    else
                    {
                        ciphertext += grid[int.Parse(pair[1].ToString()) - 1, int.Parse(pair[0].ToString()) - 1];
                    }
                }

                Output output = new Output(ciphertext);
                output.Show();
            }
            else if (enOrDe == 'd')
            {
                string numbers = "";

                for (int i = 0; i < originalText.Length; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            if (originalText[i] == grid[j, k]) // if the ciphertext letter matches the grid letter
                            {
                                if (btnOrder.Text == "(ROW, COL)")
                                {
                                    numbers += (j + 1).ToString() + (k + 1).ToString(); // the coordinates are written (row, column)
                                }
                                else
                                {
                                    numbers += (k + 1).ToString() + (j + 1).ToString(); // the coordinates are written (column, row)
                                }
                            }
                        }
                    }
                }

                int[,,] groups = new int[numberOfGroups, 2, period]; // [group number, row/col, position in the group]

                int numCounter = 0;
                int mod = originalText.Length % period;

                // the numbers are put into groups
                for (int i = 0; i < numberOfGroups; i++)
                {
                    for (int j = 0; j < period; j++)
                    {
                        if (numCounter + mod < originalText.Length * 2)
                        {
                            groups[i, 0, j] = numbers[numCounter] - 48; // -48 changes the ASCII vaules of the numbers to the actual numbers (48 = 0)
                            numCounter++;
                        }
                    }
                    for (int k = 0; k < period; k++)
                    {
                        if (numCounter < originalText.Length * 2)
                        {
                            groups[i, 1, k] = numbers[numCounter] - 48;
                            numCounter++;
                        }
                    }
                }

                string orderedNums = "";

                // the numbers are ordered by group
                for (int i = 0; i < numberOfGroups; i++)
                {
                    for (int j = 0; j < period; j++)
                    {
                        if (orderedNums.Length < numbers.Length)
                        {
                            if (groups[i, 0, j] != 0)
                            {
                                orderedNums += groups[i, 0, j];
                            }
                        }
                    }
                }
                for (int i = 0; i < numberOfGroups; i++)
                {
                    for (int k = 0; k < period; k++)
                    {
                        if (orderedNums.Length < numbers.Length)
                        {
                            if (groups[i, 0, k] != 0)
                            {
                                orderedNums += groups[i, 1, k];
                            }
                        }
                    }
                }

                int[] rowNumbers = new int[orderedNums.Length / 2];
                int[] colNumbers = new int[orderedNums.Length / 2];
                int rowCounter = 0;
                int colCounter = 0;

                // the numbers are sorted into row coordinates & column coordinates
                for (int i = 0; i < orderedNums.Length; i++)
                {
                    if (i < orderedNums.Length / 2)
                    {
                        rowNumbers[rowCounter] = int.Parse(orderedNums[i].ToString());
                        rowCounter++;
                    }
                    else
                    {
                        colNumbers[colCounter] = int.Parse(orderedNums[i].ToString());
                        colCounter++;
                    }
                }

                orderedNums = "";

                for (int i = 0; i < rowNumbers.Length; i++)
                {
                    orderedNums += rowNumbers[i];
                    orderedNums += colNumbers[i];
                }

                string plaintext = ""; // plaintext characters will be appended to this
                string[] digitPairs = Digrams(orderedNums);

                foreach (string pair in digitPairs) // need to read top then bottom, not left to right
                {
                    if (pair != "00")
                    {
                        if (btnOrder.Text == "(ROW, COL)")
                        {
                            plaintext += grid[int.Parse(pair[0].ToString()) - 1, int.Parse(pair[1].ToString()) - 1];
                        }
                        else
                        {
                            plaintext += grid[int.Parse(pair[1].ToString()) - 1, int.Parse(pair[0].ToString()) - 1];
                        }
                    }
                }

                Output output = new Output(plaintext);
                output.Show();
            }
        }
        

        // procedures for checking inputs are valid
        private bool TextEntered() // will ensure the main text box hasn't been left blank
        {
            bool textEntered = false;
            if (textBox.Text != "")
            {
                textEntered = true;
            }
            else // an error message is shown if the user has not entered any text to be encrypted/decrypted
            {
                textEntered = false;
                MessageBox.Show("You haven't entered any text.", "Message");
            }
            return textEntered;
        }

        private void CaesarCheck()
        {
            int shift; // if a shift value is given it will be stored here
            bool isNumeric = int.TryParse(textBoxKey.Text, out shift); // checks if the given key is an int
            bool inRange = false;

            if (Enumerable.Range(1, 25).Contains(shift)) // checks if the number given is between 1-25
            {
                inRange = true;
            }
            else
            {
                inRange = false;
            }

            if (textBoxKey.Text.ToLower() == "b" || inRange == true) // if all inputs are valid or brute force has been chosen
            {
                if (textBoxKey.Text.ToLower() == "b") // brute force
                {
                    Caesar(textBox.Text, textBoxKey.Text, true); // run procedure
                }
                else // without brute force
                {
                    Caesar(textBox.Text, textBoxKey.Text, false);
                }
            }
            else if (inRange == false) // if the shift value isn't in range, an error message is shown
            {
                MessageBox.Show("Your shift value is invalid.", "Message");
            }
        }

        private void SubstitutionCheck()
        {
            if (textBoxKey.Text.Length != 26)
            {
                MessageBox.Show("The key must be 26 characters long.", "Message");
            }
            else
            {
                Match match = Regex.Match(textBoxKey.Text, @"^[A-Za-z-]+$"); // if the key textbox only contains letters and/or hyphens the match will be successful

                if (match.Success)
                {
                    Substitution(textBox.Text, textBoxKey.Text.ToLower());
                }
                else // error message
                {
                    MessageBox.Show("Key must only contain letters and/or hyphens (-).", "Message");
                }

            }
        }

        private void AffineCheck()
        {
            if (textBoxA.Text == "" && textBoxB.Text == "") // brute force
            {
                Affine(textBox.Text.ToLower(), 1, 0, true);
            }
            else
            {
                int A;
                bool isANumeric = int.TryParse(textBoxA.Text, out A); // checks if the given A value is an int
                int B;
                bool isBNumeric = int.TryParse(textBoxB.Text, out B); // checks if the given B value is an int

                if (isANumeric == false) // if the A value is not a whole number
                {
                    MessageBox.Show("'A' must be a whole number.", "Message");
                }
                else
                {
                    if (isBNumeric == false)
                    {
                        MessageBox.Show("'B' must be a whole number.", "Message");
                    }
                    else
                    {
                        if (A == 0)
                        {
                            MessageBox.Show("'A' cannot be 0.", "Message");
                        }
                        else if (A > 25)
                        {
                            MessageBox.Show("'A' must be less than 26.", "Message");
                        }
                        else
                        {
                            if (GetGCDByModulus(A, 26) == 1) // if A is coprime with 26, it is acceptable
                            {
                                Affine(textBox.Text, A, B, false);
                            }
                            else
                            {
                                MessageBox.Show("Your 'A' value is not coprime with 26. The possible values that A could be are 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, and 25.", "Message");
                            }
                        }
                    }
                }
            }
        }
        
        private void RailFenceCheck()
        {
            int railKey;
            bool isKeyNumeric = int.TryParse(textBoxKey.Text, out railKey); // checks if the given railkey value is an int

            if (isKeyNumeric == false)
            {
                if (textBoxKey.Text.ToLower() == "b") // brute force
                {
                    RailFence(textBox.Text, textBox.Text.Length, true);
                }
                else
                {
                    MessageBox.Show("The rail key must be an integer. Alternatively, enter 'b' for brute force.", "Message");
                }
            }
            else
            {
                if (railKey <= 1)
                {
                    MessageBox.Show("The rail key must be greater than 1.", "Message");
                }
                else
                {
                    RailFence(textBox.Text, railKey, false); // runs only when all inputs are valid
                }
            }
        }

        private void VigCheck()
        {
            int key;
            bool isKeyNumeric = int.TryParse(textBoxKey.Text, out key); // checks if the given key value is an int

            if (isKeyNumeric == false)
            {
                bool keyIsAllLetters = textBoxKey.Text.All(Char.IsLetter);

                if (keyIsAllLetters == true)
                {
                    // key is known and valid
                    Vigenère(textBox.Text, textBoxKey.Text.ToUpper());
                }
                else if (keyIsAllLetters == false)
                {
                    MessageBox.Show("Every character in the key must be a letter. If you don't know the key, enter the " +
                        "size that you think the key is.", "Message");
                }
            }
            else if (isKeyNumeric == true)
            {
                if (enOrDe == 'e')
                {
                    MessageBox.Show("You must provide a keyword to encrypt with.", "Message");
                }
                else
                {
                    // the key size is known but the actual keyword isn't known

                    if (key == 1)
                    {
                        MessageBox.Show("The key size should not be 1; if it is, it is just a Caesar cipher.", "Message");
                    }
                    else if (key <= textBox.Text.Length && key > 1)
                    {
                        VigSolver vS = new VigSolver(textBox.Text, key); // opens a new form that can be used to find the key
                        vS.Show();
                    }
                    else
                    {
                        MessageBox.Show("Key should be less than or equal to text length.", "Message");
                    }
                }
            }
        }

        private void BeaufortCheck()
        {
            bool keyIsAllLetters = textBoxKey.Text.All(Char.IsLetter); // checks if the key given is made up of letters only

            if (keyIsAllLetters == true)
            {
                // key is known and valid
                Beaufort(textBox.Text, textBoxKey.Text.ToUpper());
            }
            else if (keyIsAllLetters == false)
            {
                MessageBox.Show("Every character in the key must be a letter.", "Message");
            }
        }

        private void ColumnarCheck()
        {
            int key;
            bool isKeyNumeric = int.TryParse(textBoxKey.Text, out key); // checks if the given key value is an int
            string originalText = new String(textBox.Text.ToUpper().Where(Char.IsLetter).ToArray()); // only keeps the letters

            if (isKeyNumeric == false)
            {
                bool keyIsAllLetters = textBoxKey.Text.All(Char.IsLetter);

                if (keyIsAllLetters == true)
                {
                    // key is known and valid
                    if (enOrDe == 'e') // encrypt
                    {
                        Columnar(originalText, textBoxKey.Text.ToUpper());
                    }
                    else if (enOrDe == 'd') // decrypt
                    {
                        Columnar(originalText, textBoxKey.Text.ToUpper());
                    }
                }
                else
                {
                    MessageBox.Show("Every character in the key must be a letter. If you don't know the key, enter the " +
                        "size that you think the key is.", "Message");
                }
            }
            else if (isKeyNumeric == true) // if a key size has been entered instead of the actual key
            {
                if (enOrDe == 'd')
                {
                    int noOfRows = originalText.Length / key;
                    if (originalText.Length % key != 0)
                    {
                        MessageBox.Show("The ciphertext is not divisible by the key.", "Message");
                    }
                    else
                    {
                        string[] columns = new string[key];
                        int start = 0; // this value will increase by the number of rows so that each column contains the no. of rows amount of characters

                        for (int i = 0; i < columns.Length; i++)
                        {
                            columns[i] = originalText.Substring(start, noOfRows); // each column is filled with the same number of characters
                            start += noOfRows;
                        }

                        char[,] grid = new char[noOfRows, key]; // stores the text in grid form

                        for (int i = 0; i < key; i++)
                        {
                            char[] columnChars = columns[i].ToArray(); // stores each character in the current column

                            for (int j = 0; j < noOfRows; j++)
                            {
                                grid[j, i] = columnChars[j]; // goes through each row one at a time and places the current column char in the right column using the index
                            }
                        }

                        // so far, we have the grid with the ciphertext written in columns
                        // a datagridview is made using the inputs given. This is needed in the form that will open, allowing the user to manually solve the cipher
                        DataGridView colTransDGV = new DataGridView();
                        colTransDGV.ColumnCount = key;
                        colTransDGV.AllowUserToAddRows = false;
                        colTransDGV.AllowUserToDeleteRows = false;
                        colTransDGV.MultiSelect = false;
                        colTransDGV.AllowUserToResizeRows = false;
                        colTransDGV.AllowUserToResizeColumns = false;
                        colTransDGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                        colTransDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        colTransDGV.BackgroundColor = Color.White;
                        colTransDGV.BorderStyle = BorderStyle.None;
                        colTransDGV.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        colTransDGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                        DataGridViewCellStyle style = colTransDGV.ColumnHeadersDefaultCellStyle;
                        style.BackColor = Color.DimGray;
                        myFont = new Font(fonts.Families[0], 12.5F);
                        style.Font = myFont;
                        style.ForeColor = Color.White;
                        style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        style.WrapMode = DataGridViewTriState.True;
                        style.SelectionBackColor = Color.DarkGray;
                        DataGridViewCellStyle defaultCellStyle = colTransDGV.DefaultCellStyle;
                        defaultCellStyle.BackColor = Color.White;
                        defaultCellStyle.Font = myFont;
                        defaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
                        defaultCellStyle.SelectionBackColor = Color.White;
                        defaultCellStyle.SelectionForeColor = Color.FromArgb(64, 64, 64);
                        defaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DataGridViewCellStyle rowHeadersStyle = colTransDGV.RowHeadersDefaultCellStyle;
                        rowHeadersStyle.BackColor = Color.White;
                        colTransDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                        colTransDGV.RowHeadersWidth = 4;
                        colTransDGV.EnableHeadersVisualStyles = false;
                        colTransDGV.GridColor = Color.DimGray;
                        colTransDGV.ReadOnly = true;
                        colTransDGV.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                        colTransDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        colTransDGV.ScrollBars = ScrollBars.Vertical;

                        for (int i = 0; i < key; i++)
                        {
                            colTransDGV.Columns[i].Name = (i + 1).ToString();
                            colTransDGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            colTransDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }

                        for (int r = 0; r < noOfRows; r++)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(colTransDGV);

                            for (int c = 0; c < key; c++)
                            {
                                row.Cells[c].Value = grid[r, c];
                            }

                            colTransDGV.Rows.Add(row);
                        }

                        ColTransposition ct = new ColTransposition(colTransDGV, grid); // new form opens
                        ct.Show();
                    }
                }
                else
                {
                    MessageBox.Show("A keyword must be provided for encryption.", "Message");
                }
            }
        }

        private void PlayfairCheck()
        {
            bool keyIsAllLetters = textBoxKey.Text.All(Char.IsLetter);
            string originalText = new String(textBox.Text.ToUpper().Where(Char.IsLetter).ToArray()); // only keeps the letters

            if (keyIsAllLetters == true)
            {
                if (textBoxKey.Text.ToUpper().Contains("J"))
                {
                    MessageBox.Show("The key should not contain the character 'J' - replace it or remove it.", "Message");
                }
                else
                {
                    Playfair(originalText, textBoxKey.Text.ToUpper());
                }
            }
            else
            {
                MessageBox.Show("Every character in the key must be a letter.", "Message");
            }
        }

        private void PolybiusCheck()
        {
            if (textBoxKey.Text.Length != 25)
            {
                MessageBox.Show("The key must be 25 characters long.", "Message");
            }
            else
            {
                Match match = Regex.Match(textBoxKey.Text, @"^[A-Za-z-]+$"); // if the key textbox only contains letters and/or hyphens the match will be successful

                if (match.Success)
                {
                    Polybius(textBox.Text.ToUpper(), textBoxKey.Text.ToUpper());
                }
                else
                {
                    MessageBox.Show("Key must only contain letters and/or hyphens (-).", "Message");
                }
            }
        }

        private void AutokeyCheck()
        {
            int key;
            bool isKeyNumeric = int.TryParse(textBoxKey.Text, out key); // checks if the given key value is an int

            if (isKeyNumeric == false)
            {
                bool keyIsAllLetters = textBoxKey.Text.All(Char.IsLetter);

                if (keyIsAllLetters == true)
                {
                    // key is known and valid
                    Autokey(new String(textBox.Text.Where(Char.IsLetter).ToArray()).ToUpper(), textBoxKey.Text.ToUpper());
                }
                else if (keyIsAllLetters == false)
                {
                    MessageBox.Show("Every character in the key must be a letter. If you don't know the key, enter the " +
                        "size that you think the key is.", "Message");
                }
            }
            else
            {
                if (enOrDe == 'e')
                {
                    MessageBox.Show("You must provide a keyword to encrypt with.", "Message");
                }
                else
                {
                    if (key <= textBox.Text.Length && key > 1) // if key size given is a valid size
                    {
                        AutokeySolver auto = new AutokeySolver(textBox.Text, key); // a new form opens
                        auto.Show();
                    }
                    else
                    {
                        MessageBox.Show("Key should be less than or equal to text length.", "Message");
                    }
                }
            }
        }

        private void ADFGVXCheck()
        {
            if (String.IsNullOrWhiteSpace(tbKeyword.Text))
            {
                MessageBox.Show("You must enter a keyword.", "Message");
            }
            else
            {
                if (cipherType == "adfgvx")
                {
                    if (textBoxKey.Text.Length != 36)
                    {
                        MessageBox.Show("The key must be 36 characters long.", "Message");
                    }
                    else
                    {
                        ADFGVX(textBox.Text.ToUpper(), textBoxKey.Text.ToUpper(), tbKeyword.Text.ToUpper());
                    }
                }
                else
                {
                    if (textBoxKey.Text.Length != 25)
                    {
                        MessageBox.Show("The key must be 25 characters long.", "Message");
                    }
                    else
                    {
                        Match match = Regex.Match(textBoxKey.Text, @"^[A-Za-z]+$"); // checks that the key contains letters only

                        if (match.Success)
                        {
                            ADFGX(textBox.Text.ToUpper(), textBoxKey.Text.ToUpper(), tbKeyword.Text.ToUpper());
                        }
                        else
                        {
                            MessageBox.Show("The key must contain letters only.", "Message");
                        }
                    }
                }
            }
        }

        private void HillCheck()
        {
            Match match = Regex.Match(textBoxKey.Text, @"[0-9 ]+"); // the key must contain numbers and spaces only

            if (!match.Success)
            {
                MessageBox.Show("The key must only contain numbers (or letters) and spaces.", "Message");
            }
            else
            {
                List<string> inputValues = new List<string>();
                inputValues.AddRange(textBoxKey.Text.ToUpper().Split(' '));
                double result = Math.Sqrt(inputValues.Count);
                bool isSquare = result % 1 == 0; // checks if the number of values entered is a square number

                if (isSquare == false)
                {
                    MessageBox.Show("The matrix must be square - the number of values given in the key text box must be a square number.", "Message");
                }
                else
                {
                    List<int> matrixValues = new List<int>();
                    foreach (string value in inputValues)
                    {
                        Match isLetter = Regex.Match(value, @"[A-Za-z]");
                        if (isLetter.Success)
                        {
                            char letter = value[0];
                            int temp = (int)(Char.ToUpper(letter)) - 65; // converts letters to numbers
                            matrixValues.Add(temp);
                        }
                        else
                        {
                            matrixValues.Add(int.Parse(value));
                        }
                    }

                    Hill(new String(textBox.Text.Where(Char.IsLetter).ToArray()).ToUpper(), (int)result, matrixValues);
                }
            }
        }

        private void BifidCheck()
        {
            if (textBoxKey.Text.Length == 25)
            {
                Match match = Regex.Match(textBoxKey.Text.ToUpper(), @"[A-Z]+");
                if (match.Success)
                {
                    // square
                    if (BifidPeriodCheck() == true)
                    {
                        Bifid(textBox.Text.ToUpper().Replace(" ", ""), textBoxKey.Text.ToUpper(), int.Parse(tbKeyword.Text));
                    }
                }
                else
                {
                    MessageBox.Show("Not all characters entered in the key textbox are letters.", "Message");
                }
            }
            else
            {
                MessageBox.Show("Invalid input. The key textbox must contain 25 letters.", "Message");
            }
        }

        private bool BifidPeriodCheck()
        {
            int period = 0;
            return int.TryParse(tbKeyword.Text, out period);
        }


        // cell clicked in datagridview
        private void dgvCipherType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) // if the cell that is clicked on is not the header
            {
                // each time a new cipher type is clicked, the text box size resets & all the key textboxes are hidden by default because only the appropriate ones will be enabled for each cipher
                textBox.Size = new System.Drawing.Size(488, 124);
                HideKeyBoxes();
                lblCharCount.Location = new Point(453, 171); // the label location is reset

                if (dgvCipherType.CurrentCell.Value.ToString() == " Caesar Cipher")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the shift number (1-25) or enter 'b' for brute force.";
                    textBoxKey.MaxLength = 2; // the maximum length of input which will be valid is 2 characters
                    cipherType = "caesar";
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Atbash Cipher")
                {
                    textBox.Size = new System.Drawing.Size(488, 191); // the main textbox is resized to fill in the space where the key textbox(es) would be
                    lblCharCount.Location = new Point(453, 238);
                    cipherType = "atbash";
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Substitution Cipher")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the alphabet you want to use (the key).";
                    textBoxKey.MaxLength = 26; // the key needs to be 26 characters long
                    cipherType = "substitution";
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Affine Cipher")
                {
                    EnableABTextBoxes(); // 2 textboxes are needed for the key to be inputted as the key is made up of an A value and a B value

                    labelInstruction.Text = "'A' Value     'B' Value";
                    labelExtra.Text = "(leave A and B blank for brute force)";
                    labelExtra.Visible = true;
                    cipherType = "affine";
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Rail Fence Cipher")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the rail key (number of lines) or 'b' for brute force.";
                    textBoxKey.MaxLength = 5; // 5 characters is a reasonable maximum length for a rail fence key
                    cipherType = "railfence";

                    btnZigzagDirection.Visible = true;
                    btnZigzagDirection.BringToFront();

                    if (btnZigzagDirection.Text == "ZIGZAG UP") // will be set to 'DOWN' by default whenever rail fence cipher is selected
                    {
                        btnZigzagDirection_Click(btnZigzagDirection, EventArgs.Empty);
                    }
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Vigenère Cipher")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the key (a word/phrase) or the size of the key.";
                    textBoxKey.MaxLength = 30; // 30 characters should be long enough to accommodate for the key
                    cipherType = "vig";
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Beaufort Cipher")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the keyword.";
                    textBoxKey.MaxLength = 30;
                    cipherType = "beaufort";
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Columnar Transposition")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the keyword or the key size.";
                    textBoxKey.MaxLength = 30;
                    cipherType = "columnar";
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Playfair Cipher")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the keyword/key.";
                    textBoxKey.MaxLength = 30;
                    cipherType = "playfair";
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Polybius Square")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the keysquare.";
                    textBoxKey.MaxLength = 25; // the amount of letters that a Polybius Square key needs is 25
                    cipherType = "polybius";

                    btnOrder.Visible = true;
                    btnOrder.BringToFront();

                    if (btnOrder.Text == "(COL, ROW)")  // the button is initially set to (Row, Col)
                    {
                        btnOrder_Click(btnOrder, EventArgs.Empty);
                    }
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Autokey Cipher")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the keyword or key size.";
                    textBoxKey.MaxLength = 50;
                    cipherType = "autokey";
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " ADFGX Cipher" || dgvCipherType.CurrentCell.Value.ToString() == " ADFGVX Cipher" || dgvCipherType.CurrentCell.Value.ToString() == " Bifid Cipher") // all these ciphers require an additional key textbox
                {
                    EnableKeyTextBox();

                    // the size of the form increases to fit the extra textbox
                    this.Size = new Size(780, 506);
                    dgvCipherType.Size = new Size(240, 443);

                    if (makeBox == true) // if the extra key textbox has not yet been made
                    {
                        BoxForADFGVX();
                        makeBox = false; // prevents more textboxes being made
                    }
                    else // if the extra textbox and label have already been made, they are made visible
                    {
                        lblKeyword.Visible = true;
                        tbKeyword.Visible = true;
                    }

                    if (dgvCipherType.CurrentCell.Value.ToString() == " ADFGX Cipher")
                    {
                        cipherType = "adfgx";
                        lblKeyword.Text = "Enter the keyword.";
                        labelInstruction.Text = "Enter the alphabet you want to use (the key).";
                        textBoxKey.MaxLength = 25; // 25 characters need to be given to fill the 5x5 grid that makes up part of the key
                    }
                    else if (dgvCipherType.CurrentCell.Value.ToString() == " Bifid Cipher")
                    {
                        cipherType = "bifid";
                        labelInstruction.Text = "Enter the keysquare.";
                        textBoxKey.MaxLength = 25; // 25 characters need to be given to fill the 5x5 grid that makes up part of the key
                        lblKeyword.Text = "Enter the period.";

                        btnOrder.Visible = true;
                        btnOrder.BringToFront();

                        if (btnOrder.Text == "(COL, ROW)") // the button is initially set to say (Row, Col)
                        {
                            btnOrder_Click(btnOrder, EventArgs.Empty);
                        }
                    }
                    else
                    {
                        cipherType = "adfgvx";
                        lblKeyword.Text = "Enter the keyword.";
                        labelInstruction.Text = "Enter the key (must contain the full alphabet and digits 0-9).";
                        textBoxKey.MaxLength = 36; // 36 characters are needed to fill the 6x6 grid
                    }
                }
                else if (dgvCipherType.CurrentCell.Value.ToString() == " Hill Cipher")
                {
                    EnableKeyTextBox();

                    labelInstruction.Text = "Enter the key, separating each number (or letter) by a space.";
                    textBoxKey.MaxLength = 50;
                    cipherType = "hill";
                }
            }
        }


        // texbox shortcuts
        private void textBoxKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // allows the enter key to be pressed instead of the GO button
            {
                btnGo_Click(this, new EventArgs());
                e.Handled = true;
                e.SuppressKeyPress = true; // stops the error noise when enter is pressed
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

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            lblCharCount.Text = "Character Count: " + textBox.Text.Length + "     Letter Count: " + new String(textBox.Text.Where(Char.IsLetter).ToArray()).Length; // the character and letter counts are updated as the user types text into the box
        }


        // buttons
        private void radioButtonE_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonE.Checked == true)
            {
                enOrDe = 'e'; // encryption mode
            }
        }

        private void radioButtonD_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonD.Checked == true)
            {
                enOrDe = 'd'; // decryption mode
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            bool textEntered = TextEntered();

            bool keyEntered = false; // an error message will be shown if the user has not given a key.
            if (textBoxKey.Text != "")
            {
                keyEntered = true;
            }
            else
            {
                if (cipherType == "affine")
                {
                    keyEntered = true; // the Affine cipher uses different text boxes; setting this to 'true' allows the program to check other things later
                }
                else if (cipherType == "atbash")
                {
                    keyEntered = true;
                }
                else
                {
                    keyEntered = false;
                    MessageBox.Show("You haven't entered a key.", "Message");
                }
            }

            if (textEntered == true && keyEntered == true)
            {
                if (cipherType == "caesar") // if the user has selected 'Caesar Cipher'
                {
                    CaesarCheck();
                }

                else if (cipherType == "atbash")
                {
                    Atbash(textBox.Text);
                }

                else if (cipherType == "substitution")
                {
                    SubstitutionCheck();
                }

                else if (cipherType == "affine")
                {
                    AffineCheck();
                }

                else if (cipherType == "railfence")
                {
                    RailFenceCheck();
                }
                else if (cipherType == "vig")
                {
                    VigCheck();
                }
                else if (cipherType == "beaufort")
                {
                    BeaufortCheck();
                }
                else if (cipherType == "columnar")
                {
                    ColumnarCheck();
                }
                else if (cipherType == "playfair")
                {
                    PlayfairCheck();
                }
                else if (cipherType == "polybius")
                {
                    PolybiusCheck();
                }
                else if (cipherType == "autokey")
                {
                    AutokeyCheck();
                }
                else if (cipherType == "adfgvx" || cipherType == "adfgx")
                {
                    ADFGVXCheck();
                }
                else if (cipherType == "hill")
                {
                    HillCheck();
                }
                else if (cipherType == "bifid")
                {
                    BifidCheck();
                }
            }
        } // handles what happens when the go button is pressed

        private void btnMap_Click(object sender, EventArgs e)
        {
            MapLetters map = new MapLetters();
            map.Show();
        } // opens the form that allows the user to map letters in the alphabet

        private void btnReverse_Click(object sender, EventArgs e) // reverses the text in the main textbox
        {
            string input = textBox.Text;
            char[] inputarray = input.ToCharArray();
            Stack<char> stack = new Stack<char>();
            inputarray = stack.ReverseArray(inputarray);
            textBox.Text = new string(inputarray);
        }

        private void btnLettersOnly_Click(object sender, EventArgs e) // replaces the text in the main textbox with the same text, but without any characters that are not letters
        {
            textBox.Text = new String(textBox.Text.Where(Char.IsLetter).ToArray());
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.Show();
            this.Hide();
        } // opens the options form

        private void btnFreq_Click(object sender, EventArgs e)
        {
            FrequencyAnalysis freq = new FrequencyAnalysis();
            freq.Show();
        } // opens the frequency analysis form

        private void btnIofC_Click(object sender, EventArgs e)
        {
            // http://practicalcryptography.com/cryptanalysis/stochastic-searching/cryptanalysis-vigenere-cipher/
            // http://practicalcryptography.com/cryptanalysis/text-characterisation/index-coincidence/

            IndexOfCoincidence iOfC = new IndexOfCoincidence();
            iOfC.Show();
        } // opens the index of coincidence form

        private void btnFindReplace_Click(object sender, EventArgs e)
        {
            FindReplace fr = new FindReplace();
            fr.Show();
        } // opens the find & replace form


        // help button
        private bool isHovering = false;

        private void ButtonHelp_MouseDown(object sender, MouseEventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpClick;
        }

        private void ButtonHelp_MouseHover(object sender, EventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpHover;

            timerHelp.Enabled = true;
            isHovering = true;
        }

        private void ButtonHelp_MouseLeave(object sender, EventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpNormal;
            isHovering = false;
        }

        private void ButtonHelp_MouseUp(object sender, MouseEventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpNormal;
        }

        private void ButtonHelp_MouseClick(object sender, MouseEventArgs e)
        {
            btnHelp.BackgroundImage = Properties.Resources.helpClick;

            if (cipherType == "caesar")
            {
                MessageBox.Show("The Caesar cipher is a type of substitution cipher in which each letter in the plaintext is 'shifted'" +
                    " a certain number of places down the alphabet. For example, with a shift of 1, A would be replaced by B, " +
                    "B would become C, and so on.\r\n'Brute force' tries all possibilities in an attempt to quickly find the solution without knowing the key." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter " +
                    "a shift value between 1 and 25 in the smaller textbox, or input\t    'b' in the textbox to use brute force.\r\n3. Select 'Encrypt' " +
                    "or 'Decrypt' accordingly.\r\n4. Press GO to see the encrypted/decrypted message.", "The Caesar Cipher");
            }
            else if (cipherType == "atbash")
            {
                MessageBox.Show("The Atbash cipher is a type of monoalphabetic substitution cipher that was originally " +
                    "used to encode the Hebrew alphabet, but can also be used with English letters. The cipher " +
                    "alphabet is formed by reversing the normal alphabet so that the first letter becomes the " +
                "last letter, the second letter becomes the second to last letter etc.\nThe Atbash cipher is not very secure because" +
                " even if someone did not know the ciphertext was encrypted using the Atbash cipher, they could still break it if they assumed it was a" +
                " substitution cipher and used frequency analysis to find the plaintext characters. It can " +
                "also be decrypted using the Affine cipher with an ‘a’ value of 25 and a ‘b’ value of 25." +
                "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Press GO to see the encrypted/decrypted message.", "The Atbash Cipher");
            }
            else if (cipherType == "substitution")
            {
                MessageBox.Show("The simple substitution cipher consists of substituting every plaintext character for a different " +
                    "ciphertext character. It differs from the Caesar cipher in that the cipher alphabet is not simply the alphabet shifted," +
                    " it is completely jumbled. Keys for the simple substitution cipher usually consist of 26 letters." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter " +
                    "the key in the smaller textbox - this should be a string of length     26 containing letters and/or hyphens (-) " +
                    "in place of letters that are           unknown.\r\n3. Press GO to see the encrypted/decrypted message.", "The Substitution Cipher");
            }
            else if (cipherType == "affine")
            {
                MessageBox.Show("The Affine cipher uses modular arithmetic to transform the integer that each plaintext letter corresponds" +
                    " to into another integer that corresponds to a ciphertext letter.\nThe encryption function for a single letter is E(x) = (ax + b) mod m," +
                    " where m is the alphabet size and a and b are the keys of the cipher. The value a must be chosen such that a and m are coprime." +
                    " If the alphabet is 26 characters long, then a only has 12 possible values, and b has 26 values, so there are only 312 possible keys." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox. If you would like\t    to use brute force, skip steps 2 and 3.\r\n2. Enter " +
                    "an 'A' value - this could be any one of the following numbers:\t    1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25. " +
                    "\r\n3. Enter a 'B' value - this should be between 0 and 25.\r\n4. Select 'Encrypt' or 'Decrypt' accordingly.\r\n5. Press GO to see the encrypted/decrypted message.", "The Affine Cipher");
            }
            else if (cipherType == "railfence")
            {
                MessageBox.Show("The Rail Fence cipher is a form of transposition cipher that gets its name from the way in which it is encoded. " +
                    "In the rail fence cipher, the plaintext is written downwards on successive 'rails' of an imaginary fence, then moving up when we get to the bottom." +
                    " The message is then read off in rows. See https://en.wikipedia.org/wiki/Transposition_cipher for more information." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox. If you would like\t    to use brute force, skip step 2 and enter 'b' in the smaller textbox.\r\n2. Enter " +
                    "a rail key - this should be an integer.\r\n3. Choose a 'zigzag' direction by pressing the button (the setting\t\t    displayed on the button is the setting that will be used). The default\t    setting is 'Zigzag Down'." +
                    "\r\n4. Select 'Encrypt' or 'Decrypt' accordingly.\r\n5. Press GO to see the encrypted/decrypted message.", "The Rail Fence Cipher");
            }
            else if (cipherType == "vig")
            {
                MessageBox.Show("The Vigenère cipher is a method of encrypting alphabetic text by using a series of interwoven Caesar ciphers based on the" +
                    "letters of a keyword. It is a form of polyalphabetic substitution.\nTo encrypt, a table of alphabets can be used. It consists of the alphabet written out " +
                    "26 times in different rows, each alphabet shifted cyclically to the left compared to the previous alphabet, corresponding to" +
                    " the 26 possible Caesar ciphers. At different points in the encryption process, the cipher uses a different alphabet from one of the rows. " +
                    "The alphabet used at each point depends on a repeating keyword.\nSee https://en.wikipedia.org/wiki/Vigen%C3%A8re_cipher for more information." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the key, or if you don't know the key, try to find the key length\t    using the Index of Coincidence " +
                    "tool, then enter the key size in the key\t    textbox." +
                    "\r\n3. Select 'Encrypt' or 'Decrypt' accordingly.\r\n4. Press GO to see the encrypted/decrypted message.", "The Vigenère Cipher");
            }
            else if (cipherType == "beaufort")
            {
                MessageBox.Show("The Beaufort cipher is a polyalphabetic substitution cipher similar to the Vigenère cipher. The difference is that the Beaufort cipher" +
                    " encrypts messages using a slightly different algorithm. Encryption is carried out using the same tabula recta as the Vigenère cipher and a chosen keyword." +
                    " The keyword is written repeatedly above the plaintext. Then, the column corresponding to the plaintext letter to encrypt is found. The corresponding key " +
                    "letter is found in the column and the ciphertext letter is the row heading." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the key - this should be a string of letters." +
                    "\r\n3. Select 'Encrypt' or 'Decrypt' accordingly.\r\n4. Press GO to see the encrypted/decrypted message.", "The Beaufort Cipher");
            }
            else if (cipherType == "columnar")
            {
                MessageBox.Show("The Columnar Transposition cipher is a transposition cipher that follows a simple rule for mixing up the characters in the plaintext to form the ciphertext." +
                    " The message is written out in rows of a fixed length, and then read out again column by column; however, the columns are chosen in some scrambled order. " +
                    "Both the width of the rows and the permutation of the columns are usually defined by a keyword. For example, the keyword ZEBRAS is of length 6 (so the rows are of length " +
                    "6), and the permutation is defined by the alphabetical order of the letters in the keyword. In this case, the order would be '6 3 2 4 1 5'." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the key - this should be a string of letters. Alternatively, enter\t    the key size - this will open a form allowing you to manipulate the\t    columns after you press GO." +
                    "\r\n3. Select 'Encrypt' or 'Decrypt' accordingly.\r\n4. Press GO to see the encrypted/decrypted message.", "The Columnar Transposition Cipher");
            }
            else if (cipherType == "playfair")
            {
                MessageBox.Show("The Playfair cipher was the first practical digraph substitution cipher. The technique encrypts pairs of letters (digraphs), instead of single letters as in" +
                " the simple substitution cipher. The Playfair is significantly harder to break since the frequency analysis used for simple substitution ciphers does not work with it." +
                "\nSee http://practicalcryptography.com/ciphers/playfair-cipher/ for more information." +
                "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the key - this should be a string of letters." +
                    "\r\n3. Select 'Encrypt' or 'Decrypt' accordingly.\r\n4. Press GO to see the encrypted/decrypted message.", "The Playfair Cipher");
            }
            else if (cipherType == "polybius")
            {
                MessageBox.Show("The Polybius Square uses a 5 x 5 grid containing the alphabet (omitting/combining 'j' with 'i'). Each column and row is labelled 1-5 and each letter in the grid" +
                    " is represented by its co-ordinates. The co-ordinates can be read either way round but it must be consistent throughout e.g. (column, row) or (row, column). The cipher " +
                    "is not particularly secure because each plaintext letter is represented by the same pair of digits every time, and thus an intercept is susceptible to frequency analysis." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the key - this should be a string of 25 letters." +
                    "\r\n3. Select 'Encrypt' or 'Decrypt' accordingly.\r\n4. Press GO to see the encrypted/decrypted message.", "The Polybius Square");
            }
            else if (cipherType == "autokey")
            {
                MessageBox.Show("The Autokey cipher is a polyalphabetic substitution cipher. It uses a keyword and the plaintext to encrypt. " +
                    "The term autokey refers to any cipher where the key is based on the original plaintext. To encrypt a message, the keyword " +
                    "is written above the plaintext, then after the keyword is written, the plaintext is written. This long string is the key and " +
                    "should be the same length as the plaintext. The same method for encryption as the Vigenère cipher is then used." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the key - this should be a string of letters." +
                    "\r\n3. Select 'Encrypt' or 'Decrypt' accordingly.\r\n4. Press GO to see the encrypted/decrypted message.", "The Autokey Cipher");
            }
            else if (cipherType == "adfgx")
            {
                MessageBox.Show("The ADFGX cipher is a fractionating transposition cipher which combines a modified Polybius square with a single columnar transposition. " +
                    "A 5 x 5 grid containing the alphabet (omitting/combining 'j' with 'i') with column and row headings 'ADFGX' is used to encrypt messages by " +
                    "reading off the row and column heading of each plaintext letter, so that each letter in the plaintext forms two intermediate letters. " +
                    "A columnar transposition is then performed on the new letters using the keyword provided to form the final ciphertext." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the keysquare - this should be a string of 25 letters." +
                    "\r\n3. Enter a keyword. This should be a string of letters.\r\n4. Select 'Encrypt' or 'Decrypt' accordingly.\r\n5. Press GO to see the encrypted/decrypted message.", "The ADFGX Cipher");
            }
            else if (cipherType == "adfgvx")
            {
                MessageBox.Show("The ADFGVX cipher is a fractionating transposition cipher that combines a modified Polybius square with a single columnar transposition. " +
                    "A 6 x 6 grid containing all the letters of the alphabet and the digits 0-9 in a random order is used to encrypt " +
                    "messages. The columns and rows are labelled ADFGVX and each plaintext letter is converted into ciphertext by reading the column " +
                    "and row headings of the plaintext letter in the grid (typically in the order (row, column). Then, a keyword is used and a columnar " +
                    "transposition is applied to produce the final ciphertext." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the keysquare - this should be a string containing 26 letters\t    and the digits 0-9." +
                    "\r\n3. Enter a keyword. This should be a string of letters.\r\n4. Select 'Encrypt' or 'Decrypt' accordingly.\r\n5. Press GO to see the encrypted/decrypted message.", "The ADFGVX Cipher");
            }
            else if (cipherType == "hill")
            {
                MessageBox.Show("The Hill cipher encrypts messages using matrix multiplication. The key is a square n x n matrix (generally 3 x 3) containing " +
                    "values between 0-25 chosen by the user. The plaintext is split into many n-letter chunks (if the length of the text is not a multiple " +
                    "of n, extra letters can be appended to make it work) and each chunk is converted into a n x 1 vector using the numerical values of the " +
                    "letters (where A = 0, B = 1 etc.). Each plaintext vector is multiplied by the key matrix to form ciphertext vectors which are converted " +
                    "to letters.\r\n\r\nTo enter the key matrix into this program, type in each number (or letter) from the matrix row by row, separating each value with a space." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the key - this should be a string of either integers OR letters,\t    each separated by a space. There should be a square number of\t    letters/integers." +
                    "\r\n3. Select 'Encrypt' or 'Decrypt' accordingly.\r\n4. Press GO to see the encrypted/decrypted message.", "The Hill Cipher");
            }
            else if (cipherType == "bifid")
            {
                MessageBox.Show("The Bifid cipher works by using a 25 letter keysquare to find the 'coordinates' of each plaintext letter. These coordinates " +
                    "are then written on top of each other so that all of the row values are in one line and all the column values are in a line beneath the row " +
                    "values. The numbers are then grouped into blocks of a certain size (this is called the period, and forms part of the key). Then, each block is " +
                    "read from left to right; the top (row) block is read first, then the (col) block underneath it is read, then the next row block is read and so on. " +
                    "The string is then converted into ciphertext using the original keysquare." +
                    "\r\n\r\nInstructions:\r\n1. Enter the plaintext/ciphertext in the main textbox.\r\n2. Enter the keysquare - this should be a string of 25 letters." +
                    "\r\n3. Enter the period. This should be an integer.\r\n4. Select 'Encrypt' or 'Decrypt' accordingly.\r\n5. Press GO to see the encrypted/decrypted message.", "The Bifid Cipher");
            }
        }

        private void helpTimer_Tick(object sender, EventArgs e)
        {
            int before;
            int after;

            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                before = 550;
                after = 750;
            }
            else
            {
                before = btnHelp.Location.X - 185;
                after = before + 200;
            }

            int step = 20; // move label by 20 pixels per tick
            if (isHovering == true && (labelHelp.Location.X) > (before))
            {
                //Move from right to left by decreasing x
                labelHelp.Location = new Point(labelHelp.Location.X - step, labelHelp.Location.Y);
            }

            else if (isHovering == false)
            {
                if ((labelHelp.Location.X) < (after))
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
    }
}
