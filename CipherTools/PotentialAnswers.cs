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
    public partial class PotentialAnswers : Form
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

            myFont = new Font(fonts.Families[0], 14.0F);
            textBoxOut.Font = myFont;
        }

        public PotentialAnswers(List<string> potentialAnswers, List<int> comWordCount)
        {
            InitializeComponent();
            SetFont();

            string[] solutions = potentialAnswers.ToArray();
            int[] counts = comWordCount.ToArray();

            Array.Sort(counts, solutions); // sorts the count array into ascending order and sorts the corresponding solution

            // reversing the array using the stack
            Stack<string> stack = new Stack<string>();
            solutions = stack.ReverseArray(solutions);
            
            foreach (var solution in solutions)
            {
                textBoxOut.Text += solution + "\r\n\r\n";
            }
        }

        private void textBoxOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A) // Ctrl + A shortcut for select all
            {
                textBoxOut.SelectAll();

                // prevents 'ding' noise when Ctrl + A is pressed
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
