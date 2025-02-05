﻿
using System.Drawing;

/* author =@anupamSiwakoti */
namespace demoProgrammingLanguage
{
    // Filename: Triangle.cs
    /// <summary>
    /// About
    /// -----
    ///        Triangle class is used to draw Triangle, whenever we need to draw a Triangle we ask Triangle object from shapeFactory class
    ///        and draw Triangle using drawTo and moveTo methods, it extends Shape abstract class, which help us to achieve dynamic polymorphism
    ///        for factory pattern
    /// </summary>
    internal class Triangle:Shape
    {
        int side1, side2, side3;
        public Triangle() : base()
        {
        }

        /// <summary>
        /// About
        /// ------
        ///       we call this method whenever user calls triangle method with proper params,this then draws
        ///       the triangle. If fill is false then it only draws outline of triangle else fills the triangle
        /// <param name="g">Graphs object that we use to draw in our picture box</param>
        /// <param name="fill"> bool value to spcecify whether to fill the shape or not</param>
        public override void drawTo(Graphics g, bool fill)
        {
            int final_point = 0;
            int temp_point = 0;

            //pen that draws the outline of triangle
            Pen pen = new Pen(colour, 2);

            //this uses the theorem of triangle, no side must be greater than sum of other two sides
            if (side1 + side2 > side3 && side3 + side1 > side2 && side2 + side3 > side1) {

                //if side 2 is greater than point 1 then this condition is satisfied
                if (side2 > side1) {
                    if (side3 > side2)
                    {
                        temp_point = side3;
                        side3 = side1;
                        side3 = temp_point;
                    }
                    else {
                        temp_point = side2;
                        side2 = side3;
                        side3 = temp_point;
                    }
                }
                //if side 3 is greater than side 1 then this condition is satisfied
                if (side3 > side1) {
                    temp_point = side3;
                    side3 = side1;
                    side1 = temp_point;
                }

                /*formulae for drawing a triangle using 3 sides 
                 * 
                 *  s = (a + b + c)/2
                 *  Area = √[s(s-a)(s-b)(s-c)]
                 *  calc = 2 * Area/ a
                 *  calc_point = (calc * calc) - (b*b)
                 *  
                 */
                double formula = (side1 + side2 + side3) / 2;
                double area = System.Math.Sqrt(formula * (formula - side1) * (formula - side2) * (formula - side3));
                double calc = 2 * area / side1;
                double calc_point = (calc * calc) - (side2 * side2);
                int calc2 = System.Convert.ToInt32(calc);
                if (calc_point < 0)
                {
                    calc_point = calc_point * (-1);
                    double value = System.Math.Sqrt(calc_point);
                    final_point = System.Convert.ToInt32(value);
                }
                else 
                { 
                    double value = System.Math.Sqrt(calc_point);
                    final_point= System.Convert.ToInt32(value);
                }

                // points generated by calculating sides from three sides
                Point[] point = new Point[3];
                point[0] = new Point(initialX , initialY);
                point[1] = new Point(initialX, side1 + initialY);
                point[2] = new Point(calc2 + initialX, final_point+initialY);

                /* 
                 * using GraphicsPath for drawing and filling our triangle, 
                 * we use three line for drawing using GraphicsPath 
                 */
                var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddLines(point);
                path.CloseFigure();

                //if user wants to fill the circle then program flows through this condition
                if (fill)
                {
                    // Fill Triangle
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(colour);
                    g.FillPath(myBrush, path);
                }
                else {
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(Color.White);
                    g.FillPath(myBrush, path);
                }
                
                // Draw Triangle
                g.DrawPath(pen, path);

            }
        }
        public override void moveTo(Graphics g)
        {

        }
        /// <summary>
        /// About
        /// -----
        ///     set method initializes the colour and initial point where the triangle should be drawn
        ///     if no intial color is give then it draws black color
        ///     if no initial point are given the it drawn from the top left conor(point 0,0)
        /// </summary>
        /// <param name="colour"> colour with which we want to draw our triangle</param>
        /// <param name="list"> list of parameters that we need to draw triangle, in this case side a, b, c</param>
        public override void set(Color colour, params int[] list)
        {
            /*
             * list[0] is initialX, list[1] is initialY, 
             * list[2] is point1, list[3] is point2, list[4] is point3
             */
            base.set(colour, list[0], list[1]);

            this.side1 = list[2];
            this.side2 = list[3];
            this.side3 = list[4];  
        }

    }
}
