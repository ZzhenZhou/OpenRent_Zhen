using System;
using System.Linq;

namespace OpenRent
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please provide input (or type 'exit' to quit):");

                var input = Console.ReadLine();

                if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input. Try again.\n");
                    continue;
                }

                try
                {
                    var result = OpenRent(input);
                    Console.WriteLine($"Result: {result}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}\n");
                }
            }
        }

        /// <summary>
        /// Builds output string from:
        /// 1. reversed input
        /// 2. smallest alphabetical character (case-insensitive, letters only)
        /// 3. vowel parity suffix ("open" if odd, "rent" if even)
        /// </summary>
        private static string OpenRent(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input must be a non-empty string.", nameof(input));

            if (!input.Any(char.IsLetter))
                return "Invalid input: no letters";

            return ReverseString(input)
                   + GetSmallestLetterByUnicodeCaseInsensitive(input)
                   + GetVowelParitySuffix(input);
        }

        private static string ReverseString(string input)
        {
            return new string(input.Reverse().ToArray());
        }

        /// <summary>
        /// Returns the smallest alphabetical character in the string (case-insensitive),
        /// ignoring non-letter characters.
        /// Assumes input contains at least one letter.
        /// </summary>
        private static char GetSmallestLetterByUnicodeCaseInsensitive(string input)
        {
            return input
                .Where(char.IsLetter)
                .Select(char.ToLower)
                .Min();
        }

        /// <summary>
        /// Counts vowels (a, e, i, o, u) case-insensitively.
        /// Returns "open" if odd count, "rent" if even.
        /// </summary>
        private static string GetVowelParitySuffix(string input)
        {
            int vowelCount = input.Count(c =>
                "aeiouAEIOU".Contains(c));

            return vowelCount % 2 == 0 ? "rent" : "open";
        }
    }
}