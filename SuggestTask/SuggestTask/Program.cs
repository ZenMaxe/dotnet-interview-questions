namespace SuggestTask
{
    internal static class Program
    {
        // Run:
        // dotnet run "mobile, mouse, moneypot, monitor, mousepad" "mouse"
        // dotnet run "havana" "havana"
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Please Provide Product List and Keyword");
            }

            string[] productList = args[0].Split(',').Select(x => x.Trim()).ToArray();
            if (productList.Length < 1)
            {
                throw new ArgumentException("Product List is Empty!");
            }


            string keyword = args[1];

            Sort(ref productList);
            List<List<string>> output = new();
            List<string> list = new List<string>();

            for (int i = 0; i < keyword.Length; i++) 
            {
                string currentChar = keyword.Substring(0, i);
                list = productList
                        .Where(x => x.StartsWith(currentChar, StringComparison.OrdinalIgnoreCase)) // Ignore Case Sensitive thigns
                        .OrderBy(x => x) // Sort
                        .Take(3)
                        .ToList();
            }


            // Without LINQ

            for(int i = 0; i < keyword.Length; i++)
            {
                List<string> suggestions = new();
                string currentChar = keyword.Substring(0, i);
                Console.WriteLine($"Current Character For Search: {currentChar}");

                foreach (var product in productList)
                {
                    if (product.StartsWith(currentChar, StringComparison.OrdinalIgnoreCase))
                    {
                        suggestions.Add(product);
                        if (suggestions.Count == 3)
                        {
                            break;
                        }
                    }
                    
                }
                output.Add(suggestions);

            }
            Console.WriteLine("All Suggestions:");
            foreach (var suggestionList in output)
            {
                Console.WriteLine($"[{string.Join(", ", suggestionList)}]");
            }

        }

        public static void Sort(ref string[] array)
        {
            for (int i = 0; i < array.Length; i++ )
            {
                string key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j].CompareTo(key) > 0 )
                {
                    array[j + 1] = array[j];

                    j = j - 1;
                }
                array[j + 1] = key;
            }
        }

        
    }
}
