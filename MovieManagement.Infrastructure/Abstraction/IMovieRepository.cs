using MovieManagement.Domain.Movie;
using MovieManagement.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Infrastructure.Abstraction
{
    public interface  IMovieRepository
    {
        Task<Movie> GetById(int id);
        Task<IEnumerable<Movie>> GetAll();
        Task<int> Add(MovieDto movie);
        Task<int> Update(MovieDto movie, int Id);
        Task<int> Delete(int id);
    }
}
