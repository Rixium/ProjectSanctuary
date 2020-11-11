using System;
using Microsoft.Xna.Framework;

namespace Application.Utils
{
    internal static class ColorHelpers
    {
        public static Color HsvToRgb(double hue, double saturation, double value)
        {
            double red;
            double green;
            double blue;

            var num = hue / 60.0;
            var i = (int) Math.Floor(num);
            var f = num - i;
            var pv = value * (1.0 - saturation);
            var qv = value * (1.0 - saturation * f);
            var tv = value * (1.0 - saturation * (1.0 - f));

            switch (i)
            {
                case 0:
                    red = value;
                    green = tv;
                    blue = pv;
                    break;
                case 1:
                    red = qv;
                    green = value;
                    blue = pv;
                    break;
                case 2:
                    red = pv;
                    green = value;
                    blue = tv;
                    break;
                case 3:
                    red = pv;
                    green = qv;
                    blue = value;
                    break;
                case 4:
                    red = tv;
                    green = pv;
                    blue = value;
                    break;
                case 5:
                    red = value;
                    green = pv;
                    blue = qv;
                    break;
                case 6:
                    red = value;
                    green = tv;
                    blue = pv;
                    break;
                case -1:
                    red = value;
                    green = pv;
                    blue = qv;
                    break;
                default:
                    red = green = blue = value;
                    break;
            }

            return new Color(
                MathHelper.Clamp((int) (red * 255.0), 0, 255),
                MathHelper.Clamp((int) (green * 255.0), 0, 255),
                MathHelper.Clamp((int) (blue * 255.0), 0, 255));
        }
    }
}