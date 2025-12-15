using Api;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json;
 
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
 
namespace API.APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        //Créer le constructeur 
        static List<Category> categories;


        public CategoriesController()
        {
            if (categories == null)
            {
                categories = new List<Category>
                {
                    new Category { Id = 1, Description = "animal" },
                    new Category { Id = 2, Description = "dev" },
                    new Category { Id = 3, Description = "food" }
                };
            }
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategories()
        {
            // Appel API Chuck Norris 
            var client = new HttpClient();
            var json = client.GetStringAsync("https://api.chucknorris.io/jokes/categories").Result;

            // Désérialisation du json de l'api
            var remoteNames = JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            var categories = new List<Category>();

            // Ajout dans la nouvelle liste
            foreach (var item in remoteNames)
            {
                categories.Add(new Category
                {
                    Description = item.ToString()
                });
            }
            return Ok(categories);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById_IActionResult(int id)
        {
            var category = categories.Find(c => c.Id == id);
            return category == null ? NotFound() : Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Post([FromBody] string description)
        {
            // Etape 1 : vérifier le paramètre

            // Etape 2 : créer la nouvelle catégorie
            Category category = new Category
            {
                Id = categories.Last().Id + 1,
                Description = description
            };

            // Etape 3 : stocker la nouvelle catégorie (ajouter à la liste)
            categories.Add(category);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Put(int id, [FromBody] string description)
        {
            // modifier la description
            Category category = categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                category.Description = description;
            }
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Delete(int id)
        {
            // supprimer la valeur
            Category category = categories.FirstOrDefault(c => c.Id == id);
            categories.Remove(category);
        }
    }
}