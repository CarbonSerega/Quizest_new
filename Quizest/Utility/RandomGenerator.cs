using System;
using System.Linq;

namespace Utility
{
    public static class RandomGenerator
    {
        public static string GenerateHexKey(int size = 24)
        {
            var random = new Random();
            byte[] buffer = new byte[size / 2];
            random.NextBytes(buffer);
            string result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (size % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X").ToLower();
        }
    }
}
