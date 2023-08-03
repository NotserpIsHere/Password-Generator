using System;
using System.Linq;
using System.Security.Cryptography;

namespace PasswordGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the length of the password: ");
            int length = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Include lowercase letters? (y/n): ");
            bool includeLowercase = Console.ReadLine().ToLower() == "y";

            Console.WriteLine("Include uppercase letters? (y/n): ");
            bool includeUppercase = Console.ReadLine().ToLower() == "y";

            Console.WriteLine("Include numbers? (y/n): ");
            bool includeNumbers = Console.ReadLine().ToLower() == "y";

            Console.WriteLine("Include special characters? (y/n): ");
            bool includeSpecial = Console.ReadLine().ToLower() == "y";

            Console.WriteLine("How many passwords do you want to generate? ");
            int count = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(GeneratePassword(length, includeLowercase, includeUppercase, includeNumbers, includeSpecial));
            }
        }

        public static string GeneratePassword(int length, bool includeLowercase, bool includeUppercase, bool includeNumbers, bool includeSpecial)
        {
            string valid = "";
            if (includeLowercase) valid += "abcdefghijklmnopqrstuvwxyz";
            if (includeUppercase) valid += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (includeNumbers) valid += "1234567890";
            if (includeSpecial) valid += "!@#$%^&*()_-+=<>?/{}~|";

            using (var rng = new RNGCryptoServiceProvider())
            {
                return new string(Enumerable.Range(0, length)
                    .Select(x => valid[GetInt(rng, valid.Length)])
                    .ToArray());
            }
        }

        private static int GetInt(RNGCryptoServiceProvider rng, int max)
        {
            var data = new byte[4];
            rng.GetBytes(data);
            return Math.Abs(BitConverter.ToInt32(data, 0)) % max;
        }
    }
}
