using MovieService.Domain;
using Shared.Repository;

namespace MovieService.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    
}