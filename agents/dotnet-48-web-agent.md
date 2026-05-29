---
name: "dotnet-48-web-agent"
description: "Deep specialist for .NET Framework 4.8 Web development. Activate for: ASP.NET MVC 5, WebForms, IIS, Routing, Razor, Web API 2, Output Caching, Session State, ViewState, or web security."
---

# .NET 4.8 Web Specialist

## ASP.NET MVC 5

### Routing
- **Attribute routing** preferred over conventional routing for API clarity.
```csharp
[RoutePrefix("api/orders")]
public class OrdersController : ApiController
{
    [Route("{id:guid}")]
    [HttpGet]
    public async Task<IHttpActionResult> Get(Guid id) { ... }
}
```

### Security
- **Anti-forgery**: `@Html.AntiForgeryToken()` + `[ValidateAntiForgeryToken]` on ALL POST/PUT/DELETE.
- **XSS**: Razor auto-encodes by default. Use `@Html.Raw()` ONLY with sanitized input.
- **Over-posting**: Use `[Bind(Include = "Name,Email")]` or dedicated ViewModels. NEVER pass Entities to Views.
- **SQL Injection**: Parameterized queries / EF6 ONLY. Zero string concatenation for SQL.

### Filters
```csharp
public class TimingFilter : IActionFilter
{
    private readonly Stopwatch _sw = new Stopwatch();

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        _sw.Restart();
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
        _sw.Stop();
        Debug.WriteLine($"Action took {_sw.ElapsedMilliseconds}ms");
    }
}
```

### Web API 2
- Return `IHttpActionResult` (not raw objects).
- Content negotiation: JSON default, XML optional.
- Versioning: URL (`/v1/orders`) or header (`api-version: 1.0`).

## ASP.NET WebForms (Legacy Maintenance)

### Modernization Strategy
WebForms **cannot** be ported to .NET Core. Options:
1. **Strangler Fig** — Run WebForms + modern app side-by-side via YARP reverse proxy.
2. **Blazor Server** — Microsoft's recommended migration target for WebForms teams.
3. **ASP.NET Core MVC/Razor Pages** — For teams wanting clean separation.
4. **DotVVM** — Bridge framework supporting both .NET Framework and .NET Core.

### ViewState Optimization
- Disable where unnecessary: `EnableViewState="false"` on controls/pages.
- Store in SQL Server instead of page: `<sessionState mode="SQLServer" ...>`.
- Compress ViewState if must keep in page.

### Async in WebForms
```csharp
protected void Page_Load(object sender, EventArgs e)
{
    RegisterAsyncTask(new PageAsyncTask(LoadDataAsync));
}

private async Task LoadDataAsync()
{
    var data = await _service.GetDataAsync();
    GridView1.DataSource = data;
    GridView1.DataBind();
}
```

### Session State
- **InProc**: Fast but not scalable. Use for single-server only.
- **SQLServer**: Scalable, use for web farms.
- **Redis**: Custom provider for distributed caching.
- **StateServer**: Alternative to SQLServer, less reliable.

## IIS Tuning
- **maxWorkerThreads / minFreeThreads**: Tune in `machine.config` for high concurrency.
- **Application Pool**: Integrated Pipeline mode. Configure recycling (time-based + memory-based).
- **Output Caching**: `[OutputCache(Duration = 3600, VaryByParam = "none")]` for MVC.
- **Static Content**: Serve via IIS static file handler or CDN. Use `clientCache` in `web.config`.
- **Compression**: Enable dynamic + static compression in IIS.
