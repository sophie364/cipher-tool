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
    public partial class IofCOutput : Form
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

            myFont = new Font(fonts.Families[0], 14.0F);
            textBox.Font = myFont;

            myFont = new Font(fonts.Families[0], 12.5F);
            buttonSort.Font = myFont;
        }

        public IofCOutput(string byPeriod, string byAVG)
        {
            InitializeComponent();

            SetFont();

            textBox.Text = byPeriod; // displays in order of period by default

            sortByPeriod = byPeriod;
            sortByAVG = byAVG;
        }

        string sortByPeriod;
        string sortByAVG;

        private void buttonSort_Click(object sender, EventArgs e)
        {
            if (buttonSort.Text == "SORT BY AVERAGE I.C.")
            {
                textBox.Text = sortByAVG;
                buttonSort.Text = "SORT BY PERIOD";
                buttonSort.BackColor = Color.DeepPink;
                buttonSort.FlatAppearance.BorderColor = Color.DeepPink;
            }
            else if (buttonSort.Text == "SORT BY PERIOD")
            {
                textBox.Text = sortByPeriod;
                buttonSort.Text = "SORT BY AVERAGE I.C.";
                buttonSort.BackColor = Color.DodgerBlue;
                buttonSort.FlatAppearance.BorderColor = Color.DodgerBlue;
            }
        }
    }
}
