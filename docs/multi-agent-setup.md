# Multi-Agent Setup Guide

This skill uses the **Orchestrator-Worker** pattern — the most deployed multi-agent architecture in production (70% of deployments). citeweb_search:7#12

## How It Works

```
[User Request]
    ↓
[Orchestrator] — classifies intent, decides delegation
    ↓
    +--→ [Desktop Agent] — WPF/WinForms
    +--→ [Web Agent] — ASP.NET MVC/WebForms
    +--→ [Data Agent] — EF6/ADO.NET
    +--→ [Security Agent] — TLS/Auth/OWASP
    +--→ [Test Agent] — TDD/Testing
    +--→ [Modernize Agent] — Migration
    ↓
[Aggregator] — synthesizes results
    ↓
[User Response]
```

## Sub-Agent Files

All sub-agents live in `agents/` folder. Each is a standalone SKILL.md-compatible file:

| File | Name | Description |
|---|---|---|
| `dotnet-48-desktop-agent.md` | dotnet-48-desktop-agent | WPF/WinForms specialist |
| `dotnet-48-web-agent.md` | dotnet-48-web-agent | ASP.NET specialist |
| `dotnet-48-data-agent.md` | dotnet-48-data-agent | EF6/ADO.NET specialist |
| `dotnet-48-security-agent.md` | dotnet-48-security-agent | Security specialist |
| `dotnet-48-test-agent.md` | dotnet-48-test-agent | Testing specialist |
| `dotnet-48-modernize-agent.md` | dotnet-48-modernize-agent | Modernization specialist |

## Platform-Specific Installation

### Claude Code
```bash
# Clone repo
git clone https://github.com/YOUR_ORG/dotnet-48-master-architect-skill.git

# Install orchestrator + all sub-agents
cp -r dotnet-48-master-architect-skill/.claude/skills/* ~/.claude/skills/

# Verify
ls ~/.claude/skills/dotnet-48-master-architect/SKILL.md
ls ~/.claude/skills/dotnet-48-desktop-agent/SKILL.md
# ... etc for each sub-agent
```

### OpenAI Codex CLI
```bash
cp -r dotnet-48-master-architect-skill/.codex/skills/* ~/.codex/skills/
```

### Gemini CLI
```bash
cp -r dotnet-48-master-architect-skill/.gemini/skills/* ~/.gemini/skills/
```

### Cline
```bash
cp -r dotnet-48-master-architect-skill/.cline/skills/* ~/.cline/skills/
```

### GitHub Copilot
```bash
# Project-level (committed to repo)
cp -r dotnet-48-master-architect-skill/.github/skills/* .github/skills/
```

## Customizing Sub-Agents

You can disable sub-agents you don't need by removing their folder from the skills directory. The orchestrator will skip missing agents gracefully.

## Adding Your Own Sub-Agent

1. Create `agents/my-custom-agent.md` with YAML frontmatter:
```yaml
---
name: "my-custom-agent"
description: "Activate for: [your trigger keywords]"
---
```
2. Add to `agents/` folder.
3. Update orchestrator's Sub-Agent Registry table.
4. Restart your AI agent session.
