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

namespace Image_Fusion_Application
{
    public partial class MainWindow : Form
    {
        private string fileName;
        private double bmpScaleX;
        private double bmpScaleY;
        private int bmpOffsetX;
        private int bmpOffsetY;
        private Image_Fusion_Application.IrbFileReader reader, readerVisible;
        private Image_Fusion_Application.IrbImg imgStream, imgStreamVisible;

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
            fileName = Path.GetFileName(filePath.Text).Split('.')[0];
            reader = new Image_Fusion_Application.IrbFileReader(filePath.Text, "infrared");
            imgStream = new Image_Fusion_Application.IrbImg(reader,"infrared");
            //hoertekek alapjan a hokeprol az objektum kiszedese
            getMask(imgStream.EnvironmentalTemp, imgStream.tempMatrix, imgStream.GetWidth(), imgStream.GetHeight());
            ShowNextFrame();
        }

        //rgb kep kiolvasasa a fajlbol
        public void readVisibleImage() {
            fileName = Path.GetFileName(filePath.Text).Split('.')[0];
            readerVisible = new Image_Fusion_Application.IrbFileReader(filePath.Text, "visible");
            imgStreamVisible = new Image_Fusion_Application.IrbImg(readerVisible,"visible", imgStream.Palette);
            ShowNextVisibleFrame();
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
                    bmpScaleX = Double.Parse(line[1].Split('\r')[0]);
                if (line[0].CompareTo("BMP_Scale_Y") == 0)
                    bmpScaleY = Double.Parse(line[1].Split('\r')[0]);
                if (line[0].CompareTo("BMP_Offset_X") == 0)
                    bmpOffsetX = Int32.Parse(line[1].Split('\r')[0]);
                if (line[0].CompareTo("BMP_Offset_Y") == 0)
                    bmpOffsetY = Int32.Parse(line[1].Split('\r')[0]);

            }

            //rgb kep kirajzolasa
            Bitmap bmp = new Bitmap(w, h);

            var maxRedValue = float.MinValue;
            var minRedValue = float.MaxValue;

            var maxGreenValue = float.MinValue;
            var minGreenValue = float.MaxValue;

            var maxBlueValue = float.MinValue;
            var minBlueValue = float.MaxValue;


            for (int i = 0; i < w; i++)
                for(int j = 0; j< h; j++)
                    for(int k = 0; k < 3; k++)
                    {
                        maxRedValue = Math.Max(maxRedValue, img[i,j,0]);
                        minRedValue = Math.Min(minRedValue, img[i,j,0]);

                        maxGreenValue = Math.Max(maxGreenValue, img[i, j, 1]);
                        minGreenValue = Math.Min(minGreenValue, img[i, j, 1]);

                        maxBlueValue = Math.Max(maxBlueValue, img[i, j, 2]);
                        minBlueValue = Math.Min(minBlueValue, img[i, j, 2]);
                    }

            float scaleRed = 255.0f / (maxRedValue - minRedValue);
            float scaleGreen = 255.0f / (maxGreenValue - minGreenValue);
            float scaleBlue = 255.0f / (maxBlueValue - minBlueValue);

            for (int i = 0; i < w; i++) {
                for (int j = 0; j < h; j++)
                {
                    var red = (int)((img[i,j,0] - minRedValue) * scaleRed);
                    var green = (int)((img[i, j, 1] - minGreenValue) * scaleGreen);
                    var blue = (int)((img[i, j, 2] - minBlueValue) * scaleBlue);
                    var color = Color.FromArgb(red, green, blue);
                    //if (i % 100 == 0 && j % 100 == 0)
                    //    continue;
                    bmp.SetPixel(i, j, color);
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
