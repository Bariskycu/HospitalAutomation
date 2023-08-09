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
    public partial class HastaKayit : Form
    {

        public HastaKayit()
        {
            InitializeComponent();
        }
        string _connString = @"Data Source=PC-029;Initial Catalog=HospitalAutomation;Integrated Security=true";


        private void btnClose_Click(object sender, EventArgs e)
        {
            Giris kek = new Giris();
            kek.Show();
            this.Hide();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (SqlConnection _conn = new SqlConnection(_connString))
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();
                using (SqlCommand _command = new SqlCommand("SP_HastaInsert", _conn))
                {
                    //_command.Parameters.AddWithValue("@IllId", txtHId.Text);
                    _command.Parameters.AddWithValue("@UserName", txtUserName.Text);
                    _command.Parameters.AddWithValue("@Password", txtPassword.Text);
                    _command.Parameters.AddWithValue("@IllName", txtName.Text);
                    _command.Parameters.AddWithValue("@IllLastName", txtLastName.Text);
                    _command.Parameters.AddWithValue("@Email", txtMail.Text);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.ExecuteNonQuery();
                   
                }
            }
            MessageBox.Show("Kaydınızı Tamamlandı.");
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
                        using (SqlCommand _command = new SqlCommand("SP_HastaDelete", _conn))
                        {
                            _command.Parameters.AddWithValue("@IllId", txtHId.Text);
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
                        using (SqlCommand _command = new SqlCommand("SP_HastaUpdate", _conn))
                        {
                            _command.Parameters.AddWithValue("@IllId", txtHId.Text);
                            _command.Parameters.AddWithValue("@UserName", txtUserName.Text);
                            _command.Parameters.AddWithValue("@Password", txtPassword.Text);
                            _command.Parameters.AddWithValue("@IllName", txtName.Text);
                            _command.Parameters.AddWithValue("@IllLastName", txtLastName.Text);
                            _command.Parameters.AddWithValue("@Email", txtMail.Text);
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
            MessageBox.Show("Kaydınız Güncellendi.");
        }

        private void HastaKayit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(@"Data Source=PC-029;Initial Catalog=HospitalAutomation;Integrated Security=true");
            SqlCommand komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Select * from Ill where Email=@Email";
            komut.Parameters.AddWithValue("@Email", txtMail.Text);
            komut.ExecuteNonQuery();
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                txtHId.Text = dr["IllId"].ToString();
                txtUserName.Text = dr["UserName"].ToString();
                txtPassword.Text = dr["Password"].ToString();
                txtName.Text = dr["IllName"].ToString();
                txtLastName.Text = dr["IllLastName"].ToString();
                txtMail.Text = dr["Email"].ToString();

            }
            else
            {
                MessageBox.Show("Veri çekilemedi");
            }
        }
    }
}
