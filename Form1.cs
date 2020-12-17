using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsDataGridDraw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // データグリッドビューに2行分追加
            dataGridView1.Rows.Add();
            dataGridView1.Rows.Add();

            dataGridView1.Rows[0].Cells[0].Value = "1行目";
            dataGridView1.Rows[1].Cells[0].Value = "2行目";

            dataGridView1.Rows[0].Cells[0].ReadOnly = true;
            dataGridView1.Rows[1].Cells[0].ReadOnly = true;
          
            dataGridView1.Rows[0].Cells[1].Value = "文字ですーーーーーーーーーーーーー";

            dataGridView1.Rows[1].Cells[1].Value = "文字ですーーーーーーーーーーーーー";

            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //ヘッダー以外のセルで、背景を描画する時
            if (e.ColumnIndex == 1 && e.RowIndex >= 0 &&
                (e.PaintParts & DataGridViewPaintParts.Background) ==
                    DataGridViewPaintParts.Background)
            {
                //選択されているか調べ、色を決定する
                //bColor1が開始色、bColor2が終了色
                Color bColor1, bColor2;
                if ((e.PaintParts & DataGridViewPaintParts.SelectionBackground) ==
                        DataGridViewPaintParts.SelectionBackground &&
                    (e.State & DataGridViewElementStates.Selected) ==
                        DataGridViewElementStates.Selected)
                {
                    bColor1 = e.CellStyle.SelectionBackColor;
                    bColor2 = Color.Black;
                }
                else
                {
                    bColor1 = e.CellStyle.BackColor;
                    bColor2 = Color.LemonChiffon;
                }

                using (System.Drawing.Drawing2D.LinearGradientBrush b =
                    new System.Drawing.Drawing2D.LinearGradientBrush(
                    e.CellBounds, bColor1, bColor2,
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
                {
                    //セルを塗りつぶす
                    e.Graphics.FillRectangle(b, e.CellBounds);
                }

                //e.Graphics.DrawString("テスト", new Font("Tahoma", 10.5f), Brushes.Black, 5, 5);

                ////背景以外が描画されるようにする
                DataGridViewPaintParts paintParts =
                   e.PaintParts & ~DataGridViewPaintParts.Background;
                //セルを描画する
                e.Paint(e.ClipBounds, paintParts);


                e.Graphics.DrawString("オーバーレイ", e.CellStyle.Font,
                    Brushes.Crimson, e.CellBounds.X + 20,
                    e.CellBounds.Y + 2, StringFormat.GenericDefault);

                //描画が完了したことを知らせる
                e.Handled = true;
            }

        }
    }
}
