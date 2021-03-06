﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_Fusion_Application
{
    public partial class MainWindow
    {
        //szivarvany skala kirajzolasa a hokepen
        private Color MapRainbowColor(float value, float maximum_value, float minimum_value)
        {
            // a hoertek konvertalasa 0 és 1023 koze
            int int_value = (int)(1023 * (value - maximum_value) /
                (minimum_value - maximum_value));

            // az ertek atkonvertalasa a megfelelo szinre
            if (int_value < 256)
            {
                return Color.FromArgb(0, int_value, 255);
            }
            else if (int_value < 512)
            {
                int_value -= 256;
                return Color.FromArgb(0, 255, 255 - int_value);
            }
            else if (int_value < 768)
            {
                int_value -= 512;
                return Color.FromArgb(int_value, 255, 0);
            }
            else
            {
                int_value -= 768;
                return Color.FromArgb(255, 255 - int_value, 0);
            }
        }

        private Color MapSevenRainbowColor(float value, float maximum_value, float minimum_value)
        {
            // a hoertek konvertalasa 0 és 1023 koze
            int int_value = (int)(1023 * (value - maximum_value) /
                (minimum_value - maximum_value));

            // az ertek atkonvertalasa a megfelelo szinre
            if (int_value < 171)
            {
                int_value = (int)(255 * (int_value - 0) /
                (170 - 0));
                return Color.FromArgb(0, 0, int_value);
            }
            else if (int_value < 341)
            {
                //int_value -= 170;
                int_value = (int)(255 * (int_value - 170) /
                (340 - 170));
                return Color.FromArgb(0, int_value, 255);
            }
            else if (int_value < 511)
            {
                //int_value -= 340;
                int_value = (int)(255 * (int_value - 340) /
                (510 - 340));
                return Color.FromArgb(0, 255, 255-int_value);
            }
            else if (int_value < 681)
            {
                //int_value -= 510;
                int_value = (int)(255 * (int_value - 510) /
                (680 - 510));
                return Color.FromArgb(int_value, 255, 0);
            }
            else if (int_value < 851)
            {
                //int_value -= 680;
                int_value = (int)(255 * (int_value - 680) /
                (850 - 680));
                return Color.FromArgb(255, 255-int_value, 0);
            }
            else
            {
                //int_value -= 850;
                int_value = (int)(255 * (int_value - 850) /
                (1023 - 850));
                return Color.FromArgb(255, int_value, int_value);
            }
        }

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (v < 0) v = 0;
            if (p < 0) p = 0;
            if (q < 0) q = 0;
            if (t < 0) t = 0;

            if (v > 255) v = 255;
            if (p > 255) p = 255;
            if (q > 255) q = 255;
            if (t > 255) t = 255;

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        private Color MapFireColor(float value, float maximum_value, float minimum_value)
        {
            // a hoertek konvertalasa 0 és 1023 koze
            int int_value = (int)(1023 * (value - maximum_value) /
                (minimum_value - maximum_value));

            // az ertek atkonvertalasa a megfelelo szinre
            if (int_value < 342)
            {
                int_value = (int)(255 * (int_value - 0) /
                (342 - 0));
                return Color.FromArgb(int_value, 0, 0);
            }
            else if (int_value < 684)
            {
                int_value = (int)(255 * (int_value - 342) /
                (684 - 342));
                return Color.FromArgb(255, int_value, 0);
            }
            else
            {
                int_value = (int)(255 * (int_value - 684) /
                (1023 - 684));
                return Color.FromArgb(255, 255, int_value);
            }
        }

        private Color MapBlueToRedColor(float value, float maximum_value, float minimum_value) {

            //ertek atkonvertalasa 0 255 koze
            int int_value = (int)(1023 * (value - maximum_value) /
                (minimum_value - maximum_value));

            // az ertek atkonvertalasa a megfelelo szinre
            if (int_value < 342)
            {
                int_value = (int)(255 * (int_value - 0) /
                (342 - 0));
                return Color.FromArgb(0, 0, int_value);
            }
            else if (int_value < 684)
            {
                int_value = (int)(255 * (int_value - 342) /
                (684 - 342));
                return Color.FromArgb(int_value, int_value, 255);
            }
            else
            {
                int_value = (int)(255 * (int_value - 684) /
                (1023 - 684));
                return Color.FromArgb(255, 255-int_value, 255 - int_value);
            }
        }
    }
}
