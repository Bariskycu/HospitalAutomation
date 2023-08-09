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
    public partial class RandevuAlma : Form
    {
        int[] docID = new int[999];

        public RandevuAlma()
        {
            InitializeComponent();
        }
        string _connString = @"Data Source=PC-029;Initial Catalog=HospitalAutomation;Integrated Security=true";


        private void btnClose_Click(object sender, EventArgs e)
        {
            Giris geri = new Giris();
            geri.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult _result = MessageBox.Show(string.Format("{0} adlı kategori silinecek. Emin misiniz?", txtIll.Text), "ONAY", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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
                        using (SqlCommand _command = new SqlCommand("SP_RandevuDelete", _conn))
                        {
                            _command.Parameters.AddWithValue("@AppId", txtAppId.Text);
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
            MessageBox.Show("Randevunuz Silindi.");
        }

        

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (SqlConnection _conn = new SqlConnection(_connString))
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();
                using (SqlCommand _command = new SqlCommand("SP_RandevuInsert", _conn))
                {
                    //_command.Parameters.AddWithValue("@Doctor", txtAppId.Text);
                    _command.Parameters.AddWithValue("@IllName", txtIll.Text);
                    _command.Parameters.AddWithValue("@DoctorName", cmbDoctor.Text);
                    _command.Parameters.AddWithValue("@Branch", cmbBranch.Text);
                    _command.Parameters.AddWithValue("@Hour", dateTimePicker2.Text);
                    _command.Parameters.AddWithValue("@Date", dateTimePicker1.Text);
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.ExecuteNonQuery();
                    
                }
                MessageBox.Show("Randevunuz Kaydedildi");
            
                
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult _result = MessageBox.Show(string.Format("{0} adlı kategori üzerinde güncelleme yapılacak. Emin misiniz?", txtIll.Text), "ONAY", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

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
                        using (SqlCommand _command = new SqlCommand("SP_RandevuUpdate", _conn))
                        {
                            _command.Parameters.AddWithValue("@AppId", txtAppId.Text);
                            _command.Parameters.AddWithValue("@IllName", txtIll.Text);
                            _command.Parameters.AddWithValue("@DoctorName", cmbDoctor.Text);
                            _command.Parameters.AddWithValue("@Branch", cmbBranch.Text);
                            _command.Parameters.AddWithValue("@Hour", dateTimePicker2.Text);
                            _command.Parameters.AddWithValue("@Date", dateTimePicker1.Text);
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
            MessageBox.Show("Randevunuz Güncellendi.");
        }

        private void RandevuAlma_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=PC-029;Initial Catalog=HospitalAutomation;Integrated Security=true");
            SqlCommand komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Select * from Doctor";
            komut.ExecuteNonQuery();
            SqlDataReader dr = komut.ExecuteReader();
            int counter = 0;
            while (dr.Read())
            {
                docID[counter] = Convert.ToInt32(dr["DoctorId"]);
                cmbDoctor.Items.Add(dr["Name"] + " " + dr["LastName"]);
               
                counter++;
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a=cmbDoctor.SelectedIndex;
            txtAppId.Text = docID[a].ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
