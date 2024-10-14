using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using urban_leaf.Models;
using urban_leaf.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace urban_leaf
{
    public partial class Form1Control : UserControl
    {
        private string placeholderText = "Pesquisar";

        public Form1Control()
        {
            InitializeComponent();

            this.Load += Form1_Load;

            CustomizeTextBox();

            button4.Paint += Button4_Paint;
        }

        private void CustomizeTextBox()
        {
            textBoxSearch.Text = placeholderText;
            textBoxSearch.ForeColor = Color.Gray;

            textBoxSearch.Enter += TextBox_Enter;
            textBoxSearch.Leave += TextBox_Leave;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == placeholderText)
            {
                textBoxSearch.Text = "";
                textBoxSearch.ForeColor = Color.Black;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                textBoxSearch.Text = placeholderText;
                textBoxSearch.ForeColor = Color.Gray;
            }
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSearch.Text == placeholderText)
            {
                return;
            }

            string searchValue = textBoxSearch.Text.ToLower();

            foreach (Control control in panel9.Controls)
            {
                if (control is TableLayoutPanel tableLayoutPanel)
                {
                    Label label = (Label)tableLayoutPanel.Controls[0].Controls[1];

                    if (label.Text.ToLower().Contains(searchValue))
                    {
                        tableLayoutPanel.Visible = true;
                    }
                    else
                    {
                        tableLayoutPanel.Visible = false;
                    }
                }
            }
        }
        private void UpdateTableVisibility(string searchValue)
        {
            foreach (Control control in panel9.Controls)
            {
                if (control is TableLayoutPanel tableLayoutPanel)
                {
                    Label label = (Label)tableLayoutPanel.Controls[0].Controls[1];

                    if (label.Text.ToLower().Contains(searchValue))
                    {
                        tableLayoutPanel.Visible = true;
                    }
                    else
                    {
                        tableLayoutPanel.Visible = false;
                    }
                }
            }
        }
        private void Button4_Paint(object sender, PaintEventArgs e)
        {
            int radius = 20;

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(button4.Width - radius, 0, radius, radius), -90, 90);
            path.AddArc(new Rectangle(button4.Width - radius, button4.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, button4.Height - radius, radius, radius), 90, 90);

            path.CloseAllFigures();

            button4.Region = new Region(path);
        }
        private void labelPercentage_Click(object sender, EventArgs e)
        {
        }

        private int tableCount = 0;
        private bool isButtonClicked = false;

        private void button4_Click(object sender, EventArgs e)
        {
            string previousText = textBoxSearch.Text;

            AddProductModal modal = new AddProductModal();

            if (modal.ShowDialog() == DialogResult.OK)
            {
                string comboBox1Value = modal.ComboBox1Value;
                string comboBox2Value = modal.ComboBox2Value;

                string textBox1Value = DateTime.Parse(modal.TextBox1Value).ToString("dd/MM/yyyy");
                string textBox2Value = modal.TextBox2Value;
                string textBox3Value = DateTime.Parse(modal.TextBox3Value).ToString("dd/MM/yyyy");
                string textBox4Value = modal.TextBox4Value;

                Panel marginPanel = new Panel
                {
                    Height = 10,
                    Dock = DockStyle.Top,
                    BackColor = SystemColors.Control
                };

                TableLayoutPanel newTableLayoutPanel = new TableLayoutPanel
                {
                    ColumnCount = 7,
                    RowCount = 1,
                    BackColor = Color.White,
                    Padding = new Padding(0),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Dock = DockStyle.Top,
                    Height = 42,
                    AutoSize = false,
                    BorderStyle = BorderStyle.FixedSingle
                };

                TableLayoutPanel column0Layout = new TableLayoutPanel
                {
                    ColumnCount = 2,
                    RowCount = 1,
                    Dock = DockStyle.Fill,
                    Height = 30
                };

                column0Layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37));
                column0Layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

                PictureBox pictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 37,
                    Height = 30,
                    Dock = DockStyle.None
                };

                if (comboBox1Value == "Batata")
                {
                    pictureBox.Image = Properties.Resources.batata;
                }
                else if (comboBox1Value == "Beterraba")
                {
                    pictureBox.Image = Properties.Resources.beterraba;
                }
                else if (comboBox1Value == "Maçã")
                {
                    pictureBox.Image = Properties.Resources.maça;
                }
                else if (comboBox1Value == "Cenoura")
                {
                    pictureBox.Image = Properties.Resources.cenoura;
                }
                else if (comboBox1Value == "Banana")
                {
                    pictureBox.Image = Properties.Resources.banana;
                }
                else
                {
                    pictureBox.Image = null;
                }

                Label labelComboBox1 = new Label
                {
                    Text = comboBox1Value,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleRight,
                    Margin = new Padding(0)
                };

                column0Layout.Controls.Add(pictureBox, 0, 0);
                column0Layout.Controls.Add(labelComboBox1, 1, 0);

                newTableLayoutPanel.Controls.Add(column0Layout, 0, 0);

                for (int i = 0; i < 7; i++)
                {
                    newTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
                }

                newTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

                newTableLayoutPanel.Controls.Add(new Label { Text = textBox1Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 1, 0);
                newTableLayoutPanel.Controls.Add(new Label { Text = textBox2Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 2, 0);
                newTableLayoutPanel.Controls.Add(new Label { Text = textBox3Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 3, 0);
                newTableLayoutPanel.Controls.Add(new Label { Text = comboBox2Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 4, 0);

                Panel progressContainer = new Panel
                {
                    Dock = DockStyle.Fill,
                    Size = new Size(35, 35),
                    Margin = new Padding(0),
                };

                Panel panelProgressCircle = new Panel
                {
                    Width = 35,
                    Height = 35,
                    Margin = new Padding(20, 0, 0, 0)
                };

                InitializeProgressCircle(panelProgressCircle, 0);

                progressContainer.Paint += (s, pe) => CenterProgressCircle(panelProgressCircle, progressContainer);
                progressContainer.Controls.Add(panelProgressCircle);

                newTableLayoutPanel.Controls.Add(progressContainer, 5, 0);
                newTableLayoutPanel.Controls.Add(new Label { Text = textBox4Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 6, 0);

                panel9.Controls.Add(marginPanel);
                panel9.Controls.Add(newTableLayoutPanel);

                tableCount++;
                label2.Text = $"{tableCount}";

                Product newProduct = new Product
                {
                    Name = comboBox1Value,
                    LastPlantation = DateTime.Parse(textBox1Value),
                    Sector = textBox2Value,
                    CollectionForecast = DateTime.Parse(textBox3Value),
                    Status = comboBox2Value,
                    RegistrationNumber = textBox4Value
                };

                List<Product> products = ProductService.LoadProducts();
                products.Add(newProduct);
                ProductService.SaveProducts(products);
            }
        }

        private void AddProductToTable(Product product)
        {
            foreach (Control control in panel9.Controls)
            {
                if (control is TableLayoutPanel table &&
                    table.Controls.OfType<Label>().Any(label => label.Text == product.Name))
                {
                    return;
                }
            }

            string comboBox1Value = product.Name;
            string textBox1Value = product.LastPlantation.ToString("dd/MM/yyyy");
            string textBox2Value = product.Sector;
            string textBox3Value = product.CollectionForecast.ToString("dd/MM/yyyy");
            string comboBox2Value = product.Status;
            string textBox4Value = product.RegistrationNumber;

            Panel marginPanel = new Panel
            {
                Height = 10,
                Dock = DockStyle.Top,
                BackColor = SystemColors.Control
            };

            TableLayoutPanel newTableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 7,
                RowCount = 1,
                BackColor = Color.White,
                Padding = new Padding(0),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Dock = DockStyle.Top,
                Height = 42,
                AutoSize = false,
                BorderStyle = BorderStyle.FixedSingle
            };

            TableLayoutPanel column0Layout = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Fill,
                Height = 30
            };

            column0Layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37));
            column0Layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            PictureBox pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 37,
                Height = 30,
                Dock = DockStyle.None
            };

            if (comboBox1Value == "Batata")
            {
                pictureBox.Image = Properties.Resources.batata;
            }
            else if (comboBox1Value == "Beterraba")
            {
                pictureBox.Image = Properties.Resources.beterraba;
            }
            else if (comboBox1Value == "Maçã")
            {
                pictureBox.Image = Properties.Resources.maça;
            }
            else if (comboBox1Value == "Cenoura")
            {
                pictureBox.Image = Properties.Resources.cenoura;
            }
            else if (comboBox1Value == "Banana")
            {
                pictureBox.Image = Properties.Resources.banana;
            }
            else
            {
                pictureBox.Image = null;
            }

            Label labelComboBox1 = new Label
            {
                Text = comboBox1Value,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Margin = new Padding(0)
            };

            column0Layout.Controls.Add(pictureBox, 0, 0);
            column0Layout.Controls.Add(labelComboBox1, 1, 0);

            newTableLayoutPanel.Controls.Add(column0Layout, 0, 0);

            for (int i = 0; i < 7; i++)
            {
                newTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28F));
            }

            newTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));

            newTableLayoutPanel.Controls.Add(new Label { Text = textBox1Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 1, 0);
            newTableLayoutPanel.Controls.Add(new Label { Text = textBox2Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 2, 0);
            newTableLayoutPanel.Controls.Add(new Label { Text = textBox3Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 3, 0);
            newTableLayoutPanel.Controls.Add(new Label { Text = comboBox2Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 4, 0);

            Panel progressContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Size = new Size(35, 35),
                Margin = new Padding(0),
            };

            Panel panelProgressCircle = new Panel
            {
                Width = 35,
                Height = 35,
                Margin = new Padding(20, 0, 0, 0)
            };

            InitializeProgressCircle(panelProgressCircle, 0);

            progressContainer.Paint += (s, pe) => CenterProgressCircle(panelProgressCircle, progressContainer);
            progressContainer.Controls.Add(panelProgressCircle);

            newTableLayoutPanel.Controls.Add(progressContainer, 5, 0);
            newTableLayoutPanel.Controls.Add(new Label { Text = textBox4Value, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter }, 6, 0);

            panel9.Controls.Add(marginPanel);
            panel9.Controls.Add(newTableLayoutPanel);

            tableCount++;
            label2.Text = $"{tableCount}";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            panel9.Controls.Clear();
            tableCount = 0;

            var products = ProductService.LoadProducts();

            foreach (var product in products)
            {
                AddProductToTable(product);
            }

            label2.Text = $"{tableCount}";
        }

        private void InitializeProgressCircle(Panel panel, int percentage)
        {
            System.Windows.Forms.Timer timerProgressCircle = new System.Windows.Forms.Timer();
            timerProgressCircle.Interval = 1500;
            timerProgressCircle.Tag = percentage;
            timerProgressCircle.Tick += (s, e) =>
            {
                int currentPercentage = (int)timerProgressCircle.Tag;

                if (currentPercentage < 100)
                {
                    currentPercentage += 1;
                    timerProgressCircle.Tag = currentPercentage;
                    panel.Invalidate();
                }
                else
                {
                    timerProgressCircle.Stop();
                }
            };
            timerProgressCircle.Start();

            panel.Paint += (s, e) => PanelProgressCircle_Paint(s, e, panel, (int)timerProgressCircle.Tag);
        }

        private void CenterProgressCircle(Panel progressCircle, Panel container)
        {
            progressCircle.Location = new Point((container.Width - progressCircle.Width) / 2,
                                                (container.Height - progressCircle.Height) / 2);
        }
        private void PanelProgressCircle_Paint(object sender, PaintEventArgs e, Panel panel, int percentage)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int thickness = 5;
            Rectangle rect = new Rectangle(thickness, thickness, panel.Width - 2 * thickness, panel.Height - 2 * thickness);

            int startAngle = -90;
            int sweepAngle = (int)(360 * (percentage / 100.0));

            using (Pen penBackground = new Pen(Color.LightGray, thickness))
            {
                e.Graphics.DrawArc(penBackground, rect, 0, 360);
            }

            using (Pen penProgress = new Pen(Color.Green, thickness))
            {
                penProgress.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                penProgress.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                e.Graphics.DrawArc(penProgress, rect, startAngle, sweepAngle);
            }

            using (Brush circleBrush = new SolidBrush(Color.White))
            {
                int innerCircleDiameter = rect.Width - thickness * 2;
                Rectangle innerCircle = new Rectangle(rect.X + thickness, rect.Y + thickness, innerCircleDiameter, innerCircleDiameter);
                e.Graphics.FillEllipse(circleBrush, innerCircle);
            }

            string percentageText = $"{percentage}%";
            Font font = new Font("Arial", 6);
            SizeF textSize = e.Graphics.MeasureString(percentageText, font);

            float textX = (panel.Width - textSize.Width) / 2;
            float textY = (panel.Height - textSize.Height) / 2;

            using (Brush textBrush = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(percentageText, font, textBrush, textX, textY);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FilterModal filterModal = new FilterModal();

            filterModal.ShowDialog();
        }

    }
}
