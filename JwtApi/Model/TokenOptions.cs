namespace JwtApi.Model
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public long AccessTokenExpiration { get; set; }
        public string Secret { get; set; }

        public static TokenOptions GetDefault()
        {
            return new TokenOptions
            {
                Audience = "SampleAudience",
                Issuer = "JWPAPI",
                AccessTokenExpiration = 300,
                Secret = "SecretSecret_3333333_testtesttest_test"
            };
        }
    }
}
