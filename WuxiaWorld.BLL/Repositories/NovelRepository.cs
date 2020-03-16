namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Threading.Tasks;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class NovelRepository : INovelRepository {

        private readonly WuxiaWorldDbContext _dbContext;
        private readonly IMapper _mapper;

        public NovelRepository(WuxiaWorldDbContext dbContext, IMapper mapper) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(WuxiaWorldDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(WuxiaWorldDbContext));
        }

        public async Task<Novels> Create(NovelModel input) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync();

                var newNovel = _mapper.Map<Novels>(input);

                newNovel.TimeCreated = DateTime.UtcNow;

                await _dbContext.AddAsync(newNovel);

                var result = await _dbContext.SaveChangesAsync();

                return result == 1 ? newNovel : null;
            }
        }
    }

}