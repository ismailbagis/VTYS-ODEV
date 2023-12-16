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
    public partial class Calisan : Form
    {

        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=DENE2; user Id=postgres; password=12345");
        public Calisan()
        {
            InitializeComponent();
            comboBox7.Items.Add("HEPSİ");
            comboBox7.Items.Add("TAMİRCİLER");
            comboBox7.Items.Add("TEMİZLİK ELEMANLARI");
            comboBox7.Items.Add("OFİS MEMURLARI");
            comboBox7.Items.Add("ŞOFÖRLER");
            comboBox7.Items.Add("MÜDÜRLER");
            comboBox7.SelectedIndex = 0;

        }

        private void button_listele_Click(object sender, EventArgs e)
        {

            if (comboBox7.SelectedItem.ToString() == "HEPSİ")
            {
                baglanti.Open();
                string sorgu = "select   * from public.\"calisan\"";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridViewCalisan.DataSource = ds.Tables[0];
                baglanti.Close();

            }
            if (comboBox7.SelectedItem.ToString() == "TAMİRCİLER")
            {
                baglanti.Open();
                string sorgu = "select  * from public.\"calisan\"";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridViewCalisan.DataSource = ds.Tables[0];
                string sorgu2 = "select  * from public.\"tamirci\"";
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sorgu2, baglanti);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                dataGridViewCalisan.DataSource = ds2.Tables[0];
                baglanti.Close();

            }
            if (comboBox7.SelectedItem.ToString() == "TEMİZLİK ELEMANLARI")
            {
                baglanti.Open();
                string sorgu = "select  * from public.\"calisan\"";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridViewCalisan.DataSource = ds.Tables[0];
                string sorgu2 = "select  * from public.\"temizlikelemani\"";
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sorgu2, baglanti);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                dataGridViewCalisan.DataSource = ds2.Tables[0];
                baglanti.Close();

            }
            if (comboBox7.SelectedItem.ToString() == "OFİS MEMURLARI")
            {
                baglanti.Open();
                string sorgu = "select  * from public.\"calisan\"";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridViewCalisan.DataSource = ds.Tables[0];
                string sorgu2 = "select  * from \"ofismemuru\"";
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sorgu2, baglanti);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                dataGridViewCalisan.DataSource = ds2.Tables[0];
                baglanti.Close();

            }
            if (comboBox7.SelectedItem.ToString() == "ŞOFÖRLER")
            {
                baglanti.Open();
                string sorgu = "select  * from public.\"calisan\"";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridViewCalisan.DataSource = ds.Tables[0];
                string sorgu2 = "select  * from \"sofor\"";
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sorgu2, baglanti);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                dataGridViewCalisan.DataSource = ds2.Tables[0];
                baglanti.Close();

            }
            if (comboBox7.SelectedItem.ToString() == "MÜDÜRLER")
            {
                baglanti.Open();
                string sorgu = "select  * from public.\"calisan\"";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridViewCalisan.DataSource = ds.Tables[0];
                string sorgu2 = "select  * from \"mudur\"";
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sorgu2, baglanti);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                dataGridViewCalisan.DataSource = ds2.Tables[0];
                baglanti.Close();

            }

        }

        private void Calisan_Load(object sender, EventArgs e)
        {
            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter("select * from public.\"tir\"", baglanti);
            DataTable dt7 = new DataTable();
            da7.Fill(dt7);
            comboBoxTirid.DisplayMember = "tir_marka";
            comboBoxTirid.ValueMember = "tir_id";
            comboBoxTirid.DataSource = dt7;
            baglanti.Close();
           
            NpgsqlDataAdapter da8 = new NpgsqlDataAdapter("select * from public.\"sirket\"", baglanti);
            DataTable dt8 = new DataTable();
            da8.Fill(dt8);
            comboBoxSirket.DisplayMember = "sirket_adi";
            comboBoxSirket.ValueMember = "sirket_id";
            comboBoxSirket.DataSource = dt8;
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sirket sirket = new Sirket();
            sirket.Show();
            this.Hide();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (comboBox7.SelectedItem.ToString() == "HEPSİ")
                {

                

                try
                {
                    NpgsqlCommand komut2 = new NpgsqlCommand("insert into public.\"tamirci\" values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
                    komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut2.Parameters.AddWithValue("@p2", txtcalisanadi.Text);
                    komut2.Parameters.AddWithValue("@p3", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                    komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisanyas.Text));
                    komut2.Parameters.AddWithValue("@p5", int.Parse(txtcalisankilo.Text));
               
                    komut2.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch 
                {
                    MessageBox.Show("\"HEPSİ\" SEÇİLİYKEN EKELEME YAPAMAZSINIZ. \n LÜTFEN VİR KATEGÖRİ SEÇİNİZ");
                }
                }
                if (comboBox7.SelectedItem.ToString() == "TAMİRCİLER")
                {
                    baglanti.Open();

                    NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p1", baglanti);
                    checkCommand.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount > 0)
                    {
                        MessageBox.Show("Bu ID'ye sahip biri zaten bulunuyor..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                    }
                    else    {
                        NpgsqlCommand komut2 = new NpgsqlCommand("insert into public.\"tamirci\" values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
                        komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                        komut2.Parameters.AddWithValue("@p2", txtcalisanadi.Text);
                        komut2.Parameters.AddWithValue("@p3", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisanyas.Text));
                        komut2.Parameters.AddWithValue("@p5", int.Parse(txtcalisankilo.Text));
                        komut2.Parameters.AddWithValue("@p6", int.Parse(calisanmaas.Text));
                        komut2.Parameters.AddWithValue("@p7", izingun.Text);
                        komut2.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    
                }
                else if (comboBox7.SelectedItem.ToString() == "ŞOFÖRLER")
                {


                    

                    baglanti.Open();
                    NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p1", baglanti);
                    checkCommand.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount > 0)
                    {
                        MessageBox.Show("Bu ID'ye sahip biri zaten bulunuyor..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        NpgsqlCommand komut2 = new NpgsqlCommand("insert into public.\"sofor\" values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglanti);
                        komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                        komut2.Parameters.AddWithValue("@p2", txtcalisanadi.Text);
                        komut2.Parameters.AddWithValue("@p3", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisanyas.Text));
                        komut2.Parameters.AddWithValue("@p5", int.Parse(txtcalisankilo.Text));
                        komut2.Parameters.AddWithValue("@p6", int.Parse(comboBoxTirid.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p7", int.Parse(calisanmaas.Text));
                        komut2.Parameters.AddWithValue("@p8", izingun.Text);

                        komut2.ExecuteNonQuery();

                        MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }


                    
                }
                else if (comboBox7.SelectedItem.ToString() == "TEMİZLİK ELEMANLARI")
                {
                    baglanti.Open();
                    
                    NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p1", baglanti);
                    checkCommand.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount > 0)
                    {
                        MessageBox.Show("Bu ID'ye sahip biri zaten bulunuyor..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        NpgsqlCommand komut2 = new NpgsqlCommand("insert into public.\"temizlikelemani\" values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
                        komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                        komut2.Parameters.AddWithValue("@p2", txtcalisanadi.Text);
                        komut2.Parameters.AddWithValue("@p3", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisanyas.Text));
                        komut2.Parameters.AddWithValue("@p5", int.Parse(txtcalisankilo.Text));
                        komut2.Parameters.AddWithValue("@p6", int.Parse(calisanmaas.Text));
                        komut2.Parameters.AddWithValue("@p7", izingun.Text);
                        komut2.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                if (comboBox7.SelectedItem.ToString() == "OFİS MEMURLARI")
                {
                    

                    baglanti.Open();
 
                    NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p1", baglanti);
                    checkCommand.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount > 0)
                    {
                        MessageBox.Show("Bu ID'ye sahip biri zaten bulunuyor..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        NpgsqlCommand komut2 = new NpgsqlCommand("insert into public.\"ofismemuru\" values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
                        komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                        komut2.Parameters.AddWithValue("@p2", txtcalisanadi.Text);
                        komut2.Parameters.AddWithValue("@p3", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisanyas.Text));
                        komut2.Parameters.AddWithValue("@p5", int.Parse(txtcalisankilo.Text));
                        komut2.Parameters.AddWithValue("@p6", int.Parse(calisanmaas.Text));
                        komut2.Parameters.AddWithValue("@p7", izingun.Text);
                        komut2.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                if (comboBox7.SelectedItem.ToString() == "MÜDÜRLER")
                {
                    baglanti.Open();
                   
                    NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p1", baglanti);
                    checkCommand.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount > 0)
                    {
                        MessageBox.Show("Bu ID'ye sahip biri zaten bulunuyor..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        NpgsqlCommand komut2 = new NpgsqlCommand("insert into public.\"mudur\" values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglanti);
                        komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                        komut2.Parameters.AddWithValue("@p2", txtcalisanadi.Text);
                        komut2.Parameters.AddWithValue("@p3", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisanyas.Text));
                        komut2.Parameters.AddWithValue("@p5", int.Parse(txtcalisankilo.Text));
                        komut2.Parameters.AddWithValue("@p6", int.Parse(calisanmaas.Text));
                        komut2.Parameters.AddWithValue("@p7", izingun.Text);
                        komut2.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarıyla oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
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

        private void txtaractipi_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox7.SelectedItem.ToString() == "HEPSİ")
                {
                    baglanti.Open();
                    NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p4", baglanti);
                    checkCommand.Parameters.AddWithValue("@p4", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount == 0)
                    {
                        MessageBox.Show("Silmeye çalıştığınız kişi mevcut değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; 
                    }

                    NpgsqlCommand komut2 = new NpgsqlCommand("delete from public.\"calisan\" where \"calisan_id\"=@p1;", baglanti);
                    komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut2.ExecuteNonQuery();

                    baglanti.Close();

    
                    DialogResult result = MessageBox.Show("Silme işlemi onaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show("Silme işlemi Başarılı !!!");
                    }
                }
                else if (comboBox7.SelectedItem.ToString() == "TAMİRCİLER")
                {
                    NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p4", baglanti);
                    checkCommand.Parameters.AddWithValue("@p4", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount == 0)
                    {
                        MessageBox.Show("Silmeye çalıştığınız kişi mevcut değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; 
                    }
                    baglanti.Open();

                    NpgsqlCommand komut = new NpgsqlCommand("delete from public.\"tamirci\" where \"calisan_id\"=@p1;", baglanti);
                    komut.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut.ExecuteNonQuery();

                    NpgsqlCommand komut2 = new NpgsqlCommand("delete from public.\"calisan\" where \"calisan_id\"=@p1;", baglanti);
                    komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut2.ExecuteNonQuery();

                    baglanti.Close();

              
                    DialogResult result = MessageBox.Show("Silme işlemi onaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show("Silme işlemi Başarılı !!!");
                    }
                }
                else if (comboBox7.SelectedItem.ToString() == "ŞOFÖRLER")
                {
                    NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p4", baglanti);
                    checkCommand.Parameters.AddWithValue("@p4", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount == 0)
                    {
                        MessageBox.Show("Silmeye çalıştığınız kişi mevcut değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    baglanti.Open();

                    NpgsqlCommand komut = new NpgsqlCommand("delete from public.\"sofor\" where \"calisan_id\"=@p1;", baglanti);
                    komut.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut.ExecuteNonQuery();

                    NpgsqlCommand komut2 = new NpgsqlCommand("delete from public.\"calisan\" where \"calisan_id\"=@p1;", baglanti);
                    komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut2.ExecuteNonQuery();

                    baglanti.Close();

  
                    DialogResult result = MessageBox.Show("Silme işlemi onaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show("Silme işlemi Başarılı !!!");
                    }
                }
                else if (comboBox7.SelectedItem.ToString() == "TEMİZLİK ELEMANLARI")
                {
                    NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p4", baglanti);
                    checkCommand.Parameters.AddWithValue("@p4", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount == 0)
                    {
                        MessageBox.Show("Silmeye çalıştığınız kişi mevcut değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; 
                    }
                    baglanti.Open();

                    NpgsqlCommand komut = new NpgsqlCommand("delete from public.\"temizlikelemani\" where \"calisan_id\"=@p1;", baglanti);
                    komut.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut.ExecuteNonQuery();

                    NpgsqlCommand komut2 = new NpgsqlCommand("delete from public.\"calisan\" where \"calisan_id\"=@p1;", baglanti);
                    komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut2.ExecuteNonQuery();

                    baglanti.Close();

 
                    DialogResult result = MessageBox.Show("Silme işlemi onaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show("Silme işlemi Başarılı !!!");
                    }
                }
                else if (comboBox7.SelectedItem.ToString() == "OFİS MEMURLARI")
                {
                      NpgsqlCommand checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM public.\"calisan\" WHERE calisan_id = @p4", baglanti);
                    checkCommand.Parameters.AddWithValue("@p4", int.Parse(txtcalisanid.Text));

                    int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (existingCount == 0)
                    {
                        MessageBox.Show("Silmeye çalıştığınız kişi mevcut değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; 
                    }
                    baglanti.Open();

                    NpgsqlCommand komut = new NpgsqlCommand("delete from public.\"ofismemuru\" where \"calisan_id\"=@p1;", baglanti);
                    komut.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut.ExecuteNonQuery();

                    NpgsqlCommand komut2 = new NpgsqlCommand("delete from public.\"calisan\" where \"calisan_id\"=@p1;", baglanti);
                    komut2.Parameters.AddWithValue("@p1", int.Parse(txtcalisanid.Text));
                    komut2.ExecuteNonQuery();

                    baglanti.Close();


                    DialogResult result = MessageBox.Show("Silme işlemi onaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show("Silme işlemi Başarılı !!!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox7.SelectedItem.ToString() == "HEPSİ")
                {



                    try
                    {
                        NpgsqlCommand komut4 = new NpgsqlCommand("UPDATE public.\"calisan\" SET \"calisan_adi\"=@p1, \"sirket_id\"=@p2, \"yas\"=@p3, \"kilo\"=@p4  " +
                               "WHERE \"calisan_id\"=@p5", baglanti);
                        komut4.Parameters.AddWithValue("@p1", txtcalisanadi.Text);
                        komut4.Parameters.AddWithValue("@p2", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut4.Parameters.AddWithValue("@p3", int.Parse(txtcalisanyas.Text));
                        komut4.Parameters.AddWithValue("@p4", int.Parse(txtcalisankilo.Text));
                        komut4.Parameters.AddWithValue("@p5", int.Parse(txtcalisanid.Text));
                    }
                    catch
                    {
                        MessageBox.Show("\"HEPSİ\" SEÇİLİYKEN güncelleme YAPAMAZSINIZ. \n LÜTFEN VİR KATEGÖRİ SEÇİNİZ");
                    }
                }
                if (comboBox7.SelectedItem.ToString() == "ŞOFÖRLER")
                {
                    try
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }

                        NpgsqlCommand komut4 = new NpgsqlCommand("UPDATE public.\"sofor\" SET \"calisan_adi\"=@p1, \"sirket_id\"=@p2, \"yas\"=@p3, \"kilo\"=@p4 ,\"sofor_maas\"=@p5 ,\"sofor_izin\"=@p6 ,\"tir_id\"=@p7 " +
                             "WHERE \"calisan_id\"=@p8", baglanti);
                        komut4.Parameters.AddWithValue("@p1", txtcalisanadi.Text);
                        komut4.Parameters.AddWithValue("@p2", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut4.Parameters.AddWithValue("@p3", int.Parse(txtcalisanyas.Text));
                        komut4.Parameters.AddWithValue("@p4", int.Parse(txtcalisankilo.Text));
                        komut4.Parameters.AddWithValue("@p5", int.Parse(calisanmaas.Text));
                        komut4.Parameters.AddWithValue("@p6", izingun.Text);
                        komut4.Parameters.AddWithValue("@p7", int.Parse(comboBoxTirid.SelectedValue.ToString()));
                        komut4.Parameters.AddWithValue("@p8", int.Parse(txtcalisanid.Text));
                        komut4.ExecuteNonQuery();
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


                if (comboBox7.SelectedItem.ToString() == "TAMİRCİLER")
                {
                    try
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }

                        NpgsqlCommand komut2 = new NpgsqlCommand("update public.\"tamirci\" set \"calisan_adi\"=@p1, \"sirket_id\"=@p2, \"yas\"=@p3, \"kilo\"=@p4 ," +
                    "\"tamir_maas\"=@p5 ,\"tamir_izin\"=@p6 where \"calisan_id\"=@p7", baglanti);
                        komut2.Parameters.AddWithValue("@p1", txtcalisanadi.Text);
                        komut2.Parameters.AddWithValue("@p2", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p3", int.Parse(txtcalisanyas.Text));
                        komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisankilo.Text));
                        komut2.Parameters.AddWithValue("@p5", int.Parse(calisanmaas.Text));
                        komut2.Parameters.AddWithValue("@p6", izingun.Text);
                        komut2.Parameters.AddWithValue("@p7", int.Parse(txtcalisanid.Text));
                        komut2.ExecuteNonQuery();
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
                if (comboBox7.SelectedItem.ToString() == "TEMİZLİK ELEMANLARI")
                {
                    try
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }

                        NpgsqlCommand komut2 = new NpgsqlCommand("update public.\"temizlikelemani\" set \"calisan_adi\"=@p1, \"sirket_id\"=@p2, \"yas\"=@p3, \"kilo\"=@p4 ," +
                        "\"temizlik_elemani_maas\"=@p5 ,\"temizlik_elemani_izin\"=@p6 where \"calisan_id\"=@p7", baglanti);
                        komut2.Parameters.AddWithValue("@p1", txtcalisanadi.Text);
                        komut2.Parameters.AddWithValue("@p2", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p3", int.Parse(txtcalisanyas.Text));
                        komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisankilo.Text));
                        komut2.Parameters.AddWithValue("@p5", int.Parse(calisanmaas.Text));
                        komut2.Parameters.AddWithValue("@p6", izingun.Text);
                        komut2.Parameters.AddWithValue("@p7", int.Parse(txtcalisanid.Text));
                        komut2.ExecuteNonQuery();
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
                if (comboBox7.SelectedItem.ToString() == "OFİS MEMURLARI")
                {
                    try
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }

                        NpgsqlCommand komut2 = new NpgsqlCommand("update public.\"ofismemuru\" set \"calisan_adi\"=@p1, \"sirket_id\"=@p2, \"yas\"=@p3, \"kilo\"=@p4 ," +
                        "\"ofis_maas\"=@p5 ,\"ofis_izin\"=@p6 where \"calisan_id\"=@p7", baglanti);
                        komut2.Parameters.AddWithValue("@p1", txtcalisanadi.Text);
                        komut2.Parameters.AddWithValue("@p2", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p3", int.Parse(txtcalisanyas.Text));
                        komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisankilo.Text));
                        komut2.Parameters.AddWithValue("@p5", int.Parse(calisanmaas.Text));
                        komut2.Parameters.AddWithValue("@p6", izingun.Text);
                        komut2.Parameters.AddWithValue("@p7", int.Parse(txtcalisanid.Text));
                        komut2.ExecuteNonQuery();
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
                if (comboBox7.SelectedItem.ToString() == "MÜDÜRLER")
                {
                    try
                    {
                        if (baglanti.State == ConnectionState.Closed)
                        {
                            baglanti.Open();
                        }

                        NpgsqlCommand komut2 = new NpgsqlCommand("update public.\"mudur\" set \"calisan_adi\"=@p1, \"sirket_id\"=@p2, \"yas\"=@p3, \"kilo\"=@p4 ," +
                        "\"mudur_maas\"=@p5 ,\"mudur_izin\"=@p6 where \"calisan_id\"=@p7", baglanti);
                        komut2.Parameters.AddWithValue("@p1", txtcalisanadi.Text);
                        komut2.Parameters.AddWithValue("@p2", int.Parse(comboBoxSirket.SelectedValue.ToString()));
                        komut2.Parameters.AddWithValue("@p3", int.Parse(txtcalisanyas.Text));
                        komut2.Parameters.AddWithValue("@p4", int.Parse(txtcalisankilo.Text));
                        komut2.Parameters.AddWithValue("@p5", int.Parse(calisanmaas.Text));
                        komut2.Parameters.AddWithValue("@p6", izingun.Text);
                        komut2.Parameters.AddWithValue("@p7", int.Parse(txtcalisanid.Text));
                        komut2.ExecuteNonQuery();
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

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (comboBox7.SelectedItem.ToString() == "HEPSİ")
            {
                dataGridViewCalisan.CurrentRow.Selected = true;
                DataGridViewRow selectedRow = dataGridViewCalisan.Rows[e.RowIndex];
                txtcalisanid.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_id"].Value.ToString();
                txtcalisanadi.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_adi"].Value.ToString();
                comboBoxSirket.SelectedValue = selectedRow.Cells["sirket_id"].Value;
                txtcalisanyas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["yas"].Value.ToString();
                txtcalisankilo.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["kilo"].Value.ToString();


            }

            if (comboBox7.SelectedItem.ToString() == "TAMİRCİLER")
            {
                dataGridViewCalisan.CurrentRow.Selected = true;
                DataGridViewRow selectedRow = dataGridViewCalisan.Rows[e.RowIndex];
                txtcalisanid.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_id"].Value.ToString();
                txtcalisanadi.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_adi"].Value.ToString();
                comboBoxSirket.SelectedValue = selectedRow.Cells["sirket_id"].Value;
                txtcalisanyas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["yas"].Value.ToString();
                txtcalisankilo.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["kilo"].Value.ToString();
                calisanmaas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["tamir_maas"].Value.ToString();
                izingun.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["tamir_izin"].Value.ToString();
              

            }

            if (comboBox7.SelectedItem.ToString() == "ŞOFÖRLER")
            {
                dataGridViewCalisan.CurrentRow.Selected = true;
                DataGridViewRow selectedRow = dataGridViewCalisan.Rows[e.RowIndex];
                txtcalisanid.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_id"].Value.ToString();
                txtcalisanadi.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_adi"].Value.ToString();
                comboBoxSirket.SelectedValue = selectedRow.Cells["sirket_id"].Value;
                txtcalisanyas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["yas"].Value.ToString();
                txtcalisankilo.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["kilo"].Value.ToString();
                calisanmaas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["sofor_maas"].Value.ToString();
                izingun.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["sofor_izin"].Value.ToString();
                comboBoxTirid.SelectedValue = selectedRow.Cells["tir_id"].Value;
            }
            if (comboBox7.SelectedItem.ToString() == "TEMİZLİK ELEMANLARI")
            {
                dataGridViewCalisan.CurrentRow.Selected = true;
                DataGridViewRow selectedRow = dataGridViewCalisan.Rows[e.RowIndex];
                txtcalisanid.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_id"].Value.ToString();
                txtcalisanadi.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_adi"].Value.ToString();
                comboBoxSirket.SelectedValue = selectedRow.Cells["sirket_id"].Value;
                txtcalisanyas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["yas"].Value.ToString();
                txtcalisankilo.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["kilo"].Value.ToString();
                calisanmaas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["temizlik_elemani_maas"].Value.ToString();
                izingun.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["temizlik_elemani_izin"].Value.ToString();

            }
            if (comboBox7.SelectedItem.ToString() == "OFİS MEMURLARI")
            {
                dataGridViewCalisan.CurrentRow.Selected = true;
                DataGridViewRow selectedRow = dataGridViewCalisan.Rows[e.RowIndex];
                txtcalisanid.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_id"].Value.ToString();
                txtcalisanadi.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_adi"].Value.ToString();
                comboBoxSirket.SelectedValue = selectedRow.Cells["sirket_id"].Value;
                txtcalisanyas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["yas"].Value.ToString();
                txtcalisankilo.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["kilo"].Value.ToString();
                calisanmaas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["ofis_maas"].Value.ToString();
                izingun.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["ofis_izin"].Value.ToString();

            }
            if (comboBox7.SelectedItem.ToString() == "MÜDÜRLER")
            {
                dataGridViewCalisan.CurrentRow.Selected = true;
                DataGridViewRow selectedRow = dataGridViewCalisan.Rows[e.RowIndex];
                txtcalisanid.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_id"].Value.ToString();
                txtcalisanadi.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["calisan_adi"].Value.ToString();
                comboBoxSirket.SelectedValue = selectedRow.Cells["sirket_id"].Value;
                txtcalisanyas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["yas"].Value.ToString();
                txtcalisankilo.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["kilo"].Value.ToString();
                calisanmaas.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["mudur_maas"].Value.ToString();
                izingun.Text = dataGridViewCalisan.Rows[e.RowIndex].Cells["mudur_izin"].Value.ToString();

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string calisanAraKelime = calisan_ara.Text.Trim();
            string sorgu = "SELECT  * FROM public.\"calisan\" WHERE \"calisan_adi\" ILIKE '%' || @kelime || '%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);

            da.SelectCommand.Parameters.AddWithValue("@kelime", calisanAraKelime);

            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridViewCalisan.DataSource = ds.Tables[0];
            baglanti.Close();


        }

        private void comboBoxTirid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void calisan_ara_TextChanged(object sender, EventArgs e)
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

        private void Calisan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void comboBox7_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedItem.ToString() == "HEPSİ")
            {
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("select public.\"calisan_say\"()", baglanti);
                NpgsqlDataReader read = komut2.ExecuteReader();
                while (read.Read())
                    txttoplamcalisan.Text = read[0].ToString();

                baglanti.Close();
            }
            else if (comboBox7.SelectedItem.ToString() == "MÜDÜRLER")
            {
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("select public.\"mudur_say\"()", baglanti);
                NpgsqlDataReader read = komut2.ExecuteReader();
                while (read.Read())
                    txttoplamcalisan.Text = read[0].ToString();

                baglanti.Close();
            }
            else if (comboBox7.SelectedItem.ToString() == "ŞOFÖRLER")
            {
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("select public.\"sofor_say\"()", baglanti);
                NpgsqlDataReader read = komut2.ExecuteReader();
                while (read.Read())
                    txttoplamcalisan.Text = read[0].ToString();

                baglanti.Close();
            }
            else if (comboBox7.SelectedItem.ToString() == "TAMİRCİLER")
            {
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("select public.\"tamirci_say\"()", baglanti);
                NpgsqlDataReader read = komut2.ExecuteReader();
                while (read.Read())
                    txttoplamcalisan.Text = read[0].ToString();

                baglanti.Close();
            }
            else if (comboBox7.SelectedItem.ToString() == "TEMİZLİK ELEMANLARI")
            {
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("select public.\"temizlikelemani_say\"()", baglanti);
                NpgsqlDataReader read = komut2.ExecuteReader();
                while (read.Read())
                    txttoplamcalisan.Text = read[0].ToString();

                baglanti.Close();
            }
            else if (comboBox7.SelectedItem.ToString() == "OFİS MEMURLARI")
            {
                baglanti.Open();
                NpgsqlCommand komut2 = new NpgsqlCommand("select public.\"ofismemuru_say\"()", baglanti);
                NpgsqlDataReader read = komut2.ExecuteReader();
                while (read.Read())
                    txttoplamcalisan.Text = read[0].ToString();

                baglanti.Close();
            }

        }
    }
}
