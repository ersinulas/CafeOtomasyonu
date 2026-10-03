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
    public partial class MasaAyarlari : Form
    {
        int id;
        CafeEntities db= new CafeEntities();
        public MasaAyarlari()
        {
            InitializeComponent();
        }

        private void bigTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pnlKategori.Visible = false;
        }
        public void ktgListele()
        {
            var liste=db.MasaKategori.ToList();
            dgvKategori.DataSource = liste;
            pnlKategori.Visible = true;
            btnKtgGuncelle.Visible = false;
            btnKtgSil.Visible = false;
            btnKtgKayit.Visible = true;
            txtKtgAd.Text = "";
        }
        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            
            ktgListele();
        }

        private void btnKtgKayit_Click(object sender, EventArgs e)
        {
            MasaKategori ms=new MasaKategori();
            ms.KategoriAdi = txtKtgAd.Text;
            db.MasaKategori.Add(ms);
            db.SaveChanges();
            ktgListele();
            btnKtgGuncelle.Visible = false;
            btnKtgSil.Visible = false;
        }

        private void dgvKategori_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dgvKategori.CurrentRow.Cells["id"].Value);
            var veri = db.MasaKategori.FirstOrDefault(v => v.id == id);
            txtKtgAd.Text = veri.KategoriAdi;
            btnKtgGuncelle.Visible = true;
            btnKtgSil.Visible = true;
            btnKtgKayit.Visible = false;

        }

        private void btnKtgGuncelle_Click(object sender, EventArgs e)
        {
            var veri = db.MasaKategori.FirstOrDefault(g => g.id == id);
            veri.KategoriAdi = txtKtgAd.Text;
            db.SaveChanges();
           ktgListele();
        }

        private void btnKtgSil_Click(object sender, EventArgs e)
        {
            var veri = db.MasaKategori.FirstOrDefault(g => g.id == id);
            db.MasaKategori.Remove(veri);
            db.SaveChanges();
            ktgListele();
        }
        public void masaListele()
        {
            var masalar = db.Masa.ToList();
            dgvmasalar.DataSource = masalar;
            cbKategori.DataSource = db.MasaKategori.ToList();
            cbKategori.DisplayMember = "KategoriAdi";
            cbKategori.ValueMember = "id";
            cbKategori.SelectedIndex = -1;
        }
        private void MasaAyarlari_Load(object sender, EventArgs e)
        {
            masaListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Masa ms = new Masa();
            ms.MasaAdi = txtMasaAd.Text;
            ms.MasaKategoriİd = Convert.ToInt32(cbKategori.SelectedValue);
            db.Masa.Add(ms);
            db.SaveChanges();
            masaListele();
        }

        private void dgvmasalar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dgvmasalar.CurrentRow.Cells["id"].Value);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var veri = db.Masa.FirstOrDefault(f => f.id == id);
            db.Masa.Remove(veri);
            db.SaveChanges();
            masaListele();  
        }
    }
}
