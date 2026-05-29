# .NET Framework 4.8 Master Architect Skill

[![SkillsMP](https://img.shields.io/badge/SkillsMP-Universal-green)](https://skillsmp.com/search?q=dotnet+framework+4.8)
[![Agents](https://img.shields.io/badge/Agents-Claude%20%7C%20Codex%20%7C%20Gemini%20%7C%20Cline%20%7C%20Copilot-blue)]()
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> **Universal Orchestrator Skill** for .NET Framework 4.8/4.8.1 — works with Claude Code, OpenAI Codex CLI, Gemini CLI, Cline, GitHub Copilot, and any AI agent supporting the open SKILL.md standard.

## What Makes This Different?

Unlike single-purpose skills, this is an **Orchestrator** that delegates to **6 specialized sub-agents**:

- 🖥️ **Desktop Agent** — WPF (MVVM) + WinForms (MVP), UI threading, memory leaks
- 🌐 **Web Agent** — ASP.NET MVC 5 + WebForms, IIS tuning, caching
- 🗄️ **Data Agent** — EF6 + ADO.NET + Dapper, query optimization
- 🔒 **Security Agent** — TLS 1.2, OWASP, JWT, OAuth2 via OWIN
- 🧪 **Test Agent** — TDD, characterization tests, legacy coverage
- 🚀 **Modernize Agent** — Strangler Fig, .NET Standard 2.0, migration paths

## Multi-Agent Architecture

```
                    [Orchestrator]
                         |
        +--------+-------+--------+--------+
        |        |        |        |        |
    [Desktop] [Web]   [Data] [Security] [Test] [Modernize]
        |        |        |        |        |        |
        +--------+--------+--------+--------+--------+
                         |
                    [Synthesized Response]
```

The Orchestrator classifies intent, delegates to specialists, and synthesizes results. Each sub-agent has an isolated context window and domain-specific expertise. citeweb_search:7#12

## Supported AI Platforms

| Platform | Installation Path | Status |
|---|---|---|
| **Claude Code** | `~/.claude/skills/dotnet-48-master-architect/` | ✅ Ready |
| **OpenAI Codex CLI** | `~/.codex/skills/dotnet-48-master-architect/` | ✅ Ready |
| **Gemini CLI** | `~/.gemini/skills/dotnet-48-master-architect/` | ✅ Ready |
| **Cline** | `~/.cline/skills/dotnet-48-master-architect/` | ✅ Ready |
| **GitHub Copilot** | `.github/skills/dotnet-48-master-architect/` | ✅ Ready |
| **Cursor** | `.cursorrules` in project root | ✅ Ready |
| **Agents** | `~/.agents/skills/dotnet-48-master-architect/` | ✅ Ready |
| **Aider** | Use `docs/aider-system-prompt.md` | ✅ Ready |
| **Continue.dev** | Use `docs/continue-system-prompt.md` | ✅ Ready |

## Quick Install

### Claude Code
```bash
git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git
cp -r dotnet-48-master-architect-skill/.claude/skills/* ~/.claude/skills/
```

### Agents
```bash
git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git
cp -r dotnet-48-master-architect-skill/.agents/skills/* ~/.agents/skills/
```

### Codex CLI
```bash
git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git
cp -r dotnet-48-master-architect-skill/.codex/skills/* ~/.codex/skills/
```

### Gemini CLI
```bash
git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git
cp -r dotnet-48-master-architect-skill/.gemini/skills/* ~/.gemini/skills/
```

### Cline
```bash
git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git
cp -r dotnet-48-master-architect-skill/.cline/skills/* ~/.cline/skills/
```

### GitHub Copilot (VS Code)
```bash
git clone https://github.com/vzubcu/dotnet-48-master-architect-skill.git
cp -r dotnet-48-master-architect-skill/.github/skills/* .github/skills/
cp dotnet-48-master-architect-skill/.github/copilot-instructions.md .github/
```

### Cursor
```bash
cp dotnet-48-master-architect-skill/.cursorrules ./MyProject/
```

## Features

### Core Domains
- ✅ **Desktop**: WPF (MVVM) + WinForms (MVP), cross-thread UI, GDI+ safety, COM interop
- ✅ **Web**: ASP.NET MVC 5 + WebForms modernization, IIS tuning, output caching
- ✅ **Services**: WCF lifecycle, bindings, migration to CoreWCF/gRPC
- ✅ **Data**: EF6 performance (`AsNoTracking`, compiled queries), ADO.NET async, Dapper
- ✅ **Security**: TLS 1.2, OWASP Top 10 mitigation, JWT in .NET 4.8, OWIN OAuth2
- ✅ **Performance**: GC tuning, LOH monitoring, IIS thread tuning, async deadlock prevention

### Testing (First-Class Citizen)
- ✅ **TDD**: Red-Green-Refactor for new code
- ✅ **Characterization Tests**: Lock legacy behavior BEFORE refactoring
- ✅ **Unit Testing**: NUnit/xUnit/MSTest + Moq/NSubstitute
- ✅ **Integration Testing**: EF6 + LocalDB, WCF self-host, file system isolation
- ✅ **Coverage Targets**: New code >80%, legacy >60%, critical paths 100% branch
- ✅ **Test Data Builders**: Pattern for complex test setup

### Patterns (Backported from Modern .NET)
- ✅ **Result<T>** — Explicit success/failure instead of exceptions
- ✅ **Guard Clauses** — Fail fast with clear validation
- ✅ **Async Composition** — `Task.WhenAll`, `ConfigureAwait(false)`
- ✅ **Immutable Value Objects** — `sealed class` + `readonly` fields + `IEquatable<T>`
- ✅ **Constructor Injection** — Autofac/Ninject/Unity
- ✅ **IReadOnlyList<T>** — Read-only collection exposure
- ✅ **Options Pattern** — `FromConfiguration()` manual binding

### Modernization
- ✅ **Strangler Fig Pattern** — Incremental migration without big bang
- ✅ **.NET Standard 2.0** — Extract business logic to shared libraries
- ✅ **YARP Gateway** — Route between legacy and modern endpoints
- ✅ **Technology Migration Matrix** — WebForms→Blazor, WCF→gRPC, EF6→EF Core

## Tags

`dotnet`, `dotnet-framework`, `csharp`, `wpf`, `winforms`, `aspnet`, `mvc`, `webforms`, `wcf`, `ef6`, `legacy-modernization`, `enterprise`, `architecture`, `testing`, `tdd`, `multi-agent`, `orchestrator`, `claude`, `codex`, `gemini`, `copilot`, `cline`

## License

MIT License — see [LICENSE](LICENSE)
