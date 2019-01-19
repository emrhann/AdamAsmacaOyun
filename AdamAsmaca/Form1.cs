using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AdamAsmaca
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string kelime_dosya_yolu = Application.StartupPath + "/kelimeler.txt";
        string skor_dosya_yolu = Application.StartupPath + "/skorlist.txt";
        string üretilen_kelime;
        int hak_sayi=0;
        Label[] lbl = new Label[12];
        private void Form1_Load(object sender, EventArgs e)
        {
            tahmin_buton.Enabled = false;
            tahmin_textbox.Enabled = false;

            string[] dizi1 = File.ReadAllLines(skor_dosya_yolu);
            for (int i = 0; i < dizi1.Length; i++)
            {
                listBox1.Items.Add(dizi1[i]);
            }

        }
        public void DosyayaYaz(string kelime)
        {
            if (File.Exists(kelime_dosya_yolu))
            {
                //Dosya varsa
                StreamWriter yaz = new StreamWriter(kelime_dosya_yolu, true);
                yaz.WriteLine(kelime);
                yaz.Close();
            }
        }
        public void Skor_Ekle()
        {
            if (File.Exists(skor_dosya_yolu))
            {
                //Dosya varsa
                StreamWriter yaz = new StreamWriter(skor_dosya_yolu, true);
                yaz.WriteLine(textBox2.Text +":"+hak_sayi.ToString());
                yaz.Close();
            }

        }
        public void Tahmin(string harf)
        {
            bool kntrl=false;
            bool kntrl2 = false;
            
            char[] dizi_harf = üretilen_kelime.ToCharArray();
            for (int i = 0; i < dizi_harf.Length; i++)
            {
                if (harf==dizi_harf[i].ToString())
                {
                    lbl[i].Text = dizi_harf[i].ToString();
                    kntrl = true;
                    
                }
            }
            // Bilinemeyen harfleri labelden alıp char dizisi oluştuşturup o şekilde kontrol ettim.
            char[] dizi_harf2 = label4.Text.ToCharArray();
            for (int i = 0; i < dizi_harf2.Length; i++)
             {
                if (harf==dizi_harf2[i].ToString())
                {
                    MessageBox.Show("DİKKAT Bilinemeyen aynı harfi girdiniz !");
                    kntrl2 = true;
                }
              }
            if (kntrl==false) // Bulamazsa harfi dışarı yazdırıyor
            {
                if(kntrl2==false)
                {
                    label4.Text = label4.Text + harf;
                    hak_sayi = hak_sayi - 1;
                }
                
            }
            label2.Text = hak_sayi.ToString();
            int x = 0;
            for (int i = 0; i < dizi_harf.Length; i++)
            {
                if (lbl[i].Text == dizi_harf[i].ToString())
                {
                    x++;
                }
            }
            if (x==dizi_harf.Length)
            {
                MessageBox.Show("!.. TEBRİKLER KAZANDINIZ ..!");
                Skor_Ekle();
            }

            if(hak_sayi==0)
            {
                MessageBox.Show("GAME OVER CINIM"+ "  Kelime : "+üretilen_kelime);
                tahmin_buton.Enabled = false;
                label4.Text = null;
                button1.Enabled = true;
                button3.Enabled = true;
                üretilen_kelime = null;
                Array.Clear(lbl, 0, lbl.Length);
                Skor_Ekle();
            }
        }

        public void Belirli_sayida(int sayi)
        {
            bool deger=false;
            if (sayi<3)
            {
                MessageBox.Show("En az 3 harfli sayi üretilebilir");
            }
            else
            {
             string[] K_dizi = File.ReadAllLines(kelime_dosya_yolu);
                for (int i = 0; i < K_dizi.Length; i++)
                {
                char[] dizi_harf = K_dizi[i].ToCharArray();
                    if (dizi_harf.Length==sayi)
                    {
                        deger = true;
                        üretilen_kelime=K_dizi[i];
                        for (int j = 0; j < dizi_harf.Length; j++)
                        {
                            lbl[j] = new Label();
                            lbl[j].AutoSize = true;
                            lbl[j].Location = new Point(200 + (20 * j), 100);
                            lbl[j].Text = "----";
                            Controls.Add(lbl[j]);
                        }
                       break;
                    }
                }
                if (deger==false)
                    {
                        MessageBox.Show("Belirtilen sayıda harften oluşan kelime yok !");
                        üretilen_kelime = null;
                    }
            }
            button3.Enabled = false;
            button1.Enabled = false;
        }
    
        public void Rastgele_üret()
        {
            Random rnd = new Random();

            string[] K_dizi = File.ReadAllLines(kelime_dosya_yolu);
            int randm = rnd.Next(0, K_dizi.Length);
            char[] dizi_harf = K_dizi[randm].ToCharArray();
            üretilen_kelime = K_dizi[randm];
            for (int i = 0; i < dizi_harf.Length; i++)
            {
                lbl[i] = new Label();
                lbl[i].AutoSize = true;
                lbl[i].Location = new Point(200 + (20 * i), 100);
                lbl[i].Text = "----";
                Controls.Add(lbl[i]);
            }
            button3.Enabled = false;
            button1.Enabled = false;
        }

        public void DosyadanSil(string silinecek_kelime)
        {
             string[] dizi1 = File.ReadAllLines(kelime_dosya_yolu);
            if (File.Exists(kelime_dosya_yolu))
            {
                //Dosya varsa
                StreamWriter yaz = new StreamWriter(kelime_dosya_yolu);
                yaz.Write("");
                yaz.Close();
            }
            List<string> liste = new List<string>(dizi1);

            liste.Remove(silinecek_kelime);
            if (File.Exists(kelime_dosya_yolu))
            {
                StreamWriter yaz = new StreamWriter(kelime_dosya_yolu,true);
                for (int i = 0; i < liste.Count; i++)
                {
                    yaz.WriteLine(liste[i]);   
                }
                yaz.Close();
            }
        }

        private void button_Ekle_Click(object sender, EventArgs e)
        {
            DosyayaYaz(textBox1.Text);
        }

        private void button_Sil_Click(object sender, EventArgs e)
        {
            DosyadanSil(textBox1.Text.ToUpper());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="")
            {
                MessageBox.Show("Lütfen Önce Adınızı Giriniz");
            }
            else
            {
            Rastgele_üret();
            tahmin_buton.Enabled = true;
            tahmin_textbox.Enabled = true;
            textBox3.Text = üretilen_kelime;
            hak_sayi = üretilen_kelime.Length + 2;
            label2.Text = hak_sayi.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Lütfen Önce Adınızı Giriniz");
            }
            else
            {
                tahmin_buton.Enabled = true;
                tahmin_textbox.Enabled = true;
                Belirli_sayida(Convert.ToInt16(numericUpDown1.Value));
                if(üretilen_kelime==null)
                {
                    hak_sayi = 0;
                }
                else
                {
                    textBox3.Text =üretilen_kelime;
                    hak_sayi = üretilen_kelime.Length + 2;
                }
            
                label2.Text = hak_sayi.ToString();
            }
            
        }

        private void tahmin_buton_Click(object sender, EventArgs e)
        {
            Tahmin(tahmin_textbox.Text.ToUpper());
        }
    }
}