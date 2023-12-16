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
  
    public partial class Siparis : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");
        public Siparis()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }


                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"siparis\" WHERE siparis_id = @p1", baglanti);
                checkCommand.Parameters.AddWithValue("@p1", int.Parse(txtsiparisid.Text));

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount > 0)
                {
                    MessageBox.Show("Bu sipariş zaten oluşturulmuş .", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO public.\"siparis\" VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(txtsiparisid.Text));
                komut.Parameters.AddWithValue("@p2", int.Parse(comboBoxmusteri.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p3", int.Parse(comboBoxtir.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p4", int.Parse(txtsiparistutari.Text));
                komut.Parameters.AddWithValue("@p5", txtyukleme.Text);
                komut.Parameters.AddWithValue("@p6", txtbosaltma.Text);
                komut.Parameters.AddWithValue("@p7", txtsiparistarihi.Text);
                komut.Parameters.AddWithValue("@p8", txtsiparisbilgi.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Sipariş kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button_listele_Click(object sender, EventArgs e)
        {
            string sorgu = " select * from siparis ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridViewSiparis.DataSource = dt.Tables[0];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Siparis_Load(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("select * from public.\"musteri\"", baglanti);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            comboBoxmusteri.DisplayMember = "musteri_adi";
            comboBoxmusteri.ValueMember = "musteri_id";
            comboBoxmusteri.DataSource = dt7;
            baglanti.Close();
            NpgsqlDataAdapter da8 = new NpgsqlDataAdapter("select * from public.\"tir\"", baglanti);
            DataTable dt8 = new DataTable();
            da8.Fill(dt8);
            comboBoxtir.DisplayMember = "tir_plaka";
            comboBoxtir.ValueMember = "tir_id";
            comboBoxtir.DataSource = dt8;
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                NpgsqlCommand komut = new NpgsqlCommand("update public.\"siparis\" set \"musteri_id\"=@p1,\"tir_id\"=@p2,\"siparis_tutari\"=@p3,\"yukleme\"=@p4," +
                    " \"bosaltma\"=@p5,\"siparis_tarihi\"=@p6,\"siparis_bilgi\"=@p7 where \"siparis_id\"=@p8", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(comboBoxmusteri.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p2", int.Parse(comboBoxtir.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p3", int.Parse(txtsiparistutari.Text));
                komut.Parameters.AddWithValue("@p4", txtyukleme.Text);
                komut.Parameters.AddWithValue("@p5", txtbosaltma.Text);
                komut.Parameters.AddWithValue("@p6", txtsiparistarihi.Text);
                komut.Parameters.AddWithValue("@p7", txtsiparisbilgi.Text);
                komut.Parameters.AddWithValue("@p8", int.Parse(txtsiparisid.Text));
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void dataGridViewSiparis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewSiparis.CurrentRow.Selected = true;
            DataGridViewRow selectedRow = dataGridViewSiparis.Rows[e.RowIndex];
            txtsiparisid.Text = dataGridViewSiparis.Rows[e.RowIndex].Cells["siparis_id"].Value.ToString();
            txtsiparistarihi.Text = dataGridViewSiparis.Rows[e.RowIndex].Cells["siparis_tarihi"].Value.ToString();
            txtsiparistutari.Text = dataGridViewSiparis.Rows[e.RowIndex].Cells["siparis_tutari"].Value.ToString();
            txtbosaltma.Text = dataGridViewSiparis.Rows[e.RowIndex].Cells["bosaltma"].Value.ToString();
            txtyukleme.Text = dataGridViewSiparis.Rows[e.RowIndex].Cells["yukleme"].Value.ToString();
            txtsiparisbilgi.Text = dataGridViewSiparis.Rows[e.RowIndex].Cells["siparis_bilgi"].Value.ToString();
            comboBoxmusteri.SelectedValue = selectedRow.Cells["musteri_id"].Value;
            comboBoxtir.SelectedValue = selectedRow.Cells["tir_id"].Value;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"siparis\" WHERE siparis_id = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", int.Parse(txtsiparisid.Text));

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount == 0)
                {
                    MessageBox.Show("Sipariş bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                DialogResult result = MessageBox.Show("Bu sipariş kaydını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NpgsqlCommand deleteCommand = new NpgsqlCommand("DELETE FROM public.\"siparis\" WHERE siparis_id = @p4", baglanti);
                    deleteCommand.Parameters.AddWithValue("@p4", int.Parse(txtsiparisid.Text));
                    deleteCommand.ExecuteNonQuery();
                    MessageBox.Show("Sipariş kaydı başarıyla silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void comboBoxtir_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxFatura_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            string sorgu = "select * from public.\"siparis\" where \"siparis_bilgi\" ILIKE '%" + siparisAra.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewSiparis.DataSource = ds.Tables[0];
            baglanti.Close();
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

        private void Siparis_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("select public.\"siparis_say\"()", baglanti);
            NpgsqlDataReader read = komut2.ExecuteReader();
            while (read.Read())
                txttoplamsiparis.Text = read[0].ToString();

            baglanti.Close();
        }
    }
}
