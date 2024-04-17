using Animals.Interfaces;
using Animals.Models;
using Microsoft.AspNetCore.Mvc;

namespace Animals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnimalController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Animal>))]
        public IActionResult GetAnimals()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var animals = _animalRepository.GetAnimals();

            return Ok(animals);
        }

        [HttpGet("{orderBy}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Animal>))]
        public IActionResult GetAnimals(string orderBy)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (orderBy.Equals("IdAnimal") || orderBy.Equals("Name") || orderBy.Equals("Description") || orderBy.Equals("Category") || orderBy.Equals("Area"))
            {

                var animals = _animalRepository.GetAnimalsSorted(orderBy);
                return Ok(animals);

            }

            return BadRequest("Incorect column name");
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAnimal([FromBody]Animal animal)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            if (_animalRepository.CreateAnimal(animal) <= 0)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfuly created");

        }

        [HttpPut("{animalId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAnimal(int animalId,[FromBody] Animal updatedAnimal)
        {
            if (updatedAnimal == null)
                return BadRequest(ModelState);

            if (animalId != updatedAnimal.IdAnimal)
                return BadRequest(ModelState);

            if (!_animalRepository.IsExist(animalId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_animalRepository.UpdateAnimal(updatedAnimal) <= 0)
            {
                ModelState.AddModelError("", "Something went wrong updating animal");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{animalId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteItem(int animalId)
        {
            if (!_animalRepository.IsExist(animalId))
                return NotFound();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_animalRepository.DeleteAnimal(animalId) <= 0)
            {
                ModelState.AddModelError("", "Something went wrong deleting item");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }




    }
}
