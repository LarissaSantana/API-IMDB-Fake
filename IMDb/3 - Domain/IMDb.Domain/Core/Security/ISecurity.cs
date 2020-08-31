namespace IMDb.Domain.Core.Security
{
    public interface ISecurity
    {
        string Encrypt(string value, string salt);
    }
}
