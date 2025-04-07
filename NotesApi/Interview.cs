using Microsoft.AspNetCore.Mvc;

namespace NotesApi
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class NoteRepository
    {
        List<Note> _notes = new();
        int _idCounter = 1;

        public List<Note> GetAll() { 
            return _notes;
        }
        public Note? GetById(int id)
        {
            try
            {
                return _notes.FirstOrDefault(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching note by Id", ex);
            }
        }
        public Note Create(Note note) {
            
            
            _idCounter++;
            note.CreatedAt = DateTime.UtcNow;
            _notes.Add(note);
            return note;

        }
        public Note? Update(int id, Note updatedNote) { 
            var note = GetById(id);

            note.Title = updatedNote.Title;

            note.Content = updatedNote.Content;

            return note;


        }
        public bool Delete(int id) {
            try
            {
                var note = GetById(id);

                
                if (note == null)
                    return false;

                
                return _notes.Remove(note);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting note", ex);
            }

        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteRepository _noteRepository;

        public NoteController(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet]
        public ActionResult<List<Note>> GetAllNotes()
        {
            var notes = _noteRepository.GetAll();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public ActionResult<Note> GetNoteById(int id)
        {
            var note = _noteRepository.GetById(id);
            if (note == null)
                return NotFound(new { message = "Note not found" });

            return Ok(note);
        }

        [HttpPost]
        public ActionResult<Note> CreateNote([FromBody] Note note)
        {
            var createdNote = _noteRepository.Create(note);
            return CreatedAtAction(nameof(GetNoteById), new { id = createdNote.Id }, createdNote);
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var success = _noteRepository.Delete(id);

            if (!success)
                return NotFound(new { message = "Note not found" });

            return NoContent(); 
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, [FromBody] Note note) { 

            _noteRepository.Update(id, note);

            return Ok(note); 
        }


    }

}
