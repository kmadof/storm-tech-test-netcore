using System.Collections.Generic;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }
        public ICollection<string> SortOrders { get; }

        public TodoListDetailViewmodel(int todoListId, string title, ICollection<TodoItemSummaryViewmodel> items, ICollection<string> availableSortOrders)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            SortOrders = availableSortOrders;
        }
    }
}