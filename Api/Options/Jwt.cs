namespace WebAppAuthorization.Options;

public class Jwt
{
    public static string JwtConfig = "JWTConfig";
    
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double DurationInMinutes { get; set; }  
}