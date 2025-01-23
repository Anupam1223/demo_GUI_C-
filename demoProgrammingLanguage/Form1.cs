using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Collections.Specialized;
using System.Threading;
using System.Collections.Generic;

/* author =@anupamSiwakoti */
namespace demoProgrammingLanguage
{
    // Filename: Form1.cs
    /// <summary>
    /// About
    /// -----
    ///     Whenever we create a form using windows form in .NET framework, we will automatically get this class
    ///     when we add textBox or pictureBox then under the hood, all this are instantiated. 
    /// </summary>
    public partial class Form1 : Form
    {
        //saves command written in commandParser text box
        private string codeWritten;

        //stores single line command passed from commandLine textbox
        private string[] commandsOnCommandLine;

        private string commandLine;
        //stores validation message sent from validation class
        private string validation;

        //stores each command split from codeWritten
        private string[] command;

        //stores the current running command
        private ArrayList runningCommand = new ArrayList();

        //location of endif
        int whereIsEndif = 0;

        //location of endif
        int whereIsEndLoop = 0;

        //location of endmethod
        int whereIsEndmethod = 0;

        //finds the location if endif
        private ArrayList findEndIf = new ArrayList();

        //position of the shape
        private int positionX=0, positionY=0, finalPositionX = 0, finalPositionY = 0;

        //colour for drawing shapes
        private Color colour = Color.Black;

        //to fill the shape if user wants the program to do so
        private bool fill = false;

        //for declaring variable types
        ListDictionary variableList = new ListDictionary();

        // ------------- singleton classes ------------------------------------------

        //object of class that runs while loop
        Loop runWhileLoop = Loop.GetInstance;

        //object of class that runs loop and if condition
        Condition runIfCondition = Condition.GetInstance;

        //object of class that runs repititve codes for circle triangle and rectangle
        Repititve repititiveCircleRectangleTriangle = Repititve.GetInstance;

        //object of class that runs method
        Method runMethodObject = Method.GetInstance;

        // ---------------------------------------------------------------------------

        //Dictionary that stores command inside methof
        Dictionary<string, List<string>> commandWithRespectiveMethod = new Dictionary<string, List<string>>();

        //threads for flashing color when running command
        Thread colorThreads;

        //check if colour command is activated or not
        bool running = false;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// About
        /// -----
        ///      flashColorThread methods runs whenever execute button is clicked, to flash color at border of picture box
        ///      three color combination needs to be execute so we will have three threads that will run in a syncronized
        ///      manner
        /// </summary>
        /// <param name="colours"> string of colors that should flash</param>
        /// <param name="running"> boolean value if user wants to run the threads</param>
        public void flashColorThread(string[] colours, bool running) 
        {
            Graphics e = pictureBox1.CreateGraphics();
            while (true) {
                while (running == true)
                {
                    for (int i = 0; i < colours.Length; i++)
                    {
                        /*
                         * changes the colour of border of pictureBox1 according to the colour commands given by user
                         * user can give these following commands
                         *          redgreen, blueyellow, blackwhite
                         */
                        if (colours[i] == "Red")
                        {
                            ControlPaint.DrawBorder(e, pictureBox1.ClientRectangle, Color.Red, ButtonBorderStyle.Solid);
                            Thread.Sleep(1000);
                        }
                        if (colours[i] == "Green")
                        {
                            ControlPaint.DrawBorder(e, pictureBox1.ClientRectangle, Color.Green, ButtonBorderStyle.Solid);
                            Thread.Sleep(1000);
                        }
                        if (colours[i] == "Blue")
                        {
                            ControlPaint.DrawBorder(e, pictureBox1.ClientRectangle, Color.Blue, ButtonBorderStyle.Solid);
                            Thread.Sleep(1000);
                        }
                        if (colours[i] == "Yellow")
                        {
                            ControlPaint.DrawBorder(e, pictureBox1.ClientRectangle, Color.Yellow, ButtonBorderStyle.Solid);
                            Thread.Sleep(1000);
                        }
                        if (colours[i] == "Black")
                        {
                            ControlPaint.DrawBorder(e, pictureBox1.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
                            Thread.Sleep(1000);
                        }
                        if (colours[i] == "White")
                        {
                            ControlPaint.DrawBorder(e, pictureBox1.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
                            Thread.Sleep(1000);
                            
                        }
                    }
                }
            }

        }

        /// <summary>
        /// About
        /// -----
        ///       Whenever execute button is clicked from UI, program flows here, first of all it splits the user input 
        ///       and stores it in string array. Since each object in this array is a particular command, this command is then sent to
        ///       validation class for validating command syntax and its related parameters
        /// </summary>
        /// <param name="sender"> automtic generated params when using windows.forms</param>
        /// <param name="e"> automtic generated params when using windows.forms</param>
        private void button1_Click(object sender, EventArgs e)
        {
            commandLine = textBox1.Text;
            //id commands is not empty in commandLine
            if (commandLine != "")
            {
                commandsOnCommandLine = commandLine.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                //if the command passed is run then this condition is runned
                if (commandsOnCommandLine.Length == 1 && commandsOnCommandLine[0] == "run")
                {
                    codeWritten = commandParser.Text;
                    command = codeWritten.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                    Validation validateValue = new Validation(command);
                    //validation saves the error message whie validation commands, if not found it saves null
                    validation = validateValue.checkDrawingCommandError();
                    //if it doesnt contain any commands error then it checks for parameter errors
                    if (validation.Length == 0)
                    {
                        //checks for parameter errors
                        validation = validateValue.checkParamsError();
                        //if found any then it is shown in error box
                        textBox2.Text = validation;

                        //if not then we call run_command for executing commands
                        if (validation == "") {
                            run_command(command);
                        }
                    }
                    else
                    {
                        //shows command error if found any
                        textBox2.Text = validation;
                    }
                    codeWritten = "";
                    Array.Clear(command, 0, command.Length);
                }

                // if clear command is passed as single line command
                else if (commandsOnCommandLine.Length == 1 && commandsOnCommandLine[0] == "clear")
                {
                    pictureBox1.Image = null;
                    textBox2.Clear();
                }
                // if reset command is passed as single line command, it resets the position of point
                else if (commandsOnCommandLine.Length == 1 && commandsOnCommandLine[0] == "reset")
                {
                    positionX = 0;
                    positionY = 0;
                }
                // if stop command is passed as single line command, it stops the color thread.
                else if (commandsOnCommandLine.Length == 1 && commandsOnCommandLine[0] == "stop")
                {
                    colorThreads.Abort();
                    Graphics resetThread = pictureBox1.CreateGraphics();
                    ControlPaint.DrawBorder(resetThread, pictureBox1.ClientRectangle, Color.Silver, ButtonBorderStyle.Solid);
                }
                else // if command except run/reset/clear is passed then it shows error 
                {
                    textBox2.Text = "wrong command "+commandLine;
                }
            }
            else {
                textBox2.Text = "";
                    }
        }

        /// <summary>
        /// About
        /// -----
        ///     Once all the commands and its related parameters are validated the program flows here
        ///     It runs commands one by one in a loop. runningCommand saves the current command and its paramters
        ///     runningCommand is an ArrayList so it checks which command are we running in using Contains method.
        ///<example>
        ///     if the user inputs
        ///     
        ///     radius = 10
        ///     circle radius
        ///     
        ///     then our command[0] array has radius = 10
        ///     and our runningCommand has [radius,=,10]
        ///     
        ///     now our program checks
        ///     if(runningCommand[i] == "radius") which we have so the program flows this condition
        ///     and saves the variable radius with its corresponding value using ListDictionary collection
        ///     
        /// 
        /// </example>
        /// </summary>
        /// <param name="command"></param>
        public void run_command(string[] command) {
            /*
             * running loop to find the exact position of endif and endloop command
             * if there is if and while in our program then we need to find upto which we
             * have to run our command so we use this loop to find the position upto which
             * we have to run our command
             */
            for (int x = 0; x < command.Length; x++)
            {
                //checks the existence if endif by splitting a command sent by user
                findEndIf.AddRange(command[x].Split(' '));

                /*if 'endif' or 'endloop' or 'endmethod' is present in
                 * the command then give the location of endif 
                */
                if (findEndIf.Contains("endif"))
                {
                    whereIsEndif = x;
                    findEndIf.Clear();
                }
                if (findEndIf.Contains("endloop")) {
                    whereIsEndLoop = x;
                    findEndIf.Clear();
                }
                if (findEndIf.Contains("endmethod"))
                {
                    whereIsEndmethod = x;
                    findEndIf.Clear();
                }
            }


            //run for all the command passed from user
            for (int i = 0; i < command.Length; i++)
            {
                runningCommand.AddRange(command[i].Split(' '));
                //if moveTo command is passed by user
                if (runningCommand.Contains("moveTo"))
                {
                    positionX = Int16.Parse((string)runningCommand[1]);
                    positionY = Int16.Parse((string)runningCommand[2]);
                    runningCommand.Clear();
                }
                if (runningCommand.Contains("drawTo"))
                {
                    /*
                     * final position for line (finalPositionX and finalPositionY)
                     * positionX and positionY are the initial position for line
                     */
                    finalPositionX = Int16.Parse((string)runningCommand[1]);
                    finalPositionY = Int16.Parse((string)runningCommand[2]);
                    Pen pen = new Pen(colour, 2);
                    Graphics drawLine = pictureBox1.CreateGraphics();
                    drawLine.DrawLine(pen, positionX, positionY, finalPositionX, finalPositionY);
                    runningCommand.Clear();

                }
                //if circle command is passed by user then this condition is runned
                if (runningCommand.Contains("circle"))
                {
                    /*
                     * repititiveCircleRectangleTriangle calls repitituveCircleCommands to draw circle, this is done 
                     * to achieve seperation of concern 
                     */

                    repititiveCircleRectangleTriangle.repititveCircleCommands(variableList, runningCommand, positionX,
                        positionY, colour, fill, pictureBox1);
                    runningCommand.Clear();

                }
                if (runningCommand.Contains("rectangle"))
                {
                    /*
                     * repititiveCircleRectangleTriangle calls repitituveRectangleCommands to draw circle, this is done 
                     * to achieve seperation of concern 
                     */
                    repititiveCircleRectangleTriangle.repititveRectangleCommands(variableList, runningCommand, positionX,
                        positionY, colour, fill, pictureBox1, textBox2);
                    runningCommand.Clear();
                }
                if (runningCommand.Contains("triangle"))
                {
                    /*
                     * repititiveCircleRectangleTriangle calls repititiveTriangleCommands to draw circle, this is done 
                     * to achieve seperation of concern 
                     */
                    repititiveCircleRectangleTriangle.repititveTriangleCommands(variableList, runningCommand, positionX,
                        positionY, colour, fill, pictureBox1, textBox2);
                    runningCommand.Clear();
                }
                if (runningCommand.Contains("if"))
                {
                    /*
                     * runIfCondition object calls runIFCommands to run if condition, this is done 
                     * to achieve seperation of concern 
                     */
                    runIfCondition.runIfCondition(runningCommand, variableList, whereIsEndif,
                        command, positionX, positionY, colour, fill, pictureBox1, i, textBox2);
                    i = whereIsEndif;
                    runningCommand.Clear();
                }
                if (runningCommand.Contains("while"))
                {
                    /*
                     * runWhileLoop object calls runWhileLoop to run while loop, this is done 
                     * to achieve seperation of concern 
                     */
                    runWhileLoop.runWhileLoop(runningCommand, variableList, whereIsEndLoop,
                        command, positionX, positionY, colour, fill, pictureBox1, i, textBox2);
                    i = whereIsEndLoop;
                    runningCommand.Clear();
                    variableList.Clear();
                }

                if (runningCommand.Contains("method"))
                {
                    /*
                     * runMethodObject object calls defineMethod to define method, this method returns 
                     * Dictionray with method name as key and its respective commands as variables 
                     */
                    commandWithRespectiveMethod = runMethodObject.defineMethod(runningCommand, variableList, command, whereIsEndmethod, i);
                    i = whereIsEndmethod;
                    runningCommand.Clear();
                    //variableList.Clear();
                }
                if (runningCommand.Contains("radius") || runningCommand.Contains("height")
                    || runningCommand.Contains("width") || runningCommand.Contains("count"))
                {

                    repititiveCircleRectangleTriangle.repititiveVariableCommands(runningCommand, variableList);
                    runningCommand.Clear();
                }
                /*
                 * whenever user passes colour command with its params this condition is runned
                 * we check which colours do the user wants to run and call our flashColorThreads method to flash
                 * color in pictureBox1 border
                 */
                if (runningCommand.Contains("colour"))
                {
                    string colorInCommand = (string)runningCommand[1];
                    string[] colours = new string[2];
                    running = true;
                    if (colorInCommand == "redgreen")
                    {
                        colours[0] = "Red";
                        colours[1] = "Green";
                        colorThreads = new Thread(() => flashColorThread(colours, running));
                    }
                    if (colorInCommand == "blueyellow")
                    {
                        colours[0] = "Blue";
                        colours[1] = "Yellow";
                        colorThreads = new Thread(() => flashColorThread(colours, running));
                    }
                    if (colorInCommand == "blackwhite")
                    {
                        colours[0] = "Black";
                        colours[1] = "White";
                        colorThreads = new Thread(() => flashColorThread(colours, running));
                    }
                    colorThreads.Start();
                    runningCommand.Clear();
                }

                /*
                 * checks the method name if user has passed for calling any method
                 * checks the user command with keys of commandWithRespactiveMethod dictionary
                 * and calles runMethod from Method class to run the requested method
                 */
                foreach (KeyValuePair<string, List<string>> methods in commandWithRespectiveMethod)
                {

                    if (runningCommand.Contains(methods.Key))
                    {
                        string runningMethod = (string)runningCommand[0];
                        runMethodObject.runMethod(runningMethod, commandWithRespectiveMethod, textBox2, 
                            positionX, positionY, colour, fill, pictureBox1, variableList);
                        runningCommand.Clear();
                    }

                }
            }

        }
        /// <summary>
        /// About
        /// -----
        ///     gives red color when clicked red from tools->pen
        ///     
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colour = Color.Red;
        }
        /// <summary>
        /// About
        /// -----
        ///     gives red green when clicked blue from tools->pen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colour = Color.Green;
        }
        /// <summary>
        /// About
        /// -----
        ///     gives blue color when clicked blue from tools->pen
        ///      
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colour = Color.Blue;
        }
        /// <summary>
        /// About
        /// -----
        ///     gives black color when clicked default from tools->pen
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colour = Color.Black;
        }
        /// <summary>
        /// About
        /// -----
        ///     sets the fill variable to true when selected on from tools->fill
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fill = true;
        }

        /// <summary>
        /// About
        /// -----
        ///      if save option selected from options than a dialouge box is opened 
        ///      and let us to choose where to save our text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDrawingCommand = new SaveFileDialog();
            if (saveDrawingCommand.ShowDialog() == DialogResult.OK) {
                using (Stream fileName = File.Open(saveDrawingCommand.FileName, FileMode.CreateNew))
                using (StreamWriter saveFile = new StreamWriter(fileName)) {
                    saveFile.Write(commandParser.Text);
                }
            }  
        }

        /// <summary>
        /// About
        /// -----
        ///       if open option selected from options than a dialouge box is opened to 
        ///       choose which file we want to select
        /// </summary>
        /// <param name="sender"> automatic generated</param>
        /// <param name="e"> automatic generated</param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //uses stream inbuilt class to load file from the system
            Stream streamLoadFile;
            //opens the dialouge for choosing file that we want to open
            OpenFileDialog openFile = new OpenFileDialog();
            //if user selects ok then program enters this condition
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK) {

                try {
                    if ((streamLoadFile = openFile.OpenFile()) != null)
                    {
                        //user chooses the filename
                        string filename = openFile.FileName;
                        //reads all the content from choosen file
                        string filetext = File.ReadAllText(filename);
                        //loaded in our commandParser text box
                        commandParser.Text = filetext;
                    }
                }
                catch (FileNotFoundException)
                {
                    //if user selects the file that doesnt exists
                    MessageBox.Show("Error", "Cannot find the asked file");
                }
            }
        }
        /// <summary>
        /// About
        /// -----
        ///      sets the fill variable to false when selected on from tools->fill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fill = false;
        }
    }
 }
