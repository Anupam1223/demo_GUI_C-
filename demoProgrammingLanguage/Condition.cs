using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;

namespace demoProgrammingLanguage
{
    /* author =@anupamSiwakoti */
    // Filename: Condition.cs
    /// <summary>
    /// About
    /// -----
    ///     It is a singleton class which will only have one object throughout the execution of program
    ///     , when user inputs 'if' for running if condition then we use this class in Form1.
    ///     
    /// Variables
    /// --------
    ///      * conditionOperator, condition1, condition2 -> this command will keep the state in running command
    ///      * runIfConditionInstance -> static variable that will store the object of Condition if created else it will store null
    /// </summary>
    internal sealed class Condition
    {
        string conditionOperator, condition1, condition2;
        //object of class that runs repititve codes for circle triangle and rectangle
        Repititve repititiveCircleRectangleTriangle = Repititve.GetInstance;

        //static variable that will store the object of Condition if created else it will store null
        private static Condition runIfConditionInstance = null;

        /// <summary>
        /// About
        /// -----
        ///      Since, our Condition class is singleton, only once instance of this class can be created.
        ///      It folows singleton pattern, one object of this class can perform all the operation of this class
        ///      So, GetInstance will instantiate a class if it doesnt have any object else it will not create.
        ///      We have used this in Form1 ///< see cref = "Form1" > see this class </ see >
        /// </summary>
        public static Condition GetInstance
        {
            get
            {
                if (runIfConditionInstance == null)
                    runIfConditionInstance = new Condition();
                return runIfConditionInstance;
            }
        }

        /// <summary>
        ///  About
        ///  -----
        ///       runIfCondition method is created to acheive separation of concern and loose coupling.
        ///       here we do all the task related to if condition
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
        public void runIfCondition(ArrayList runningCommand, ListDictionary variableList,
    int whereIsEndif, string[] command, int positionX, int positionY, Color colour, bool fill, PictureBox pictureBox1, int i, TextBox textBox2)
        {
            //has condition 1 for if statement
            condition1 = (string)runningCommand[1];
            //comparision operator for if statement
            conditionOperator = (string)runningCommand[2];
            //condition for checking with condition 1
            condition2 = (string)runningCommand[3];

            if (conditionOperator == "==")
            {
                //saves commands that are after if command in program
                ArrayList commandInsideIf = new ArrayList();
                //checks the variables that are used inside if condition
                foreach (DictionaryEntry keyValue in variableList)
                    //if the variable that user sent matches variable inside variableList
                    if ((string)keyValue.Key == condition1)
                    {
                        //value of variable used by user
                        condition1 = keyValue.Value.ToString();
                        if (Int16.Parse(condition1) == Int16.Parse(condition2))
                        {
                            //run until endif
                            for (int j = i + 1; j < whereIsEndif; j++)
                            {
                                commandInsideIf.AddRange(command[j].Split(' '));
                                if (commandInsideIf.Contains("circle"))
                                {
                                    ///< see cref = "Repititve" > see this class </ see >
                                     repititiveCircleRectangleTriangle.repititveCircleCommands(variableList, commandInsideIf, positionX,
                                            positionY, colour, fill, pictureBox1);
                                }
                                if (commandInsideIf.Contains("rectangle"))
                                {
                                    repititiveCircleRectangleTriangle.repititveRectangleCommands(variableList, commandInsideIf, positionX,
                                        positionY, colour, fill, pictureBox1, textBox2);
                                }
                                if (commandInsideIf.Contains("triangle"))
                                {
                                    repititiveCircleRectangleTriangle.repititveTriangleCommands(variableList, commandInsideIf, positionX,
                                            positionY, colour, fill, pictureBox1, textBox2);
                                }
                                if (commandInsideIf.Contains("moveTo"))
                                {
                                    positionX = Int16.Parse((string)commandInsideIf[1]);
                                    positionY = Int16.Parse((string)commandInsideIf[2]);
                                }
                                commandInsideIf.Clear();
                            }
                        }
                    }
            }
        }

    }
}
