using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;


namespace demoProgrammingLanguage
{
    /* author =@anupamSiwakoti */
    // Filename: Repititive.cs
    /// <summary>
    /// About
    /// -----
    ///     Repititive class is created to make our code cleaner and help reduce complexity. Previosuly this program was running same code again and again
    ///     repedeatly now this class has separate code for drawing triangle, circle and rectangle. This also has repititiveVariableCommands to use same
    ///     code for working with variable related tasks. It is singleton class i.e same object will be used throughout our program
    /// </summary>
    public sealed class Repititve
    {
        //this command will keep the state in running command
        string conditionOperator, condition1, condition2;
        //shapeFactory object to get object whenever we need them
        private ShapeFactory factory = new ShapeFactory();
        //shape type for referencing to those child class of which we want the object of
        private Shape shape;

        //static variable that will store the object of Condition if created else it will store null
        private static Repititve repititiveCircleRectangleTriangle = null;

        /// <summary>
        /// About
        /// -----
        ///      Since, our Repititve class is singleton, only once instance of this class can be created.
        ///      It folows singleton pattern, one object of this class can perform all the operation of this class
        ///      So, GetInstance will instantiate a class if it doesnt have any object else it will not create.
        ///      We have used this in Form1 ///< see cref = "Form1" > see this class </ see >
        /// </summary>
        public static Repititve GetInstance
        {
            get
            {
                if (repititiveCircleRectangleTriangle == null)
                    repititiveCircleRectangleTriangle = new Repititve();
                return repititiveCircleRectangleTriangle;
            }
        }


        /// <summary>
        /// About
        /// -----
        ///       This method runs circle command, whenever our program finds circle command then we call this method to draw circle
        ///       This works for every context, as Loop class and Condition class also use this method, this can handle looping and condition related
        ///       circle commands
        /// Parameters
        /// ----------
        ///        variableList contains all the variables used in our running program, runningCommand contains the current command, positionX and positionY
        ///        is the position of cursor, colour is used for pen colour, fill tells whether to fill shape or not, and pictureBox1 will draw
        ///        picture in the PictureBox
        /// </summary>
        /// <param name="variableList">list of variables with its value is sent from runCommand method of Form1 class</param>
        /// <param name="runningCommand">arraylist sent from runCommand method of Form1 class which contains runningCommands </param>
        /// <param name="positionX"> x coordinate </param>
        /// <param name="positionY"> y coordinate</param>
        /// <param name="colour"> color of pen</param>
        /// <param name="fill"> whether to fill shape or not</param>
        /// <param name="pictureBox1"> where shape are drawn</param>
        public void repititveCircleCommands(ListDictionary variableList, ArrayList runningCommand, int positionX, 
            int positionY, Color colour, bool fill, PictureBox pictureBox1) {
            int radius = 0;

            condition1 = (string)runningCommand[1];
            /*
             * if variable is sent as radiuis then this condition will run
             * checks the availability of radius in variableList and use it to draw circle
             */
            if (variableList.Count > 0)
            {
                foreach (DictionaryEntry keyValue in variableList)
                    if ((string)keyValue.Key == condition1)
                    {
                        condition1 = keyValue.Value.ToString();
                        radius = Int16.Parse(condition1);

                    }
                if (int.TryParse(condition1, out radius))
                {
                    radius = Int16.Parse(condition1);
                }
            }
            /*
             * if variable is not sent as parameter for drawing circle
             */
            else
            {
                radius = Int16.Parse((string)runningCommand[1]);
            }

            //we ask for circle object from our factory class
            shape = factory.getShape("circle");
            //sets colour and position for drawing shape 
            shape.set(colour, positionX, positionY, radius);
            //creates graphics image for drawing in pictureBox
            Graphics drawCircle = pictureBox1.CreateGraphics();
            //calls drawTo method to draw actual circle with given radius
            shape.drawTo(drawCircle, fill);
        }
        public void repititveRectangleCommands(ListDictionary variableList, ArrayList runningCommand, int positionX,
            int positionY, Color colour, bool fill, PictureBox pictureBox1, TextBox textBox2)
        {

            int height = 0, width = 0;
            //condition1 will have height
            condition1 = (string)runningCommand[1];
            //condition1=2 will have width
            condition2 = (string)runningCommand[2];
            /*
             * if user sends height and width variable then this condition will run and 
             * checks the availability of height and width in variableList and use it to draw rectangle
             */
            if (variableList.Count > 0)
            {
                foreach (DictionaryEntry keyValue in variableList)
                {
                    //if height is matched then this condition will run
                    if ((string)keyValue.Key == condition1)
                    {
                        condition1 = keyValue.Value.ToString();
                        height = Int16.Parse(condition1);
                    }
                    //tries to convert condition1 to integer if it cant convert then this means that user didnt sent height as a variable
                    if (int.TryParse(condition1, out height))
                    {
                        height = Int16.Parse(condition1);
                    }
                    //if width is matched then this condition will run
                    if ((string)keyValue.Key == condition2)
                    {
                        condition2 = keyValue.Value.ToString();
                        width = Int16.Parse(condition2);
                    }
                    //tries to convert condition2 to integer if it cant convert then this means that user didnt sent width as a variable
                    if (int.TryParse(condition2, out width))
                    {
                        width = Int16.Parse(condition2);
                    }
                }

            }
            /*
             * if height and width is not sent as parameter for drawing rectangle
             */
            else
            {
                height = Int16.Parse((string)runningCommand[1]);
                width = Int16.Parse((string)runningCommand[2]);
            }


            //we ask for rectangle object from our factory class
            shape = factory.getShape("rectangle");
            //sets colour and position for drawing shape 
            shape.set(colour, positionX, positionY, height, width);
            //creates graphics image for drawing in pictureBox
            Graphics drawRectangle = pictureBox1.CreateGraphics();
            //calls drawTo method to draw actual rectangle with given width and height
            shape.drawTo(drawRectangle, fill);
        }
        public void repititveTriangleCommands(ListDictionary variableList, ArrayList runningCommand, int positionX,
            int positionY, Color colour, bool fill, PictureBox pictureBox1, TextBox textBox2)
        {
            int sideA = 0, sideB = 0, sideC = 0;

            //condition1 will have sideA
            condition1 = (string)runningCommand[1];
            //condition2 will have sideB
            condition2 = (string)runningCommand[2];
            //conditionOperator will have sideC
            conditionOperator = (string)runningCommand[3];
            /*
             * if user sends sideA, sideB and sideC variable then this condition will run and 
             * checks the availability of sideA, sideB and sideC in variableList and use it to draw triangle
             */
            if (variableList.Count > 0)
            {
                foreach (DictionaryEntry keyValue in variableList)
                {
                    //if sideA is matched then this condition will run
                    if ((string)keyValue.Key == condition1)
                    {
                        condition1 = keyValue.Value.ToString();
                        sideA = Int16.Parse(condition1);
                    }
                    //tries to convert condition1 to integer if it cant convert then this means that user didnt sent sideA as a variable
                    if (int.TryParse(condition1, out sideA))
                    {
                        sideA = Int16.Parse(condition1);
                    }
                    //if sideB is matched then this condition will run
                    if ((string)keyValue.Key == condition2)
                    {
                        condition2 = keyValue.Value.ToString();
                        sideB = Int16.Parse(condition2);
                        //textBox2.Text = sideB.ToString();
                    }
                    //tries to convert condition2 to integer if it cant convert then this means that user didnt sent sideB as a variable
                    if (int.TryParse(condition2, out sideB))
                    {
                        sideB = Int16.Parse(condition2);
                    }
                    //if sideC is matched then this condition will run
                    if ((string)keyValue.Key == conditionOperator)
                    {
                        conditionOperator = keyValue.Value.ToString();
                        sideC = Int16.Parse(conditionOperator);

                    }
                    //tries to convert conditionOperator to integer if it cant convert then this means that user didnt sent sideC as a variable
                    if (int.TryParse(conditionOperator, out sideC))
                    {
                        sideC = Int16.Parse(conditionOperator);
                    }
                }
            }
            /*
             * if sideA,sideB and sideC is not sent as parameter for drawing traingle
             */
            else
            {
                sideA = Int16.Parse(condition1);
                sideB = Int16.Parse(condition2);
                sideC = Int16.Parse(conditionOperator);
            }
            //we ask for triangle object from our factory class
            shape = factory.getShape("triangle");
            //sets colour and position for drawing shape 
            shape.set(colour, positionX, positionY, sideA, sideB, sideC);
            //creates graphics image for drawing in pictureBox
            Graphics drawTriangle = pictureBox1.CreateGraphics();
            //calls drawTo method to draw actual triangle with given width and height
            shape.drawTo(drawTriangle, fill);

        }

        public void repititiveVariableCommands(ArrayList runningCommand, ListDictionary variableList) {

            //whichVariable has the variable name that is sent by user
            string whichVariable = (string)runningCommand[0];
            //if this variable is already used then update the variable
            if (variableList.Contains(whichVariable))
            {
                //checks all the variables in variableList
                foreach (DictionaryEntry keyValue in variableList)
                {
                    //update the changed value
                    variableList[(string)keyValue.Key] = Int16.Parse((string)runningCommand[2]);
                    break;
                }
            }
            //if the variable is used for the first time then add that variable to variableList
            else
            {
                variableList.Add(whichVariable, Int16.Parse((string)runningCommand[2]));
            }
        }
    }
}
