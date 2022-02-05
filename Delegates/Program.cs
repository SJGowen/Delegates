using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var leftSide = PromptForAnInteger("Please enter your first number:");
            var operation = PromptForOperation("Please enter operation", "+-*/%");
            var rightSide = PromptForAnInteger("Please enter your second number:");
            while (operation == '/' && rightSide == 0)
            {
                Console.WriteLine($"You can not divide by zero. Please try again!");
                rightSide = PromptForAnInteger("Please enter your second number:");
            }

            Console.WriteLine(PerformOperation(leftSide, operation, rightSide));
        }

        private static string PerformOperation(int leftSide, char operation, int rightSide)
        {
            var calculation = OperationToCalculation(operation);
            var result = calculation(leftSide, rightSide);
            return $"{leftSide} {operation} {rightSide} = {result:0.###}";
        }

        private static Func<int, int, decimal> OperationToCalculation(char operation)
        {
            switch (operation)
            {
                case '+': return Addition;
                case '-': return Subtraction;
                case '*': return Multiplication;
                case '/': return Division;
                case '%': return Modulus;
                default: throw new InvalidOperationException($"Operation '{operation}' is not defined.");
            }
        }

        private static char PromptForOperation(string prompt, string input)
        {
            string operation;
            var displayedInput = $"'{string.Join("','", input.ToCharArray())}'";
            do
            {
                Console.WriteLine($"{prompt}, one of ({displayedInput}):");
                operation = Console.ReadLine();
            } while (!CheckForValidOperation(operation));

            return operation[0];

            bool CheckForValidOperation(string operation)
            {
                var result = operation.Length != 1 || input.IndexOf(operation) == -1;
                if (result) Console.WriteLine($"Your input of '{operation}' is not one of ({displayedInput}). Please try again!");
                return !result;
            }
        }

        private static int PromptForAnInteger(string prompt)
        {
            string numberString;
            do
            {
                Console.WriteLine(prompt);
                numberString = Console.ReadLine();
            } while (!CheckForValidInteger(numberString));

            return int.Parse(numberString);

            static bool CheckForValidInteger(string numberString)
            {
                if (int.TryParse(numberString, out var _))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Your input of '{numberString}' is not an integer. Please try again!");
                    return false;
                }
            }
        }

        private static decimal Addition(int leftSide, int rightSide) => leftSide + rightSide;

        private static decimal Subtraction(int leftSide, int rightSide) => leftSide - rightSide;

        private static decimal Multiplication(int leftSide, int rightSide) => leftSide * rightSide;

        private static decimal Division(int leftSide, int rightSide) => decimal.Divide(leftSide, rightSide);

        private static decimal Modulus(int leftSide, int rightSide) => leftSide % rightSide;
    }
}