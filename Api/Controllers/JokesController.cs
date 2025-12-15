using Api;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {

        static List<JokeChuckNorris> jokes = new List<JokeChuckNorris>();

        public JokesController() { 
        if (jokes == null)
            {
                jokes = new List<JokeChuckNorris>
                {
                    new JokeChuckNorris { id = "vxcvbbsdbdf", value = "Why did the chicken cross the road? To get to the other side!" },
                    new JokeChuckNorris { id = "iibilkzovkv", value = "I told my computer I needed a break, and now it won't stop sending me Kit-Kat ads." },
                    new JokeChuckNorris { id = "pppscjvjeje", value = "Why do programmers prefer dark mode? Because light attracts bugs!" }
                };
            }
        }



        // GET: api/<ValuesController>
        [HttpGet]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            try 
            {
                var client = new HttpClient();
                var json = client.GetStringAsync("https://api.chucknorris.io/jokes/random").Result;

                var remoteJoke = JsonSerializer.Deserialize<Joke>(json);

                Joke joke = new Joke
                {
                    Id = remoteJoke.Id,
                    Description = remoteJoke.Description
                };

                return Ok(joke);
            }
            catch (Exception)
            {
                // En cas d'erreur, retournez un message d'erreur 500
                return StatusCode(500, "Erreur");
            }
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById_IActionResult(string id)
        {
            //var jokes = JokesController.jokes.FirstOrDefault(j => j.id == id);

            //return jokes == null ? NotFound() : Ok(jokes);

            return Ok();
        }

        // POST api/<ValuesController>
        [HttpPost]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post([FromBody] string description)
        {
            // Etape 1 : vérifier le paramètre

            // Etape 2 : stocker la blague appelée depuis l'API externe via la méthode get dans la base de données
            



            return Ok(joke);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Put(int id, [FromBody] string description)
        {
            // modifier la description
            //Joke joke = jokes.FirstOrDefault(c => c.id == id);
            //if (joke != null)
            //{
            //    joke.value = description;
            //}
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType<Category>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Delete(int id)
        {
        }
    }
}
