using System.Drawing;

/* author =@anupamSiwakoti */
namespace demoProgrammingLanguage
{
    // Filename: Shape.cs
    /// <summary>
    /// About
    /// -----
    ///       Abstract class that implements Shapes interface, it help us use factory pattern
    ///       also it has some methods that help initialize our rectangle, triangle and circle class
    ///       set method initializes them and help us achieve separation of concern
    ///       
    /// reference variables
    /// -------------------
    ///             color -> shape's colour
    ///             initialX, initialY -> initial point for x and y
    /// constructor
    /// -----------
    ///             only default construtor is there, set is used for initializing our shapes
    ///             
    /// </summary>
    internal abstract class Shape: Shapes
    {
        protected Color colour;
        protected int initialX, initialY;

        public Shape()
        {
        }

        /// <summary>
        /// About
        /// -----
        ///     set is declared as virtual so it can be overridden by a more specific child version
        ///     but is here so it can be called by that child version to do the generic stuff
        ///     note the use of the param keyword to provide a variable parameter
        ///     list to cope with some shapes having more setup information than others  
        /// 
        /// </summary>
        /// <param name="colour"> colour of pen that will draw shape</param>
        /// <param name="list"> list of parameters thet are used for drawing shape</param>
        public virtual void set(Color colour, params int[] list)
        {
            this.colour = colour;
            this.initialX = list[0];
            this.initialY = list[1];
        }


        /// <summary>
        /// About
        /// -----
        ///       the two methods below are from the Shapes interface
        ///       here we are passing on the obligation to implement them to the derived classes by declaring them as abstract
        ///       any derrived class must implement drawTo and moveTo method
        /// </summary>
        /// <param name="g"> graphics instantiation to draw shape in picture box</param>
        /// <param name="fill"> bool value to specify whether to fill the shapes</param>
        public abstract void drawTo(Graphics g, bool fill); 
        public abstract void moveTo(Graphics g);
    }
}
