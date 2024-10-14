using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using urban_leaf.Models;
using System.Text.Json;

namespace urban_leaf
{
    public partial class EditCrops : Form
    {
        public EditCrops()
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


            List<CropsColor> panelColors = LoadPanelColors();
            panelColors.RemoveAll(pc => pc.PanelIndex == SelectedPanelIndex);
            panelColors.Add(new CropsColor { PanelIndex = SelectedPanelIndex, Color = SelectedColor.ToArgb().ToString() });
            SavePanelColors(panelColors);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SavePanelColors(List<CropsColor> panelColors)
        {
            string json = JsonSerializer.Serialize(panelColors);
            File.WriteAllText("cropsColors.json", json);
        }

        private List<CropsColor> LoadPanelColors()
        {
            if (File.Exists("cropsColors.json"))
            {
                string json = File.ReadAllText("cropsColors.json");
                return JsonSerializer.Deserialize<List<CropsColor>>(json) ?? new List<CropsColor>();
            }
            return new List<CropsColor>();
        }
    }
}