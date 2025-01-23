using System;

/* author =@anupamSiwakoti */
namespace demoProgrammingLanguage
{
    // Filename: ShapeFactory.cs
    /// <summary>
    /// About
    /// -----
    ///     This factory generates the object and provide us whenever we need one via dynamic polymorphism,
    ///     It help us mantain loose coupling between classes
    ///     Factory classes helps us in memory management and also makes our program a lot clean by, 
    ///     producing object only at the time we need it.
    /// </summary>
    internal class ShapeFactory
    {
        /// <summary>
        /// About
        /// -----
        ///     It is the factory that produces object of circle, rectagle and triangle.
        ///     We should type the type of object that we want as argument.
        /// </summary>
        /// <param name="shapeType"></param>
        /// <returns> return object asked by other components of our program </returns>
        public Shape getShape(String shapeType)
        {
            //you could argue that you want a specific word string to create an object but I'm allowing any case combination
            shapeType = shapeType.ToUpper().Trim(); 

            //if we ask for circle object
            if (shapeType.Equals("CIRCLE"))
            {
                return new Circle();

            }
            //if we ask for rectangle object
            else if (shapeType.Equals("RECTANGLE"))
            {
                return new Rectangle();

            }
            //if we ask for triangle object
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle();
            }
            else
            {
                //if we get here then what has been passed in is inkown so throw an appropriate exception
                System.ArgumentException argEx = new System.ArgumentException("Factory error: " + shapeType + " does not exist");
                throw argEx;
            }


        }
    }
}
