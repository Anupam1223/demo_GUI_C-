using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;

namespace demoProgrammingLanguage
{
    /* author =@anupamSiwakoti */
    // Filename: Method.cs
    /// <summary>
    /// About
    /// -----
    ///     It is a singleton class which will only have one object throughout the execution of program
    ///     , when user inputs 'method' for running method.
    ///     
    /// Variables
    /// --------
    ///      * runMethodInstance -> static variable that will store the object of Method if created else it will store null
    ///      * repititiveCircleTriangleRectangle -> object of class that runs repititve codes for circle triangle and rectangle
    ///      * runCommand -> all commands that needs to run inside method
    ///      * commandWithRespectiveMethod is a dictionary that stores method name as key and its value is all the commands inside method
    /// </summary>
    public sealed class Method
    {
        ArrayList runCommands = new ArrayList();
        List<string> commandInsideMethod = new List<string>();
        Dictionary<string, List<string>> commandWithRespectiveMethod = new Dictionary<string, List<string>>();
        private static Method runMethodInstance = null;
        Repititve repititiveCircleTriangleRectangle = Repititve.GetInstance;

        /// <summary>
        /// About
        /// -----
        ///      Since, our Method class is singleton, only once instance of this class can be created.
        ///      It folows singleton pattern, one object of this class can perform all the operation of this class
        ///      So, GetInstance will instantiate a class if it doesnt have any object else it will not create.
        ///      We have used this in Form1 ///< see cref = "Form1" > see this class </ see >
        /// </summary>
        public static Method GetInstance
        {
            get
            {
                if (runMethodInstance == null)
                    runMethodInstance = new Method();
                return runMethodInstance;
            }
        }

        /// <summary>
        /// About
        /// -----
        ///      defineMethod defines method by keeping track of method name and command inside it,
        ///      Already defined method can also be updated, when user tries to update existing method
        ///      then it will update the values of the key (method) that the user wants to update
        /// </summary>
        /// <param name="runningCommand"> current running command sent from <see cref="Form1"> run_command() method, </param>
        /// <param name="variableList"> variableList where variables are stored, it is used incase of methods with parameters</param>
        /// <param name="command"> command has all the commands written in current program but it runs only commands inside a method</param>
        /// <param name="whereIsEndloop"> the position of 'endloop'</param>
        /// <param name="i"> current position of command</param>
        /// <param name="textBox2"> for testing purpose </param>
        /// <returns></returns>
        public Dictionary<string, List<string>> defineMethod(ArrayList runningCommand, ListDictionary variableList, 
            string[] command,  int whereIsEndmethod, int i)
        {
                    //if method already exists, then this condition is runned and method is updated
                    if (commandWithRespectiveMethod.ContainsKey((string)runningCommand[1]))
                    {
                        commandInsideMethod.Clear();
                        for (int j = i + 1; j < whereIsEndmethod; j++)
                        {
                            commandInsideMethod.Add((string)command[j]);
                        }
                        commandWithRespectiveMethod[(string)runningCommand[1]] = commandInsideMethod;
                    }
                    //else new method is created
                    else
                    {
                        for (int j = i + 1; j < whereIsEndmethod; j++)
                        {
                            commandInsideMethod.Add((string)command[j]);
                        }

                        commandWithRespectiveMethod.Add((string)runningCommand[1], commandInsideMethod);
                    }

            return commandWithRespectiveMethod;
        }
        /// <summary>
        /// About
        /// -----
        ///      When user wants to call and run a method we call runMethod,
        ///      it looks for the name of the method and runs its specific method one by one
        /// </summary>
        /// <param name="runningMethod"> name of the method that the user wants to run</param>
        /// <param name="commandsOfMethod"> has commands inside of method that the user is calling</param>
        /// <param name="textBox2"> for testing purpose </param>
        /// <param name="positionX"> position of x </param>
        /// <param name="positionY">position of y </param>
        /// <param name="colour"> pen colour</param>
        /// <param name="fill"> bool value, if user wants to fill the diagram then it fills the diagram</param>
        /// <param name="pictureBox1"> picture box where images are drawn</param>
        /// <param name="variableList"> variables used while running program</param>

        public void runMethod(string runningMethod, Dictionary<string, List<string>> commandsOfMethod, TextBox textBox2,
            int positionX, int positionY, Color colour, bool fill, PictureBox pictureBox1, ListDictionary variableList)
        {
            foreach (KeyValuePair<string, List<string>> methods in commandsOfMethod)
            {
                if (methods.Key.Contains(runningMethod))
                {
                    /*
                     * takes the commands (values) from the 'commandsOfMethod' dictionary and runs them one by one
                     * uses instance of Repititive class to draw shapes and execute other commands
                     */
                    foreach (var commands in methods.Value) 
                    {
                        runCommands.AddRange(commands.Split(' '));
                        if (runCommands.Contains("circle"))
                        {
                            ///< see cref = "Repititve" > see this class </ see >
                            repititiveCircleTriangleRectangle.repititveCircleCommands(variableList, runCommands, positionX,
                                   positionY, colour, fill, pictureBox1);
                        }
                        if (runCommands.Contains("rectangle"))
                        {
                            repititiveCircleTriangleRectangle.repititveRectangleCommands(variableList, runCommands, positionX,
                                positionY, colour, fill, pictureBox1, textBox2);
                        }
                        if (runCommands.Contains("triangle"))
                        {
                            repititiveCircleTriangleRectangle.repititveTriangleCommands(variableList, runCommands, positionX,
                                    positionY, colour, fill, pictureBox1, textBox2);
                        }
                        if (runCommands.Contains("moveTo"))
                        {
                            positionX = Int16.Parse((string)runCommands[1]);
                            positionY = Int16.Parse((string)runCommands[2]);
                        }
                        runCommands.Clear();
                    }
                }
            } 
        }
    }
}
