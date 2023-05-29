public class TokenParameters
{
    public string Issuer => "issuer";
    public string Audience => "audience";
    public string SecretKey => "this is my custom Secret key for authentication";
    public DateTime Expiry => DateTime.Now.AddDays(1);
}