# Legacy Modernization Audit Prompt for .NET 4.8

Analyze this .NET Framework 4.8 codebase and provide a modernization roadmap:

1. **Architecture Assessment**: Is business logic coupled to System.Web/System.Windows.Forms?
2. **Technology Inventory**: List all legacy technologies (WebForms, WCF, EF6 EDMX, ASMX, Remoting).
3. **Extraction Feasibility**: What can be moved to .NET Standard 2.0 immediately?
4. **Migration Priority Matrix**: Rank by effort vs value (Quick Wins, Medium, Long-term).
5. **Risk Assessment**: What breaks if we migrate X? Rollback strategy?
6. **Testing Strategy**: Characterization tests needed? Coverage gaps?
7. **Target Architecture**: Propose final state (.NET 10 + Blazor + gRPC + EF Core).
8. **Phase Plan**: 4-week increments with deliverables and validation criteria.

Output as structured markdown with tables and diagrams.
