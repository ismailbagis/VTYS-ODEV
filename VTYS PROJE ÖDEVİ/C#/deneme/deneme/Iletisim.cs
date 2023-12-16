using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deneme
{
    public partial class Iletisim : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");
        public Iletisim()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_listele_Click(object sender, EventArgs e)
        {
            string sorgu = " select * from iletisim ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridViewIletisim.DataSource = dt.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand kontrolKomut = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"iletisim\" WHERE \"iletisim_adresi\"=@p3", baglanti);
                kontrolKomut.Parameters.AddWithValue("@p3", txtiletisimadresi.Text);

                int kayitSayisi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu iletişim adresine sahip bir kayıt zaten mevcut!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO public.\"iletisim\" VALUES(@p1, @p2, @p3, @p4)", baglanti);
                    komut.Parameters.AddWithValue("@p1", int.Parse(txtiletisimid.Text));
                    komut.Parameters.AddWithValue("@p2", txtiletisimturu.Text);
                    komut.Parameters.AddWithValue("@p3", txtiletisimadresi.Text);
                    komut.Parameters.AddWithValue("@p4", int.Parse(comboBoxmusteri.SelectedValue.ToString()));

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Müşterinin iletişim kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

        }

        private void Iletisim_Load(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("select * from public.\"musteri\"", baglanti);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            comboBoxmusteri.DisplayMember = "musteri_adi";
            comboBoxmusteri.ValueMember = "musteri_id";
            comboBoxmusteri.DataSource = dt7;
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                NpgsqlCommand kontrolKomut = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"iletisim\" WHERE \"iletisim_adresi\"=@p2", baglanti);
                kontrolKomut.Parameters.AddWithValue("@p2", txtiletisimadresi.Text);

                int kayitSayisi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

                // Eğer aynı adresle kayıt varsa
                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Bu iletişim adresine sahip bir kayıt zaten mevcut!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    NpgsqlCommand komut = new NpgsqlCommand("UPDATE public.\"iletisim\" SET \"iletisim_tipi\"=@p1, \"iletisim_adresi\"=@p2, \"musteri_id\"=@p3 WHERE \"iletisim_id\"=@p4", baglanti);
                    komut.Parameters.AddWithValue("@p1", txtiletisimturu.Text);
                    komut.Parameters.AddWithValue("@p2", txtiletisimadresi.Text);
                    komut.Parameters.AddWithValue("@p3", int.Parse(comboBoxmusteri.SelectedValue.ToString()));
                    komut.Parameters.AddWithValue("@p4", int.Parse(txtiletisimid.Text));

                    komut.ExecuteNonQuery();
                    MessageBox.Show("Güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }


        }

        private void dataGridViewSube_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewIletisim.CurrentRow.Selected = true;
            DataGridViewRow selectedRow = dataGridViewIletisim.Rows[e.RowIndex];
            txtiletisimid.Text = dataGridViewIletisim.Rows[e.RowIndex].Cells["iletisim_id"].Value.ToString();
            txtiletisimturu.Text = dataGridViewIletisim.Rows[e.RowIndex].Cells["iletisim_tipi"].Value.ToString();
            txtiletisimadresi.Text = dataGridViewIletisim.Rows[e.RowIndex].Cells["iletisim_adresi"].Value.ToString();
            comboBoxmusteri.SelectedValue = selectedRow.Cells["musteri_id"].Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand kontrolKomut = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"iletisim\" WHERE \"iletisim_id\"=@p1", baglanti);
                kontrolKomut.Parameters.AddWithValue("@p1", int.Parse(txtiletisimid.Text));

                int kayitSayisi = Convert.ToInt32(kontrolKomut.ExecuteScalar());

                if (kayitSayisi > 0)
                {

                    DialogResult result = MessageBox.Show("İletişim kaydını silme işlemi onaylıyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                    if (result == DialogResult.Yes)
                    {

                        NpgsqlCommand komut = new NpgsqlCommand("DELETE FROM public.\"iletisim\" WHERE \"iletisim_id\"=@p1", baglanti);
                        komut.Parameters.AddWithValue("@p1", int.Parse(txtiletisimid.Text));
                        komut.ExecuteNonQuery();
                        MessageBox.Show("İletişim kaydı başarıyla silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi iptal edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Belirtilen ID'ye sahip bir iletişim kaydı bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }

        }

        private void dataGridViewSube_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sirket sirket = new Sirket();
            sirket.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Otopark otoparkgecis = new Otopark();
            otoparkgecis.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Musteriler musterigecis = new Musteriler();
            musterigecis.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Sube subegecis = new Sube();
            subegecis.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Yonetici yonetici = new Yonetici();
            yonetici.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Iletisim iletisimgecis = new Iletisim();
            iletisimgecis.Show();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Calisan calisan = new Calisan();
            calisan.Show();
            this.Hide();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Tir tir = new Tir();
            tir.Show();
            this.Hide();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Siparis siparis = new Siparis();
            siparis.Show();
            this.Hide();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Fatura fatura = new Fatura();
            fatura.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AdminPanel admin = new AdminPanel();
            admin.Show();
            this.Hide();
        }

        private void Iletisim_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
