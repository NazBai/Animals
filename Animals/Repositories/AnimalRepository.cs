using Animals.Interfaces;
using Animals.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Animals.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {

        private IConfiguration _configuration;

        public AnimalRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ICollection<Animal> GetAnimalsSorted(string sorting)
        {
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            con.Open();

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY " + sorting;

            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();
            while (dr.Read())
            {
                var animal = new Animal
                {
                    IdAnimal = (int)dr["IdAnimal"],
                    name = dr["Name"].ToString(),
                    description = dr["Description"].ToString(),
                    category = dr["Category"].ToString(),
                    area = dr["Area"].ToString()
                };
                animals.Add(animal);
            }

            return animals;
        }

   
        public ICollection<Animal> GetAnimals()
        {
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            con.Open();

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY Name";

            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();
            while (dr.Read())
            {
                var animal = new Animal
                {
                    IdAnimal = (int)dr["IdAnimal"],
                    name = dr["Name"].ToString(),
                    description = dr["Description"].ToString(),
                    category = dr["Category"].ToString(),
                    area = dr["Area"].ToString()
                };
                animals.Add(animal);
            }

            return animals;
        }

        public int CreateAnimal(Animal animal)
        {
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            con.Open();

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Animal(IdAnimal, Name, Description, Category, Area) VALUES(@IdAnimal, @Name, @Description, @Category, @Area)";
            cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
            cmd.Parameters.AddWithValue("@Name", animal.name);
            cmd.Parameters.AddWithValue("@Description", animal.description);
            cmd.Parameters.AddWithValue("@Category", animal.category);
            cmd.Parameters.AddWithValue("@Area", animal.area);

            var affectedCount = cmd.ExecuteNonQuery();
            return affectedCount;
        }

        public int UpdateAnimal(Animal animal)
        {
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            con.Open();

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Animal SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @IdAnimal";
            cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
            cmd.Parameters.AddWithValue("@Name", animal.name);
            cmd.Parameters.AddWithValue("@Description", animal.description);
            cmd.Parameters.AddWithValue("@Category", animal.category);
            cmd.Parameters.AddWithValue("@Area", animal.area);

            var affectedCount = cmd.ExecuteNonQuery();
            return affectedCount;
        }
        public int DeleteAnimal(int idToDelete)
        {
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            con.Open();

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
            cmd.Parameters.AddWithValue("@IdAnimal", idToDelete);

            var affectedCount = cmd.ExecuteNonQuery();
            return affectedCount;
        }

        public bool IsExist(int animalId)
        {
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            con.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT TOP 1 * FROM Animal WHERE IdAnimal = @IdAnimal";
            cmd.Parameters.AddWithValue("@IdAnimal", animalId);

            SqlDataReader reader = cmd.ExecuteReader();

            return reader.HasRows;
        }
    }
}
