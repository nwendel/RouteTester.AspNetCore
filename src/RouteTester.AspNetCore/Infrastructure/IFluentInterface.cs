using System.ComponentModel;

namespace RouteTester.AspNetCore.Infrastructure;

[EditorBrowsable(EditorBrowsableState.Never)]
public interface IFluentInterface
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("Design", "CA1024:Use properties where appropriate", Justification = "Name must match System.Object.GetType")]
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Name must match System.Object.GetType")]
    Type GetType();

    [EditorBrowsable(EditorBrowsableState.Never)]
    int GetHashCode();

    [EditorBrowsable(EditorBrowsableState.Never)]
    string? ToString();

    [EditorBrowsable(EditorBrowsableState.Never)]
    bool Equals(object obj);
}
