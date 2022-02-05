using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var leftSide = PromptForAnInteger("Please enter your first number:");
            var operation = PromptForOperation("Please enter operation:", "+-*/");
            var rightSide = PromptForAnInteger("Please enter your second number:");

            Console.WriteLine(PerformOperation(leftSide, operation, rightSide));
        }

        private static string PerformOperation(int leftSide, char operation, int rightSide)
        {
            var calculation = OperationToCalculation(operation);
            var result = calculation(leftSide, rightSide);
            return result % 1 > 0
                ? $"{leftSide} {operation} {rightSide} = {result:N3}"
                : $"{leftSide} {operation} {rightSide} = {result:N0}";
        }

        private static Func<int, int, decimal> OperationToCalculation(char operation)
        {
            switch (operation)
            {
                case '+': return Addition;
                case '-': return Subtraction;
                case '*': return Multiplication;
                case '/': return Division;
                default: throw new InvalidOperationException($"Operation '{operation}' is not defined.");
            }
        }

        private static char PromptForOperation(string prompt, string input)
        {
            string operation;
            var displayedInput = SeparateWithCommas(input);
            do
            {
                Console.WriteLine($"{prompt}, one of ({displayedInput}).");
                operation = Console.ReadLine();
            } while (!CheckForValidOperation(operation));
            return operation[0];

            static string SeparateWithCommas(string input)
            {
                var output = "";
                foreach (char c in input)
                {
                    output += $"{c},";
                }
                return output[0..^1];
            }

            bool CheckForValidOperation(string operation)
            {
                var result = operation.Length != 1 || input.IndexOf(operation) == -1;
                if (result) Console.WriteLine($"Your input of '{operation}' is not one of ({displayedInput}). Please try again.");
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
                    Console.WriteLine($"Your input of '{numberString}' is not an integer. Please try again.");
                    return false;
                }
            }
        }

        private static decimal Addition(int leftSide, int rightSide) => leftSide + rightSide;

        private static decimal Subtraction(int leftSide, int rightSide) => leftSide - rightSide;

        private static decimal Multiplication(int leftSide, int rightSide) => leftSide * rightSide;

        private static decimal Division(int leftSide, int rightSide)
        {
            if (rightSide == 0)
                return 0;
            return decimal.Divide(leftSide, rightSide);
        }
    }
}