using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class GenerateRandomPassword
    {

        public static string GeneratePassword()
        {
      
            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    
            "abcdefghijkmnopqrstuvwxyz",    
            "0123456789",                  
            "!@$?"                       
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < 12
                || chars.Distinct().Count() < 9; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
