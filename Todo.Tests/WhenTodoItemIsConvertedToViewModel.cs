using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models;
using Todo.Models.TodoItems;
using Xunit;

namespace Todo.Tests
{
    public class WhenTodoItemIsConvertedToViewModel
    {
        private readonly static TodoList todoList;

        static WhenTodoItemIsConvertedToViewModel()
        {
            todoList = new TestTodoListBuilder(new IdentityUser("alice@example.com"), "shopping")
                    .WithItem("bread", Importance.High, 7)
                    .WithItem("Irn-Bru", Importance.Medium, 11)
                    .Build();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(SortOrder.Importance)]
        [InlineData(SortOrder.Rank)]
        public void ShouldNotHaveAlreadySelectedSortOrder(string sortOrder)
        {
            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList, sortOrder);
            Assert.All(viewmodel.SortOrders, so => Assert.True(so != sortOrder));
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ShouldHaveItemsSorted(string sortOrder, ICollection<TodoItemSummaryViewmodel> sortedItems)
        {
            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList, sortOrder);
            Assert.Equal(sortedItems.First().Title, viewmodel.Items.First().Title);
        }

        public static IEnumerable<object[]> Data()
        {
            var breadItem = todoList.Items.Single(item => item.Title == "bread");
            var bread = TodoItemSummaryViewmodelFactory.Create(breadItem);

            var irnbruItem = todoList.Items.Single(item => item.Title == "Irn-Bru");
            var irnBru = TodoItemSummaryViewmodelFactory.Create(irnbruItem);

            var sortedByImportance = new List<TodoItemSummaryViewmodel> { bread, irnBru };
            var sortedByRank = new List<TodoItemSummaryViewmodel> { irnBru, bread };

            return new List<object[]>
            {
                    new object[] { null, sortedByImportance },
                    new object[] { SortOrder.Importance, sortedByImportance },
                    new object[] { SortOrder.Rank, sortedByRank },
            };
        }
    }
}
