# Security Audit Prompt for .NET 4.8

Perform a security audit on this .NET 4.8 application:

1. **Transport**: Is ServicePointManager.SecurityProtocol set to Tls12? No Tls13 claims?
2. **Authentication**: ASP.NET Identity 2 / Windows Auth / JWT — which is used? Configured correctly?
3. **Authorization**: [Authorize] attributes present? Role checks in code?
4. **Input Validation**: All inputs validated? Regex whitelist where appropriate?
5. **SQL Injection**: 100% parameterized queries? No string concatenation?
6. **XSS**: Razor auto-encoding? @Html.Raw() usage reviewed?
7. **CSRF**: AntiForgeryToken on all state-changing requests?
8. **Secrets**: web.config encrypted? DPAPI used? No hardcoded passwords?
9. **File Uploads**: Extension + MIME + magic bytes + size validation?
10. **OWASP Top 10**: Address each item for .NET 4.8 specifically.

Output findings with [CRITICAL], [HIGH], [MEDIUM], [LOW] severity.
Include CVE references where applicable (CVE-2026-32177, CVE-2026-35433).
