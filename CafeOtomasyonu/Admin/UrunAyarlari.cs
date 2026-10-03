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
    public partial class UrunAyarlari : Form
    {
        CafeEntities db=new CafeEntities();
        int urnId,ktgId;
        
        public UrunAyarlari()
        {
            InitializeComponent();
        }
        public void listele()
        {
            var urn=db.Urun.ToList();
            var ktg=db.MenuKategori.ToList();
            dgvkategoriler.DataSource = ktg;
            dgvurunler.DataSource = urn;
            txtUrnKategori.Text = "";
            btnKtgGuncelle.Visible= false;
            btnKtgSil.Visible= false;
            btnKtgKayit.Visible= true;
            cbkategori.DataSource = ktg;
            cbkategori.DisplayMember = "KategoriAdi";
            cbkategori.ValueMember = "id";
            cbkategori.SelectedIndex = -1;
            btnUrnGuncelle.Visible = false;
            btnUrnSil.Visible = false;
            btnUrnKaydet.Visible = true;
            
        }

        private void btnKtgKayit_Click(object sender, EventArgs e)
        {
            MenuKategori mn=new MenuKategori();
            mn.KategoriAdi = txtUrnKategori.Text;
            db.MenuKategori.Add(mn);
            db.SaveChanges();
            listele();
        }

        private void dgvkategoriler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ktgId = Convert.ToInt32(dgvkategoriler.CurrentRow.Cells["id"].Value);
            var deger=db.MenuKategori.FirstOrDefault(d=>d.id==ktgId);
            txtUrnKategori.Text = deger.KategoriAdi;
            btnKtgGuncelle.Visible = true;
            btnKtgSil.Visible = true;
            btnKtgKayit.Visible = false;
        }

        private void btnKtgSil_Click(object sender, EventArgs e)
        {
            var veri=db.MenuKategori.FirstOrDefault(f=>f.id==ktgId);
            db.MenuKategori.Remove(veri);
            db.SaveChanges();
            listele();
        }

        private void btnKtgGuncelle_Click(object sender, EventArgs e)
        {
            var veri = db.MenuKategori.FirstOrDefault(f => f.id == ktgId);
            veri.KategoriAdi= txtUrnKategori.Text;
            db.SaveChanges();
            listele();
        }

        private void btnUrnKaydet_Click(object sender, EventArgs e)
        {
            Urun urn=new Urun();
            urn.UrunAdi = txtad.Text;
            urn.Fiyat = Convert.ToDecimal(txtfiyat.Text);
            urn.MenuKategoriID = Convert.ToInt32(cbkategori.SelectedValue);
            if (cbdurum.SelectedIndex == 0)
            {
                urn.AktifMi = 1;
            }
            else
            {
                urn.AktifMi= 0;
            }
            db.Urun.Add(urn);
            db.SaveChanges();
            listele();
        }

        private void dgvurunler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            urnId = Convert.ToInt32(dgvurunler.CurrentRow.Cells["id"].Value);
            var bilgi=db.Urun.FirstOrDefault(s=>s.id == urnId);
            txtad.Text = bilgi.UrunAdi;
            txtfiyat.Text = bilgi.Fiyat.ToString();
            cbkategori.SelectedValue = bilgi.MenuKategoriID;
            if (bilgi.AktifMi == 1) {
                cbdurum.SelectedIndex = 0;
            }
            else
            {
                cbdurum.SelectedIndex = 1;
            }
        }

        private void UrunAyarlari_Load(object sender, EventArgs e)
        {
            listele();
        }
    }
}
