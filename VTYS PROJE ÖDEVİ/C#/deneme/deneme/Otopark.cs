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
    public partial class Otopark : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");
        public Otopark()
        {
            InitializeComponent();
        }

        private void button_listele_Click(object sender, EventArgs e)
        {
            string sorgu = " select * from otopark ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridViewotopark.DataSource = dt.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

              
                NpgsqlCommand checkCommand2 = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"otopark\" WHERE otopark_id = @p4", baglanti);
                checkCommand2.Parameters.AddWithValue("@p4", int.Parse(txtotoparkid.Text));

                int existingCount2 = Convert.ToInt32(checkCommand2.ExecuteScalar());

                if (existingCount2 > 0)
                {
                    MessageBox.Show("Bu otopark zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }
                

                NpgsqlCommand komut = new NpgsqlCommand("insert into public.\"otopark\" values(@p1,@p2,@p3)", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(txtotoparkid.Text));
                komut.Parameters.AddWithValue("@p2", txtotoparkadi.Text);
                komut.Parameters.AddWithValue("@p3", int.Parse(comboBoxSirket.SelectedValue.ToString()));

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Otopark kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Otopark_Load(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("select * from public.\"sirket\"", baglanti);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            comboBoxSirket.DisplayMember = "sirket_adi";
            comboBoxSirket.ValueMember = "sirket_id";
            comboBoxSirket.DataSource = dt7;
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
           
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

            
                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"otopark\" WHERE otopark_id = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", txtotoparkid.Text);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount == 0)
                {
                    MessageBox.Show("Bu otopark bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

 
                DialogResult result = MessageBox.Show("Bu otopark kaydını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NpgsqlCommand komut = new NpgsqlCommand("delete from public.\"otopark\" where \"otopark_id\"=@p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", int.Parse(txtotoparkid.Text));
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show(" Otopark silme işlemi onaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
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

        private void txtotoparkid_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                NpgsqlCommand komut = new NpgsqlCommand("update public.\"otopark\" set \"otopark_adi\"=@p1, \"sirket_id\"=@p2 " +
                "where \"otopark_id\"=@p3", baglanti);
                komut.Parameters.AddWithValue("@p1", txtotoparkadi.Text);
                komut.Parameters.AddWithValue("@p2", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p3", int.Parse(txtotoparkid.Text));
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

        private void dataGridViewotopark_CellClick(object sender, DataGridViewCellEventArgs e)
        {




            dataGridViewotopark.CurrentRow.Selected = true;
            DataGridViewRow selectedRow = dataGridViewotopark.Rows[e.RowIndex];
            txtotoparkid.Text = dataGridViewotopark.Rows[e.RowIndex].Cells["otopark_id"].Value.ToString();
            txtotoparkadi.Text = dataGridViewotopark.Rows[e.RowIndex].Cells["otopark_adi"].Value.ToString();
            comboBoxSirket.SelectedValue = selectedRow.Cells["sirket_id"].Value;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from public.\"otopark\" where \"otopark_adi\" ILIKE '%" + otoparkAra.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewotopark.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewotopark_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Otopark_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Musteriler musterigecis = new Musteriler();
            musterigecis.Show();
            this.Hide();
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
