using BTL_QLCuaHangBanQuanAo.Model.Class;
using BTL_QLCuaHangBanQuanAo.Model.Database;
using BTL_QLCuaHangBanQuanAo.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLCuaHangBanQuanAo
{
    public partial class Form1 : Form
    {
		private Form activeForm;

		private void LoadMutilForm(Form form)
		{
			if (this.panelMain.Controls.Count > 0)
            {
				this.panelMain.Controls.RemoveAt(0);
            }
			activeForm = form;
			form.TopLevel = false;
			form.FormBorderStyle = FormBorderStyle.None;
			form.Dock = DockStyle.Fill;
			this.panelMain.Controls.Add(form);
			this.panelMain.Tag = form;
			form.BringToFront();
			form.Show();
			lblTieuDe.Text = form.Text;
		}

		public Form1()
        {
            InitializeComponent();

			if (StaticData.datatable != null)
			{
				lblTenUser.Text = StaticData.datatable.Rows[0]["TenDangNhap"].ToString();
                lblEmail.Text = StaticData.datatable.Rows[0]["Email"].ToString();
                lblQuyen.Text = StaticData.datatable.Rows[0]["Quyen"].ToString();
            }
		}

        private void btnBanHang_Click(object sender, EventArgs e)
        {
			LoadMutilForm(new Form_BanHang());
        }

        private void btnSp_Click(object sender, EventArgs e)
        {
			LoadMutilForm(new Form_SanPham());
		}

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
			LoadMutilForm(new Form_NhanVien());
		}

		private void pictureBox1_Click(object sender, EventArgs e)
        {
			LoadMutilForm(new Form_HienThiMatHang());
		}

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
			LoadMutilForm(new Form_KhachHang());
        }

		private void btnCongViec_Click(object sender, EventArgs e)
		{
			LoadMutilForm(new Form_CongViec());
		}

		private void btnTheLoai_Click(object sender, EventArgs e)
		{
            LoadMutilForm(new Form_TheLoai());
        }

		private void btnCo_Click(object sender, EventArgs e)
		{
            LoadMutilForm(new Form_Co());
        }
		
		private void btnChatLieu_Click(object sender, EventArgs e)
		{
            LoadMutilForm(new Form_ChatLieu());
        }

		private void btnMau_Click(object sender, EventArgs e)
		{
            LoadMutilForm(new Form_Mau());
        }

		private void btnDoiTuong_Click(object sender, EventArgs e)
		{
            LoadMutilForm(new Form_DoiTuong());
        }

		private void btnMua_Click(object sender, EventArgs e)
		{
            LoadMutilForm(new Form_Mua());
        }

		private void btnNoiSanXuat_Click(object sender, EventArgs e)
		{
            LoadMutilForm(new Form_NoiSanXuat());
        }

		private void Form1_Load(object sender, EventArgs e)
		{

		}

        private void btnLoad_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (flowLayoutPanel1.Controls.Count == 0)
            {
                string query = "select * from SanPham";
                System.Data.DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dataRow = dt.Rows[i];

                    Panel panel_FL = new Panel();
                    panel_FL.Size = new Size(80, 200);

                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(80, 100);
                    pb.Image = new Bitmap(dataRow[6].ToString());
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;

                    Label lbl = new Label();
                    lbl.Text = dataRow[1].ToString();
                    lbl.Location = new System.Drawing.Point(58, 149);

                    panel_FL.Controls.Add(pb);
                    panel_FL.Controls.Add(lbl);

                    flowLayoutPanel1.Controls.Add(panel_FL);
                }

            }
        }
    }
}
