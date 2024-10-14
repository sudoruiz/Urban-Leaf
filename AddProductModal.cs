using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace urban_leaf
{
    public partial class AddProductModal : Form
    {
        public string ComboBox1Value => comboBox1.SelectedItem?.ToString();
        public string ComboBox2Value => comboBox2.SelectedItem?.ToString();
        public string TextBox1Value => dateTimePicker1.Text;
        public string TextBox2Value => textBox2.Text;
        public string TextBox3Value => dateTimePicker2.Text;
        public string TextBox4Value => textBox4.Text;
        public AddProductModal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ComboBox1Value))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(ComboBox2Value))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TextBox1Value))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TextBox2Value))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TextBox3Value))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TextBox4Value))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
