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
    public partial class Form_BanHang : Form
    {
        DataProvider dataProvider = new DataProvider();
        public Form_BanHang()
        {
            InitializeComponent();
        }

        private void Form_BanHang_Load(object sender, EventArgs e)
        {
            string qr = $"select distinct MaNCC from NhaCungCap";
            dataProvider.FillCBO(qr,cboNhacungcap, "MaNCC");
            qr = $"select distinct MaNV from NhanVien";
            dataProvider.FillCBO(qr, cboTenthuoc, "MaNV");
            // Truong gia tri
        }
    }
}
