# Homeocentrum.Niga.OldAPI

ASP.NET Core 2.2 login / legacy API solution.

| Project | Role |
|---------|------|
| `Homeocentrum.Niga.OldAPI.sln` | Solution |
| `Homeocentrum.Niga.OldAPI` | Web host (port 5001) |
| `Homeocentrum.Niga.OldAPI.Business` | Business layer |
| `Homeocentrum.Niga.OldAPI.Common` | Shared helpers |
| `Homeocentrum.Niga.OldAPI.Entity` | EF entities |
| `Homeocentrum.Niga.OldAPI.Model` | DTOs / models |

```bash
dotnet build Homeocentrum.Niga.OldAPI.sln
dotnet run --project Homeocentrum.Niga.OldAPI/Homeocentrum.Niga.OldAPI.csproj --urls http://127.0.0.1:5001
```

Namespaces: `Homeocentrum.Niga.OldAPI*`. JWT issuer `Homeocentrum.Niga.OldAPI`, audience `Homeocentrum.Niga.Client` — users must sign in again after deploy.
