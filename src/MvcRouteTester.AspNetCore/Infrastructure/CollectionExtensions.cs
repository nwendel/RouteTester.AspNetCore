namespace MvcRouteTester.AspNetCore.Infrastructure;

internal static class CollectionExtensions
{
    public static void RemoveWhere<TItem>(this ICollection<TItem> self, Func<TItem, bool> predicate)
    {
        GuardAgainst.Null(self);
        GuardAgainst.Null(predicate);

        var remove = self.Where(predicate).ToList();
        self.RemoveRange(remove);
    }

    public static void RemoveRange<TItem>(this ICollection<TItem> self, IEnumerable<TItem> items)
    {
        GuardAgainst.Null(self);
        GuardAgainst.Null(items);

        foreach (var item in items)
        {
            self.Remove(item);
        }
    }
}
