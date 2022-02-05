using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var leftSide = PromptForAnInteger("Please enter your first number");
            var operation = PromptForOperation("Please enter operation", "+-*/");
            var rightSide = PromptForAnInteger("Please enter your second number");

            Console.WriteLine(PerformOperation(leftSide, operation, rightSide));
        }

        private static string PerformOperation(int leftSide, char operation, int rightSide)
        {
            var calculation = OperationToCalculation(operation);
            var result = calculation(leftSide, rightSide);
            return result % 1 > 0
                ? $"The result of {leftSide} {operation} {rightSide} is {result:N3}"
                : $"The result of {leftSide} {operation} {rightSide} is {result:N0}";
        }

        private static Func<int, int, decimal> OperationToCalculation(char operation)
        {
            switch (operation)
            {
                case '+': return Addition;
                case '-': return Subtraction;
                case '*': return Multiplication;
                case '/': return Division;
                default: throw new InvalidOperationException($"Operation '{operation}' is not defined");
            }
        }

        private static char PromptForOperation(string prompt, string input)
        {
            string operation;
            do
            {
                Console.WriteLine($"{prompt}, one of ({input})");
                operation = Console.ReadLine();
            } while (operation.Length != 1 || input.IndexOf(operation) == -1);
            return operation[0];
        }

        private static int PromptForAnInteger(string prompt)
        {
            int number;
            do
            {
                Console.WriteLine(prompt);
            } while (!int.TryParse(Console.ReadLine(), out number));
            return number;
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
