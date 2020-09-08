using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Fusion_Application
{
    public partial class MainWindow
    {
        public Color ApplyContrast(Color inputColor, int threshold) {


            var contrast = Math.Pow((100.0 + threshold) / 100.0, 2);
            var alpha = inputColor.B;
            var red = (((((inputColor.G * 1) / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
            var green = (((((inputColor.R * 0.5) / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
            var blue = ((((inputColor.A / 255.0) - 0.5) * contrast) + 0.5) * 255.0;
            if (red > 255) red = 255;
            if (red < 0) red = 0;
            if (green > 255) green = 255;
            if (green < 0) green = 0;
            if (blue > 255) blue = 255;
            if (blue < 0) blue = 0;

            return Color.FromArgb((int)red, (int)green, (int)blue);

        }

        public Image ApplyBrightness(Image inputImage, float value) {
            float[][] colorMatrixElements = {
            new float[] {1,0,0,0,0},
            new float[] {0,1,0,0,0},
            new float[] {0,0,1,0,0},
            new float[] {0,0,0,1,0},
            new float[] {value,value,value,0,1}
            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            Image _img = inputImage;
            //PictureBox1.Image
            Graphics _g = default(Graphics);
            Bitmap bm_dest = new Bitmap(Convert.ToInt32(_img.Width), Convert.ToInt32(_img.Height));
            _g = Graphics.FromImage(bm_dest);
            _g.DrawImage(_img, new Rectangle(0, 0, bm_dest.Width + 1, bm_dest.Height + 1), 0, 0, bm_dest.Width + 1, bm_dest.Height + 1, GraphicsUnit.Pixel, imageAttributes);
            return bm_dest;
        }
    }
}
