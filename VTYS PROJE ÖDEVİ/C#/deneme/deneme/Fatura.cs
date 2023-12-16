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
    public partial class Fatura : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");
        public Fatura()
        {
            InitializeComponent();
        }

        private void Fatura_Load(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("select * from public.\"musteri\"", baglanti);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            comboBoxmusteri.DisplayMember = "musteri_adi";
            comboBoxmusteri.ValueMember = "musteri_id";
            comboBoxmusteri.DataSource = dt7;
            baglanti.Close();
            NpgsqlDataAdapter da8 = new NpgsqlDataAdapter("select * from public.\"siparis\"", baglanti);
            DataTable dt8 = new DataTable();
            da8.Fill(dt8);
            comboBoxSiparis.DisplayMember = "siparis_bilgi";
            comboBoxSiparis.ValueMember = "siparis_id";
            comboBoxSiparis.DataSource = dt8;
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

 
                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"fatura\" WHERE fatura_id = @p1", baglanti);
                checkCommand.Parameters.AddWithValue("@p1", int.Parse(txtfaturaid.Text));

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount > 0)
                {
                    MessageBox.Show("Bu ID'ye sahip bir fatura zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO public.\"fatura\" VALUES (@p1, @p2, @p3, @p4,@p5,@p6)", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(txtfaturaid.Text));
                komut.Parameters.AddWithValue("@p2", int.Parse(txtfaturatutar.Text));
                komut.Parameters.AddWithValue("@p3", txtfaturaetiketi.Text);
                komut.Parameters.AddWithValue("@p4", txtfaturatarih.Text);
                komut.Parameters.AddWithValue("@p5", int.Parse(comboBoxmusteri.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p6", int.Parse(comboBoxSiparis.SelectedValue.ToString()));

                komut.ExecuteNonQuery();

                MessageBox.Show("Fatura kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string sorgu = " select * from fatura ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridViewFatura.DataSource = dt.Tables[0];
        }

        private void dataGridViewFatura_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewFatura.CurrentRow.Selected = true;
            DataGridViewRow selectedRow = dataGridViewFatura.Rows[e.RowIndex];
            comboBoxSiparis.SelectedValue = selectedRow.Cells["siparis_id"].Value;
            comboBoxmusteri.SelectedValue = selectedRow.Cells["musteri_id"].Value;
            txtfaturatutar.Text = dataGridViewFatura.Rows[e.RowIndex].Cells["fatura_tutari"].Value.ToString();
            txtfaturaetiketi.Text = dataGridViewFatura.Rows[e.RowIndex].Cells["fatura_etiket"].Value.ToString();
            txtfaturatarih.Text = dataGridViewFatura.Rows[e.RowIndex].Cells["fatura_tarihi"].Value.ToString();
            txtfaturaid.Text = dataGridViewFatura.Rows[e.RowIndex].Cells["fatura_id"].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

          
                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"fatura\" WHERE fatura_id = @p1", baglanti);
                checkCommand.Parameters.AddWithValue("@p1", int.Parse(txtfaturaid.Text));

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount == 0)
                {
                    MessageBox.Show("Bu ID'ye sahip bir fatura bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

          
                DialogResult result = MessageBox.Show("Bu fatura kaydını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
   
                    NpgsqlCommand deleteCommand = new NpgsqlCommand("DELETE FROM public.\"fatura\" WHERE fatura_id = @p1", baglanti);
                    deleteCommand.Parameters.AddWithValue("@p1", int.Parse(txtfaturaid.Text));
                    deleteCommand.ExecuteNonQuery();
                    MessageBox.Show("Fatura kaydı başarıyla silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
    
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                NpgsqlCommand komut = new NpgsqlCommand("update public.\"fatura\" set \"fatura_tutari\"=@p1,\"fatura_etiket\"=@p2,\"fatura_tarihi\"=@p3,\"musteri_id\"=@p4,\"siparis_id\"=@p5 " +
                    "where \"fatura_id\"=@p6", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(txtfaturatutar.Text));
                komut.Parameters.AddWithValue("@p2", txtfaturaetiketi.Text);
                komut.Parameters.AddWithValue("@p3", txtfaturatarih.Text);
                komut.Parameters.AddWithValue("@p4", int.Parse(comboBoxmusteri.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p5", int.Parse(comboBoxSiparis.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p6", int.Parse(txtfaturaid.Text));
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

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from public.\"fatura\" where \"fatura_etiket\" ILIKE '%" + faturaAra.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewFatura.DataSource = ds.Tables[0];
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

        private void Fatura_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxFatura_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
