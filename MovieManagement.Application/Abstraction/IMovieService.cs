using MovieManagement.Application.Dtos;
using MovieManagement.Domain.Movie;
using MovieManagement.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Application.Abstraction
{
    public interface IMovieService
    {
        Task<ServiceResponse> GetById(int id);
        Task<ServiceResponse> GetAll();
        Task<ServiceResponse> Add(MovieDto movie);
        Task<ServiceResponse> Update(MovieDto movie,int Id);
        Task<ServiceResponse> Delete(int id);
    }
}
