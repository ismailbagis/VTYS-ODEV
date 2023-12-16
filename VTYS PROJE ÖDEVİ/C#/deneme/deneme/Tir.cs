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
    public partial class Tir : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");
        public Tir()
        {
            InitializeComponent();
        }

        private void Tir_Load(object sender, EventArgs e)
        {
            

            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("select * from public.\"sirket\"", baglanti);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            comboBoxSirketid.DisplayMember = "sirket_adi";
            comboBoxSirketid.ValueMember = "sirket_id";
            comboBoxSirketid.DataSource = dt7;
            baglanti.Close();

        }

        private void txtbosarac_TextChanged(object sender, EventArgs e)
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
                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"tir\" WHERE tir_plaka = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", txttirplaka.Text);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount > 0)
                {
                    MessageBox.Show("Bu plakaya sahip bir tır zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO public.\"tir\" VALUES(@p1,@p2,@p3,@p4,@p5)", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(txttirid.Text));
                komut.Parameters.AddWithValue("@p2", txttirmarka.Text);
                komut.Parameters.AddWithValue("@p3", txttirmodel.Text);
                komut.Parameters.AddWithValue("@p4", txttirplaka.Text);
                komut.Parameters.AddWithValue("@p5", int.Parse(comboBoxSirketid.SelectedValue.ToString()));
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


        private void button_listele_Click(object sender, EventArgs e)
        {
            string sorgu = " select * from tir ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridViewTir.DataSource = dt.Tables[0];
        }

        private void comboBoxmarka_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void comboBoxmodel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSirketid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e) 
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"tir\" WHERE tir_plaka = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", txttirplaka.Text);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount == 0)
                {
                    MessageBox.Show("Bu plakaya sahip bir tır bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult result = MessageBox.Show("Bu tır kaydını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NpgsqlCommand deleteCommand = new NpgsqlCommand("DELETE FROM public.\"tir\" WHERE tir_plaka = @p4", baglanti);
                    deleteCommand.Parameters.AddWithValue("@p4", txttirplaka.Text);
                    deleteCommand.ExecuteNonQuery();
                    MessageBox.Show("Tır kaydı başarıyla silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                NpgsqlCommand komut = new NpgsqlCommand("update public.\"tir\" set \"tir_marka\"=@p1,\"tir_model\"=@p2,\"tir_plaka\"=@p3,\"sirket_id\"=@p4 " +
                    "where \"tir_id\"=@p5", baglanti);
                komut.Parameters.AddWithValue("@p1", txttirmarka.Text);
                komut.Parameters.AddWithValue("@p2", txttirmodel.Text);
                komut.Parameters.AddWithValue("@p3", txttirplaka.Text);
                komut.Parameters.AddWithValue("@p4", int.Parse(comboBoxSirketid.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p5", int.Parse(txttirid.Text));
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
            string sorgu = "select * from public.\"tir\" where \"tir_marka\" ILIKE '%" + tirAra.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewTir.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void dataGridViewTir_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewTir.CurrentRow.Selected = true;
            DataGridViewRow selectedRow = dataGridViewTir.Rows[e.RowIndex];
            txttirid.Text = dataGridViewTir.Rows[e.RowIndex].Cells["tir_id"].Value.ToString();
            txttirmarka.Text = dataGridViewTir.Rows[e.RowIndex].Cells["tir_marka"].Value.ToString();
            txttirmodel.Text = dataGridViewTir.Rows[e.RowIndex].Cells["tir_model"].Value.ToString();
            txttirplaka.Text = dataGridViewTir.Rows[e.RowIndex].Cells["tir_plaka"].Value.ToString();
            comboBoxSirketid.SelectedValue = selectedRow.Cells["sirket_id"].Value;
        }

        private void dataGridViewTir_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void Tir_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
