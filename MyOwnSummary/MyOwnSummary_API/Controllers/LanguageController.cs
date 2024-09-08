using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOwnSummary_API.Data;
using MyOwnSummary_API.Models;
using MyOwnSummary_API.Models.Dtos.CategoryDtos;
using MyOwnSummary_API.Models.Dtos.LanguageDtos;
using MyOwnSummary_API.Repositories;
using MyOwnSummary_API.Repositories.IRepository;
using System.Net;
using System.Security.Claims;

namespace MyOwnSummary_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogger<LanguageController> _logger;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;
        public LanguageController(ILanguageRepository languageRepository, ILogger<LanguageController> logger, IMapper mapper)
        {
           _languageRepository = languageRepository;
            _logger = logger;
            _mapper = mapper;
            _apiResponse = new();
        }
        [HttpGet("{id:int}", Name = "GetLanguage")]
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
                var language = await _languageRepository.Get(x => x.Id == id);
                if (language == null)
                {
                    _logger.LogError($"El lenguage con id {id} no existe", id);
                    _apiResponse.Errors.Add($"El lenguage con id {id} no existe");
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                _apiResponse.Result = _mapper.Map<LanguageDto>(language);
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
                var languages = await _languageRepository.GetAll();
                _apiResponse.Result = _mapper.Map<IEnumerable<LanguageDto>>(languages);
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
                if (!identity.Claims.Any())
                {
                    _apiResponse.Errors.Add("Token invalido");
                    _apiResponse.StatusCode = HttpStatusCode.Unauthorized;
                    return Unauthorized(_apiResponse);
                }
                var u = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                if (u != "User1")
                {
                    _apiResponse.Errors.Add("No tienes permisos para eliminar");
                    _apiResponse.StatusCode = HttpStatusCode.Unauthorized;
                    return Unauthorized(_apiResponse);
                }
                if (id == 0)
                {
                    _logger.LogError("El id por parametro no puede ser 0", id);
                    _apiResponse.Errors.Add("El id no puede ser 0");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var language = await _languageRepository.Get(x => x.Id == id);
                if (language == null)
                {
                    _logger.LogError($"El lenguage con id {id} no existe", id);
                    _apiResponse.Errors.Add($"El lenguage con id {id} no existe");
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                await _languageRepository.Remove(language);
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
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateLanguageDto createLanguage)
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
                if (await _languageRepository.Get(x => x.Name == createLanguage.Name) != null)
                {
                    _apiResponse.Errors.Add("Este lenguage ya existe");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }
                var l = _mapper.Map<Language>(createLanguage);
                await _languageRepository.Create(l);
                _apiResponse.IsSuccess = true;
                _apiResponse.StatusCode = HttpStatusCode.Created;
                _apiResponse.Result = _mapper.Map<LanguageDto>(l);
                return CreatedAtRoute("GetLanguage", new { id = l.Id }, _apiResponse);
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
        public async Task<IActionResult> Update([FromBody] LanguageDto language, int id)
        {
            try
            {
                if (id != language.Id)
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
                var l = await _languageRepository.Get(x => x.Id == id, false);
                if (l == null)
                {
                    _apiResponse.Errors.Add($"El lenguage con id {id} no existe");
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                var lRepetido = await _languageRepository.Get(x => x.Name == l.Name && x.Id != id);
                if (lRepetido != null)
                {
                    _apiResponse.Errors.Add("Este lenguage ya existe");
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_apiResponse);
                }

                l = _mapper.Map<Language>(language);
                await _languageRepository.Update(l);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                var languageReturn = _mapper.Map<LanguageDto>(l);
                _apiResponse.Result = languageReturn;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Errors.Add(ex.Message);
                _apiResponse.IsSuccess = false;
            }
            return BadRequest(_apiResponse);
        }
    }
}
