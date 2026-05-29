// JWT Authorization Filter for Web API 2 (.NET 4.8)
public class JwtAuthorizationFilter : AuthorizationFilterAttribute
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly byte[] _secret;

    public JwtAuthorizationFilter(string issuer, string audience, string secret)
    {
        _issuer = issuer;
        _audience = audience;
        _secret = Encoding.UTF8.GetBytes(secret);
    }

    public override void OnAuthorization(HttpActionContext actionContext)
    {
        var auth = actionContext.Request.Headers.Authorization;
        if (auth == null || auth.Scheme != "Bearer")
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            return;
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(_secret),
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(auth.Parameter, validationParameters, out validatedToken);

            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;
        }
        catch (Exception)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
