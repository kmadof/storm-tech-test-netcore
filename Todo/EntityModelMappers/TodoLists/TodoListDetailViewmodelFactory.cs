using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, string sortOrder = null)
        {
            var items = todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create).ToList();

            if (sortOrder != null && sortOrder.Equals(SortOrder.Rank, System.StringComparison.InvariantCultureIgnoreCase))
            {
                items = items.OrderByDescending(item => item.Rank).ToList();
            }
            else
            {
                items = items.OrderBy(item => item.Importance).ToList();
            }

            var availableSortOrders = GetAvailableSortOrders(sortOrder);

            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items, availableSortOrders);
        }

        private static ICollection<string> GetAvailableSortOrders(string sortOrder)
        {
            var allSortOrders = new List<string>
            {
                SortOrder.Importance,
                SortOrder.Rank
            };

            if(string.IsNullOrEmpty(sortOrder))
            {
                allSortOrders.Remove(SortOrder.Importance);
            }
            else
            {
                allSortOrders = allSortOrders.Where(so => so.ToLower() != sortOrder.ToLower()).ToList(); ;
            }

            return allSortOrders;
        }
    }
}