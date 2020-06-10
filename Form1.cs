using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ColorCount
{
    public partial class Form1 : Form
    {
        BmpUnpack bmp;
        ColorList colorList;
        ImageRender imageRender;
        ImageSlice imgSlice;

        SolidBrush blackBrush;
        SolidBrush whiteBrush;
        private string currentFilePath;
        private bool processing = false;

        public Form1()
        {
            InitializeComponent();
            blackBrush = new SolidBrush(Color.Black);
            whiteBrush = new SolidBrush(Color.White);
            colorList = new ColorList(whiteBrush, blackBrush);
            //bmp = new BmpUnpack(colorList);

            //imageRender = new ImageRender(bmp);
            imageRender = new ImageRender();
            imgSlice = new ImageSlice();
            imageRender.ImageRenderFinish += ImageRender_ImageRenderFinish;
            imageRender.ProgressChange += ImageRender_ProgressChange;
        }

        private void ImageRender_ProgressChange(int obj)
        {
            SetNewValue(obj);
        }

        private void ImageRender_ImageRenderFinish(Bitmap obj)
        {
            SetImagePicture(obj);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
       
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            start(files[0]);
        }
        private void setData(BmpUnpack bmp, string file, bool updateImage = true)
        {
            if (processing)
                return;
            this.currentFilePath = file;
            this.Text = currentFilePath;
            if (updateImage)
                myPictureBox.ImageLocation = currentFilePath;
            if (this.bmp == null)
            {
                bmp.Unpack(file, Application.StartupPath);
                this.bmp = bmp;
                imageRender.BMP = bmp;
                imgSlice.BMP = bmp;
            }
            else
            {
                if (!this.bmp.Equals(bmp))
                {
                    bmp.Unpack(file, Application.StartupPath);
                    this.bmp = bmp;
                    
                    imgSlice.BMP = bmp;
                    imageRender.BMP = bmp;
                }
            }
            // MessageBox.Show(this.bmp.colorCountList.Sun().ToString());
        }
        private void start(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return;
                // bmp.Unpack(currentFilePath, Application.StartupPath);
                processFile(filePath);

                enableControl(btn_process, "Processar", true);
                enableControl(btn_save, "", true);
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
        private void processFile(string file)
        {
            BmpUnpack tempbmp = new BmpUnpack(colorList);

            setData(tempbmp, file);
            //ColorCountControl ccc = new ColorCountControl(bmp.colorCountList,this.currentFilePath);
            ColorCountControl ccc = new ColorCountControl(tempbmp);
            //ccc.Dock = DockStyle.Top;
            ccc.Click += (b) => {
                setData(b, b.FilePath);

            };
            ccc.ColorClick += (sender, bm) =>
            {
                setData(bm, bm.FilePath, false);

                DrawColor((int)sender.Tag);
                textBox_colorHex.Text = HexConverter(sender.BackColor);
            };
            flowLayoutPanel_counter.Controls.Add(ccc);


        }
        private delegate void SetValue(int value);
        private delegate void SetPBValue(int maximum, bool visible);
        private delegate void SetImage(Image image);
        private delegate void DisableEnableControl(Control control, string msg, bool enable);

        private void enableControl(Control control, string msg, bool enable)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DisableEnableControl(enableControl), control, msg, enable);
            }
            else
            {
                control.Enabled = enable;
                control.Text = msg;
            }
        }

        private void SetImagePicture(Image image)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetImage(SetImagePicture), image);
            }
            else
            {
                myPictureBox.Image = image;
                //myPictureBox.Width = image.Width;
                //myPictureBox.Height = image.Height;
            }
        }
        private void SetNewValue(int value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetValue(SetNewValue), value);
            }
            else
            {
                pb_loader.Value = value;
            }
        }
        private void SetProgressBarValue(int maximum, bool visible)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetPBValue(SetProgressBarValue), maximum, visible);
            }
            else
            {
                pb_loader.Maximum = maximum;
                pb_loader.Visible = visible;
            }
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            //criar uma thread aqui
            Thread t = new Thread(new ThreadStart(saveImage));
            t.IsBackground = true;
            t.Start();


        }
        private void saveImage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(saveImage));
            }
            else
            {
                enableControl(btn_save, "", false);
                SaveFileDialog of = new SaveFileDialog();
                of.Filter = "Imagens |*.png";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    myPictureBox.Image.Save(of.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    //myPictureBox.Image = ((Image)((new ComponentResourceManager(typeof(Form1))).GetObject("myPictureBox.Image")));
                    myPictureBox.ImageLocation = currentFilePath;
                }
                enableControl(btn_save, "", true);
            }
        }
        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            imageRender.Divider = (int)numericUpDown1.Value;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            //if (bmp != null)
            //    DrawCounter();
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            //if (bmp != null)
            //    DrawCounter();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            imageRender.VPixelSize = (int)numericUpDown2.Value;

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            imageRender.Reset();
            if (bmp != null && !string.IsNullOrEmpty(currentFilePath))
                bmp.Unpack(currentFilePath, "");
            panel_ignoredColor.BackColor = imageRender.IgnoredColor;
        }

        private void btn_process_Click(object sender, EventArgs e)
        {
            imgSlice.Slice();
            MessageBox.Show(imgSlice.Count.ToString());


            if (bmp != null)
            {

                Thread t = new Thread(new ThreadStart(DrawCounter));
                t.IsBackground = true;
                t.Start();

            }
        }
        private void DrawCounter()
        {
            try
            {
                processing = true;
                enableControl(btn_process, "Aguarde...", false);
                SetProgressBarValue(imageRender.TotalSize, true);
                imageRender.DrawCounter();
             
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {

                SetProgressBarValue(0, false);
                enableControl(btn_process, "Processar", true);
                processing = false;
            }
        }
        private void DrawColor(int index)
        {
            try
            {
                enableControl(btn_process, "Aguarde...", false);
                SetProgressBarValue(imageRender.TotalSize, true);
                imageRender.DrawColor(index);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {

                SetProgressBarValue(0, false);
                enableControl(btn_process, "Processar", true);
            }
        }
        private void cb_grid_Click(object sender, EventArgs e)
        {
            imageRender.Grid = cb_grid.Checked;
            imageRender.VPixelSize = (int)numericUpDown2.Value;
        }

        private void cb_tags_Click(object sender, EventArgs e)
        {
            imageRender.Tags = cb_tags.Checked;
        }
        private void btn_ignoredColor_Click(object sender, EventArgs e)
        {

            myPictureBox.Eyedropper = true;
        }
        private string HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
        private void myPictureBox_IgnoredColorChange(Color obj)
        {
            panel_ignoredColor.BackColor = obj;
            imageRender.IgnoredColor = obj;
            imgSlice.IgnoredColor = obj;
            textBox_colorHex.Text = HexConverter(obj);
        }

        private void btn_swatches_Click(object sender, EventArgs e)
        {
            Swatches swatches = new Swatches(this.colorList);
            swatches.Show();
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<ColorTagged> list = SaveLoadColorList.Load($"{Application.StartupPath}\\swatche.xml");
            if (list == null)
                return;
            for (int i = 0; i < list.Count; i++)
            {
                this.colorList.Add(list[i]);
            }
            this.colorList.Sort();
            this.bmp = new BmpUnpack(this.colorList);
            imageRender.BMP = this.bmp;
            imgSlice.BMP = bmp;
            start("C:\\Users\\bonus\\OneDrive\\Ambiente de Trabalho\\8 METROS TAQUEADO PARA CONTAGEM .bmp");
           //btn_process_Click(null, null);

        }

        private void btn_save_EnabledChanged(object sender, EventArgs e)
        {
            if (btn_save.Enabled)
                btn_save.Image = Properties.Resources.save;
            else
                btn_save.Image = Properties.Resources.save_disable;
        }

        private void btn_save_MouseHover(object sender, EventArgs e)
        {
            if (btn_save.Enabled)
                btn_save.Image = Properties.Resources.save_hover;
        }

        private void btn_save_MouseLeave(object sender, EventArgs e)
        {
            if (btn_save.Enabled)
                btn_save.Image = Properties.Resources.save;
        }
    }
}
