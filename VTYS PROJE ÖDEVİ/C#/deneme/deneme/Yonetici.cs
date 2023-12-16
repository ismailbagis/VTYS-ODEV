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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace deneme
{
    public partial class Yonetici : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");
        public Yonetici()
        {
            InitializeComponent();
        }

        private void button_listele_Click(object sender, EventArgs e)
        {
            string sorgu = " select * from yonetici ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridView5.DataSource = dt.Tables[0];
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Yonetici_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"yonetici\" WHERE yonetici_adi = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", txtyoneticiadi.Text);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount > 0)
                {
                    MessageBox.Show("Bu Yönetici zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NpgsqlCommand komut = new NpgsqlCommand("insert into public.\"yonetici\" values(@p1,@p2)", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(txtyoneticiid.Text));
                komut.Parameters.AddWithValue("@p2", txtyoneticiadi.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yönetici kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                komut.ExecuteNonQuery();
                MessageBox.Show("Tır kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"yonetici\" WHERE yonetici_adi = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", txtyoneticiadi.Text);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount == 0)
                {
                    MessageBox.Show("Yönetici mevcut olmadığı için silinemiyor. \n var olan bir yöneticiyi silebilirsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bu yönetici kaydını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NpgsqlCommand komut = new NpgsqlCommand("delete from public.\"yonetici\" where \"yonetici_id\"=@p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", int.Parse(txtyoneticiid.Text));
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show(" silme işlemi onaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
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

                NpgsqlCommand komut = new NpgsqlCommand("update public.\"yonetici\" set \"yonetici_adi\"=@p1 " +
                "where \"yonetici_id\"=@p2", baglanti);
                komut.Parameters.AddWithValue("@p1", txtyoneticiadi.Text);
                komut.Parameters.AddWithValue("@p2", int.Parse(txtyoneticiid.Text));
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

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

       
            
                dataGridView5.CurrentRow.Selected = true;
                txtyoneticiid.Text = dataGridView5.Rows[e.RowIndex].Cells["yonetici_id"].Value.ToString();
                txtyoneticiadi.Text = dataGridView5.Rows[e.RowIndex].Cells["yonetici_adi"].Value.ToString();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            string sorgu = "select * from public.\"yonetici\" where \"yonetici_adi\" ILIKE '%" + yonetici_ara.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView5.DataSource = ds.Tables[0];
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

        private void Yonetici_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
