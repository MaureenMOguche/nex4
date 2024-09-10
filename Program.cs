namespace nex4;


internal class Program
{
    static void Main(string[] args)
    {

        var calculator = new Calculator();
        var inputHandler = new InputHandler();

        while (true)
        {
            Console.WriteLine("Enter numbers to add (or 'x' to exit):");
            string input = Console.ReadLine();

            if (input?.ToLower() == "x")
                break;

            try
            {
                var result = calculator.Add(input);
                Console.WriteLine($"Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

            }
        }
    }
}

public class Calculator
{
    public int Add(string numbers)
    {
        if (string.IsNullOrWhiteSpace(numbers))
            return 0;

        var inputHandler = new InputHandler();
        var parsedNumbers = inputHandler.ParseInput(numbers);

        ValidateNumbers(parsedNumbers);

        return parsedNumbers.Sum();
    }

    private void ValidateNumbers(List<int> numbers)
    {
        var negatives = numbers.Where(n => n < 0).ToList();
        if (negatives.Any())
        {
            throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negatives)}");
        }
    }
}

public class InputHandler
{
    public List<int> ParseInput(string input)
    {
        var delimiters = new List<string> { ",", "\n" };
        var numbers = input;

        if (input.StartsWith("//"))
        {
            var customDelimiterInput = input.Substring(2, input.IndexOf("\n") - 2);
            delimiters.AddRange(ParseCustomDelimiters(customDelimiterInput));
            numbers = input.Substring(input.IndexOf("\n") + 1);
        }

        return numbers.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                      .Select(ParseNumber)
                      .Where(n => n <= 1000)
                      .ToList();
    }

    private List<string> ParseCustomDelimiters(string input)
    {
        if (input.StartsWith("[") && input.EndsWith("]"))
        {
            return input.Trim('[', ']')
                        .Split(new[] { "][" }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
        }
        return new List<string> { input };
    }

    private int ParseNumber(string number)
    {
        return int.TryParse(number, out int result) ? result : 0;
    }
}

