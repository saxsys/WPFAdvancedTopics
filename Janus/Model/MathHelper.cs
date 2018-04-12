namespace Janus.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MathHelper
    {
        public static void CalculateIfSecondOperator(string input, ref string output)
        {
            var operatorList = new List<string> { "/", "*", "-", "+" };

            var operatorChar = string.Empty;
            var operatorCount = 0;
            foreach (char item in output)
            {
                if (operatorList.Contains(item.ToString()))
                {
                    operatorChar = item.ToString();
                    operatorCount++;
                }
            }

            if (operatorCount < 1)
            {
                output += input;
                return;
            }

            var parts = output.Split(operatorList.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            var input1 = parts.First();
            var input2 = parts.Last();

            double number1;
            double number2;
            double.TryParse(input1, out number1);
            double.TryParse(input2, out number2);

            var result = Calc(operatorChar, number2, number1, ref output);

            output = result.ToString();

            if (!input.Equals("="))
            {
                output += input;
            }
        }

        private static double Calc(string operatorChar, double number2, double number1, ref string output)
        {
            var result = 0.0;
            switch (operatorChar)
            {
                case "/":
                    if (number2 == 0)
                    {
                        output = "Div by ZERO Panic!!11elf";
                        return result;
                    }

                    result = number1 / number2;
                    break;
                case "*":
                    result = number1 * number2;
                    break;
                case "-":
                    result = number1 - number2;
                    break;
                case "+":
                    result = number1 + number2;
                    break;
            }

            return result;
        }
    }
}