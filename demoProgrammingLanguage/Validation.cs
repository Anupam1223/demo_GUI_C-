using System.Collections;

/* author =@anupamSiwakoti */
namespace demoProgrammingLanguage
{
    // Filename: Validation.cs
    /// <summary>
    /// About
    /// ------
    ///      validation class is used for validating our command and showing error when encountered, 
    ///      it has two method checkDrawingCommandError and checkParamsError.
    ///      
    /// reference variables
    /// ----------
    ///     * commandSyntax save all the command passed and help us check them one bye one
    ///     * parameters save all the command passed and also the parameters passed with them which help us check them one bye one
    ///     * command that we get in our constructor is saved in command variable
    ///     * commands that our program supports will be stored here and they will be checked one by ine
    /// 
    /// constructor
    /// ----------
    ///     * this constructor takes an array of string as parameter and makes our validation class ready for validation
    /// </summary>
    public class Validation
    {
        // --------------- declaration of variables ---------------------------------------------------------------------------------
        private ArrayList commandSyntax= new ArrayList();
        private ArrayList parameters= new ArrayList();
        private string[] command;
        private string[] drawingRelatedCommands = new string[20];
        int count = 1;
        // --------------------------------------------------------------------------------------------------------------------------
        public Validation (string[] command) {
            this.command = command;
            for (int i = 0; i < command.Length; i++)
            {
              commandSyntax.Add(command[i].Split(' ')[0]);
            }
            drawingRelatedCommands = (string[])commandSyntax.ToArray(typeof(string));
        }

        /// <summary>
        /// checks command passed by user, since this is demo programming language there are limited commands that we can use
        /// if user doesnt write the specified command or keyword then our program throws error
        /// Commands accepted by our program:
        /// *moveTo, drawTo, cirle, rectangle, triangle, if, while radius, height, width, endif, count, while, endloop, colour, drawMethod
        /// </summary>
        /// <returns> returns validation error regarding command syntax and name if found else returns empty string</returns>
        public string checkDrawingCommandError()
        {
            try
            {
                //runs for every command syntax
                for (int i = 0; i < commandSyntax.Count; i++)
                {
                    //if the syntax doesnt match moveTo, drawTo, circle, rectangle or triangle then it throws error with specific line no
                    if (drawingRelatedCommands[i] != "moveTo" && drawingRelatedCommands[i] != "drawTo" && drawingRelatedCommands[i] != "circle"
                        && drawingRelatedCommands[i] != "rectangle" && drawingRelatedCommands[i] != "triangle" && drawingRelatedCommands[i] != "if"
                        && drawingRelatedCommands[i] != "radius" && drawingRelatedCommands[i] != "height" && drawingRelatedCommands[i] != "width"
                        && drawingRelatedCommands[i] != "endif" && drawingRelatedCommands[i] != "count" && drawingRelatedCommands[i] != "while"
                        && drawingRelatedCommands[i] != "endloop" && drawingRelatedCommands[i] != "colour" && drawingRelatedCommands[i] != "method"
                        && drawingRelatedCommands[i] != "endmethod" && drawingRelatedCommands[i] != "drawDiagram()" 
                        && drawingRelatedCommands[i] != "drawDiagram(radius,height,width)")
                    {
                        return "cannot recognize '" + drawingRelatedCommands[i] + "' command at line " + count;
                    }
                    count++;
                }
                count = 1;
                return "";
            }
            catch (System.IndexOutOfRangeException) {
                return "user sent command with exceeding value";
            }

        }

        /// <summary>
        /// checks the parameter send by user when writing program, it checks each parameter individually for 
        /// all the commands passed in, also if they are assignment operation then it checks the syntax
        /// </summary>
        /// <returns>returns error message if found any else return empty string</returns>
        public string checkParamsError()
        {
            /*
             * if command array gets index that exceeds the capacity of command then catch will catch the error 
             * and send back the error response
             */
            try
            {
                for (int i = 0; i < command.Length; i++)
                {
                    //this breaks down the command in a list and assign saved as individual index in parameters
                    parameters.AddRange(command[i].Split(' '));
                    //moveTo takes only 2 params so checks if it has less than or more than 2 params
                    if (parameters.Contains("moveTo") && parameters.Count != 3)
                    {
                        return "parameter error at line " + count + ",  'moveTo' command takes two parameters";
                    }
                    //drawTo takes only 2 params so checks if it has less than or more than 2 params
                    if (parameters.Contains("drawTo") && parameters.Count != 3)
                    {
                        return "parameter error at line " + count + ",  'drawTo' command takes two parameters";
                    }
                    //circleTo takes only 1 params so checks if it has less than or more than 1 params
                    if (parameters.Contains("circle") && parameters.Count != 2)
                    {
                        return "parameter error at line " + count + ",  'circle' command takes only one parameters";
                    }
                    //rectangle takes only 2 params so checks if it has less than or more than 2 params
                    if (parameters.Contains("rectangle") && parameters.Count != 3)
                    {
                        return "parameter error at line " + count + ",  'rectangle' command takes two parameters";
                    }
                    //triangle takes only 3 params so checks if it has less than or more than 3 params
                    if (parameters.Contains("triangle") && parameters.Count != 4)
                    {
                        return "parameter error at line " + count + ",  'triangle' command takes 3 parameters";
                    }
                    //if takes only 4 params so checks if it has less than or more than 4 params
                    if (parameters.Contains("if") && parameters.Count != 4)
                    {
                        return "syntax error at line " + count + ", for  'if' command";

                    }
                    //while commands has syntax of atmost 4 commands
                    if (parameters.Contains("while") && parameters.Count != 4)
                    {
                        return "At line " + count + ", syntax for 'while' loop doesnt match";
                    }
                    //checks the operators of if condition
                    if (parameters.Contains("if"))
                    {
                        if ((string)parameters[2] != "==" && (string)parameters[2] != "!=")
                        {
                            return "if condition doesnt recognize " + (string)parameters[2] + " operator at line " + count;
                        }
                    }
                    //checks the operators of if condition
                    if (parameters.Contains("while"))
                    {
                        if ((string)parameters[2] != ">" && (string)parameters[2] != "<")
                        {
                            return "while loop doesnt recognize " + (string)parameters[2] + " operator at line " + count;
                        }
                    }
                    //checks the erros for assignment operation
                    if (parameters.Contains("=") && parameters.Count > 5)
                    {
                        return "assignment error at " + count + " count " + parameters.Count;
                    }
                    count++;
                    //clears paramaters arraylist for storing and checking next command on arraylist
                    parameters.Clear();
                }
                count = 1;
                return "";
            }
            catch (System.IndexOutOfRangeException) {
                return "user sent command with exceeding value";
            }
        }    
    }
}
