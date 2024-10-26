using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::ToDoApp.Application.Services;
    using global::ToDoApp.Domain.Entities;
    using global::ToDoApp.Domain.Interfaces;
    using Moq;
    using NUnit.Framework;
    

    namespace ToDoApp.Tests
    {
        [TestFixture]
        public class ToDoServiceTests
        {
            private Mock<IToDoRepository> _mockRepository;
            private ToDoService _service;

            [SetUp]
            public void Setup()
            {
                _mockRepository = new Mock<IToDoRepository>();
                _service = new ToDoService(_mockRepository.Object);
            }

            [Test]
            public async Task GetAllToDosAsync_ShouldReturnToDoList()
            {
                var todos = new List<ToDo>
            {
                new ToDo { Id = 1, Name = "Test Task 1", CreatedAt = DateTime.Now, Status = false },
                new ToDo { Id = 2, Name = "Test Task 2", CreatedAt = DateTime.Now, Status = false }
            };
                _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(todos);

                var result = await _service.GetAllToDosAsync();

                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count());
            }

            [Test]
            public async Task AddToDoAsync_ShouldAddNewToDo()
            {
                var todoName = "New Task";
                _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<ToDo>())).Returns(Task.CompletedTask);

                await _service.AddToDoAsync(todoName);

                _mockRepository.Verify(repo => repo.AddAsync(It.Is<ToDo>(t => t.Name == todoName)), Times.Once);
            }

            [Test]
            public async Task MarkToDoAsCompleteAsync_ShouldMarkTaskAsComplete()
            {
                var todo = new ToDo { Id = 1, Name = "Test Task", CreatedAt = DateTime.Now, Status = false };
                _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(todo);
                _mockRepository.Setup(repo => repo.UpdateAsync(todo)).Returns(Task.CompletedTask);

                await _service.MarkToDoAsCompleteAsync(1);

                Assert.IsTrue(todo.Status); // Assuming MarkAsComplete sets Status to true
                _mockRepository.Verify(repo => repo.UpdateAsync(todo), Times.Once);
            }

            [Test]
            public async Task DeleteToDoAsync_ShouldCallDeleteOnRepository()
            {
                var todoId = 1;

                await _service.DeleteToDoAsync(todoId);

                _mockRepository.Verify(repo => repo.DeleteAsync(todoId), Times.Once);
            }
        }
    }

}
