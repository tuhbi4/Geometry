using System;

namespace Geometry
{
    public static class Validator
    {
        public static int ParseNumber()
        {
            if (!int.TryParse(Console.ReadLine(), out int answer))
            {
                Console.WriteLine("This is not a number. Please enter the correct answer:");
                return ParseNumber();
            }
            return answer;
        }
        public static int InputValidation(string valueName, int min, int max)
        {
            int valueOut;
            Console.WriteLine($"Set the value of {valueName}: (minimum is: {min}; maximum is {max})");
            while (true)
            {
                valueOut = ParseNumber();
                if (valueOut < min)
                {
                    Console.WriteLine($"Please enter a value greater than or equal to {min}");
                }
                else if (valueOut > max)
                {
                    Console.WriteLine($"Please enter a value less than or equal to {max}");
                }
                else
                {
                    return valueOut;
                }
            }
        }
    }
}