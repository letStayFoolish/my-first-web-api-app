using Microsoft.AspNetCore.Mvc;

namespace my_first_web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzaController : ControllerBase
{
  public PizzaController()
  {
  }

  // GET all action
  [HttpGet]
  public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

  // GET by Id action
  [HttpGet("{id}")]
  public ActionResult<Pizza> Get(int id)
  {
    var pizza = PizzaService.Get(id);

    if (pizza is null) return NotFound();

    return pizza;
  }

  // POST action
  [HttpPost]
  public IActionResult Create(Pizza pizza)
  {
    PizzaService.Add(pizza);
    // nameof - to avoid it hard-coding the action name;
    // CreatedAtAction uses the action name to generate a location HTTP response header with a URL to the newly created pizza, as explained in the previous unit.
    return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
  }

  // PUT action
  [HttpPut("{id}")]
  // requires that the `id` parameter's value is included in the URL segment after `pizza/`
  // Returns `IActionResult`, because the `ActionResult` return type is not known until runtime.
  // Because the controller is annotated with the [ApiController] attribute, it's implied that the Pizza parameter will be found in the request body.
  public IActionResult Update(int id, Pizza pizza)
  {
    if (id != pizza.Id) return BadRequest();

    var existingPizza = PizzaService.Get(id);
    if (existingPizza is null)
    {
      return NotFound();
    }
    
    PizzaService.Update(pizza);
    
    return NoContent();
  }

  // DELETE action
  [HttpDelete("{id}")]
  // Queries the in-memory cache for a pizza that matches the provided id parameter.
  public IActionResult Delete(int id)
  {
    var pizza = PizzaService.Get(id);
    
    if (pizza == null) return NotFound();
    
    PizzaService.Delete(id);
    
    return NoContent();
  }
}