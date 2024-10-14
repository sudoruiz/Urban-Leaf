using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using urban_leaf.Models;

namespace urban_leaf
{
    public partial class EditIrrigation : Form
    {
        public EditIrrigation()
        {
            InitializeComponent();
        }

        public int SelectedPanelIndex { get; private set; }
        public Color SelectedColor { get; private set; }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            SelectedPanelIndex = comboBoxPanel.SelectedIndex;

            switch (comboBoxColor.SelectedItem?.ToString())
            {
                case "Perfeito":
                    SelectedColor = Color.FromArgb(18, 139, 85);
                    break;
                case "Atenção":
                    SelectedColor = Color.FromArgb(255, 213, 0);
                    break;
                case "Ativo":
                    SelectedColor = Color.FromArgb(163, 163, 163);
                    break;
                case "Com problema":
                    SelectedColor = Color.FromArgb(246, 52, 50);
                    break;
                default:
                    SelectedColor = Color.Transparent;
                    break;
            }

            List<PanelColor> panelColors = LoadPanelColors();
            panelColors.RemoveAll(pc => pc.PanelIndex == SelectedPanelIndex);
            panelColors.Add(new PanelColor { PanelIndex = SelectedPanelIndex, Color = SelectedColor.ToArgb().ToString() });
            SavePanelColors(panelColors);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SavePanelColors(List<PanelColor> panelColors)
        {
            string json = JsonSerializer.Serialize(panelColors);
            File.WriteAllText("panelColors.json", json);
        }

        private List<PanelColor> LoadPanelColors()
        {
            if (File.Exists("panelColors.json"))
            {
                string json = File.ReadAllText("panelColors.json");
                return JsonSerializer.Deserialize<List<PanelColor>>(json) ?? new List<PanelColor>();
            }
            return new List<PanelColor>();
        }
    }
}
