using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PoladTask4
{
    public partial class Form1 : Form
    {
        // Alınmış biletlərin yadda saxlanılması üçün siyahı
        private List<string> alinanBiletler = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Haradan və Haraya siyahısına şəhərlərin əlavə olunması
            string[] seherler = { "Baki", "Izmir", "paris", "london", "madrid" };
            comboBox1.Items.AddRange(seherler);
            comboBox2.Items.AddRange(seherler);

            // Yer siyahısına nömrələrin əlavə olunması (1-20)
            for (int i = 1; i <= 20; i++)
            {
                comboBox3.Items.Add(i.ToString());
            }
        }

        // button2: Haradan və Haraya məlumatlarının yerini dəyişən düymə (< / >)
        private void button2_Click(object sender, EventArgs e)
        {
            var temp = comboBox1.SelectedItem;
            comboBox1.SelectedItem = comboBox2.SelectedItem;
            comboBox2.SelectedItem = temp;
        }

        // button1: Bilet al düyməsi
        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Xanaların boş olub-olmadığının yoxlanılması
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null ||
                comboBox3.SelectedItem == null || string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Lütfən bütün xanaları doldurun!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Haradan və Haraya eyni şəhər seçilə bilməz
            if (comboBox1.SelectedItem.ToString() == comboBox2.SelectedItem.ToString())
            {
                MessageBox.Show("Haradan və Haraya eyni şəhər ola bilməz!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. FIN kod yoxlanışı (Dəqiq 7 simvol olmalıdır)
            string fin = textBox2.Text.Trim();
            if (fin.Length != 7)
            {
                MessageBox.Show("FIN kod dəqiq 7 simvoldan ibarət olmalıdır!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Email yoxlanışı (Regex istifadə olunaraq)
            string email = textBox4.Text.Trim();
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Düzgün e-mail ünvanı daxil edin!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Eyni yerin 2-ci dəfə alınmasının qarşısının alınması
            string biletAcar = $"{comboBox1.SelectedItem}-{comboBox2.SelectedItem}-{maskedTextBox1.Text}-{maskedTextBox2.Text}-{comboBox3.SelectedItem}";

            if (alinanBiletler.Contains(biletAcar))
            {
                MessageBox.Show("Bu marşrut, tarix, saat və yer üçün artıq bilet alınıb! Başqa yer seçin.", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // Bilet uğurla alındıqda siyahıya əlavə olunur
            alinanBiletler.Add(biletAcar);
            MessageBox.Show($"Bilet uğurla alındı!\n\nSərnişin: {textBox1.Text}\nMarşrut: {comboBox1.SelectedItem} -> {comboBox2.SelectedItem}\nYer: {comboBox3.SelectedItem}", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // button3: Siyahıdan sil düyməsi (Xanaları təmizləyir)
        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            maskedTextBox1.Clear();
            maskedTextBox2.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        // button4: Programdan cixin düyməsi
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}