using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;    
using System.Windows.Forms;

namespace CafeOtomasyonu.Admin
{
    public partial class PersonelYontici : Form
    {
        int id;
        CafeEntities db = new CafeEntities();
        public PersonelYontici()
        {
            InitializeComponent();
        }
        public void listele()

        {
            var veri= db.PersonelBilgileri.ToList();
            dataPersonel.DataSource = veri;
            txttelefon.Text = "";
            txtsoyad.Text = "";
            txtad.Text = "";
            txtmaas.Text = "";  

        }
        private void PersonelYontici_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }

        private void txtsoyad_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            AdminPanel ad= new AdminPanel();
            ad.Show();
            this.Hide();
        }

        private void btnkayit_Click(object sender, EventArgs e)
        {
            PersonelBilgileri pb= new PersonelBilgileri();
            pb.Adi = txtad.Text;
            pb.Soyad = txtsoyad.Text;
            pb.Tel = Convert.ToInt32(txttelefon.Text);
            pb.maas=Convert.ToInt32(txtmaas.Text);
            db.PersonelBilgileri.Add(pb);
            db.SaveChanges();
            listele();
            int id = pb.id;
            Random rnd = new Random();
            string sifre = rnd.Next(1000, 9999).ToString();
            PersonelGiris pg= new PersonelGiris();
            pg.Kul = txtsoyad.Text;
            pg.Pass = sifre;
            pg.PersonelBilgileri = id;
            db.PersonelGiris.Add(pg);
            db.SaveChanges();
            MessageBox.Show("personel giriş şifreniz : " + sifre);

        }

        private void dataPersonel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dataPersonel.CurrentRow.Cells["id"].Value);
            var personel = db.PersonelBilgileri.FirstOrDefault(c => c.id == id);
            txtad.Text = personel.Adi;
            txtsoyad.Text=personel.Soyad;
            txttelefon.Text = personel.Tel.ToString();
            txtmaas.Text = personel.maas.ToString();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            var guncel = db.PersonelBilgileri.FirstOrDefault(f => f.id == id);
            guncel.Adi= txtad.Text;
            guncel.Soyad= txtsoyad.Text;
            guncel.Tel = Convert.ToInt32(txttelefon.Text);
            guncel.maas=Convert.ToDecimal(txtmaas.Text);
            db.SaveChanges();
            listele();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            var silb = db.PersonelBilgileri.FirstOrDefault(v => v.id == id);
            db.PersonelBilgileri.Remove(silb);
            db.SaveChanges();
            var silg = db.PersonelGiris.FirstOrDefault(d=>d.PersonelBilgileri==id);
            db.PersonelGiris.Remove(silg);
            db.SaveChanges();
            listele();
        }
    }
}
