namespace WuxiaWorld.BLL.ActionFilters {

    using System;

    using DAL.Entities;

    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.EntityFrameworkCore;


    public class DbContextActionFilter : IActionFilter {

        private readonly WuxiaWorldDbContext _dbContext;

        public DbContextActionFilter(WuxiaWorldDbContext dbContext) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void OnActionExecuting(ActionExecutingContext context) {

            _dbContext.Database.OpenConnectionAsync().ConfigureAwait(false).GetAwaiter();
        }

        public void OnActionExecuted(ActionExecutedContext context) {

            _dbContext.Database.CloseConnectionAsync().ConfigureAwait(false).GetAwaiter();

            _dbContext.DisposeAsync().ConfigureAwait(false).GetAwaiter();
        }
    }

}