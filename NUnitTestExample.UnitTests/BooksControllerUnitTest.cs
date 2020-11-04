using Microsoft.AspNetCore.Mvc;
using NUnitTestExample.Books.Controllers;
using NUnitTestExample.Books.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NUnitTestExample.UnitTests
{
    public class BooksControllerUnitTest
    {
        //Arrange
        BooksController controller;
        public BooksControllerUnitTest()
        {
            controller = new BooksController();
        }

        [Fact]
        public async Task GetBooks_OK()
        {

            //Act
            var result = await controller.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        /*
        [Fact]
        public async Task GetBooks_NotFound()
        {
            //Act
            var result = await controller.Get();

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        */

        [Fact]
        public async Task GetBookById_OK()
        {
            int knownId = 3;
            //Act
            var result = await controller.Get(knownId); //Known Id

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetBookById_NotFound()
        {
            //Act
            var result = await controller.Get(-1); //Not exist Id

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddValidBook_OK()
        {
            //Arrange
            Book book = new Book()
            {
                Name = "NewBook",
                Author = "NewAuthor",
                Price = 200
            };

            //Act
            var response = await controller.Post(book);

            //Assert
            Assert.IsType<OkObjectResult>(response.Result);

        }

        /*
        [Fact]
        public async Task AddValidBook_OK_ReturnedResponseHasCreatedItem()
        {
            //Arrange
            Book book = new Book()
            {
                Name = "NewBook",
                Author = "NewAuthor",
                Price = 200
            };
            //Act
            var response = await controller.Post(book);
            var postedBook = response.Value as Book;

            //Assert
            Assert.IsType<Book>(postedBook);
            Assert.Equal("NewBook", postedBook.Name);
        }
        */

        [Fact]
        public async Task AddInvalidBook_BadRequest()
        {
            //Arrange
            Book book = new Book()
            {
                Author = "NewAuthor",
                Price = 200
            };
            controller.ModelState.AddModelError("Name", "Required");

            //Act
            var response = await controller.Post(book);

            //Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task Remove_NotExistingBook_ReturnedNotFound()
        {
            //Arrange
            int notExistingId = -1;

            //Act
            var response = await controller.Delete(notExistingId);

            //Assert
            Assert.IsType<NotFoundResult>(response);

        }

        [Fact]
        public async Task Remove_ExistingBook_ReturnedOK()
        {
            //Arrange
            int existingId = 1;

            //Act
            var response = await controller.Delete(existingId);

            //Assert
            Assert.IsType<OkObjectResult>(response);
        }
    }


        
}
