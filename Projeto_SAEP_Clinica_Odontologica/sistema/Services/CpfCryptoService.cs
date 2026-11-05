using System.Security.Cryptography;
using System.Text;
namespace ClinicaOdontologica.Services;
public class CpfCryptoService
{
    private readonly byte[] _key;
    public CpfCryptoService(IConfiguration configuration)
    {
        var raw = configuration["Security:CpfKey"] ?? throw new InvalidOperationException("Configure Security:CpfKey.");
        _key = SHA256.HashData(Encoding.UTF8.GetBytes(raw));
    }
    public string Encrypt(string value)
    {
        using var aes = Aes.Create();
        aes.Key = _key; aes.GenerateIV();
        using var encryptor = aes.CreateEncryptor();
        var plain = Encoding.UTF8.GetBytes(value);
        var cipher = encryptor.TransformFinalBlock(plain, 0, plain.Length);
        return Convert.ToBase64String(aes.IV.Concat(cipher).ToArray());
    }
    public string Decrypt(string value)
    {
        var bytes = Convert.FromBase64String(value);
        using var aes = Aes.Create();
        aes.Key = _key; aes.IV = bytes[..16];
        using var decryptor = aes.CreateDecryptor();
        var plain = decryptor.TransformFinalBlock(bytes, 16, bytes.Length - 16);
        return Encoding.UTF8.GetString(plain);
    }
}
