using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MovieManagement.Domain.Movie;
using MovieManagement.Infrastructure.Abstraction;
using MovieManagement.Infrastructure.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
#pragma warning disable

namespace MovieManagement.Infrastructure.Implementation
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string conncetionString;
        private readonly IConfiguration _con;
        public MovieRepository(IConfiguration con)
        {
            _con = con;
            conncetionString = _con["ConnectionStrings:dbConnection"].ToString();
        }

        public async Task<int> Add(MovieDto movie)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Insert Into [dbo].[Movies] (Name,Description,ReleaseDate,Rating,TicketPrice,Country,DateCreated,Genres)
                                     Values(@Name,@Description,@ReleaseDate,@Rating,@TicketPrice,@Country,@DateCreated,@Genres)";



                var parameters = new DynamicParameters();

                parameters.Add("Name", movie.Name);
                parameters.Add("Description", movie.Description);
                parameters.Add("ReleaseDate", movie.ReleaseDate);
                parameters.Add("Rating", movie.Rating);
                parameters.Add("TicketPrice", movie.TicketPrice);
                parameters.Add("Country", movie.Country);                
                parameters.Add("DateCreated", DateTime.Now);
                parameters.Add("Genres", movie.Genres);

                var result = await sqlConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);
                return result;
            }
            catch (Exception ex)
            {

                Log.Error($"Error at Add: {ex}");
                return -1;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Delete From [dbo].[Movies]";

                var parameters = new DynamicParameters();

                var result = await sqlConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {

                Log.Error($"Error at Delete: {ex}");
                return -1;
            }
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Select * From [dbo].[Movies]";

                var parameters = new DynamicParameters();
                
                var result = await sqlConnection.QueryAsync<Movie>(query, parameters, commandType: CommandType.Text);

                return result.ToList();
            }
            catch (Exception ex)
            {

                Log.Error($"Error at GetById: {ex}");
                return null;
            }
        }

        public async Task<Movie> GetById(int id)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Select * From [dbo].[Movies]  Where Id = @Id";

                var parameters = new DynamicParameters();
                parameters.Add("Id", id);
                var result = await sqlConnection.QueryFirstOrDefaultAsync<Movie>(query, parameters, commandType: CommandType.Text);

                return result;
            }
            catch (Exception ex)
            {

                Log.Error($"Error at GetById: {ex}");
                return null;
            }
        }

        public async Task<int> Update(MovieDto movie, int Id)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(conncetionString);

                var query = @"Update [dbo].[Movies] Set Name = @Name,Description=@Description,ReleaseDate=@ReleaseDate,Rating=@Rating,
                          TicketPrice=@TicketPrice,Country =@Country,Genres =@Genres,ModifiedDate=@ModifiedDate Where Id = @Id";


                var parameters = new DynamicParameters();
                parameters.Add("Id", Id);
                parameters.Add("Name", movie.Name);
                parameters.Add("Description", movie.Description);
                parameters.Add("ReleaseDate", movie.ReleaseDate);
                parameters.Add("Rating", movie.Rating);
                parameters.Add("TicketPrice", movie.TicketPrice);
                parameters.Add("Country", movie.Country);
                parameters.Add("Genres", movie.Genres);
                parameters.Add("ModifiedDate",DateTime.Now);

                var result = await sqlConnection.ExecuteAsync(query, parameters, commandType: CommandType.Text);
                return result;
            }
            catch (Exception ex)
            {

                Log.Error($"Error at Update: {ex}");
                return -1;
            }
            
        }


       
    }
}
