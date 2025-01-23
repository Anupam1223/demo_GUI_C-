using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;

namespace demoProgrammingLanguage
{
    /* author =@anupamSiwakoti */
    // Filename: Loop.cs
    /// <summary>
    /// About
    /// -----
    ///     If user give 'while' command then we use this class for acheiving loop related operation
    ///     it has runWhileLoop method which is called by Form1 when program encounters while command
    /// </summary>
    internal sealed class Loop
    {
        //this command will keep the state in running command
        string conditionOperator, condition1, condition2;

        //object of class that runs repititve codes for circle triangle and rectangle
        Repititve repititiveCircleRectangleTriangle = Repititve.GetInstance;

        //static variable that will store the object of Condition if created else it will store null
        private static Loop runWhileLoopInstance = null;

        /// <summary>
        /// About
        /// -----
        ///      Since, our Condition class is singleton, only once instance of this class can be created.
        ///      It folows singleton pattern, one object of this class can perform all the operation of this class
        ///      So, GetInstance will instantiate a class if it doesnt have any object else it will not create.
        ///      We have used this in Form1 ///< see cref = "Form1" > see this class </ see >
        /// </summary>
        public static Loop GetInstance
        {
            get
            {
                if (runWhileLoopInstance == null)
                    runWhileLoopInstance = new Loop();
                return runWhileLoopInstance;
            }
        }

        /// <summary>
        ///  About
        ///  -----
        ///       runWhileLoop method is created to acheive separation of concern and loose coupling.
        ///       here we do all the task related to while loop
        /// </summary>
        /// <param name="runningCommand"> arraylist sent from runCommand method of Form1 class which contains runningCommands </param>
        /// <param name="variableList"> list of variables with its value is sent from runCommand method of Form1 class</param>
        /// <param name="whereIsEndif"> upto which line is if condition effective, position of endif </param>
        /// <param name="command"></param>
        /// <param name="positionX"> x coordinate</param>
        /// <param name="positionY"> y coordinate</param>
        /// <param name="colour"> color of pen </param>
        /// <param name="fill"> whether to fill shape or not </param>
        /// <param name="pictureBox1"> where shape are drawn </param>
        /// <param name="i">position of if command</param>
        /// <param name="textBox2"> </param>
        public void runWhileLoop(ArrayList runningCommand, ListDictionary variableList, 
            int whereIsEndLoop, string[] command, int positionX, int positionY,Color colour, bool fill,PictureBox pictureBox1, int i, TextBox textBox2) {

            //has condition 1 for while loop
            condition1 = (string)runningCommand[1];
            //comparision operator for while loop
            conditionOperator = (string)runningCommand[2];
            //condition for checking with condition 1
            condition2 = (string)runningCommand[3];
            int count = 0, radius = 0, height = 0, width = 0, increaseValue = 0;

            if (conditionOperator == "<")
            {
                //saves commands that are after while command in program
                ArrayList commandInsideWhile = new ArrayList();
                //checks the variables that are used inside while loop
                foreach (DictionaryEntry keyValue in variableList)
                    if ((string)keyValue.Key == condition1)
                    {
                        condition1 = keyValue.Value.ToString();
                        count = Int16.Parse(condition1);
                    }
                //run until count value exceeds condition 2
                do
                {
                    for (int j = i + 1; j < whereIsEndLoop; j++)
                    {
                        commandInsideWhile.AddRange(command[j].Split(' '));
                        if (commandInsideWhile.Contains("circle"))
                        {
                            ///< see cref = "Repititve" > see this class </ see >
                            repititiveCircleRectangleTriangle.repititveCircleCommands(variableList, commandInsideWhile, positionX,
                                positionY, colour, fill, pictureBox1);
                            commandInsideWhile.Clear();
                        }
                        if (commandInsideWhile.Contains("rectangle"))
                        {
                            repititiveCircleRectangleTriangle.repititveRectangleCommands(variableList, commandInsideWhile, positionX,
                                positionY, colour, fill, pictureBox1, textBox2);
                            commandInsideWhile.Clear();
                        }
                        if (commandInsideWhile.Contains("triangle"))
                        {
                            repititiveCircleRectangleTriangle.repititveTriangleCommands(variableList, commandInsideWhile, positionX,
                                positionY, colour, fill, pictureBox1, textBox2);
                            commandInsideWhile.Clear();
                        }
                        if (commandInsideWhile.Contains("moveTo"))
                        {
                            //increase the position of x and y for each rotation of loop
                            int increaseX = 0, increaseY = 0;
                            increaseX = Int16.Parse(commandInsideWhile[1].ToString());
                            increaseY = Int16.Parse(commandInsideWhile[2].ToString());
                            positionX = positionX + increaseX;
                            positionY = positionY + increaseY;
                            commandInsideWhile.Clear();

                        }
                        if (commandInsideWhile.Contains("radius"))
                        {
                            // value to increase for radius
                            increaseValue = Int16.Parse(commandInsideWhile[4].ToString());
                            // loop that finds radius and increase value each time the loop runs
                            foreach (DictionaryEntry keyValue in variableList)
                                //checks for radius
                                if ((string)keyValue.Key == "radius")
                                {
                                    //get the value to increase
                                    radius = Int16.Parse(keyValue.Value.ToString());
                                    radius = radius + increaseValue;
                                    //update the changed value
                                    variableList[(string)keyValue.Key] = radius;
                                    //textBox2.Text = "Radius -> " + radius.ToString();
                                    //breaks the loop if updated
                                    break;
                                }
                            commandInsideWhile.Clear();
                        }
                        if (commandInsideWhile.Contains("heigth"))
                        {
                            // value to increase for heigth
                            increaseValue = Int16.Parse(commandInsideWhile[4].ToString());
                            // loop that finds heigth and increase value each time the loop runs
                            foreach (DictionaryEntry keyValue in variableList)
                                //checks for heigth
                                if ((string)keyValue.Key == "height")
                                {
                                    //get the value to increase
                                    height = Int16.Parse(keyValue.Value.ToString());
                                    height = height + increaseValue;
                                    //update the changed value
                                    variableList[(string)keyValue.Key] = height;
                                    //breaks the loop if updated
                                    break;
                                }
                            commandInsideWhile.Clear();
                        }
                        if (commandInsideWhile.Contains("width"))
                        {
                            // value to increase for width
                            increaseValue = Int16.Parse(commandInsideWhile[4].ToString());
                            // loop that finds width and increase value each time the loop runs
                            foreach (DictionaryEntry keyValue in variableList)
                                //checks for width
                                if ((string)keyValue.Key == "width")
                                {
                                    //get the value to increase
                                    width = Int16.Parse(keyValue.Value.ToString());
                                    width = width + increaseValue;
                                    //update the changed value
                                    variableList[(string)keyValue.Key] = width;
                                    //breaks the loop if updated
                                    break;
                                }
                            commandInsideWhile.Clear();
                        }
                        if (commandInsideWhile.Contains("count"))
                        {
                            // value to increase for width
                            increaseValue = Int16.Parse(commandInsideWhile[4].ToString());

                            // loop that finds width and increase value each time the loop runs
                            foreach (DictionaryEntry keyValue in variableList)
                                //checks for count
                                if ((string)keyValue.Key == "count")
                                {
                                    //get the value to increase
                                    count = Int16.Parse(keyValue.Value.ToString());
                                    count = count + increaseValue;
                                    //update the changed value
                                    variableList[(string)keyValue.Key] = count;
                                    //breaks the loop if updated
                                    break;
                                }
                            commandInsideWhile.Clear();
                        }
                    }
                } while (count < Int16.Parse(condition2)); // runs while condition is matched with count
            }
        }
    }
}
