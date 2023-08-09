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
    public partial class DokRandevular : Form
    {
        

        public DokRandevular()
        {
            InitializeComponent();
           
        }
       SqlConnection baglanti = new SqlConnection("Data Source=PC-029;Initial Catalog=HospitalAutomation;Integrated Security=true");

        public void goster(string veri)
        {

            SqlDataAdapter da = new SqlDataAdapter(veri, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView2.DataSource = ds.Tables[0];
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Giris kek = new Giris();
            kek.Show();
            this.Hide();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

            goster("select IllName,DoctorName,Branch,Hour,Date from AppointmentList");
        }
        

        private void DokRandevular_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
