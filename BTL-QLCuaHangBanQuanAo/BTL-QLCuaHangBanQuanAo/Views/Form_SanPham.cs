using BTL_QLCuaHangBanQuanAo.Model.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLCuaHangBanQuanAo.Views
{
    public partial class Form_SanPham : Form
    {
        string connectionStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + System.IO.Directory.GetCurrentDirectory().ToString() + "\\DataBase\\Data.mdf;Integrated Security=True";

        DataProvider sqlConnect = new DataProvider();
        string img = "";
        public Form_SanPham()
        {
            InitializeComponent();
        }

        private void Form_SanPham_Load(object sender, EventArgs e)
        {
            string Query = "SELECT * FROM SanPham";
            DataTable data = sqlConnect.ExecuteQuery(Query);
            dgvData.DataSource = data;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] transfer = null;
                FileStream file = new FileStream(img, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(file);
                transfer = reader.ReadBytes((int)file.Length);

                string sql = $"INSERT INTO SanPham (MaQuanAo, TenQuanAo,SoLuong,DonGiaBan,DonGiaNhap,MaLoai,MaCo,MaChatLieu,MaMau,MaDoiTuong,MaMua,MaNSX,Img) Values('{txtMaQA.Text}',N'{txtTen.Text}',{txtSL.Text},{txtDonGiaBan.Text},{txtDonGiaNhap.Text},'{txtMaLoai.Text}','{txtMaCo.Text}','{txtMaCL.Text}','{txtMaMau.Text}','{txtMaDoiTuong.Text}','{txtMaMua.Text}','{txtMaNSX.Text}',@Img)";
                sqlConnect.ExecuteNonQuery(sql,transfer);
                dgvData.DataSource = sqlConnect.ExecuteQuery("SELECT * FROM SanPham");
                MessageBox.Show("Thêm Thành Công");
            }
            catch
            {
                MessageBox.Show("Thêm Thất Bại");
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PicSanPham.Image = Image.FromFile(ofd.FileName);
                img = ofd.FileName;
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow data = dgvData.Rows[e.RowIndex];
            //SoCCCD = data.Cells[0].Value.ToString();
            txtMaQA.Text = data.Cells[0].Value.ToString();
            txtTen.Text = data.Cells[1].Value.ToString();
            txtSL.Text = data.Cells[2].Value.ToString();
            txtDonGiaBan.Text = data.Cells[3].Value.ToString();
            txtDonGiaNhap.Text = data.Cells[4].Value.ToString();
            txtMaLoai.Text = data.Cells[5].Value.ToString();
            txtMaCo.Text = data.Cells[6].Value.ToString();
            txtMaCL.Text = data.Cells[7].Value.ToString();
            txtMaMau.Text = data.Cells[8].Value.ToString();
            txtMaDoiTuong.Text = data.Cells[9].Value.ToString();
            txtMaMua.Text = data.Cells[10].Value.ToString();
            txtMaNSX.Text = data.Cells[11].Value.ToString();
            //MessageBox.Show(data.Cells[12].Value.ToString())
            if (data.Cells[12].Value.ToString() != "")
            {
                try
                {
                    byte[] bytes = (byte[])(data.Cells[12].Value);
                    MemoryStream ms = new MemoryStream(bytes);
                    PicSanPham.Image = Image.FromStream(ms);
                }
                catch
                {

                }
                PicSanPham.Image = Image.FromFile(data.Cells[12].Value.ToString());
                //PicSanPham.ImageLocation = data.Cells[12].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $"DELETE FROM SanPham WHERE MaQuanAo = '{txtMaQA.Text}'";
                sqlConnect.ExecuteNonQuery(sql);
                dgvData.DataSource = sqlConnect.ExecuteQuery("SELECT * FROM SanPham");
                MessageBox.Show("Xoá Thành Công");
            }
            catch
            {
                MessageBox.Show("Xoá Thất Bại");
            }
        }
    }
}
