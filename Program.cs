namespace nex4;


internal class Program
{
    static void Main(string[] args)
    {

        Add();
    }


    public static void Add()
    {
        while(true)
        {
            List<int> nums = new List<int>();

            Console.WriteLine("Enter two numbers separated by comma to add");

            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine($"Sum of {string.Join(",", input)} is 0");
            }

            var stringNumbers = input.Replace(" ", "");

            if (stringNumbers.ToLower() == "x")
            {
                break;
            }

            var numbers = stringNumbers.Split(new string[] { ",", "\\n" }, StringSplitOptions.RemoveEmptyEntries);


            foreach (var number in numbers)
            {
                if (!int.TryParse(number, out _))
                {
                    continue;
                }
                else
                {
                    nums.Add(int.Parse(number));
                }
            }

            var sum = nums.Sum();
            Console.WriteLine($"Sum of {string.Join(",", nums)} is {sum}");


        }
    }
}
