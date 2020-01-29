using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Conturer
{
    public class Conturer
    {
        public int nContours;
        public Image<Gray, Byte> img;
        VectorOfVectorOfPoint contours;
        public Conturer()
        {
        }

        public List<List<int[]>> getContours(string pathToImage, double tolerance)
        {
            img = new Image<Gray, byte>(pathToImage);
            UMat uimage = new UMat();
            contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(img, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
            List<List<int[]>> allPoints = new List<List<int[]>>();
            int count = contours.Size;
            nContours = count;
            for (int i = 0; i < count; i++)
            {
                using (VectorOfPoint contour = contours[i])
                using (VectorOfPoint approxContour = new VectorOfPoint())
                {
                    CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * tolerance, true);
                    Point[] pts = approxContour.ToArray();
                    List<int[]> contPoints = new List<int[]>();
                    for (int k = 0; k < pts.Length; k++)
                    {
                        int[] pointsCord = new int[2];
                        pointsCord[0] = pts[k].X;
                        pointsCord[1] = pts[k].Y;
                        contPoints.Add(pointsCord);
                    }
                    allPoints.Add(contPoints);
                }
            }
            return allPoints;
        }
    }


}
