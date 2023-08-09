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
namespace HastaneOtomasyonu_Proje_
{
    public partial class DoktorKayit : Form
    {
        

        public DoktorKayit()
        {
            InitializeComponent();
        }
        string _connString = @"Data Source=PC-029;Initial Catalog=HospitalAutomation;Integrated Security=true";

        public void goster(string veri)
        {
            SqlDataAdapter da = new SqlDataAdapter(veri, _connString);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Giris kek = new Giris();
            kek.Show();
            this.Hide(); 
        }

        private void DoktorKayit_Load(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection _conn = new SqlConnection(_connString))
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();
                using (SqlCommand _command = new SqlCommand("SP_DoktorInsert", _conn))
                {
                    //_command.Parameters.AddWithValue("@DoctorId", txtDId.Text);
                    _command.Parameters.AddWithValue("@UserName", txtUserName.Text);
                    _command.Parameters.AddWithValue("@Password", txtPsssword.Text);
                    _command.Parameters.AddWithValue("@Name", txtName.Text);
                    _command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                    _command.Parameters.AddWithValue("@Branch", cmbDbranch.Text);
                    _command.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.ExecuteNonQuery();
                    
                }
                MessageBox.Show("Kaydınız Yapılmıştır.");

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult _result = MessageBox.Show(string.Format("{0} adlı kategori üzerinde güncelleme yapılacak. Emin misiniz?", txtUserName.Text), "ONAY", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (_result)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                    using (SqlConnection _conn = new SqlConnection(_connString))
                    {
                        if (_conn.State == ConnectionState.Closed)
                            _conn.Open();
                        using (SqlCommand _command = new SqlCommand("SP_DoktorUpdate", _conn))
                        {
                            _command.Parameters.AddWithValue("@DoctorId", txtDId.Text);
                            _command.Parameters.AddWithValue("@UserName", txtUserName.Text);
                            _command.Parameters.AddWithValue("@Password", txtPsssword.Text);
                            _command.Parameters.AddWithValue("@Name", txtName.Text);
                            _command.Parameters.AddWithValue("@LastName", txtLastName.Text);
                            _command.Parameters.AddWithValue("@Branch", cmbDbranch.Text);
                            _command.Parameters.AddWithValue("@Phone", txtPhone.Text);
                            _command.CommandType = CommandType.StoredProcedure;
                            _command.ExecuteNonQuery();
                            
                        }
                        
                    }
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
            MessageBox.Show("Güncelleme Tamamlandı.");
        }
        

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult _result = MessageBox.Show(string.Format("{0} adlı kategori silinecek. Emin misiniz?", txtUserName.Text), "ONAY", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (_result)
            {
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Abort:
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.Yes:
                    using (SqlConnection _conn = new SqlConnection(_connString))
                    {
                        if (_conn.State == ConnectionState.Closed)
                            _conn.Open();
                        using (SqlCommand _command = new SqlCommand("SP_DoktorDelete", _conn))
                        {
                            _command.Parameters.AddWithValue("@DoctorId", txtDId.Text);
                            _command.CommandType = CommandType.StoredProcedure;
                            _command.ExecuteNonQuery();
                           
                        }
                    }
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
            MessageBox.Show("Kaydınız Silindi.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"PC-029;Initial Catalog=HospitalAutomation;Integrated Security=true");
            SqlCommand komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Select * from  Doctor where Phone=@Phone";
            komut.Parameters.AddWithValue("@Phone", txtPhone.Text);
            komut.ExecuteNonQuery();
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                txtDId.Text = dr["DoctorId"].ToString();
                txtUserName.Text = dr["UserName"].ToString();
                txtPsssword.Text = dr["Password"].ToString();
                txtName.Text = dr["Name"].ToString();
                txtLastName.Text = dr["LastName"].ToString();
                cmbDbranch.Text = dr["Branch"].ToString();
                
            }
            else
            {
                MessageBox.Show("Veri çekilemedi");
            }


        }
    }
}
