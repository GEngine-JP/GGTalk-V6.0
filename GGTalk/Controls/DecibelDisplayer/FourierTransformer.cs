using System;
using System.Collections.Generic;
using System.Text;

namespace GGTalk.Controls
{
    /// <summary>
    /// 傅立叶变换。
    /// </summary>
    internal static class FourierTransformer
    {
        public static double[] FFTDb(double[] source)
        {          
            int sourceLen = source.Length;
            int nu = (int)(Math.Log(sourceLen) / Math.Log(2));
            int halfSourceLen = sourceLen / 2;
            int nu1 = nu - 1;
            double[] xre = new double[sourceLen];
            double[] xim = new double[sourceLen];
            double[] decibel = new double[halfSourceLen];
            double tr, ti, p, arg, c, s;
            for (int i = 0; i < sourceLen; i++)
            {
                xre[i] = source[i];
                xim[i] = 0.0f;
            }
            int k = 0;
            for (int l = 1; l <= nu; l++)
            {
                while (k < sourceLen)
                {
                    for (int i = 1; i <= halfSourceLen; i++)
                    {
                        p = BitReverse(k >> nu1, nu);
                        arg = 2 * (double)Math.PI * p / sourceLen;
                        c = (double)Math.Cos(arg);
                        s = (double)Math.Sin(arg);
                        tr = xre[k + halfSourceLen] * c + xim[k + halfSourceLen] * s;
                        ti = xim[k + halfSourceLen] * c - xre[k + halfSourceLen] * s;
                        xre[k + halfSourceLen] = xre[k] - tr;
                        xim[k + halfSourceLen] = xim[k] - ti;
                        xre[k] += tr;
                        xim[k] += ti;
                        k++;
                    }
                    k += halfSourceLen;
                }
                k = 0;
                nu1--;
                halfSourceLen = halfSourceLen / 2;
            }
            k = 0;
            int r;
            while (k < sourceLen)
            {
                r = BitReverse(k, nu);
                if (r > k)
                {
                    tr = xre[k];
                    ti = xim[k];
                    xre[k] = xre[r];
                    xim[k] = xim[r];
                    xre[r] = tr;
                    xim[r] = ti;
                }
                k++;
            }
            for (int i = 0; i < sourceLen / 2; i++)
            {
                decibel[i] = 10.0 * Math.Log10((float)(Math.Sqrt((xre[i] * xre[i]) + (xim[i] * xim[i]))));
            }

            return decibel;
        }

        private static int BitReverse(int j, int nu)
        {
            int j2;
            int j1 = j;
            int k = 0;
            for (int i = 1; i <= nu; i++)
            {
                j2 = j1 / 2;
                k = 2 * k + j1 - 2 * j2;
                j1 = j2;
            }
            return k;
        }
    }
}
