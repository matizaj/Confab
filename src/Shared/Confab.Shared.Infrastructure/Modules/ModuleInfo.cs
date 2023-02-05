namespace Confab.Shared.Infrastructure.Modules
{
    public record ModuleInfo(string name, string path, IEnumerable<string> policies);
}