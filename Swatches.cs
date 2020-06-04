using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorCount
{
    public partial class Swatches : Form
    {
     
        ColorList colorList;
        AcoUnpacker acoUn;

     
        public Swatches(ColorList colorList)
        {
            InitializeComponent();
            this.colorList = colorList;
            colorList.ColorTagChangeEvent += ColorList_ColorTagChangeEvent;
        }

        private void ColorList_ColorTagChangeEvent(ColorTagged obj)
        {
            Swatche temp = null;
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                temp = flowLayoutPanel1.Controls[i] as Swatche;
                if (temp.ColorTag.Equals(obj))
                {
                    temp.ChangeTag(obj.Tag);
                    flowLayoutPanel1.Controls.SetChildIndex(temp, obj.Tag);
                }
            }
        }
        private void CreateColorPanel()
        {
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < this.colorList.Count; i++)
            {
                //Panel p = new Panel();
                //p.Width = 120;
                //p.Height = 40;
                //p.BackColor = colorList[i].RGB;

                //Label l = new Label();
                //l.Width = 120;
                //l.Height = 40;
                //l.TextAlign = ContentAlignment.MiddleCenter;
                //l.AutoSize = false;
                //l.Text = colorList[i].Name;
                //p.Controls.Add(l);
                Swatche p = new Swatche(this.colorList[i]);

                flowLayoutPanel1.Controls.Add(p);
                flowLayoutPanel1.Controls.SetChildIndex(p, p.ColorTag.Tag);
            }
            
        }
        private void btn_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Amostras de cores |*.aco";
            of.Multiselect = false;
            if (of.ShowDialog() != DialogResult.OK)
                return;
           //Teste unpack .aco files
            acoUn = new AcoUnpacker();
            acoUn.FilePath = of.FileName;
            acoUn.Unpack();
            List<Color> Colors = acoUn.Colors;
            List<string> ColorsName = acoUn.ColorsName;
            //for (int i = 0; i < colors.Count; i++)
            //{
            //    colorTag.AddColor(colors[i], names[i]);
            //}
            for (int i = 0; i< Colors.Count; i++)
            {
                colorList.Add(Colors[i], ColorsName[i]);


            }
            CreateColorPanel();
        }

        private void Swatches_Load(object sender, EventArgs e)
        {
            CreateColorPanel();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            List<ColorTagged> c = new List<ColorTagged>();
            Swatche temp = null;
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                temp = flowLayoutPanel1.Controls[i] as Swatche;
                c.Add(temp.ColorTag);
                c[i].Name = temp.ColorName;
                //try
                //{
                if (temp.NewColorTag != temp.ColorTag.Tag)
                {
                    this.colorList.SetNewColorTag(temp.ColorTag, temp.NewColorTag);
                    flowLayoutPanel1.Controls.SetChildIndex(temp, temp.NewColorTag);
                }
               
                //}
                //catch(Exception err)
                //{
                //    MessageBox.Show("Não pode conter tags repetidas");
                //}

            }
            
            SaveLoadColorList.Save(this.colorList, $"{Application.StartupPath}\\swatche.xml");
            //CreateColorPanel();


        }
    }
}
