using System.Drawing;

/* author =@anupamSiwakoti */
namespace demoProgrammingLanguage
{

    // Filename: Circle.cs
    /// <summary>
    /// About
    /// -----
    ///        circle class is used to draw circle, whenever we need to draw a circle we ask circle object from shapeFactory class
    ///        and draw circle using drawTo and moveTo methods, it extends Shape abstract class, which help us to achieve dynamic polymorphism
    ///        for factory pattern
    /// </summary>
    internal class Circle:Shape
    {
        int radius;

        public Circle() : base()
        {

        }
        /// <summary>
        /// About
        /// ------
        ///       we call this method whenever user calls circle method with proper params,this then draws
        ///       the circle. If fill is false then it only draws outline of circle else fills the circle
        /// </summary>
        /// <param name="g">Graphs object that we use to draw in our picture box</param>
        /// <param name="fill"> bool value to spcecify whether to fill the shape or not</param>
        public override void drawTo(Graphics g,bool fill)
        {
            //pen that draws the outline of triangle
            Pen p = new Pen(colour, 2);

            //if user wants to fill the circle then program flows through this condition
            if (fill)
            {
                //brush to paint whole circle
                SolidBrush b = new SolidBrush(colour);
                //draws and fills eclipse
                g.FillEllipse(b, initialX, initialY, radius * 2, radius * 2);
            }
            else {
                //fill the circle white if user doesnt want to fill the circle
                SolidBrush b = new SolidBrush(Color.White);
                //draws and fills eclipse
                g.FillEllipse(b, initialX, initialY, radius * 2, radius * 2);
            }
            //if user just wants to draw a normal circle
            g.DrawEllipse(p, initialX, initialY, radius * 2, radius * 2);

        }
        public override void moveTo(Graphics g)
        {


        }

        /// <summary>
        /// About
        /// -----
        ///     set method initializes the colour and initial point where the circle should be drawn
        ///     if no intial color is give then it draws black color
        ///     if no initial point are given the it drawn from the top left conor(point 0,0)
        /// </summary>
        /// <param name="colour"> colour with which we want to draw our circle</param>
        /// <param name="list"> list of parameters that we need to draw circle, in this case radius</param>
        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is radius
            base.set(colour, list[0], list[1]);
            this.radius = list[2];
        }
    }
}
