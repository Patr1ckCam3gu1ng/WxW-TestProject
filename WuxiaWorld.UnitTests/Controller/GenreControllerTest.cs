﻿namespace WuxiaWorld.UnitTests.Controller {

    using System.Collections.Generic;

    using BLL.Services.Interfaces;

    using Controllers;

    using DAL.Models;

    using Microsoft.AspNetCore.Mvc;

    using Moq;

    using Xunit;

    public class GenreControllerTest {

        public GenreControllerTest() {

            _mockGenreService = new Mock<IGenreService>();
            _mockNovelService = new Mock<INovelService>();
            _controller = new GenreController(_mockGenreService.Object, _mockNovelService.Object);
        }

        private readonly Mock<IGenreService> _mockGenreService; 
        private readonly Mock<INovelService> _mockNovelService;
        private readonly GenreController _controller;

        // private TaskAwaiter<IActionResult> ControllerGetByName() {
        //
        //     var controllerGetByName = _controller.Get(It.IsAny<string>()).GetAwaiter();
        //     return controllerGetByName;
        // }

        [Fact]
        public void GetAll_AssertReturningOK() {
            var controllerGet = _controller.GetAll().GetAwaiter();

            if (controllerGet.IsCompleted) {

                var result = controllerGet.GetResult();

                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public void GetAll_AssertReturningResult() {

            _mockGenreService.Setup(repo => repo.GetAll())
                .ReturnsAsync(() => new List<IdNameModel> {
                    new IdNameModel(),
                    new IdNameModel()
                });

            var controllerGet = _controller.GetAll().GetAwaiter();

            if (controllerGet.IsCompleted) {

                var result = controllerGet.GetResult();

                var viewResult = Assert.IsType<OkObjectResult>(result);
                var genre = Assert.IsType<List<IdNameModel>>(viewResult.Value);
                Assert.Equal(2, genre.Count);
            }
        }

        // [Fact]
        // public void GetByName_AssertReturningOK() {
        //
        //     var controllerGetByName = ControllerGetByName();
        //
        //     if (controllerGetByName.IsCompleted) {
        //
        //         var result = controllerGetByName.GetResult();
        //
        //         Assert.IsType<OkObjectResult>(result);
        //     }
        // }

        // [Fact]
        // public void GetByName_AssertReturningResult() {
        //
        //     _mockGenreService.Setup(repo => repo.GetByName(It.IsAny<string>()))
        //         .ReturnsAsync(() => new Genres());
        //
        //     var controllerGetByName = ControllerGetByName();
        //
        //     if (controllerGetByName.IsCompleted) {
        //
        //         var result = controllerGetByName.GetResult();
        //
        //         var viewResult = Assert.IsType<OkObjectResult>(result);
        //         var genre = Assert.IsType<Genres>(viewResult.Value);
        //         Assert.Equal(0, genre.Id);
        //     }
        // }
    }

}