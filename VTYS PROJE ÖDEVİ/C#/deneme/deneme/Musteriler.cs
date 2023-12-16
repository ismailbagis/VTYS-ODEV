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
    
    public partial class Musteriler : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");
        public Musteriler()
        {
            InitializeComponent();
        }

        private void Musteriler_Load(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("select * from public.\"sirket\"", baglanti);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            comboBoxSirketMusteri.DisplayMember = "sirket_adi";
            comboBoxSirketMusteri.ValueMember = "sirket_id";
            comboBoxSirketMusteri.DataSource = dt7;
            baglanti.Close();
        }

        private void button_listele_Click(object sender, EventArgs e)
        {
            string sorgu = " select * from musteri ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridViewMusteri.DataSource = dt.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }


                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"musteri\" WHERE musteri_id = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", int.Parse(txtmusteriid.Text));

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount > 0)
                {
                    MessageBox.Show("Bu müşteri kaydı zaten mevcut.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                NpgsqlCommand komut = new NpgsqlCommand("insert into public.\"musteri\" values(@p1,@p2,@p3)", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(txtmusteriid.Text));
                komut.Parameters.AddWithValue("@p2", txtmusteriadi.Text);
                komut.Parameters.AddWithValue("@p3", int.Parse(comboBoxSirketMusteri.SelectedValue.ToString()));
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Müşteri kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantı açık değilse aç
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                NpgsqlCommand komut = new NpgsqlCommand("update public.\"musteri\" set \"musteri_adi\"=@p1,\"sirket_id\"=@p2 " +
                "where \"musteri_id\"=@p3", baglanti);
                komut.Parameters.AddWithValue("@p1", txtmusteriadi.Text);
                komut.Parameters.AddWithValue("@p2", int.Parse(comboBoxSirketMusteri.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p3", int.Parse(txtmusteriid.Text));
                komut.ExecuteNonQuery();
                MessageBox.Show("Müşteri güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"musteri\" WHERE musteri_id = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", int.Parse(txtmusteriid.Text));

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount == 0)
                {
                    MessageBox.Show("Müşteri mevcut değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bu müşteri kaydını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NpgsqlCommand komut2 = new NpgsqlCommand("delete from public.\"musteri\" where \"musteri_id\"=@p1", baglanti);
                    komut2.Parameters.AddWithValue("@p1", int.Parse(txtmusteriid.Text));
                    komut2.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Müşteri silindi.");
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

        private void dataGridViewMusteri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewMusteri.CurrentRow.Selected = true;
            DataGridViewRow selectedRow = dataGridViewMusteri.Rows[e.RowIndex];
            txtmusteriid.Text = dataGridViewMusteri.Rows[e.RowIndex].Cells["musteri_id"].Value.ToString();
            txtmusteriadi.Text = dataGridViewMusteri.Rows[e.RowIndex].Cells["musteri_adi"].Value.ToString();
            comboBoxSirketMusteri.SelectedValue = selectedRow.Cells["sirket_id"].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from public.\"musteri\" where \"musteri_adi\" ILIKE '%" + musteriAra.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewMusteri.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void dataGridViewMusteri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Musteriler_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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
    }
}
