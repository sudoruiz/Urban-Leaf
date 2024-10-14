using System;
using System.Drawing;
using System.Windows.Forms;

namespace urban_leaf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;

            LoadForm1Control();
        }

        private void LoadForm1Control()
        {
            var form1Control = new Form1Control();
            form1Control.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(form1Control);

            SetActiveButton(button1);
        }

        private void LoadForm2Control()
        {
            var form2Control = new Form2Control();
            form2Control.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(form2Control);

            SetActiveButton(button2);
        }

        private void LoadForm3Control()
        {
            var form3Control = new Form3Control();
            form3Control.Dock = DockStyle.Fill;
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(form3Control);

            SetActiveButton(button3);
        }

        private void SetActiveButton(Button activeButton)
        {
            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;

            activeButton.BackColor = Color.FromArgb(51, 181, 73);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadForm1Control();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadForm2Control();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadForm3Control();
        }
    }
}
