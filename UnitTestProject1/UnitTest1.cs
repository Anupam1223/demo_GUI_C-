using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using demoProgrammingLanguage;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

/* author =@anupamSiwakoti */
namespace UnitTestProject1
{
    /*
     * test class for our commandParser, validation, itiration, condition, threads
     * Tests are very useful for us while developing any software, it help us debug out code
     * give newer vision about our problem and help to rapid develop our software
     * 
     * Here we have some unit test for some of our unit components such as methods
     */
    // Filename: UnitTest1.cs
    [TestClass]
    public class UnitTest1
    {
        /*This are some initilizaton for acheiving our test,
         * we have input data for what is to be expected from our program
         * and if our program returns expected result that we pass the test
         * for our unit
         */
        ArrayList commandList = new ArrayList(5);
        string commandError = "cannot recognize 'moveT' command at line 2";
        string paramsError = "parameter error at line 2,  'rectangle' command takes two parameters";
        string result;
        ListDictionary variableList = new ListDictionary();
        /*
         * if user give 'moveT' command at line no 2 then it should match our commandError variable
         * because in our validation class we have a method checkDrawingCommandError, which is 
         * programmed in such a way that it should return the message assigned to commandError 
         */
        [TestMethod]
        public void TestCommandError()
        {
            commandList.Add("circle");
            commandList.Add("moveT");
            String[] testCommandParser = commandList.ToArray(typeof(string)) as string[];
            Validation checkCommandFromForm = new Validation(testCommandParser);
            result = checkCommandFromForm.checkDrawingCommandError();
            Assert.AreEqual(commandError, result);
        }

        /*
         * if user give 'rectangle 10' command at line no 2 then it should match our paramsError variable
         * because in our validation class we have a method checkParamsError, which is 
         * programmed in such a way that it should return the message assigned to paramsError 
         */

        [TestMethod]
        public void TestParamsError()
        {
            commandList.Add("circle 10");
            commandList.Add("rectangle 10");
            String[] testCommandParser = commandList.ToArray(typeof(string)) as string[];
            Validation checkParamsFromForm = new Validation(testCommandParser);
            result = checkParamsFromForm.checkParamsError();
            Assert.AreEqual(paramsError, result);
        }

        /*
         * if user give types
         *                      method drawDiagram()
         *                      circle 10
         *                      endmethod
         * as a program then our defMethod returns a dictionary which stores  
         *                      
         *                      key -> 'drawMethod()'
         *                      value -> 'circle 10'         
         *                      
         *  we are checking the 'defineMethod' if it returns the above key and values
         *  we are providing the above program as input
         */

        [TestMethod]
        public void TestMethodDefining()
        {
            //commandList[0] = 'method', commandList[1] = 'drawDiagram()'
            commandList.AddRange("method drawDiagram()".Split(' '));

            //totalCommandInProgram = ['method drawDiagram()', 'circle 10', 'endmethod']
            String[] totalCommandInProgram = new string[3]{ "method drawDiagram()", "circle 10", "endmethod" };

            //gets the instance of Method class
            Method checkDefiningMethod = Method.GetInstance;

            //key value pair, where value is list and key is a string, stores the result sent from defineMethod
            Dictionary<string, List<string>> returnedDictionary = 
                checkDefiningMethod.defineMethod(commandList, variableList, totalCommandInProgram, 2, 0);

            //checking whether the key and value match the expected result
            foreach (KeyValuePair<string, List<string>> methods in returnedDictionary) {

                //checking key
                Assert.AreEqual("drawDiagram()", methods.Key);

                //checking values under it
                foreach (var values in methods.Value) {
                    Assert.AreEqual("circle 10", values);
                }
            
            }
        }

        [TestMethod]
        public void TestVariablesDefining()
        {
            //commandList[0] = 'method', commandList[1] = 'drawDiagram()'
            commandList.AddRange("radius = 10".Split(' '));

            //gets the instance of Repititve class
            Repititve checkVariables = Repititve.GetInstance;

            //calling the method that runs for defining variables
            checkVariables.repititiveVariableCommands(commandList, variableList);

            //checks all the variables in variableList
            foreach (DictionaryEntry keyValue in variableList)
            {
                //whether key matches with variable
                Assert.AreEqual(keyValue.Key, "radius");
                //whether value matches with the value sent
                Assert.AreEqual(keyValue.Value, (Int16)10);
            }
        }

    }
}
