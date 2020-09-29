using System;

// Reminder
// https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=netframework-4.8
// https://docs.microsoft.com/en-us/dotnet/api/system.console.writeline?view=netframework-4.8
// https://docs.microsoft.com/en-us/dotnet/api/system.enum.isdefined?view=netframework-4.8
// https://docs.microsoft.com/en-us/dotnet/api/system.enum.tryparse?view=netframework-4.8

// Roadmap
// Todo: Unterstützung für Fliesskommazahlen implementieren
// Todo: Menü Schleife verbessern (Zahlen >= 7 abfangen)
// Todo: Unterstützung für mehr als zwei Input Zahlen implementieren
// Todo: Switch Case aus der Main Methode auslagern

namespace Hexadecimal_Calculator
{
    class Program
    {
        private enum UserSelection
        {
            None,
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Exit,
            Invalid
        }
        
        static void Main()
        {
            int userSelectionAsInteger;

            do
            {
                ShowMenuToUser();

                // Converting user input to Int32
                userSelectionAsInteger = TryConvertUserSelection(Console.ReadLine());

                bool tryParseWasSuccessful = Enum.TryParse(userSelectionAsInteger.ToString(), out UserSelection decisionAsEnum);
                bool isDefinedWasSuccessful = Enum.IsDefined(typeof(UserSelection), userSelectionAsInteger);

                // To define...
                // User input validation not necessary, because it already happens in TryConvertUserSelection() method
                if (tryParseWasSuccessful && isDefinedWasSuccessful)
                {
                    switch (decisionAsEnum)
                    {
                        case UserSelection.Addition:
                            Console.WriteLine("You have choosen 'Addition'");
                            int userAdditionInputA = ConvertHexInputToInt(1);
                            int userAdditionInputB = ConvertHexInputToInt(2);
                            CalculateAddition(userAdditionInputA, userAdditionInputB);
                            WaitforUserConfirmation();
                            break;
                        case UserSelection.Subtraction:
                            Console.WriteLine("You have choosen 'Subtraction'");
                            int userSubtractionInputA = ConvertHexInputToInt(1);
                            int userSubtractionInputB = ConvertHexInputToInt(2);
                            CalculateSubtraction(userSubtractionInputA, userSubtractionInputB);
                            WaitforUserConfirmation();
                            break;
                        case UserSelection.Multiplication:
                            Console.WriteLine("You have choosen 'Multiplication'");
                            int userMultiplicationInputA = ConvertHexInputToInt(1);
                            int userMultiplicationInputB = ConvertHexInputToInt(2);
                            CalculateMultiplication(userMultiplicationInputA, userMultiplicationInputB);
                            WaitforUserConfirmation();
                            break;
                        case UserSelection.Division:
                            Console.WriteLine("You have choosen 'Division'");
                            int userDivisionInputA = ConvertHexInputToInt(1);
                            int userDivisionInputB = ConvertHexInputToInt(2);
                            CalculateDivision(userDivisionInputA, userDivisionInputB);
                            WaitforUserConfirmation();
                            break;
                        case UserSelection.Exit:
                            Console.WriteLine("Goodbye...");
                            userSelectionAsInteger = 100;
                            break;
                        case UserSelection.Invalid:
                            WriteErrorMessage("Invalid input. Try again.");
                            WaitforUserConfirmation();
                            break;
                        default:
                            break;
                    }
                }
            } while (userSelectionAsInteger <= 6);
        }

        /// <summary>
        /// Show a simple selection menu to the user
        /// </summary>
        private static void ShowMenuToUser()
        {
            Console.WriteLine("Type one of the following numbers to make your selection:");
            Console.WriteLine("(1) Addition");
            Console.WriteLine("(2) Subtraction");
            Console.WriteLine("(3) Multiplication");
            Console.WriteLine("(4) Division");
            Console.WriteLine("(5) Exit");
            Console.WriteLine("_____________________\n");
        }

        /// <summary>
        /// Helper method to clear wrong user inputs
        /// </summary>
        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        /// <summary>
        /// Do nothing until user hits ENTER to continue
        /// </summary>
        private static void WaitforUserConfirmation()
        {
            Console.WriteLine("Press ENTER to return to menu selection.");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { ClearCurrentConsoleLine(); }
            Console.Clear();
        }

        /// <summary>
        /// Write red highlighted error to the user
        /// </summary>
        private static void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Method to calculate the addition of two values
        /// </summary>
        /// <param name="a">First value for addition</param>
        /// <param name="b">Second value for addition</param>
        private static void CalculateAddition(int a, int b)
        {
            int result = a + b;
            Console.WriteLine("The addition of {0:X} and {1:X} results in: {2:X}", a, b, result);
        }

        /// <summary>
        /// Method to calculate the subtraction of two values
        /// </summary>
        /// <param name="a">First value for subtraction</param>
        /// <param name="b">Second value for subtraction</param>
        private static void CalculateSubtraction(int a, int b)
        {
            if (a < b)
            {
                WriteErrorMessage("Negative subtraction is not supported. Second value has to be greater thant first.");
            }
            else
            {
                int result = a - b;
                Console.WriteLine("The substraction of {0:X} and {1:X} results in: {2:X}", a, b, result);
            }
        }

        /// <summary>
        /// Method to calculate the multiplication of two values
        /// </summary>
        /// <param name="a">First value for multiplication</param>
        /// <param name="b">Second value for multiplication</param>
        private static void CalculateMultiplication(int a, int b)
        {
            int result = a * b;
            Console.WriteLine("The multiplication of {0:X} and {1:X} results in: {2:X}", a, b, result);
        }

        /// <summary>
        /// Method to calculate the division of two values
        /// </summary>
        /// <param name="a">First value for division</param>
        /// <param name="b">Second value for division</param>
        private static void CalculateDivision(int a, int b)
        {
            if (b == 0)
            {
                WriteErrorMessage("Dividing by '0' is not allowed. Please try again.");
            }
            else
            {
                int result = a / b;
                Console.WriteLine("The division of {0:X} and {1:X} results in: {2:X}", a, b, result);
            }
        }

        /// <summary>
        /// Try converting user input into a valid menu selection
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static int TryConvertUserSelection(string input)
        {
            bool success = Int32.TryParse(input, out int convertedInput);

            if (success)
            {
                return convertedInput;
            }
            else
            {
                return 6;
            }

        }

        /// <summary>
        /// Private method to convert hexadecimal input to integer
        /// </summary>
        /// <param name="counter">Input to specifiy if first, second or other value</param>
        /// <returns>Valid integer for program internal calculation</returns>
        private static int ConvertHexInputToInt(int counter)
        {

            bool validInput = false;

            do
            {
                string userInput;
                if (counter == 1)
                {
                    Console.WriteLine("Enter the first hexadecimal number: ");
                    userInput = Console.ReadLine();
                }
                else if (counter == 2)
                {
                    Console.WriteLine("Enter the second hexadecimal number: ");
                    userInput = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Enter another hexadecimal number: ");
                    userInput = Console.ReadLine();
                }

                try
                {
                    int convertedInput = int.Parse(userInput, System.Globalization.NumberStyles.HexNumber);
                    validInput = true;
                    return convertedInput;
                }
                catch
                {
                    WriteErrorMessage("The entered value is not a valid hexadecimal number");
                }

            } while (validInput == false);

            return 0;
        }
    }
}
