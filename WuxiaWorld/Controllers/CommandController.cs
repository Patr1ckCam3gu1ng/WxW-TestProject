namespace WuxiaWorld.Controllers {

    using System;

    using BLL.ActionFilters;
    using BLL.Services.Interfaces;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    [Route("api/commands")]
    [TypeFilter(typeof(DbContextActionFilter))]
    public class CommandController : Controller {

        private readonly ICommandService _commandService;

        public CommandController(ICommandService commandService) {

            _commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));
        }

        [HttpGet]
        public IActionResult Get() {

            var commandLists = _commandService.Get();

            return Ok(commandLists);
        }
    }

}