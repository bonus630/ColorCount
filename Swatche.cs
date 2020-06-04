using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorCount
{
    public partial class Swatche : UserControl
    {
        private ColorTagged colorTagged;
        public ColorTagged ColorTag => colorTagged;
        public string ColorName => this.textBoxName.Text;
        public int NewColorTag => (int)numericUpDownTag.Value;

        public Swatche(ColorTagged colorTagged)
        {
            InitializeComponent();
            this.colorTagged = colorTagged;
            this.Load += Swatche_Load;
        }

        private void Swatche_Load(object sender, EventArgs e)
        {
            this.panel1.BackColor = colorTagged.RGB;
            numericUpDownTag.BackColor = colorTagged.RGB;
            numericUpDownTag.Value = colorTagged.Tag;
            textBoxName.BackColor = colorTagged.RGB;
            textBoxName.Text = colorTagged.Name;
            textBoxName.ForeColor = ColorList.GetColorDistance(Color.White, colorTagged.RGB) > 300 ? Color.White : Color.Black;
            numericUpDownTag.ForeColor = ColorList.GetColorDistance(Color.White, colorTagged.RGB) > 300 ? Color.White : Color.Black;
        }
        public void ChangeTag(int tag)
        {
            numericUpDownTag.Value = tag;
        }
    }
}
