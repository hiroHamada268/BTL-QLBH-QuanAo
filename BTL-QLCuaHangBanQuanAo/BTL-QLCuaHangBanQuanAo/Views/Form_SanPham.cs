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
    public partial class Form_SanPham : Form
    {
        DataProvider sqlConnect = new DataProvider();
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
    }
}
