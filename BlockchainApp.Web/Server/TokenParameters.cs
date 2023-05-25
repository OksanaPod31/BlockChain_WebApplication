public class TokenParameters
{
    public string Issuer => "issuer";
    public string Audience => "audience";
    public string SecretKey => "secretKey";
    public DateTime Expiry => DateTime.Now.AddDays(1);
}