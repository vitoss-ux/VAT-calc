using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace VAT_calc
{
    public partial class Form1 : Form
    {
        public double num;// Число для начисления/выделени НДС
        public double rate; // Ставка %
        public double resVat; // Размер НДС от суммы
        public double res; // Результат

        public Form1()
        {
            InitializeComponent();
            textBox_VAT.Validating += (s, e) => Validation(s, e, ValidateType.Double);
            textBox2.Validating += (s, e) => Validation(s, e, ValidateType.Double);
        }

        private void TextBox_VAT_Validating(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        // Начислить НДС
        private void Accrue()
        {
            num = double.Parse(textBox_VAT.Text);
            rate = double.Parse(textBox2.Text);
            resVat = num * rate / 100; // Размер НДС
            resVat = Math.Round(resVat, 2); // Округл
            res = num + resVat; // Сумма с НДС
            res = Math.Round(res, 2); // Округл
            textBox1.Text = resVat.ToString();
            textBox3.Text = res.ToString();
        }

        // Выделить НДС
        private void Allocate()
        {
            num = double.Parse(textBox_VAT.Text);
            rate = double.Parse(textBox2.Text);
            resVat = num / (rate + 100) * rate; // Размер НДС
            resVat = Math.Round(resVat, 2);
            res = num - resVat; // Сумма без НДС
            res = Math.Round(res, 2);
            textBox1.Text = resVat.ToString();
            textBox3.Text = res.ToString();
        }

        // Валидация
        private void Validation(object sender, CancelEventArgs e, ValidateType type)
        {
            var text = (sender as Control).Text;
            var errorMessage = "";

            switch (type)
            {
                case ValidateType.Int:
                    {
                        int temp;
                        if (int.TryParse(text, out temp)) return;
                        errorMessage = "Не число! Пожалуйста, введите число!";
                        break;
                    }
                case ValidateType.Double:
                    {
                        double temp;
                        if (double.TryParse(text, out temp)) return;
                        errorMessage = "Не число! Пожалуйста, введите число!";
                        break;
                    }
            }

            e.Cancel = true;
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        enum ValidateType
        {
            Int, Double
        }

        // Кнопки
        private void Button1_Click(object sender, EventArgs e) => Accrue();

        private void Button2_Click(object sender, EventArgs e) => Allocate();

        private void BtnClear_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox_VAT.Clear();
        }
    }
}
