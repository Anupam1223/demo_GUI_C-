
using System.Drawing;

/* author =@anupamSiwakoti */
namespace demoProgrammingLanguage
{
    /*
     * Shapes is the interface that our user will use to give drawing command, 
     * there are two main drawing related command in our program
     * moveTo and drawTo which is initialized by each derived classes 
     */
    internal interface Shapes
    {
        //its implementation is in derived classes, this draws the specified shape
        void drawTo(Graphics g, bool fill);

        //this moves the point to which he drawing should be drawn
        void moveTo(Graphics g);

    }
}
