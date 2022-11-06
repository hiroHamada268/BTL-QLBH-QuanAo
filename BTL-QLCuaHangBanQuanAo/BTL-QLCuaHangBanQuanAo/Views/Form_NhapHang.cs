using BTL_QLCuaHangBanQuanAo.Model.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BTL_QLCuaHangBanQuanAo.Views
{
    public partial class Form_NhapHang : Form
    {
        DataProvider data = new DataProvider();
        DataTable table = new DataTable();
        float Tien = 0;
        float OldTien = 0;
        int index = -1;
        List<string> listMaQA = new List<string>();
        public Form_NhapHang()
        {
            InitializeComponent();
        }

        private void Form_NhapHang_Load(object sender, EventArgs e)
        {

            //table.Columns.Add(new DataColumn("TongTien"));

            StartPr();
        }

        void StartPr()
        {
            Tien = 0;
            OldTien = 0;
            listMaQA.Clear();
            index = -1;
            table.Rows.Clear();

            string qr = $"select distinct MaNV from NhanVien";
            data.FillCBO(qr, cboMaNV, "MaNV");
            qr = $"select distinct MaNCC from NhaCungCap";
            data.FillCBO(qr, cboMaNCC, "MaNCC");
            qr = $"select distinct MaQuanAo from SanPham";
            data.FillCBO(qr, cboMaQA, "MaQuanAo");

            table.Columns.Add(new DataColumn("MaQuanAo"));
            table.Columns.Add(new DataColumn("SoLuong"));
            table.Columns.Add(new DataColumn("DonGia"));
            table.Columns.Add(new DataColumn("GiamGia"));
            table.Columns.Add(new DataColumn("ThanhTien"));
            string sql = $"select distinct SoHDN from HoaDonNhap";
            DataTable dttable = data.ExecuteQuery(sql);
            DataRow dataRow = dttable.Rows[dttable.Rows.Count - 1];
            string SOHDN = dataRow["SoHDN"].ToString();
            txtManhap.Text = (int.Parse(SOHDN) + 1).ToString();
        }

        private void cboMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNV.SelectedIndex != -1)
            {
                try
                {
                    data.FillTxt($"SELECT * from NhanVien WHERE MaNV = '{cboMaNV.SelectedValue.ToString()}'", txtTenNV, "TenNV");
                }
                catch
                {
                    //MessageBox.Show("looixi");
                }

            }
            else
            {
                txtTenNV.Text = "";
            }
        }

        private void cboMaQA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaQA.SelectedIndex != -1)
            {
                try
                {
                    data.FillTxt($"SELECT * from SanPham WHERE MaQuanAo = '{cboMaQA.SelectedValue.ToString()}'", txtTenSP, "TenQuanAo");
                }
                catch
                {
                    //MessageBox.Show("looixi");
                }

            }
            else
            {
                txtTenSP.Text = "";
            }
        }

        private void cboMaNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaNCC.SelectedIndex != -1)
            {
                try
                {
                    data.FillTxt($"SELECT * from NhaCungCap WHERE MaNCC = '{cboMaNCC.SelectedValue.ToString()}'", txtTenNCC, "TenNCC");
                }
                catch
                {
                    //MessageBox.Show("looixi");
                }

            }
            else
            {
                txtTenNCC.Text = "";
            }
        }

        private void btnThemlai_Click(object sender, EventArgs e)
        {
            if (listMaQA.Contains(cboMaQA.SelectedValue.ToString()))
            {
                MessageBox.Show("Sản phẩm này đã có trong đơn hàng !\nNếu bạn muốn thêm vui lòng chọn sửa !");
                return;
            }
            DataRow row;
            row = table.NewRow();
            row["MaQuanAo"] = cboMaQA.SelectedValue.ToString();
            row["SoLuong"] = txtSL.Text;
            row["DonGia"] = txtDongia.Text;
            row["GiamGia"] = txtGiamGia.Text;
            row["ThanhTien"] = txtThanhTien.Text;
            Tien += float.Parse(txtThanhTien.Text);
            txtTongtien.Text = Tien.ToString();
            table.Rows.Add(row);
            dgvdata.DataSource = table;
            listMaQA.Add(cboMaQA.SelectedValue.ToString());
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtThanhTien.Text = (float.Parse(txtSL.Text) * float.Parse(txtDongia.Text) * (1 - (float.Parse(txtGiamGia.Text) / 100))).ToString();
            }
            catch
            {

            }
            if (txtSL.Text == "" || txtDongia.Text == "" || txtGiamGia.Text == "")
            {
                txtThanhTien.Text = "";
            }
        }

        private void txtDongia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtThanhTien.Text = (float.Parse(txtSL.Text) * float.Parse(txtDongia.Text) * (1 - (float.Parse(txtGiamGia.Text) / 100))).ToString();
            }
            catch
            {

            }
            if (txtSL.Text == "" || txtDongia.Text == "" || txtGiamGia.Text == "")
            {
                txtThanhTien.Text = "";
            }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtThanhTien.Text = (float.Parse(txtSL.Text) * float.Parse(txtDongia.Text) * (1 - (float.Parse(txtGiamGia.Text) / 100))).ToString();
            }
            catch
            {

            }
            if (txtSL.Text == "" || txtDongia.Text == "" || txtGiamGia.Text == "")
            {
                txtThanhTien.Text = "";
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Tien -= OldTien;
            dgvdata.Rows.RemoveAt(index);
            btnXoa.Enabled = false;
        }

        private void dgvdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow data = dgvdata.Rows[e.RowIndex];
            index = e.RowIndex;
            //MaQA = data.Cells[0].Value.ToString();
            cboMaQA.SelectedValue = data.Cells[0].Value.ToString();
            txtSL.Text = data.Cells[1].Value.ToString();
            txtDongia.Text = data.Cells[2].Value.ToString();
            txtGiamGia.Text = data.Cells[3].Value.ToString();
            txtThanhTien.Text = data.Cells[4].Value.ToString();
            OldTien = float.Parse(data.Cells[4].Value.ToString());
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow data = dgvdata.Rows[index];
            data.Cells[0].Value = cboMaQA.SelectedValue.ToString();
            data.Cells[1].Value = txtSL.Text;
            data.Cells[2].Value = txtDongia.Text;
            data.Cells[3].Value = txtGiamGia.Text;
            data.Cells[4].Value = txtThanhTien.Text;
            Tien += (float.Parse(txtThanhTien.Text) - OldTien);
            txtTongtien.Text = Tien.ToString();
            table = (DataTable)dgvdata.DataSource;
            btnSua.Enabled = false;
        }

        private void btnThemnhom_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO HoaDonNhap VALUES({txtManhap.Text},{dateTimeNH.Text},{txtTongtien.Text},'{cboMaNV.Text}','{cboMaNCC.Text}')";
            data.ExecuteNonQuery(sql);
            for (int i = 0; i < dgvdata.Rows.Count - 1; i++)
            {
                sql = $"INSERT INTO ChiTietHDN VALUES({dgvdata.Rows[i].Cells[1].Value.ToString()},{dgvdata.Rows[i].Cells[2].Value.ToString()},{dgvdata.Rows[i].Cells[3].Value.ToString()},{dgvdata.Rows[i].Cells[4].Value.ToString()},'{dgvdata.Rows[i].Cells[0].Value.ToString()}',{txtManhap.Text})";
                data.ExecuteNonQuery(sql);
            }
            MessageBox.Show("Thêm thành công");

            StartPr();
        }
    }
}
