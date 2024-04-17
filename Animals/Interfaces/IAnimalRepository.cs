using Animals.Models;

namespace Animals.Interfaces
{
    public interface IAnimalRepository
    {
        ICollection<Animal> GetAnimals();
        ICollection<Animal> GetAnimalsSorted(string orderBy);
        int CreateAnimal(Animal animal);
        int UpdateAnimal(Animal animal);
        int DeleteAnimal(int idToDelete);
        bool IsExist(int animalId);

    }
}
