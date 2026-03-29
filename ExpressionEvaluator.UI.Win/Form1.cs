using ExpressionEvaluator.Core;
namespace ExpressionEvaluator.UI.Win
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string text = btn.Text;

            // Operadores
            string operadores = "+-*/";

            if (operadores.Contains(text))
            {
                if (textBox1.Text.Length == 0)
                    return;

                char ultimo = textBox1.Text[^1];

                if (operadores.Contains(ultimo))
                    return;
            }

            // Validar punto decimal
            if (text == ".")
            {
                var partes = textBox1.Text.Split('+', '-', '*', '/');

                if (partes.Length > 0 && partes[^1].Contains("."))
                    return;
            }

            textBox1.Text += text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
        private void buttonX_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                var result = Evaluator.Evaluate(textBox1.Text);
                textBox1.Text = result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                textBox1.Text = "";
            }
        }
    }
}
