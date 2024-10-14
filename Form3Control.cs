﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using urban_leaf.Models;
using System.Text.Json;

namespace urban_leaf
{
    public partial class Form3Control : UserControl
    {
        private string placeholderText = "Pesquisar";
        private int currentPage = 0;
        private const int itemsPerPage = 22;
        private List<Panel> panels = new List<Panel>();

        public Form3Control()
        {
            InitializeComponent();

            MakeButtonRounded(page1);
            MakeButtonRounded(page2);

            panels.Add(panel19);
            panels.Add(panel10);
            panels.Add(panel13);
            panels.Add(panel16);
            panels.Add(panel20);
            panels.Add(panel23);
            panels.Add(panel26);
            panels.Add(panel29);
            panels.Add(panel34);
            panels.Add(panel37);
            panels.Add(panel40);
            panels.Add(panel43);
            panels.Add(panel46);
            panels.Add(panel49);
            panels.Add(panel52);
            panels.Add(panel55);
            panels.Add(panel58);
            panels.Add(panel61);
            panels.Add(panel64);
            panels.Add(panel67);
            panels.Add(panel70);
            panels.Add(panel73);
            panels.Add(panel79);

            LoadPage(currentPage);

            buttonNext.Click += ButtonNext_Click;
            buttonPrevious.Click += ButtonPrevious_Click;

            page1.Click += Page1_Click;
            page2.Click += Page2_Click;

            this.Resize += new EventHandler(Form3Control_Resize);

            CustomizeTextBox();

            textBox1.TextChanged += TextBox1_TextChanged;
        }

         public void SavePanelColors(List<CropsColor> panelColors)
        {
            string json = JsonSerializer.Serialize(panelColors);
            File.WriteAllText("cropsColors.json", json);
        }

        public List<CropsColor> LoadPanelColors()
        {
            if (File.Exists("cropsColors.json"))
            {
                string json = File.ReadAllText("cropsColors.json");
                return JsonSerializer.Deserialize<List<CropsColor>>(json) ?? new List<CropsColor>();
            }
            return new List<CropsColor>();

        }

        private void Form3Control_Load(object sender, EventArgs e)
        {
            ApplyPanelColors();
            UpdateLabelsWithQuantities();
        }

        private void ApplyPanelColors()
        {
            List<CropsColor> panelColors = LoadPanelColors();

            foreach (var panelColor in panelColors)
            {
                Panel panel = GetPanelByIndex(panelColor.PanelIndex);
                if (panel != null)
                {
                    panel.BackColor = Color.FromArgb(int.Parse(panelColor.Color));
                }
            }
            UpdateLabelsWithQuantities();
        }

        private void CustomizeTextBox()
        {
            textBox1.Text = placeholderText;
            textBox1.ForeColor = Color.Gray;

            textBox1.Enter += TextBox_Enter;
            textBox1.Leave += TextBox_Leave;
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == placeholderText)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = placeholderText;
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void Form3Control_Resize(object sender, EventArgs e)
        {
        }
        private Panel GetPanelByIndex(int index)
        {
            Panel[] panels = { panel32, panel11, panel14, panel17, panel21, panel24,
                       panel27, panel30, panel35, panel38, panel41, panel44,
                       panel47, panel50, panel53, panel56, panel59, panel62,
                       panel65, panel68, panel71, panel74, panel80 };

            return panels.ElementAtOrDefault(index);
        }

        private void UpdateLabelsWithQuantities()
        {
            Panel[] panels = {
        panel32, panel11, panel14, panel17, panel21, panel24,
        panel27, panel30, panel35, panel38, panel41, panel44,
        panel47, panel50, panel53, panel56, panel59, panel62,
        panel65, panel68, panel71, panel74, panel80
    };

            int countGray = 0;
            int countGreen = 0;
            int countYellow = 0;
            int countRed = 0;

            foreach (var panel in panels)
            {
                if (panel.BackColor == Color.FromArgb(163, 163, 163))
                {
                    countGray++;
                }
                else if (panel.BackColor == Color.FromArgb(18, 139, 85))
                {
                    countGreen++;
                }
                else if (panel.BackColor == Color.FromArgb(255, 213, 0))
                {
                    countYellow++;
                }
                else if (panel.BackColor == Color.FromArgb(246, 52, 50))
                {
                    countRed++;
                }
            }

            label2.Text = countGray.ToString();
            label4.Text = countGreen.ToString();
            label6.Text = countYellow.ToString();
            label8.Text = countRed.ToString();
        }

        private void LoadPage(int pageNumber)
        {
            tableLayoutPanel1.Controls.Clear();

            foreach (var panel in panels)
            {
                panel.Visible = false;
            }

            int startItemIndex = pageNumber * itemsPerPage;
            int endItemIndex = Math.Min(startItemIndex + itemsPerPage, panels.Count);

            for (int i = startItemIndex; i < endItemIndex; i++)
            {
                Panel panel = panels[i];
                panel.Visible = true;
                tableLayoutPanel1.Controls.Add(panel);
            }

            UpdateNavigationButtons();
            UpdatePageButtonStyles();
            UpdateLabelsWithQuantities();
        }

        private void UpdatePageButtonStyles()
        {
            page1.BackColor = Color.White;
            page2.BackColor = Color.White;

            if (currentPage == 0)
            {
                page1.BackColor = Color.FromArgb(51, 181, 73);
            }
            else if (currentPage == 1)
            {
                page2.BackColor = Color.FromArgb(51, 181, 73);
            }
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (currentPage < (panels.Count - 1) / itemsPerPage)
            {
                currentPage++;
                LoadPage(currentPage);
            }
        }

        private void ButtonPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                LoadPage(currentPage);
            }
        }

        private void UpdateNavigationButtons()
        {
            buttonPrevious.Enabled = currentPage > 0;
            buttonNext.Enabled = (currentPage + 1) * itemsPerPage < panels.Count;
        }

        private void Page1_Click(object sender, EventArgs e)
        {
            currentPage = 0;
            LoadPage(currentPage);
        }

        private void Page2_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadPage(currentPage);
        }

        private void MakeButtonRounded(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                Rectangle buttonRect = button.ClientRectangle;
                int cornerRadius = 20;

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(buttonRect.X, buttonRect.Y, cornerRadius, cornerRadius, 180, 90);
                    path.AddArc(buttonRect.Right - cornerRadius, buttonRect.Y, cornerRadius, cornerRadius, 270, 90);
                    path.AddArc(buttonRect.Right - cornerRadius, buttonRect.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                    path.AddArc(buttonRect.X, buttonRect.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                    path.CloseAllFigures();

                    button.Region = new Region(path);
                }
            };
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim().ToLower();

            tableLayoutPanel1.Controls.Clear();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadPage(currentPage);
                return;
            }

            foreach (var panel in panels)
            {
                bool found = false;
                foreach (Control control in panel.Controls)
                {
                    if (control is Label label)
                    {
                        if (label.Text.ToLower().Contains(searchText))
                        {
                            found = true;
                            break;
                        }
                    }
                }

                panel.Visible = found;
                if (found)
                {
                    tableLayoutPanel1.Controls.Add(panel);
                }
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (var editCropsForm = new EditCrops())
            {
                if (editCropsForm.ShowDialog() == DialogResult.OK)
                {
                    var panelNames = new List<string>
            {
                "panel32", "panel11", "panel14", "panel17", "panel21", "panel24",
                "panel27", "panel30", "panel35", "panel38", "panel41", "panel44",
                "panel47", "panel50", "panel53", "panel56", "panel59", "panel62",
                "panel65", "panel68", "panel71", "panel74", "panel80"
            };

                    int panelIndex = editCropsForm.SelectedPanelIndex;
                    Color selectedColor = editCropsForm.SelectedColor;

                    if (panelIndex >= 0 && panelIndex < panelNames.Count)
                    {
                        var panel = this.Controls.Find(panelNames[panelIndex], true).FirstOrDefault() as Panel;
                        if (panel != null)
                        {
                            panel.BackColor = selectedColor;
                            UpdateLabelsWithQuantities();
                        }
                    }
                }
            }
        }
    }
}
