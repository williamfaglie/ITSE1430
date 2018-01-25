using System;
using System.Collections;

namespace Nile.Host
{
    class Program
    {
        static void Main( string[] args )
        {
            
        }
        static void PlayingWithPrimitives ()
        {
            //Primitive
            decimal unitPrice = 10.5M;

            //Real declaration (Framework)
            System.Decimal unitPrice2 = 10.5M;

            //Current time (Must use Framework)
            System.DateTime now = DateTime.Now;
            DateTime now1 = DateTime.Now;

            System.Collections.ArrayList items;
        }

        static void PlayingWithVariables ()
        {
            int hours = 0;

            double rate = 10.25;

            //Still not assigned
            //if (false)
            //    hours = 0;

            int hours2 = hours;

            string firstName, lastName;

            //string @class;

            //Single assignment
            firstName = "Bob";
            lastName = "Miller";

            //Multiple assignment
            firstName = lastName = "Sue";

            //Math ops
            int x = 0, y = 10;
            int add = x + y;
            int subtract = x - y;
            int mulitply = x * y;
            int divide = x / y;
            int modulos = x % y;

            //x = x + 10;
            x += 10;
            double ceiling = Math.Ceiling(rate);
            double floor = ceiling;
        }
    }
}
