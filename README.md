using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeOtomasyonu.Personel
{
    public partial class FrmPersonel : Form
    {
        CafeEntities db = new CafeEntities();
        int SeciliMasaId;
        public FrmPersonel()
        {
            InitializeComponent();
        }

        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            MasalariOlustur();
        }
        public void MasalariOlustur()
        {
            flpMasalar.Controls.Clear();
            var masalar = db.Masa.Where(m => m.AktifMi == 1).ToList();
            foreach (var masa in masalar)
            {
                Button btn = new Button();
                btn.Width = 125;
                btn.Height = 80;
                btn.Text = masa.MasaAdi;
                btn.Tag = masa.id;
                btn.Margin = new Padding(10);
                if (masa.Durum == 1)
                {
                    btn.BackColor = Color.Red;
                }
                else
                {
                    btn.BackColor = Color.Green;
                }
                btn.Click += MasaButonu_Click;
                btn.MouseEnter += MasaButonu_MouseEnter;
                flpMasalar.Controls.Add(btn);
            }

        }
        private void MasaButonu_Click(object sender, EventArgs e)
        {
            Button tiklananButton = sender as Button;
            SeciliMasaId = Convert.ToInt32(tiklananButton.Tag);
            MasaAcVeyaSiparisGetir();
        }
        private void MasaButonu_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            lblBilgi.Text = btn.Text + "üzerine geldiniz.";
        }
        public void MasaAcVeyaSiparisGetir()
        {
            var siparis = db.Siparis.FirstOrDefault(s => s.MasaId == SeciliMasaId && s.KapandiMi == 0);
            if (siparis == null) 
            {
                siparis=new Siparis();
                siparis.MasaId = SeciliMasaId;
                siparis.PersonelId = 1;
                siparis.AcilisTarihi = DateTime.Now;
                siparis.KapandiMi = 0;
                db.Siparis.Add(siparis);
                Masa ms = db.Masa.Find(SeciliMasaId);
                ms.Durum = 1;
                db.SaveChanges();

            }
            SeciliMasaId = siparis.id;
            
        }
    }
}
