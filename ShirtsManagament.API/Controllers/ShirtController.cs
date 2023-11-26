using Microsoft.AspNetCore.Mvc;
using ShirtsManagament.API.Models;
using ShirtsManagament.API.Filters.ShirtAttributes;
using ShirtsManagament.API.Filters;
using ShirtsManagament.API.Repositories.IRepositories;

namespace ShirtsManagament.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShirtController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Shirt>> GetAll() 
        {
            return Ok(_unitOfWork.ShirtRepo.GetAll());
        }
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(ShirtExistFilterAttribute))]
        public ActionResult<Shirt?> Get([FromRoute] int id) 
        {
            return Ok(HttpContext.Items["shirt"]);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ShirtCreateFilter]
        public async Task<ActionResult<Shirt>> Post([FromBody] Shirt shirt)
        {
            await _unitOfWork.ShirtRepo.InsertAsync(shirt);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(Get), new {id = shirt.Id}, shirt);
        }
        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(ShirtExistFilterAttribute))]
        [ShirtUpdateFilter]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Shirt shirt) 
        {
            _unitOfWork.ShirtRepo.Update(shirt);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [IdFilter]
        [TypeFilter(typeof(ShirtExistFilterAttribute))]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            Shirt? shirt = HttpContext.Items["shirt"] as Shirt;
            _unitOfWork.ShirtRepo.Delete(shirt);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
