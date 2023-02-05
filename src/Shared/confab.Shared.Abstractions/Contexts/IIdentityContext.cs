namespace Confab.Shared.Abstractions.Contexts
{
    public interface IIdentityContext
    {
         Guid Id { get; }
         string Role { get; }
         Dictionary<string, IEnumerable<string>> Claims { get;}
        bool IsAuthenticated { get; }
    }
}