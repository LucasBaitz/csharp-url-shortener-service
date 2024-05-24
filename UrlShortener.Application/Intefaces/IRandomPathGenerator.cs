namespace UrlShortener.Application.Intefaces
{
    public interface IRandomPathGenerator
    {
        string GenerateRandomPath(int minLength, int maxLength);
    }
}
