using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var leftSide = PromptForNumber("Please enter your first number:");
            var operation = PromptForOperation("Please enter operation", "+-*/%");
            var rightSide = PromptForNumber("Please enter your second number:");
            while (operation == '/' && rightSide == 0M)
            {
                Console.WriteLine($"You can not divide by zero. Please try again!");
                rightSide = PromptForNumber("Please enter your second number:");
            }

            Console.WriteLine(PerformOperation(leftSide, operation, rightSide));
        }

        private static string PerformOperation(decimal leftSide, char operation, decimal rightSide)
        {
            var calculation = OperationToCalculation(operation);
            var result = calculation(leftSide, rightSide);
            return $"{leftSide} {operation} {rightSide} = {result:0.###}";
        }

        private static Func<decimal, decimal, decimal> OperationToCalculation(char operation)
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

        private static decimal PromptForNumber(string prompt)
        {
            string numberString;
            do
            {
                Console.WriteLine(prompt);
                numberString = Console.ReadLine();
            } while (!CheckForValidNumber(numberString));

            return decimal.Parse(numberString);

            static bool CheckForValidNumber(string numberString)
            {
                if (decimal.TryParse(numberString, out var _))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Your input of '{numberString}' is not a number. Please try again!");
                    return false;
                }
            }
        }

        private static decimal Addition(decimal leftSide, decimal rightSide) => leftSide + rightSide;

        private static decimal Subtraction(decimal leftSide, decimal rightSide) => leftSide - rightSide;

        private static decimal Multiplication(decimal leftSide, decimal rightSide) => leftSide * rightSide;

        private static decimal Division(decimal leftSide, decimal rightSide) => decimal.Divide(leftSide, rightSide);

        private static decimal Modulus(decimal leftSide, decimal rightSide) => leftSide % rightSide;
    }
}