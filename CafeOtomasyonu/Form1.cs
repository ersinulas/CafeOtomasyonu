using CafeOtomasyonu.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeOtomasyonu
{
    public partial class Form1 : Form
    {
        CafeEntities db=new CafeEntities();
        bool admin = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnvazgec_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void btnpersonel_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            admin = false;

        }

        private void btnadmin_Click(object sender, EventArgs e)
        {
            panel2
                .Visible = true;
            admin = true;
        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            if (admin)
            {
                var giris = db.Admins.FirstOrDefault(d => d.kul==txtkullanici.Text & d.pass==txtpass.Text);
                if (giris != null) 
                {
                    AdminPanel adm=new AdminPanel();
                    adm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı giriş Yaptınız Lütfen Tekrar deneyiniz");
                }
            }
        }
    }
}
