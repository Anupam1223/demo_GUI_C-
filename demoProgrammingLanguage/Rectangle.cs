using System.Drawing;

/* author =@anupamSiwakoti */
namespace demoProgrammingLanguage
{
    // Filename: Rectangle.cs
    /// <summary>
    /// About
    /// -----
    ///        Rectangle class is used to draw Rectangle, whenever we need to draw a Rectangle we ask Rectangle object from shapeFactory class
    ///        and draw Rectangle using drawTo and moveTo methods, it extends Shape abstract class, which help us to achieve dynamic polymorphism
    ///        for factory pattern
    /// </summary>
    internal class Rectangle:Shape
    {
        int width, height;
        public Rectangle() : base()
        {
        }

        /// <summary>
        /// About
        /// ------
        ///       we call this method whenever user calls rectangle method with proper params,this then draws
        ///       the rectangle. If fill is false then it only draws outline of rectangle else fills the rectangle
        /// </summary>
        /// <param name="g">Graphs object that we use to draw in our picture box</param>
        /// <param name="fill"> bool value to spcecify whether to fill the shape or not</param>
        public override void drawTo(Graphics g, bool fill)
        {
            //pen that draws the outline of triangle
            Pen p = new Pen(colour, 2);

            //if user wants to fill the circle then program flows through this condition
            if (fill)
            {
                //brush to paint whole rectangle
                SolidBrush b = new SolidBrush(colour);
                //draws and fills rectangle
                g.FillRectangle(b, initialX, initialY, width, height);
            }
            else {
                SolidBrush b = new SolidBrush(Color.White);
                g.FillRectangle(b, initialX, initialY, width, height);
            }
            //if user just wants to draw a normal rectangle
            g.DrawRectangle(p, initialX, initialY, width, height);
        }
        public override void moveTo(Graphics g)
        {
        }

        /// <summary>
        /// About
        /// -----
        ///     set method initializes the colour and initial point where the rectangle should be drawn
        ///     if no intial color is give then it draws black color
        ///     if no initial point are given the it drawn from the top left conor(point 0,0)
        /// </summary>
        /// <param name="colour"> colour with which we want to draw our rectangle</param>
        /// <param name="list"> list of parameters that we need to draw rectangle, in this case height, width</param>
        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.set(colour, list[0], list[1]);
            this.width = list[2];
            this.height = list[3];

        }

    }
}
