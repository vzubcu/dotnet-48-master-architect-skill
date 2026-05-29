---
name: "dotnet-48-security-agent"
description: "Deep specialist for .NET Framework 4.8 Security. Activate for: TLS, JWT, OAuth2, OWASP, encryption, certificates, web.config security, input validation, or authentication/authorization."
---

# .NET 4.8 Security Specialist

## Transport & Cryptography

### TLS Configuration
```csharp
// MINIMUM TLS 1.2. Do NOT claim TLS 1.3 support for .NET 4.8 runtime.
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

// For HttpClient (if using modern HttpClient in .NET 4.8 via NuGet)
var handler = new HttpClientHandler
{
    SslProtocols = SslProtocols.Tls12
};
```

### Certificate Management
```csharp
// Store in Windows Certificate Store, never hardcode
using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
{
    store.Open(OpenFlags.ReadOnly);
    var cert = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false)[0];
    // Use cert for WCF / HTTP client
}
```

## Application Security (OWASP Top 10 for .NET 4.8)

### A01: Broken Access Control
- Use `[Authorize(Roles = "Admin")]` consistently.
- Validate user ownership: `if (order.CustomerId != User.Identity.Name) return Forbid();`
- Disable directory browsing in IIS.

### A03: Injection
```csharp
// ALWAYS parameterized. NEVER string concatenation.
// GOOD:
var cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @id", conn);
cmd.Parameters.AddWithValue("@id", userId);

// BAD (NEVER):
var cmd = new SqlCommand($"SELECT * FROM Users WHERE Id = {userId}", conn);
```

### A05: Security Misconfiguration
- Remove `customErrors mode="Off"` in production.
- Remove debug compilation: `<compilation debug="false">`.
- Encrypt connection strings:
```bash
aspnet_regiis -pe "connectionStrings" -app "/MyApp"
```

### A07: Identification & Authentication
- **ASP.NET Identity 2**: For MVC 5/WebForms. Use 2FA, lockout, password complexity.
- **Windows Authentication**: For intranet apps. Kerberos/NTLM.
- **JWT in .NET 4.8**:
```csharp
public class JwtAuthorizationFilter : AuthorizationFilterAttribute
{
    public override void OnAuthorization(HttpActionContext actionContext)
    {
        var auth = actionContext.Request.Headers.Authorization;
        if (auth == null || auth.Scheme != "Bearer")
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            return;
        }

        var token = auth.Parameter;
        var principal = ValidateToken(token); // Use System.IdentityModel.Tokens.Jwt
        if (principal == null)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            return;
        }

        Thread.CurrentPrincipal = principal;
        HttpContext.Current.User = principal;
    }
}
```

### A08: Software & Data Integrity
- Verify ClickOnce manifest signatures (SHA384/SHA512 in 2026).
- Use strong-named assemblies where applicable.
- Validate file uploads: extension + MIME type + magic bytes + size limit.

## OAuth2 / OIDC via OWIN (Katana)
```csharp
// Startup.cs for OWIN
public void Configuration(IAppBuilder app)
{
    app.UseCookieAuthentication(new CookieAuthenticationOptions
    {
        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        LoginPath = new PathString("/Account/Login")
    });

    app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
    {
        ClientId = "your-client-id",
        Authority = "https://your-identity-server",
        RedirectUri = "https://your-app/signin-oidc",
        ResponseType = "code id_token",
        Scope = "openid profile email"
    });
}
```

## DPAPI for Secrets
```csharp
// Encrypt sensitive data in web.config / app.config
var encrypted = ProtectedData.Protect(
    Encoding.UTF8.GetBytes(secret),
    optionalEntropy,
    DataProtectionScope.CurrentUser);

var decrypted = ProtectedData.Unprotect(encrypted, optionalEntropy, DataProtectionScope.CurrentUser);
```
