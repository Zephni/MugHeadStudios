using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MugHeadStudios
{
    public static class Extensions
    {
        #region String
        public static float ToFloat(this string value)
        {
            return (float)Convert.ToDouble(value);
        }

        public static float ToInt(this string value)
        {
            return Convert.ToInt16(value);
        }

        public static Point ToPoint(this string value)
        {
            var temp = value.Split(',');
            return new Point(Convert.ToInt16(temp[0]), Convert.ToInt16(temp[1]));
        }

        public static List<Point> ToPointList(this string value)
        {
            List<Point> pointList = new List<Point>();
            foreach(var item in value.Split(' '))
            {
                var temp = item.Split(',');
                pointList.Add(new Point(Convert.ToInt16(temp[0]), Convert.ToInt16(temp[1])));
            }

            return pointList;
        }
        #endregion

        #region Int
        public static int Wrap(this int value, int min, int max)
        {
            return (((value - min) % (max - min)) + (max - min)) % (max - min) + min;
        }

        public static int Between(this int value, int min, int max)
        {
            if (value < min) return min;
            else if (value > max) return max;
            else return value;
        }
        #endregion

        #region float
        public static int ToInt(this float value)
        {
            return Convert.ToInt16(value);
        }

        public static float Wrap(this float value, float min, float max)
        {
            return (((value - min) % (max - min)) + (max - min)) % (max - min) + min;
        }

        public static float Between(this float value, float min, float max)
        {
            if (value < min) return min;
            else if (value > max) return max;
            else return value;
        }
        #endregion

        #region Point
        public static float GetDistance(this Point value, Point other)
        {
            float distance = (float)Math.Sqrt(Math.Pow((other.x - value.x), 2) + Math.Pow((other.y - value.y), 2));
            return distance;
        }
        #endregion

        #region Vector2
        public static float GetDistance(this Vector2 value, Vector2 other)
        {
            return (float)Math.Sqrt(Math.Pow((other.x - value.x), 2) + Math.Pow((other.y - value.y), 2));
        }

        public static Vector2 Copy(this Vector2 value, Vector2 source)
        {
            return new Vector2(source.x, source.y);
        }
        #endregion

        #region Color[]
        public static Color[] Copy1D(this Color[] origional, Rect selectArea)
        {
            Color[] copied = new Color[(int)selectArea.width * (int)selectArea.height];
            for (int x = 0; x < selectArea.width; x++)
                for (int y = 0; y < selectArea.height; y++)
                    copied[x + y * (int)selectArea.width] = origional[(x + (int)selectArea.x) + (y + (int)selectArea.y) * (int)selectArea.width];

            return copied;
        }

        public static void Shift(this Color[] origional, Point bounds2D, Rect selectArea, Point newPosition)
        {
            Rect bounds = new Rect(0, 0, bounds2D.x, bounds2D.y);
            Color[] copied = origional.Copy1D(selectArea);

            for (int x = 0; x < selectArea.width; x++)
                for (int y = 0; y < selectArea.height; y++)
                    origional[(x + newPosition.x).Wrap((int)bounds.x, (int)bounds.width) + (y + newPosition.x).Wrap((int)bounds.y, (int)bounds.height) * (int)bounds.width] = copied[x + y * (int)bounds.width];
        }
        #endregion

        #region Color[,]
        public static Color[,] Copy2D(this Color[,] origional, Rect selectArea)
        {
            Color[,] copied = new Color[(int)selectArea.width, (int)selectArea.height];
            for (int x = 0; x < selectArea.width; x++)
                for (int y = 0; y < selectArea.height; y++)
                    copied[x, y] = origional[x + (int)selectArea.x, y + (int)selectArea.y];

            return copied;
        }

        public static void Shift(this Color[,] origional, Rect selectArea, Point newPosition)
        {
            Rect bounds = new Rect(0, 0, origional.GetLength(0), origional.GetLength(1));
            Color[,] copied = origional.Copy2D(selectArea);

            for (int x = 0; x < selectArea.width; x++)
                for (int y = 0; y < selectArea.height; y++)
                    origional[(x + newPosition.x).Wrap((int)bounds.x, (int)bounds.width), (y + newPosition.y).Wrap((int)bounds.y, (int)bounds.height)] = copied[x, y];
        }
        #endregion

        #region Texture2D
        public static void Clear(this Texture2D texture)
        {
            texture = new Texture2D(texture.width, texture.height);
        }

        public static Vector2 Size(this Texture2D texture)
        {
            return new Vector2(texture.width, texture.height);
        }

        public static Color[,] To2DArray(this Texture2D texture)
        {
            Color[] colorsOne = texture.GetPixels();

            Color[,] colorsTwo = new Color[texture.width, texture.height]; //The new, easy to read 2D array
            for (int x = 0; x < texture.width; x++) //Convert!
                for (int y = 0; y < texture.height; y++)
                    colorsTwo[x, y] = colorsOne[x + y * texture.width];

            return colorsTwo; //Done!
        }

        
        public static void From2DArray(this Texture2D texture, Color[,] colors2D)
        {
            Color[] colors1D = new Color[colors2D.Length];
            for (int x = 0; x < colors2D.GetLength(0); x++)
                for (int y = 0; y < colors2D.GetLength(1); y++)
                    colors1D[x + y * colors2D.GetLength(0)] = colors2D[x, y];

            texture.SetPixels(colors1D);
        }
        #endregion
    }
}