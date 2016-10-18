using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Pet.api.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        private static List<Note> _notes;
        static NoteController()
        {
            _notes = new List<Note>();
        }


        [HttpGet]
        public IEnumerable<Note> GetAll()
        {
            return _notes.AsReadOnly();
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult GetById(string id)
        {
            var item = _notes.Find(n => n.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Note item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            item.Id = (_notes.Count + 1).ToString();
            _notes.Add(item);
            return CreatedAtRoute("GetNote", new { controller = "Note", id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _notes.RemoveAll(n => n.Id == id);
        }

        public class Note
        {
            public string Id { get; set; }
            public string Content { get; set; }
        }
    }
}