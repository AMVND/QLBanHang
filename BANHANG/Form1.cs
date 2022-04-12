using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BANHANG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

            HienThiToanBoSanPham();
        }

        private void HienThiToanBoSanPham()
        {
            SqlConnection conn = new SqlConnection();
            //Lấy dữ liệu chuỗi kết nối thông qua đối tượng Properties
            string s = ConfigurationManager.ConnectionStrings["Strconnection"].ConnectionString;
            conn.ConnectionString = s;
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "LayToanBoMatHang";
            comm.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvMatHang.DataSource = dt;
            comm.ExecuteNonQuery();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "THÊM MẶT HÀNG";
            btnSua.Enabled = true;
            btnXoa.Enabled = false;
            //Hiện sản phẩm
            HienThiToanBoSanPham();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            
            lblTieuDe.Text = "CẬP NHẬT MẶT HÀNG";
            
            btnThem.Enabled = false;
            btnXoa.Enabled = true;

            HienThiToanBoSanPham();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            string s = ConfigurationManager.ConnectionStrings["Strconnection"].ConnectionString;
            conn.ConnectionString = s;
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "Xoa";
            comm.Connection = conn;
            comm.Parameters.Add("@MaSP", SqlDbType.NChar).Value = txtMaSP.Text;
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvMatHang.DataSource = dt;
            comm.ExecuteNonQuery();
            HienThiToanBoSanPham();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            //Lấy dữ liệu chuỗi kết nối thông qua đối tượng Properties
            string s = ConfigurationManager.ConnectionStrings["Strconnection"].ConnectionString;
            conn.ConnectionString = s;
            

            
           if (btnThem.Enabled)
            {
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
                // THÊM MẶT HÀNG
            comm.CommandText = "InsertMatHang";
            comm.Connection = conn;

            comm.Parameters.Add(new SqlParameter("@MaSP", txtMaSP.Text));
            comm.Parameters.Add(new SqlParameter("@TenSP", txtTenSP.Text));
            comm.Parameters.Add(new SqlParameter("@NgaySX", dtpNgaySX.Value.Date));
            comm.Parameters.Add(new SqlParameter("@NgayHH", dtpNgayHH.Value.Date));
            comm.Parameters.Add(new SqlParameter("@DonVi", txtDonVi.Text));
            comm.Parameters.Add(new SqlParameter("@DonGia", txtDonGia.Text));
            comm.Parameters.Add(new SqlParameter("@GhiChu", txtGhiChu.Text));
            
            comm.ExecuteNonQuery();
            HienThiToanBoSanPham();
            conn.Close();
            
            }
            
            else if (btnSua.Enabled)
            {
                // CẬP NHẬT MẶT HÀNG
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "CapNhatMatHang";
            comm.Connection = conn;
            comm.Parameters.Add("@MaSP", SqlDbType.NChar).Value = txtMaSP.Text;
            comm.Parameters.Add("@TenSP",SqlDbType.NVarChar).Value = txtTenSP.Text;
            comm.Parameters.Add("@NgaySX", SqlDbType.Date).Value = dtpNgaySX.Value.Date;
            comm.Parameters.Add("@NgayHH", SqlDbType.Date).Value = dtpNgayHH.Value.Date;
            comm.Parameters.Add("@DonVi", SqlDbType.NVarChar).Value = txtDonVi.Text;
            comm.Parameters.Add("@DonGia", SqlDbType.Float).Value = txtDonGia.Text;
            comm.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = txtGhiChu.Text;
            
            comm.ExecuteNonQuery();
            conn.Close();
            HienThiToanBoSanPham();
            }
            
        }

        private void dgvMatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaSP.Text = dgvMatHang[0, e.RowIndex].Value.ToString();
                txtTenSP.Text = dgvMatHang[1, e.RowIndex].Value.ToString();
                dtpNgaySX.Value = (DateTime)dgvMatHang[2, e.RowIndex].Value;
                dtpNgayHH.Value = (DateTime)dgvMatHang[3, e.RowIndex].Value;
                txtDonVi.Text = dgvMatHang[4, e.RowIndex].Value.ToString();
                txtDonGia.Text = dgvMatHang[5, e.RowIndex].Value.ToString();
                txtGhiChu.Text = dgvMatHang[6, e.RowIndex].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }
        private void XoaTrangChiTiet()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            dtpNgaySX.Value = DateTime.Now;
            dtpNgayHH.Value = DateTime.Now;
            txtDonVi.Text = "";
            txtDonGia.Text = "";
            txtGhiChu.Text = "";
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            //xoa trang
            XoaTrangChiTiet();
            //Cam nhap
            HienThiToanBoSanPham();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            
            if (txtTKMaSP.Text.Trim() != "")
            {
                SqlConnection conn = new SqlConnection();
                string s = ConfigurationManager.ConnectionStrings["Strconnection"].ConnectionString;
                conn.ConnectionString = s;
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "TimTheoMaSP";
                comm.Connection = conn;
                comm.Parameters.Add(new SqlParameter("@MaSP", txtTKMaSP.Text));
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMatHang.DataSource = dt;
                comm.ExecuteNonQuery();
            }

            else 
            {
                SqlConnection conn = new SqlConnection();
                //Lấy dữ liệu chuỗi kết nối thông qua đối tượng Properties
                string s = ConfigurationManager.ConnectionStrings["Strconnection"].ConnectionString;
                conn.ConnectionString = s;
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "TimTheoTenSP";
                comm.Connection = conn;
                comm.Parameters.Add(new SqlParameter("@TenSP", txtTKTenSP.Text));
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMatHang.DataSource = dt;
                comm.ExecuteNonQuery();
            }
        }
    }
}
