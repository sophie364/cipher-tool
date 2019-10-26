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
    public partial class ColTransposition : Form
    {
        DataGridView colTransDGV;
        private int colIndexFromMouseDown; // stores the index of the column that is clicked on
        private int colDispIndexFromMouseDown; // stores the display index of the column that is clicked on
        DataGridViewColumn col;

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
            textBox.Font = myFont;
        } // the font of the controls is set

        private void SetupDGV()
        {
            colTransDGV.Location = new Point(12, 12);
            colTransDGV.Size = new Size(610, 294);
            colTransDGV.CellMouseEnter += new DataGridViewCellEventHandler(colTransDGV_CellMouseEnter);
            colTransDGV.MouseClick += new MouseEventHandler(colTransDGV_MouseClick);
            colTransDGV.DragEnter += new DragEventHandler(colTransDGV_DragEnter);
            colTransDGV.DragDrop += new DragEventHandler(colTransDGV_DragDrop);
            colTransDGV.AllowDrop = true;
            colTransDGV.AllowUserToOrderColumns = true;
            colTransDGV.CellMouseMove += new DataGridViewCellMouseEventHandler(colTransDGV_CellMouseMove);
            colTransDGV.CellMouseLeave += new DataGridViewCellEventHandler(colTransDGV_CellMouseLeave);
            Controls.Add(colTransDGV);
            colTransDGV.TabIndex = 1;
            colTransDGV.ShowCellToolTips = false;

            // Make every column not sortable.
            for (int i = 0; i < colTransDGV.Columns.Count; i++)
            {
                colTransDGV.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Set selection mode to Column.
            colTransDGV.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
        } // the datagridview is set up

        public ColTransposition(DataGridView ctDGV, char[,] tempGrid)
        {
            InitializeComponent();

            colTransDGV = ctDGV;
            SetFont();
            SetupDGV();

            displayIndexes = new int[colTransDGV.Columns.Count];
            grid = tempGrid; // 2D character array

            GetPlaintext();
        }

        private char[,] grid;
        private int[] displayIndexes;

        private void GetPlaintext() // reads off the rows and converts it into one string which is printed in the text box
        {
            string output = "";
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    output += grid[i, j];
                }
            }

            textBox.Text = output;
        }

        private void colTransDGV_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1)
            {
                toolTip1.SetToolTip(this.colTransDGV, "Drag and drop columns to rearrange them.");
            }
            else
            {
                toolTip1.Hide(this.colTransDGV);
            }
        }

        private void colTransDGV_MouseClick(object sender, MouseEventArgs e)
        {
            if (colTransDGV.SelectedColumns.Count == 1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    col = colTransDGV.SelectedColumns[0];
                    colIndexFromMouseDown = colTransDGV.SelectedColumns[0].Index;
                    colDispIndexFromMouseDown = colTransDGV.SelectedColumns[0].DisplayIndex;
                    colTransDGV.DoDragDrop(col, DragDropEffects.Move);

                }
            }
        }

        private void colTransDGV_DragEnter(object sender, DragEventArgs e)
        {
            if (colTransDGV.SelectedColumns.Count > 0)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void colTransDGV_DragDrop(object sender, DragEventArgs e)
        {
            int colIndexOfItemUnderMouseToDrop;
            int colDispIndexOfItemUnderMouseToDrop;
            Point clientPoint = colTransDGV.PointToClient(new Point(e.X, e.Y));
            colIndexOfItemUnderMouseToDrop = colTransDGV.HitTest(clientPoint.X, clientPoint.Y).ColumnIndex;
            colDispIndexOfItemUnderMouseToDrop = colTransDGV.Columns[colIndexOfItemUnderMouseToDrop].DisplayIndex;

            if (e.Effect == DragDropEffects.Move)
            {
                colTransDGV.Columns[colIndexFromMouseDown].DisplayIndex = colDispIndexOfItemUnderMouseToDrop;
                colTransDGV.Columns[colIndexOfItemUnderMouseToDrop].DisplayIndex = colDispIndexFromMouseDown;
                colTransDGV.ClearSelection();

                char[,] newGrid = new char[grid.GetLength(0), grid.GetLength(1)];
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        if (j == colDispIndexOfItemUnderMouseToDrop)
                        {
                            newGrid[i, colDispIndexOfItemUnderMouseToDrop] = grid[i, colDispIndexFromMouseDown];
                        }
                        else if (j == colDispIndexFromMouseDown)
                        {
                            newGrid[i, colDispIndexFromMouseDown] = grid[i, colDispIndexOfItemUnderMouseToDrop];
                        }
                        else
                        {
                            newGrid[i, j] = grid[i, j];
                        }
                    }
                }

                grid = newGrid;
                GetPlaintext();
            }
        }

        private void colTransDGV_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1) //column header
            {
                colTransDGV.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.Gray;
            }
        }

        private void colTransDGV_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex != -1) //column header
            {
                colTransDGV.Columns[e.ColumnIndex].HeaderCell.Style.BackColor = Color.DimGray;
            }
        }

        private void ColTransposition_Load(object sender, EventArgs e)
        {
            // Clear all selected cells or rows in the DGV.
            colTransDGV.ClearSelection();
        }
    }
}
