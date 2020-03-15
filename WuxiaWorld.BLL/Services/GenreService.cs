namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    using Exceptions;

    using Repositories;

    public class GenreService : IGenreService {

        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository) {

            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
        }

        public async Task<List<Genres>> GetAll() {

            var result = await _genreRepository.GetAll();

            if (result == null) {
                throw new NoRecordFoundException(string.Empty);
            }

            return result;
        }

        public async Task<Genres> GetByName(string genreName) {

            var result = await _genreRepository.GetByName(genreName);

            if (result == null) {
                throw new NoRecordFoundException(string.Empty);
            }

            return result;
        }

        public async Task<Genres> Create(GenreModel genre) {

            try {

                var newGenre = await _genreRepository.Create(genre);

                if (newGenre == null) {
                    throw new FailedCreatingNewException("Failed creating new genre");
                }

                return newGenre;
            }
            catch (Exception exception) {

                var innerExceptionMessage = exception.InnerException?.Message;

                if (!string.IsNullOrEmpty(innerExceptionMessage)) {

                    if (innerExceptionMessage.Contains("duplicate key value")) {
                        throw new GenreAlreadyExistsException("Genre already exists");
                    }
                }

                throw new Exception(ExceptionError.Message);
            }
        }
    }

}