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
    public partial class Sirket : Form
    {
        public Sirket()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");

        private void Sirket_Load(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("select * from public.\"yonetici\"", baglanti);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            comboBoxYonetici.DisplayMember = "yonetici_adi";
            comboBoxYonetici.ValueMember = "yonetici_id";
            comboBoxYonetici.DataSource = dt7;
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_listele_Click(object sender, EventArgs e)
        {
            string sorgu = " select * from sirket ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridViewSirket.DataSource = dt.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

  
                NpgsqlCommand checkCommand2 = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"sirket\" WHERE sirket_id = @p4", baglanti);
                checkCommand2.Parameters.AddWithValue("@p4", int.Parse(txtsirketid.Text));

                int existingCount2 = Convert.ToInt32(checkCommand2.ExecuteScalar());

                if (existingCount2 > 0)
                {
                    MessageBox.Show("Bu şirket zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"sirket\" WHERE sirket_adi = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", txtsirketadi.Text);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount > 0)
                {
                    MessageBox.Show("Bu şirket zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                NpgsqlCommand komut = new NpgsqlCommand("insert into public.\"sirket\" values(@p1,@p2,@p3)", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(txtsirketid.Text));
                komut.Parameters.AddWithValue("@p2", txtsirketadi.Text);
                komut.Parameters.AddWithValue("@p3", int.Parse(comboBoxYonetici.SelectedValue.ToString()));
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Şirket kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridViewSirket_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            dataGridViewSirket.CurrentRow.Selected = true;
            DataGridViewRow selectedRow = dataGridViewSirket.Rows[e.RowIndex];
            txtsirketid.Text = dataGridViewSirket.Rows[e.RowIndex].Cells["sirket_id"].Value.ToString();
            txtsirketadi.Text = dataGridViewSirket.Rows[e.RowIndex].Cells["sirket_adi"].Value.ToString();
            texttoplamsube.Text = dataGridViewSirket.Rows[e.RowIndex].Cells["sube_sayisi"].Value.ToString();
            txtotoparksayisi.Text = dataGridViewSirket.Rows[e.RowIndex].Cells["otopark_sayisi"].Value.ToString();
            comboBoxYonetici.SelectedValue = selectedRow.Cells["yonetici_id"].Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"sirket\" WHERE sirket_adi = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", txtsirketadi.Text);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount == 0)
                {
                    MessageBox.Show("Şirket mevcut olmadığı için silinemiyor. \n var olan bir şirketi silebilirsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bu şirket kaydını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NpgsqlCommand komut = new NpgsqlCommand("delete from public.\"sirket\" where \"sirket_id\"=@p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", int.Parse(txtsirketid.Text));
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Şirket başarıyla silindi");
                }
                if (result == DialogResult.No)
                {
                    MessageBox.Show("Silme işlemi iptal edildi.");
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

                NpgsqlCommand komut = new NpgsqlCommand("update public.\"sirket\" set \"sirket_adi\"=@p1,\"yonetici_id\"=@p2 " +
                "where \"sirket_id\"=@p3", baglanti);
                komut.Parameters.AddWithValue("@p1", txtsirketadi.Text);
                komut.Parameters.AddWithValue("@p2", int.Parse(comboBoxYonetici.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p3", int.Parse(txtsirketid.Text));
                komut.ExecuteNonQuery();
                MessageBox.Show(" güncelleme işlemi başarılı oldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            string sirketAraKelime = sirketara.Text.Trim();
            string sorgu = "SELECT * FROM public.\"sirket\" WHERE \"sirket_adi\" ILIKE '%' || @kelime || '%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);


            da.SelectCommand.Parameters.AddWithValue("@kelime", sirketAraKelime);

            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewSirket.DataSource = ds.Tables[0];
            baglanti.Close();

        }

        private void sirketara_TextChanged(object sender, EventArgs e)
        {

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
            Sube subegecis=new Sube();
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

        private void button5_Click(object sender, EventArgs e)
        {
            Sirket sirket = new Sirket();   
            sirket.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AdminPanel admin = new AdminPanel();
            admin.Show();
            this.Hide();
        }

        private void Sirket_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
