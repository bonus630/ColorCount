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
    public partial class ColorCountControl : UserControl
    {
        private ColorList colorList;
        //private string filePath;
        private bool expanded = true;
        private bool selected = true;
        private int colorCount = 0;
        private BmpUnpack bmp;
        public bool Expanded
        {
            get { return this.expanded; }
            set { this.expanded = value; OnExpande(); }
        }
        public bool Selected
        {
            get { return this.selected; }
            set { this.selected = value; OnSelected(); }
        }
        public event Action<ColorCountControl, bool> Expande;
        public new event Action<BmpUnpack> Click;
        public event Action<Control,BmpUnpack> ColorClick;
        private int minSizeHeight = 22;
        public ColorCountControl(ColorList colorList,string file)
        {
            InitializeComponent();
            this.colorList = colorList;
            this.label1.Text = file.Substring(file.LastIndexOf('\\')+1);
            //this.filePath = file;


            CreateColorPanel();
            SizeChange();
        }
        public ColorCountControl(BmpUnpack bmp)
        {
            InitializeComponent();
            //this.colorList = colorList;
            this.bmp = bmp;
            this.label1.Text = bmp.FilePath.Substring(bmp.FilePath.LastIndexOf('\\') + 1);
            toolTip1.SetToolTip(this.label1, bmp.FilePath);
            //this.filePath = file;


            CreateColorPanel();
            SizeChange();
        }

        private void OnExpande()
        {
            SizeChange();
            Expande?.Invoke(this, this.expanded);
        }
        private void SizeChange()
        {
            if(expanded)
            {
                btn_expander.BackgroundImage = Properties.Resources.expand_actived;
                this.Height = 20 + colorCount * 23;
            }
            else
            {
                this.Height = minSizeHeight;
                btn_expander.BackgroundImage = Properties.Resources.expand_deactived;

            }
        }
        public void OnSelected()
        {
            if(selected)
            {
                this.label1.BackColor = Color.AliceBlue;
            }
            else
                this.label1.BackColor = SystemColors.Control;
        }
        private void CreateColorPanel()
        {
            ColorList colorList = bmp.colorCountList;
            for (int i = 0; i < colorList.Count; i++)
            {
                if (colorList[i].ColorCount == 0)
                    continue;
                colorCount++;
                Panel p = new Panel();
                p.Width = 130;
                p.Height = 16;
                //p.BackColor = colors[i];

                Panel p1 = new Panel();
                p1.Width = 16;
                p1.Height = 16;
                p1.BackColor = colorList[i].Brush.Color;
                p1.Tag = colorList[i].Tag;
                p1.Cursor = Cursors.Hand;
                if (!string.IsNullOrEmpty(colorList[i].Name))
                    toolTip1.SetToolTip(p1, colorList[i].Name);
                p1.Click += (s, e) => { ColorClick?.Invoke(p1,this.bmp); };
                p.Controls.Add(p1);


                Label l = new Label();
                l.Width = 114;
                l.Height = 16;
                l.Left = 18;
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.AutoSize = false;
                l.Text = colorList[i].ColorCount.ToString();
                p.Controls.Add(l);


                flowLayoutPanel1.Controls.Add(p);
            }

        }

        private void btn_expander_Click(object sender, EventArgs e)
        {
            this.expanded = !this.expanded;
            OnExpande();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Click?.Invoke(bmp);
        }
    }
}
