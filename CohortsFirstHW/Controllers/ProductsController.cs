using CohortsFirstHW.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 9.99M },
        new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 19.99M },
        // Daha fazla ürün ekleyebilirsiniz...
    };

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public IActionResult Post(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        products.Add(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Product product)
    {
        if (id != product.Id || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var existingProduct = products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        products.Remove(product);
        return NoContent();
    }

    [HttpGet("list")]
    public IActionResult List(string name, string sort)
    {
        var result = products.AsEnumerable();

        if (!string.IsNullOrEmpty(name))
        {
            result = result.Where(p => p.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(sort) && sort == "desc")
        {
            result = result.OrderByDescending(p => p.Name);
        }
        else if (!string.IsNullOrEmpty(sort) && sort == "asc")
        {
            result = result.OrderBy(p => p.Name);
        }

        return Ok(result);
    }

}
