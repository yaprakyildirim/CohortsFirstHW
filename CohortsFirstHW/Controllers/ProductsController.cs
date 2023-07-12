using CohortsFirstHW.Models;
using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static List<Product> products = new List<Product>
    {
        //ürünler listeleniyor
        new Product { Id = 1, Name = "Shirt", Description = "Description 1", Price = 25.99M },
        new Product { Id = 2, Name = "Skirt", Description = "Description 2", Price = 19.99M },
        new Product { Id = 3, Name = "Jean", Description = "Description 2", Price = 59.99M },
        new Product { Id = 4, Name = "T-shirt", Description = "Description 2", Price = 20.00M },
    };

    //ürünlerin tamamını çağırıyoruz
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(products);
    }

    //ürünleri id göre getiriyoruz
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

    //yeni bir ürün oluşturmayı sağlar.
    //HTTP Post isteğiyle bu metoda bir Product nesnesi gönderilir ve bu nesne doğrudan model binding kullanılarak product parametresine aktarılır.
    [HttpPost]
    public IActionResult Post(Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        products.Add(product);
        //başarılı bir şekilde yeni bir kaynak oluşturulduğunu gösteren HTTP 201 status kodu ile birlikte döner. 
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    //belirli bir ürünü güncellemeyi sağlar. id ye göre
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

        //başarılı bir şekilde bir ürün güncellendiğinde, NoContent sonucu döner.  HTTP 204 status döner
        return NoContent();
    }

    //belirli ürünü silmeyi sağlar, id ye göre
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

    //listeleme ve sıralama işlevleri
    [HttpGet("list")]
    public IActionResult List(string name, string sort)
    {
        var result = products.AsEnumerable();

        if (!string.IsNullOrEmpty(name))
        {
            result = result.Where(p => p.Name.Contains(name));
        }

        // "desc" için azalan, "asc" için artan sıralama yapılır.
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
