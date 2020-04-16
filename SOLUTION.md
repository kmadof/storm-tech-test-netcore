1. Replace `<s>` tags with `<del>` as [the `<s>` tag should not be used to define replaced or deleted text, use the `<del>` tag to define replaced or deleted text.](https://www.w3schools.com/tags/tag_s.asp)

2. For task 6 I tried to split queries in two separated. However, I got an [error](https://github.com/dotnet/efcore/issues/9007) which was fixed in version 3.1. I trhink that two separate queries may perfrom better and for sure it makes code more readable.

```csharp
public static IQueryable<TodoList> RelevantTodoLists(this ApplicationDbContext dbContext, string userId)
{
    var ownLists = dbContext.TodoLists.Include(tl => tl.Owner)
        .Include(tl => tl.Items)
        .Where(tl => tl.Owner.Id == userId);

    var partiesLists = dbContext.TodoLists.Include(tl => tl.Owner)
        .Include(tl => tl.Items)
        .Where(tl => tl.Owner.Id != userId && tl.Items.Any(item => item.ResponsiblePartyId == userId));

    return ownLists.Union(partiesLists);
}
```

Since in task 6 is not written to block modification of list which user is only responsible party id I will skip that and assume that user may modify whole list.

I also changed here display name for responsible party id as I more understood what does actually mean.

3. For task 7 I skipped `Rank` in constructor as we always set it to 0, because we only set `Rank` in edit page.