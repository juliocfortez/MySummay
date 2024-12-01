using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyOwnSummary_API.Models;
using MyOwnSummary_API.Models.Dtos.CategoryDtos;
using MyOwnSummary_API.Models.Dtos.DictionaryDtos;
using MyOwnSummary_API.Models.Dtos.LanguageDtos;
using MyOwnSummary_API.Models.Dtos.NoteDtos;
using MyOwnSummary_API.Repositories.IRepository;
using System.Net;
using System.Security.Claims;

namespace MyOwnSummary_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<NoteController> _logger;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;
        public NoteController(INoteRepository noteRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, ILogger<NoteController> logger, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _noteRepository = noteRepository;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _apiResponse = new();
        }
        [HttpGet("{id:int}", Name = "GetNote")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("El id por parametro no puede ser 0", id);
                    _apiResponse.Errors.Add("El id no puede ser 0");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var note = await _noteRepository.Get(x => x.Id == id);
                if (note == null)
                {
                    _logger.LogError($"La nota con id {id} no existe", id);
                    _apiResponse.Errors.Add($"La nota con id {id} no existe");
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                _apiResponse.Result = _mapper.Map<NoteDto>(note);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.IsSuccess = false;
            }
            return _apiResponse;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                _apiResponse.Result = _mapper.Map<IEnumerable<NoteDto>>(await _noteRepository.GetAll());
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse = new();
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.IsSuccess = false;
                return _apiResponse;
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null && !identity.Claims.Any())
                {
                    _apiResponse.Errors.Add("Token invalido");
                    _apiResponse.StatusCode = HttpStatusCode.Unauthorized;
                    return Unauthorized(_apiResponse);
                }
                //var u = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                //if (u != "User1")
                //{
                //    _apiResponse.Errors.Add("No tienes permisos para eliminar");
                //    _apiResponse.StatusCode = HttpStatusCode.Unauthorized;
                //    return Unauthorized(_apiResponse);
                //}
                if (id == 0)
                {
                    _logger.LogError("El id por parametro no puede ser 0", id);
                    _apiResponse.Errors.Add("El id no puede ser 0");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var note = await _noteRepository.Get(x => x.Id == id);
                if (note == null)
                {
                    _logger.LogError($"La nota con id {id} no existe", id);
                    _apiResponse.Errors.Add($"La nota con id {id} no existe");
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                await _noteRepository.Remove(note);
                _apiResponse.StatusCode = HttpStatusCode.NoContent;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.IsSuccess = false;
            }
            return BadRequest(_apiResponse);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateNoteDto createNote)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var item in ModelState.Values)
                    {
                        foreach (var error in item.Errors)
                        {
                            _apiResponse.Errors.Add(error.ErrorMessage);
                        }
                    }
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var n = _mapper.Map<Note>(createNote);
                await _noteRepository.Create(n);
                _apiResponse.IsSuccess = true;
                _apiResponse.StatusCode = HttpStatusCode.Created;
                _apiResponse.Result = _mapper.Map<CategoryDto>(n);
                return CreatedAtRoute("GetNote", new { id = n.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.IsSuccess = false;
            }
            return _apiResponse;
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        public async Task<IActionResult> Update([FromBody] NoteDto note, int id)
        {
            //ESTE METODO NO ES IGUAL A LOS DEMAS PORQUE PUEDO CAMBIAR LA CATEGORÍA DE LA NOTA
            //throw new NotImplementedException();
            try
            {
                if (id != note.Id)
                {
                    _logger.LogError("El id por parametro no coincide con el id de la entidad a editar", id);
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.Errors.Add("El id por parametro no coincide con el id de la entidad a editar");
                    return BadRequest(_apiResponse);
                }
                if (!ModelState.IsValid)
                {
                    foreach (var item in ModelState.Values)
                    {
                        foreach (var error in item.Errors)
                        {
                            _apiResponse.Errors.Add(error.ErrorMessage);
                        }
                    }
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var n = await _noteRepository.Get(x => x.Id == id, false);
                if (n == null)
                {
                    _apiResponse.Errors.Add($"La nota con id {id} no existe");
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }

                n = _mapper.Map<Note>(note);
                await _noteRepository.Update(n);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                var noteReturn = _mapper.Map<NoteDto>(n);
                _apiResponse.Result = noteReturn;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.IsSuccess = false;
            }
            return BadRequest(_apiResponse);
        }

        [HttpGet("User",Name = "GetNotesByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> GetNotesByUserId()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                var userId = Convert.ToInt32(userIdClaim.Value);
                var notes = await _noteRepository.GetAll(x => x.UserId == userId);
                _apiResponse.Result = _mapper.Map<List<NoteDto>>(notes);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            else
            {
                _apiResponse.StatusCode = HttpStatusCode.Unauthorized;
                _apiResponse.IsSuccess = false;
                return Unauthorized(_apiResponse);
            }
        }
        [HttpGet("DataForViewCreateNote", Name = "GetDataForViewCreateNote")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> GetDataForViewCreateNote()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                ShowCreateNoteView createNoteView = new ShowCreateNoteView();
                var userId = Convert.ToInt32(userIdClaim.Value);
                var languagesByUser = await _userRepository.GetLanguagesByUser(userId);
                var categories = await _categoryRepository.GetAll();
                var languagesDto = _mapper.Map<List<LanguageDto>>(languagesByUser);
                var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
                createNoteView.UserId = userId;
                createNoteView.Languages = languagesDto;
                createNoteView.Categories = categoriesDto;
                _apiResponse.Result = createNoteView;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            else
            {
                _apiResponse.StatusCode = HttpStatusCode.Unauthorized;
                _apiResponse.IsSuccess = false;
                return Unauthorized(_apiResponse);
            }
        }

        [HttpGet("DataForViewNotes", Name = "GetDataForViewNotes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(APIResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(APIResponse))]
        public async Task<ActionResult<APIResponse>> GetDataForViewNotes()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                NoteViewDto NoteViews = new();
                var userId = Convert.ToInt32(userIdClaim.Value);
                var languagesByUser = await _userRepository.GetLanguagesByUser(userId);
                var categories = await _categoryRepository.GetAll();
                var dictionary = await _noteRepository.GetAll(x => x.UserId == userId);
                var languagesDto = _mapper.Map<List<LanguageDto>>(languagesByUser);
                var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
                var dictionaryDto = _mapper.Map<List<NoteDto>>(dictionary);
                NoteViews.UserId = userId;
                NoteViews.Languages = languagesDto;
                NoteViews.Categories = categoriesDto;
                NoteViews.Notes = dictionaryDto;
                _apiResponse.Result = NoteViews;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            else
            {
                _apiResponse.StatusCode = HttpStatusCode.Unauthorized;
                _apiResponse.IsSuccess = false;
                return Unauthorized(_apiResponse);
            }
        }
    }
}
