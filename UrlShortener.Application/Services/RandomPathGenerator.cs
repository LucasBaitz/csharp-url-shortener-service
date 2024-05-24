using System.Text;
using UrlShortener.Application.Intefaces;

namespace UrlShortener.Application.Services
{
    internal class RandomPathGenerator : IRandomPathGenerator
    {
        private readonly Random _random;
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public RandomPathGenerator()
        {
            _random = new();
        }

        public string GenerateRandomPath(int minLength, int maxLength)
        {
            int length = _random.Next(minLength, maxLength + 1);
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(_chars[_random.Next(_chars.Length)]);
            }

            return result.ToString();
        }
    }
}
