using BTL_QLCuaHangBanQuanAo.Model.Class;
using BTL_QLCuaHangBanQuanAo.Model.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_QLCuaHangBanQuanAo.Views
{
    public partial class Form_DetailSP : Form
    {
        public Form_DetailSP()
        {
            InitializeComponent();
        }

        private void Form_DetailSP_Load(object sender, EventArgs e)
        {
            if(StaticData.dataTableSp != null)
            {
                pictureBox1.Image = new Bitmap(StaticData.dataTableSp["Anh"].ToString());

                if(int.Parse(StaticData.dataTableSp["SoLuong"].ToString()) > 0)
                {
                    lblTrangThai.Text = "Còn Hàng";
                }
                else
                {
                    lblTrangThai.Text = "Hết Hàng";
                } 

                lblTieuDe.Text = StaticData.dataTableSp["TenQuanAo"].ToString();
                lblSoLuong.Text = StaticData.dataTableSp["SoLuong"].ToString();

                string queryCo = $"select * from SanPham sp join Co c on sp.MaCo = c.MaCo where c.MaCo = N'{StaticData.dataTableSp["MaCo"]}'";
                DataTable dtCo = DataProvider.Instance.ExecuteQuery(queryCo);
                if (dtCo != null)
                {
                    lblMaCo.Text = dtCo.Rows[0]["TenCo"].ToString();
                }
                lblCo.Text = StaticData.dataTableSp["MaCo"].ToString();

                lblDonGiaBan.Text = StaticData.dataTableSp["DonGiaBan"].ToString();

                string query = $"select * from SanPham sp join ChatLieu cl on sp.MaChatLieu = cl.MaChatLieu where cl.MaChatLieu = N'{StaticData.dataTableSp["MaChatLieu"]}'";
                DataTable dt = DataProvider.Instance.ExecuteQuery(query);
                if(dt != null)
                {
                    lblChatLieu.Text = dt.Rows[0]["TenChatLieu"].ToString();
                }
                lblDonGiaBan.Text = StaticData.dataTableSp["DonGiaBan"].ToString();
            }
        }
    }
}
