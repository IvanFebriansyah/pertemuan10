using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace pertemuan10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-ABD7H3A;Initial Catalog=kantinAmikom;Integrated Security=True");

        private void resetdata()
        {
            txtuser.Text = "";
            txtpwd.Text = "";
        }

        private void showdata()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM admin";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "admin");
            dgvuser.DataSource = ds;
            dgvuser.DataMember = "admin";
            dgvuser.ReadOnly = true;
        }

        private string CaesarCipher(string value, int shift)
        {
            string[] joinCipher = new string[200];
            string joinText = "";
            string[] wordArray = value.Split(' ');

            try
            {
                for(int x = 0; x < wordArray.Length; x++)
                {
                    char[] buffer = wordArray[x].ToCharArray();

                    for(int i = 0; i < buffer.Length; i++)
                    {
                        char letter = buffer[i];
                        letter = (char)(letter + shift);

                        if(letter > 'z')
                        {
                            letter = (char)(letter - 26);
                        }else if(letter < 'a')
                        {
                            letter = (char)(letter + 26);
                        }

                        buffer[i] = letter;
                    }
                    string HasilKonversi = new string(buffer);
                    joinCipher[x] = HasilKonversi;
                }

                joinText = string.Join(" ", joinCipher);
                return joinText;

            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtuser.Text == "" | txtpwd.Text == "")
            {
                MessageBox.Show("Semua data harus diisi", "Warning");
                goto berhenti;
            }

            string tekscipher = null;
            tekscipher = CaesarCipher(txtpwd.Text, 17);

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO admin values ('" + txtuser.Text + "','" + tekscipher + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            showdata();
            resetdata();

        berhenti:
            ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            resetdata();
            showdata();
        }

        private void txtpwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtpwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToLower());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(txtuser.Text == "")
            {
                MessageBox.Show("Isi user id yang akan dihapus");
                goto berhenti;
            }

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM admin WHERE userid = '" + txtuser.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            showdata();
            resetdata();

        berhenti:
            ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(txtuser.Text == "" | txtpwd.Text == "")
            {
                MessageBox.Show("Semua data harus diisi", "Peringatan");
                goto berhenti;
            }

            string tekscipher = null;
            tekscipher = CaesarCipher(txtpwd.Text, 17);

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE admin set password= '" + tekscipher + "'WHERE userid='" + txtuser.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            showdata();
            resetdata();

        berhenti:
            ;
        }
    }
}
