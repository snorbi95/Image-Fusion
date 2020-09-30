using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;

namespace Image_Fusion_Application
{
    public partial class MainWindow : Form
    {
        string fileName, fileExtension;
        double bmpScaleX;
        double bmpScaleY;
        float[] temperatures;
        int bmpOffsetX;
        int bmpOffsetY;
        Image_Fusion_Application.IrbFileReader reader, readerVisible;
        Image_Fusion_Application.IrbImg imgStream, imgStreamVisible;

        public MainWindow()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            //fuzios parameterek alapertelmezett kiiratasa
            thresholdText.Text = (thresholdScroll.Value / 100.0).ToString();
            ratioText.Text = (ratioScroll.Value / 100.0).ToString();
        }

        //hokep kiolvasa a fajlbol
        public void readInfraredImage() {
            //irb fajl nevenek kiszedese
            try
            {
                fileName = Path.GetFileName(filePath.Text).Split('.')[0];
                fileExtension = Path.GetFileName(filePath.Text).Split('.')[1];
                reader = new Image_Fusion_Application.IrbFileReader(filePath.Text, fileExtension, "infrared");
                imgStream = new Image_Fusion_Application.IrbImg(reader, "infrared");
                //hoertekek alapjan a hokeprol az objektum kiszedese
                getMask(imgStream.EnvironmentalTemp, imgStream.tempMatrix, imgStream.GetWidth(), imgStream.GetHeight());
                ShowNextFrame();
                var value = thresholdScroll.Value / 100.0;
                var textValue = Math.Round((temperatures[0] + ((temperatures[1] - temperatures[0]) * value)) - 273, 1);
                thresholdText.Text = textValue.ToString();
            }
            catch (Exception e) {
                MessageBox.Show("Error while reading thermal image!\n" + e.Message);
            }
        }

        //rgb kep kiolvasasa a fajlbol
        public void readVisibleImage() {
            try
            {
                fileName = Path.GetFileName(filePath.Text).Split('.')[0];
                fileExtension = Path.GetFileName(filePath.Text).Split('.')[1];
                readerVisible = new Image_Fusion_Application.IrbFileReader(filePath.Text,fileExtension, "visible");
                imgStreamVisible = new Image_Fusion_Application.IrbImg(readerVisible, "visible", imgStream.Palette);
                ShowNextVisibleFrame();
            }
            catch (Exception e) {
                MessageBox.Show("Error while reading RGB image!\n" + e.Message + "\n" + readerVisible.GetTextInfo());
            }
}

        private void ShowNextVisibleFrame()
        {

            var img = imgStreamVisible.GetVisibleData();
            var w = imgStreamVisible.GetWidth();
            var h = imgStreamVisible.GetHeight();
            var dataSize = w * h;

            
            //fuseImageLog.Text = readerVisible.GetBlockOffset(1).ToString();
            //szoveges informaciok kiszedese
            var text = readerVisible.GetTextInfo();
            string[] textInfo = text.Split('\n');

            for (int i = 0; i < textInfo.Length; i++)
            {
                string[] line = textInfo[i].Split('=');
                if (line[0].CompareTo("BMP_Scale_X") == 0)
                    bmpScaleX = Double.Parse(line[1],CultureInfo.InvariantCulture);
                if (line[0].CompareTo("BMP_Scale_Y") == 0)
                    bmpScaleY = Double.Parse(line[1], CultureInfo.InvariantCulture);
                if (line[0].CompareTo("BMP_Offset_X") == 0)
                    bmpOffsetX = Int32.Parse(line[1], CultureInfo.InvariantCulture);
                if (line[0].CompareTo("BMP_Offset_Y") == 0)
                    bmpOffsetY = Int32.Parse(line[1], CultureInfo.InvariantCulture);

            }

            //rgb kep kirajzolasa
            Bitmap bmp = new Bitmap(w, h);

            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++)
                {
                    var red = (int)img[i, j, 0];
                    var green = (int)img[i, j, 1];
                    var blue = (int)img[i, j, 2];
                    var color = Color.FromArgb(red, green, blue);
                    double hue;
                    double saturation;
                    double value;
                    ColorToHSV(color, out hue, out saturation, out value);
                    Color copy = ColorFromHSV(hue * 1.1, saturation * 1.5, value * 1.1);
                    //if (i % 100 == 0 && j % 100 == 0)
                    //    continue;
                    bmp.SetPixel(i, j, copy);
                }
            }

            VisibleImage.Image = bmp;
            //VisibleImage.Image = ApplyBrightness(VisibleImage.Image,0.20f);
        }


        private void ShowNextFrame()
        {

            var img = imgStream.GetData();
            var w = imgStream.GetWidth();
            var h = imgStream.GetHeight();
            var dataSize = w * h;

            var maxValue = float.MinValue;
            var minValue = float.MaxValue;

            for (int i = 0; i < dataSize; i++)
            {
                maxValue = Math.Max(maxValue, img[i]);
                minValue = Math.Min(minValue, img[i]);
            }

            if (maxValue == minValue)
            {
                maxValue = minValue + 1;
            }

            DirectBitmap bmp = new DirectBitmap(w, h);

            float scale = 255.0f / (maxValue - minValue);

            //szinezes alapjan a hokep kirajzolasa
            switch (thermalType.Text)
            {
                case "Rainbow":
                    for (int i = 0; i < dataSize; i++)
                    {
                        var x = i % w;
                        var y = i / w;
                        var c = (int)((img[i] - minValue) * scale);
                        bmp.SetPixel(x, y, MapRainbowColor(img[i], minValue, maxValue));
                    }
                    break;
                case "Grayscale":
                    for (int i = 0; i < dataSize; i++)
                    {
                        var x = i % w;
                        var y = i / w;
                        var c = (int)((img[i] - minValue) * scale);
                        bmp.SetPixel(x, y, Color.FromArgb(c, c, c));
                    }
                    break;
                case "BlueToRed":
                    for (int i = 0; i < dataSize; i++)
                    {
                        var x = i % w;
                        var y = i / w;
                        var c = (int)((img[i] - minValue) * scale);
                        bmp.SetPixel(x, y, MapBlueToRedColor(img[i], minValue, maxValue));
                    }
                    break;
                case "Fire":
                    for (int i = 0; i < dataSize; i++)
                    {
                        var x = i % w;
                        var y = i / w;
                        var c = (int)((img[i] - minValue) * scale);
                        bmp.SetPixel(x, y, MapFireColor(img[i], minValue, maxValue));
                    }
                    break;
                default:
                    for (int i = 0; i < dataSize; i++)
                    {
                        var x = i % w;
                        var y = i / w;
                        var c = (int)((img[i] - minValue) * scale);
                        bmp.SetPixel(x, y, MapSevenRainbowColor(img[i], minValue, maxValue));
                    }
                    break;
            }
            ThermalImage.Image = bmp.Bitmap;
        }

        //a hokep szelsoertekeinek meghatarozasa
        public float[] getEdgeValues(float[,] tempMatrix, int w, int h) {
            float[] returnValues = { tempMatrix[0, 0], tempMatrix[0, 0] };
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++) {
                    if (tempMatrix[i, j] < returnValues[0])
                        returnValues[0] = tempMatrix[i, j];
                    if (tempMatrix[i, j] > returnValues[1])
                        returnValues[1] = tempMatrix[i, j];
                }
            temperatures = returnValues;
            return returnValues;
        }

        //hokep maskolasa
        public void getMask(float enviromentTemp, float[,] tempMatrix, int w, int h) 
        {
            float[] tempRange = getEdgeValues(tempMatrix, w, h);
            int[,] mask = new int[w, h];
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++) {
                    var value = (int)(255 * (tempMatrix[i, j] - tempRange[0]) /
                    (tempRange[1] - tempRange[0]));
                    if (value > 25)
                        mask[i, j] = value;
                    else
                        mask[i, j] = 0;
                }
            DirectBitmap bmp = new DirectBitmap(w, h);
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    bmp.SetPixel(i,j, Color.FromArgb(mask[i,j], mask[i, j], mask[i, j]));
            maskImage.Image = bmp.Bitmap;
        }
    }
}
