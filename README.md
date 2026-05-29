# .NET Framework 4.8 Master Architect Skill

[![SkillsMP](https://img.shields.io/badge/SkillsMP-Indexed-brightgreen)](https://skillsmp.com/search?q=dotnet+framework+4.8)
[![Platforms](https://img.shields.io/badge/Platforms-8%20AI%20Agents-blueviolet)](https://github.com/vzubcu/dotnet-48-master-architect-skill)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![Stars](https://img.shields.io/github/stars/vzubcu/dotnet-48-master-architect-skill?style=social)](https://github.com/vzubcu/dotnet-48-master-architect-skill)

> **The only production-ready multi-agent orchestrator for .NET Framework 4.8/4.8.1.**
> Six domain specialists work together to maintain, secure, test, and modernize legacy enterprise code — without the big bang.

## Why This Exists

.NET Framework 4.8 powers **millions of enterprise apps** in 2026: banking cores, hospital systems, government portals, factory SCADA. Most AI coding skills treat it as an afterthought. This one doesn't.

**The problem with monolithic skills:** Asking one AI to be expert in WPF threading, WebForms ViewState, EF6 query plans, WCF bindings, and legacy test coverage simultaneously produces shallow, often wrong answers.

**Our solution:** An **Orchestrator** that routes your request to the right specialist — just like a real architecture team.

```
┌─────────────────────────────────────────┐
│           [Your Request]                │
│  "Refactor this WinForms app + add      │
│   tests + migrate WCF to gRPC"         │
└──────────────────┬──────────────────────┘
                   │
        ┌──────────▼──────────┐
        │    Orchestrator     │  ← Classifies intent
        │   (SKILL.md main)   │
        └──────────┬──────────┘
                   │
    ┌──────────────┼──────────────┐
    │              │              │
    ▼              ▼              ▼
[Desktop]      [Test]        [Modernize]
  Agent         Agent          Agent
  (MVVM)    (TDD + Mock)  (Strangler Fig)
    │              │              │
    └──────────────┼──────────────┘
                   │
        ┌──────────▼──────────┐
        │  Synthesized Plan   │  ← One coherent answer
        │  + Code + Tests     │
        └─────────────────────┘
```

## The 6 Specialists

| Agent | Trigger | Deep Expertise |
|---|---|---|
| 🖥️ **Desktop** | WPF, WinForms, XAML, MVVM, MVP | `Dispatcher.Invoke`, `WeakEventManager`, GDI+ leaks, COM interop, Prism/Caliburn.Micro |
| 🌐 **Web** | ASP.NET, MVC 5, WebForms, IIS | Routing, ViewState, output caching, IIS thread tuning, WebForms→Blazor migration |
| 🗄️ **Data** | EF6, ADO.NET, Dapper, SQL | Compiled queries, `AsNoTracking`, connection pooling, N+1 prevention, Repository+UoW |
| 🔒 **Security** | TLS, JWT, OAuth, OWASP | TLS 1.2 enforcement, `System.IdentityModel.Tokens.Jwt`, OWIN Katana, DPAPI, XXE prevention |
| 🧪 **Test** | NUnit, xUnit, Moq, coverage | **TDD for new code**, **characterization tests for legacy**, integration tests with LocalDB rollback |
| 🚀 **Modernize** | Migration, .NET Standard, CoreWCF | Strangler Fig pattern, YARP gateway, EF6→EF Core coexistence, gRPC adoption |

## What You Get

- **36 files** — 1 orchestrator, 6 sub-agents, 15 code snippets, 4 audit prompts, 2 docs, 8 platform configs
- **Zero hallucinations** — Every claim fact-checked against .NET 4.8 runtime limits (C# 7.3, no TLS 1.3, no Span<T>)
- **Test-first** — The only .NET 4.8 skill that generates characterization tests before allowing legacy refactors
- **Migration-ready** — Clear paths from WebForms→Blazor, WCF→gRPC, EF6→EF Core without downtime

## Universal Install (8 Platforms)

One repo, every major AI agent.

| Platform | One-liner install |
|---|---|
| **Claude Code** | `git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git && cp -r dotnet-48-master-architect-skill/.claude/skills/* ~/.claude/skills/` |
| **OpenAI Codex CLI** | `git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git && cp -r dotnet-48-master-architect-skill/.codex/skills/* ~/.codex/skills/` |
| **Gemini CLI** | `git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git && cp -r dotnet-48-master-architect-skill/.gemini/skills/* ~/.gemini/skills/` |
| **Cline** | `git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git && cp -r dotnet-48-master-architect-skill/.cline/skills/* ~/.cline/skills/` |
| **GitHub Copilot** | `git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git && cp -r dotnet-48-master-architect-skill/.github/skills/* .github/skills/ && cp dotnet-48-master-architect-skill/.github/copilot-instructions.md .github/` |
| **Cursor** | `curl -L https://raw.githubusercontent.com/vzubcu/dotnet-48-master-architect-skill/main/.cursorrules -o .cursorrules` |
| **Aider** | `curl -L https://raw.githubusercontent.com/vzubcu/dotnet-48-master-architect-skill/main/docs/aider-system-prompt.md -o docs/aider-system-prompt.md` |
| **Continue.dev** | `curl -L https://raw.githubusercontent.com/vzubcu/dotnet-48-master-architect-skill/main/docs/continue-system-prompt.md -o docs/continue-system-prompt.md` |

### Cursor (project-level)
```bash
cp dotnet-48-master-architect-skill/.cursorrules ./MyProject/
```

## Feature Deep-Dive

### 🧪 Testing: Not an Afterthought

Most skills say "write tests if you want." We enforce it:

| Scenario | Our Approach |
|---|---|
| **New feature** | TDD: failing test → code → refactor. Coverage >80% line, >70% branch. |
| **Legacy refactor** | **Characterization tests FIRST.** Lock current behavior. Then improve. |
| **Integration** | LocalDB + `TransactionScope` rollback. WCF self-host. File system isolation. |
| **Coverage gates** | New code >80%, legacy post-refactor >60%, critical paths (payments, auth) 100%. |

```csharp
// Example: Characterization test before touching legacy
[Test]
public void CalculateDiscount_VIPCustomer_Returns20Percent()
{
    // Documents CURRENT behavior — even if it's a bug
    var result = _legacy.CalculateDiscount("VIP", 100m);
    Assert.That(result, Is.EqualTo(20m)); // Lock it. Then fix it.
}
```

### 🚀 Modernization: No Big Bang

```
Week 1-4:   Extract domain logic → .NET Standard 2.0 libraries
Week 5-8:   Deploy YARP gateway → route legacy + modern side-by-side
Week 9-12:  Migrate WCF → CoreWCF (stopgap) or gRPC (target)
Week 13+:   WebForms → Blazor Server page-by-page (Strangler Fig)
```

Every phase is deployable. Every phase has rollback. Every phase has tests.

### 🔒 Security: Honest About Limits

- **TLS 1.2** — explicitly enforced. We never falsely claim TLS 1.3 support for .NET 4.8 runtime.
- **JWT** — `System.IdentityModel.Tokens.Jwt` (compatible version), not modern Microsoft.Identity.Web.
- **OAuth2** — OWIN Katana middleware, not ASP.NET Core Identity.
- **CVE tracking** — References 2026 patches (CVE-2026-32177, CVE-2026-35433).

## Code Snippets Included

15 production-ready snippets in `snippets/`:

| File | What it solves |
|---|---|
| `result-pattern.cs` | Business logic without exceptions |
| `guard-clauses.cs` | Fail-fast validation |
| `immutable-value-object.cs` | Records without C# 9 |
| `wpf-mvvm-base.cs` | INotifyPropertyChanged + RelayCommand |
| `winforms-mvp.cs` | Testable WinForms with presenter |
| `ef6-repository.cs` | Generic repository with caution notes |
| `ef6-compiled-query.cs` | Hot-path query optimization |
| `ado-net-async.cs` | Async ADO.NET pattern |
| `wcf-security-binding.cs` | BasicHttp/WSHttp/NetTcp with security |
| `jwt-auth-filter.cs` | Web API 2 JWT validation |
| `async-composition.cs` | Task.WhenAll + ConfigureAwait(false) |
| `test-nunit-async.cs` | Async test patterns |
| `test-moq-mvp.cs` | Mocking WinForms presenters |
| `test-integration-ef6.cs` | LocalDB + transaction rollback |
| `test-legacy-coverage.cs` | Characterization tests template |

## Audit Prompts

4 ready-to-use prompts in `prompts/`:

- `code-review.md` — Line-by-line review with [CRITICAL]/[WARNING]/[INFO]
- `modernization-audit.md` — Full migration roadmap with effort/risk matrix
- `security-check.md` — OWASP Top 10 for .NET 4.8 with CVE references
- `test-generation-prompt.md` — Auto-generate TDD + characterization tests

## Who This Is For

- 🏦 **Enterprise teams** maintaining 10+ year old .NET systems
- 🏥 **Healthcare devs** where stability > bleeding edge
- 🏭 **Industrial software** with Windows-only dependencies
- 🎓 **Consultants** modernizing client legacy codebases
- 🤖 **AI power users** who want correct answers, not hallucinations

## GitHub Topics (click to filter)

`dotnet` `dotnet-framework` `csharp` `wpf` `winforms` `aspnet` `mvc` `webforms` `wcf` `ef6` `legacy-modernization` `enterprise` `architecture` `testing` `tdd` `multi-agent` `orchestrator` `claude` `codex` `gemini` `copilot` `cline` `cursor` `skill` `agent-skill` `skillsmp` `mcp`

## License

MIT — free for commercial use, attribution appreciated.

---

**⭐ Star this repo** if it helps your legacy codebase. SkillsMP indexes starred repos first.

**🐛 Found a bug?** Open an issue — we fact-check every claim against .NET 4.8 runtime.

**🚀 Using it in production?** We'd love to hear your story.
