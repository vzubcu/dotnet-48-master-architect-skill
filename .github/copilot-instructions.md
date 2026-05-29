# .NET Framework 4.8 Coding Guidelines for Copilot

## Constraints
- Target .NET Framework 4.8.1, C# 7.3
- No records, init-only properties, Span<T>, Memory<T>
- No Minimal APIs, no ASP.NET Core Middleware
- TLS 1.2 only, do not suggest TLS 1.3
- Windows Server 2019/2022, Windows 10/11

## Preferred Patterns
- Result<T> for business failures instead of exceptions
- Constructor injection with Autofac/Ninject/Unity
- MVP for WinForms, MVVM for WPF
- Repository + Unit of Work over EF6
- Guard clauses with explicit null checks
- Immutable objects: sealed + readonly + IEquatable<T>
- ConfigureAwait(false) in all library async code

## Testing (Non-Negotiable)
- TDD: Write failing test first, then code, then refactor
- Characterization tests: Document current legacy behavior BEFORE any change
- NUnit/xUnit + Moq for unit tests
- LocalDB + transaction rollback for integration tests
- Coverage targets: New code >80%, legacy after refactor >60%
- Test ViewModels/Presenters, never Views/Forms directly

## Security
- ServicePointManager.SecurityProtocol = Tls12
- Parameterized queries / EF6 only — zero SQL string concatenation
- AntiForgeryToken on all state-changing requests
- Encrypt sensitive web.config sections
- JWT: System.IdentityModel.Tokens.Jwt (compatible version for .NET 4.8)

## Anti-patterns to Avoid
- async void (except event handlers)
- .Result or .Wait() on UI/ASP.NET threads
- ServiceLocator pattern
- Mutable static state
- God classes (>500 lines)
- Code-behind calling SQL directly
- PolySharp or unofficial backports

## Modernization
- Extract domain logic to .NET Standard 2.0 class libraries
- Strangler Fig for gradual migration
- YARP gateway for legacy/modern coexistence
- CoreWCF as WCF stopgap on modern .NET
- gRPC for new internal services
