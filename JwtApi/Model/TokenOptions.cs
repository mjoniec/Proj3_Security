namespace JwtApi.Model
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public long AccessTokenExpiration { get; set; }
        public long RefreshTokenExpiration { get; set; }
        public string Secret { get; set; }

        public static TokenOptions GetDefault()
        {
            return new TokenOptions
            {
                Audience = "SampleAudience",
                Issuer = "JWPAPI",
                AccessTokenExpiration = 30,
                RefreshTokenExpiration = 60,
                Secret = "SecretSecret_3333333_testtesttest_test"
            };
        }
    }
}
