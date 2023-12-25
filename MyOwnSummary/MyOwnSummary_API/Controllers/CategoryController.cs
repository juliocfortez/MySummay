using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyOwnSummary_API.Models;
using MyOwnSummary_API.Models.Dtos.CategoryDtos;
using MyOwnSummary_API.Models.Dtos.UserDtos;
using MyOwnSummary_API.Repositories;
using MyOwnSummary_API.Repositories.IRepository;
using System.Net;

namespace MyOwnSummary_API.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;
        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
            _apiResponse = new();
        }
        [HttpGet("{id:int}", Name = "GetCategory")]
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
                var category = await _categoryRepository.Get(x => x.Id == id);
                if (category == null)
                {
                    _logger.LogError($"La categoria con id {id} no existe", id);
                    _apiResponse.Errors.Add($"La categoria con id {id} no existe");
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_apiResponse);
                }
                _apiResponse.Result = _mapper.Map<CategoryDto>(category);
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
                _apiResponse.Result = _mapper.Map<IEnumerable<CategoryDto>>(await _categoryRepository.GetAll());
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
            return Ok();
        }
    }
}
