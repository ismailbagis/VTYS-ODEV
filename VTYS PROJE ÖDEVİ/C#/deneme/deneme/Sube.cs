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
    public partial class Sube : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");
        public Sube()
        {
            InitializeComponent();
        }

        private void button_listele_Click(object sender, EventArgs e)
        {
            string sorgu = " select * from sube ";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dt = new DataSet();

            da.Fill(dt);
            dataGridViewSube.DataSource = dt.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                NpgsqlCommand checkCommand2 = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"sube\" WHERE sube_id = @p4", baglanti);
                checkCommand2.Parameters.AddWithValue("@p4", int.Parse(txtsubeid.Text));

                int existingCount2 = Convert.ToInt32(checkCommand2.ExecuteScalar());

                if (existingCount2 > 0)
                {
                    MessageBox.Show("Bu şube zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }
                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"sube\" WHERE sube_adi = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", txtsubeadi.Text);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount > 0)
                {
                    MessageBox.Show("Bu şube zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                NpgsqlCommand komut = new NpgsqlCommand("insert into public.\"sube\" values(@p1,@p2,@p3)", baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(txtsubeid.Text));
                komut.Parameters.AddWithValue("@p2", txtsubeadi.Text);
                komut.Parameters.AddWithValue("@p3", int.Parse(comboBoxSirketsube.SelectedValue.ToString()));

                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Şube kaydı başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                baglanti.Close();
            }
            baglanti.Open();
            

        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                NpgsqlCommand komut = new NpgsqlCommand("update public.\"sube\" set \"sube_adi\"=@p1, \"sirket_id\"=@p2 " +
                "where \"sube_id\"=@p3", baglanti);
                komut.Parameters.AddWithValue("@p1", txtsubeadi.Text);
                komut.Parameters.AddWithValue("@p2", int.Parse(comboBoxSirketsube.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@p3", int.Parse(txtsubeid.Text));
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }

                NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"sube\" WHERE sube_adi = @p4", baglanti);
                checkCommand.Parameters.AddWithValue("@p4", txtsubeadi.Text);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingCount == 0)
                {
                    MessageBox.Show("Şube mevcut olmadığı için silinemiyor. \n var olan bir şubeyi silebilirsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Bu şube kaydını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NpgsqlCommand komut = new NpgsqlCommand("delete from public.\"sube\" where \"sube_id\"=@p1", baglanti);
                    komut.Parameters.AddWithValue("@p1", int.Parse(txtsubeid.Text));
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show(" şube silme işlemi onaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                }
                if(result == DialogResult.No) 
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

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "select * from public.\"sube\" where \"sube_adi\" LIKE '%" + subeara.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewSube.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void Sube_Load(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("select * from public.\"sirket\"", baglanti);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            comboBoxSirketsube.DisplayMember = "sirket_adi";
            comboBoxSirketsube.ValueMember = "sirket_id";
            comboBoxSirketsube.DataSource = dt7;
            baglanti.Close();
        }

        private void dataGridViewSube_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            dataGridViewSube.CurrentRow.Selected = true;
            DataGridViewRow selectedRow = dataGridViewSube.Rows[e.RowIndex];
            txtsubeid.Text = dataGridViewSube.Rows[e.RowIndex].Cells["sube_id"].Value.ToString();
            txtsubeadi.Text = dataGridViewSube.Rows[e.RowIndex].Cells["sube_adi"].Value.ToString();
            comboBoxSirketsube.SelectedValue = selectedRow.Cells["sirket_id"].Value;
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

        private void Sube_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
