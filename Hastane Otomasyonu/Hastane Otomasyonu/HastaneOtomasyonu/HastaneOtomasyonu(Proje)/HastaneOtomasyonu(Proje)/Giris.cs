using _BusinessLayer;
using System;
using System.Configuration;
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


    public partial class Giris : Form
    {

        DatabaseBusiness _dbBusiness = new DatabaseBusiness();
        public static string KULAD = "";

        public Giris()
        {
            InitializeComponent();
        }

        private void btnKaydol_Click(object sender, EventArgs e)
        {
            HastaKayit hasta = new HastaKayit();
            hasta.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoktorKayit doktor = new DoktorKayit();
            doktor.Show();
            this.Hide();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Kullanıcı Adı ve(ya) Şifre Hatalı");
            }
            else
            {
                SqlParameter[] _parameters = new SqlParameter[]
                {
                    new SqlParameter("@UName",textBox1.Text),
                    new SqlParameter("@Pass",textBox2.Text)
                };

                LoginState state;

                DataTable dt = _dbBusiness.ExecuteAdapter("SP_IllLogin", CommandType.StoredProcedure, _parameters, out state);

                switch (state)
                {
                    case LoginState.UserExistsPasswordWrong:
                        MessageBox.Show("Kullanıcı adına ait şifre doğrulanamadı!", "Kullanıcı Girişi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case LoginState.UserNotExists:
                        MessageBox.Show("Kullanıcı adını ve şifreyi kontrol ediniz!", "Kullanıcı Girişi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case LoginState.UserExists:

                        RandevuAlma ran = new RandevuAlma();
                        
                        ran.Show();
                        this.Hide();
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Kullanıcı Adı ve(ya) Şifre Hatalı");
            }
            else
            {
                SqlParameter[] _parameters = new SqlParameter[]
                {
                    new SqlParameter("@UName",textBox4.Text),
                    new SqlParameter("@Pass2",textBox3.Text)
                };

                LoginState state;

                DataTable dt = _dbBusiness.ExecuteAdapter("SP_DoctorLogin", CommandType.StoredProcedure, _parameters, out state);

                switch (state)
                {
                    case LoginState.UserExistsPasswordWrong:
                        MessageBox.Show("Kullanıcı adına ait şifre doğrulanamadı!", "Kullanıcı Girişi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case LoginState.UserNotExists:
                        MessageBox.Show("Kullanıcı adını ve şifreyi kontrol ediniz!", "Kullanıcı Girişi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case LoginState.UserExists:

                        DokRandevular dok = new DokRandevular();
                        KULAD = textBox4.Text;
                        dok.Show();
                        this.Hide();
                        break;
                }
            }
        }

        private void DoktorTab_Click(object sender, EventArgs e)
        {

        }

        private void HastaTab_Click(object sender, EventArgs e)
        {

        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
}
