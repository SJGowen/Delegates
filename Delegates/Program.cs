using System;

namespace Delegates
{
    class Program
    {
        public delegate int Calculation(int leftSide, int rightSide);

        static void Main(string[] args)
        {
            var leftSide = PromptForAnInteger("Please enter your first number");
            var operation = PromptForOperation("Please enter operation", "+-*/");
            var rightSide = PromptForAnInteger("Please enter your second number");
            var calculation = CharOperationToDelegateCalculation(operation);
            Console.WriteLine($"The result of {leftSide} {operation} {rightSide} is {calculation(leftSide, rightSide)}");
            Console.ReadKey();
        }

        private static Calculation CharOperationToDelegateCalculation(char operation)
        {
            switch (operation)
            {
                case '+': return Addition;
                case '-': return Subtraction;
                case '*': return Multiplication;
                case '/': return Division;
                default: throw new System.InvalidOperationException($"Operation '{operation}' is not defined");
            }
        }

        private static char PromptForOperation(string prompt, string input)
        {
            string operation;
            do
            {
                Console.WriteLine(prompt + ", one of (" + input + ")");
                operation = Console.ReadLine();
            } while (operation.Length != 1 || input.IndexOf(operation) == -1);
            return operation.ToCharArray()[0];
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

        private static int Addition(int leftSide, int rightSide)
        {
            return leftSide + rightSide;
        }

        private static int Subtraction(int leftSide, int rightSide)
        {
            return leftSide - rightSide;
        }

        private static int Multiplication(int leftSide, int rightSide)
        {
            return leftSide * rightSide;
        }

        private static int Division(int leftSide, int rightSide)
        {
            return leftSide / rightSide;
        }
    }
}
