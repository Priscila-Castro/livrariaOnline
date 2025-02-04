using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LibraryOnline.Books;
using System.Linq;

namespace LibraryOnline.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsControllers : ControllerBase   
{
    private static readonly List<Products> Products = new List<Products>
    {
        new Products {Id = 0, Name = "Livro 1", Price = 100.0m},
        new Products {Id = 1, Name = "Livro 2", Price = 200.0m},
        new Products {Id = 2, Name = "Livro 3", Price = 300.0m}
    };

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Products>> Get(int id)
    {
        var products = Products.FirstOrDefault(p=> p. Id == id);
        if (products == null)
            return NotFound();
        
        return Ok(Products);
    }

    [HttpGet]
    public ActionResult<Products> Get()
    {
        return Ok(Products);
    }

    [HttpPost]
    public ActionResult Post([FromBody] Products products)
    {
        if (products == null)
            return BadRequest("Invalid Product");

        products.Id = Products.Count;
        Products.Add(products);
        return CreatedAtAction(nameof(Get), new { id = products.Id }, products);
        

    }
}
