using Microsoft.Extensions.Options;
using MovieManagement.Application.Abstraction;
using MovieManagement.Application.Dtos;
using MovieManagement.Domain.Movie;
using MovieManagement.Infrastructure.Abstraction;
using MovieManagement.Infrastructure.Dtos;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Application.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ServiceResponseSettings _serviceResponseSettings;

        public MovieService(IMovieRepository movieRepository, IOptions<ServiceResponseSettings> serviceResponseSettings)
        {
            _movieRepository = movieRepository;
            _serviceResponseSettings = serviceResponseSettings.Value;
        }

        public async Task<ServiceResponse> Add(MovieDto movie)
        {
            var result = await _movieRepository.Add(movie);
            if (result == -1)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage };
            }
            if (result == 0)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.FailureCode, ResponseMessage = _serviceResponseSettings.FailureMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            Log.Information("About to delete movie by Id");
            var result = await _movieRepository.Delete(id);
            Log.Information($"response from delete: {JsonConvert.SerializeObject(result)}");
            if (result == -1)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage };
            }
            else if(result == 0)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.FailureCode, ResponseMessage = _serviceResponseSettings.FailureMessage };
            }
        }

        public async Task<ServiceResponse> GetAll()
        {
            Log.Information("About to retrieve all movies");
            var result = await _movieRepository.GetAll();
            Log.Information($"response from getall movies: {JsonConvert.SerializeObject(result)}");
            if (result == null)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage };
            }
            if (result.Count() == 0)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.NotFoundCode, ResponseMessage = _serviceResponseSettings.NotFoundMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseData = result, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> GetById(int id)
        {
            Log.Information("About to retrieve movie by Id");
            var result = await _movieRepository.GetById(id);
            Log.Information($"response from GetById: {JsonConvert.SerializeObject(result)}");
            if (result == null)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.NotFoundCode, ResponseMessage = _serviceResponseSettings.NotFoundMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }

        public async Task<ServiceResponse> Update(MovieDto movie, int Id)
        {
            var result = await _movieRepository.Update(movie, Id);
            if (result == -1)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.ErrorOccuredCode, ResponseMessage = _serviceResponseSettings.ErrorOccuredMessage };
            }
            if (result == 0)
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.FailureCode, ResponseMessage = _serviceResponseSettings.FailureMessage };
            }
            else
            {
                return new ServiceResponse() { ResponseCode = _serviceResponseSettings.SuccessCode, ResponseMessage = _serviceResponseSettings.SuccessMessage };
            }
        }
    }
}
