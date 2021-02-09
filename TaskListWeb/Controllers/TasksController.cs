using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace mz_sysprod.Controllers
{

    /// <summary>
    /// Requisições de Categoria
    /// </summary>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal server error</response>
    [EnableCors]
    [ApiController]
    [Authorize("Bearer")]
    [Route("v1/tasks")]
    public class ProductController : ControllerBase
    {

    //    /// <summary>
    //    /// Listar produtos
    //    /// </summary>
    //    /// <param name="productRepository"></param>
    //    /// <param name="mapper"></param>
    //    /// <returns></returns>
    //    [HttpGet]
    //    public ActionResult<List<ProductViewModelResult>> AllProducts([FromServices] IProductRepository productRepository,
    //                                                                  [FromServices] IMapper mapper)
    //    {
    //        try
    //        {

    //            var products = productRepository.GetAllProducts().Result;

    //            if (products.Count == 0)
    //                return NotFound();

    //            var viewProduct = mapper.Map<List<ProductViewModelResult>>(products);

    //            return Ok(viewProduct);


    //        } catch (Exception error)
    //        {

    //            return BadRequest(new { error = error.Message.ToString() });

    //        }

    //    }

    //    /// <summary>
    //    /// Buscar produto por Id
    //    /// </summary>
    //    /// <param name="productRepository"></param>
    //    /// <param name="mapper"></param>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    [HttpGet]
    //    [Route("{id:long}")]
    //    public ActionResult<ProductViewModelResult> ProductById([FromServices] IProductRepository productRepository,
    //                                                              [FromServices] IMapper mapper,
    //                                                              long id)
    //    {
    //        try
    //        {
    //            var product = productRepository.Get(id);

    //            if (product == null)
    //                return NotFound();

    //            var viewProduct = mapper.Map<ProductViewModelResult>(product);

    //            return Ok(viewProduct);

    //        } catch (Exception error)
    //        {

    //            return BadRequest(new { error = error.Message.ToString() });

    //        }
    //    }

    //    /// <summary>
    //    /// Buscar produto por nome
    //    /// </summary>
    //    /// <param name="productRepository"></param>
    //    /// <param name="mapper"></param>
    //    /// <param name="categoryName"></param>
    //    /// <returns></returns>
    //    [HttpGet]
    //    [Route("{categoryName}")]
    //    public ActionResult<List<ProductViewModelResult>> ProductByCategoryName([FromServices] IProductRepository productRepository,
    //                                                                      [FromServices] IMapper mapper,
    //                                                                      string categoryName)
    //    {
    //        try
    //        {
    //            var products = productRepository.GetProductsByCategoryNameAsync(categoryName);

    //            if (products == null)
    //                return NotFound();

    //            var viewProducts = mapper.Map<List<ProductViewModelResult>>(products);
    //            return Ok(viewProducts);

    //        } catch (Exception error)
    //        {

    //            return BadRequest(new { error = error.Message.ToString() });

    //        }

    //    }


    //    /// <summary>
    //    /// Inserir produto
    //    /// </summary>
    //    /// <param name="productRepository"></param>
    //    /// <param name="dataTransaction"></param>
    //    /// <param name="mapper"></param>
    //    /// <param name="product"></param>
    //    /// <returns></returns>
    //    [HttpPost]
    //    public ActionResult<ProductViewModel> Post([FromServices] IProductRepository productRepository,
    //                                               [FromServices] IDataTransaction dataTransaction,
    //                                               [FromServices] IMapper mapper,
    //                                               [FromBody] ProductViewModel product)
    //    {


    //        if (product == null)
    //            return BadRequest(new { error = "Não foi possível cadastrar produto, verifique!" });

    //        try
    //        {

    //            if (!productRepository.ValidateUniqueProduct(product.Name))
    //                return BadRequest("Produto já cadastrado");


    //            var productSent = mapper.Map<Product>(product);

    //            productRepository.Save(productSent);

    //            dataTransaction.Commit();

    //            return Ok("Produto cadastrado");

    //        } catch (Exception error)
    //        {

    //            dataTransaction.RollBack();
    //            return BadRequest(new { Error = error.Message.ToString() });

    //        }
    //    }


    //    /// <summary>
    //    /// Ataulizar produto
    //    /// </summary>
    //    /// <param name="productRepository"></param>
    //    /// <param name="dataTransaction"></param>
    //    /// <param name="mapper"></param>
    //    /// <param name="product"></param>
    //    /// <returns></returns>
    //    [HttpPut]
    //    public ActionResult<ProductViewModel> Put([FromServices] IProductRepository productRepository,
    //                                              [FromServices] IDataTransaction dataTransaction,
    //                                              [FromServices] IMapper mapper,
    //                                              [FromBody] ProductViewModel product)
    //    {

    //        if (product == null)
    //            return BadRequest(new { error = "Não foi possível atualizar produto, verifique!" });

    //        try
    //        {

    //            var productSent = mapper.Map<Product>(product);
    //            productRepository.Update(productSent);

    //            dataTransaction.Commit();

    //            return Ok(new { Success = "Produto atualizado" });

    //        } catch (Exception error)
    //        {

    //            dataTransaction.RollBack();
    //            return BadRequest(new { Error = error.Message.ToString() });

    //        }
    //    }


    //    /// <summary>
    //    /// Remover produto
    //    /// </summary>
    //    /// <param name="productRepository"></param>
    //    /// <param name="dataTransaction"></param>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    [HttpDelete]
    //    [Route("{id:long}")]
    //    public ActionResult Delete([FromServices] IProductRepository productRepository,
    //                               [FromServices] IDataTransaction dataTransaction,
    //                               int id)
    //    {

    //        var product = productRepository.Get(id);

    //        if (product == null)
    //            return NotFound();

    //        try
    //        {

    //            productRepository.Remove(product);
    //            dataTransaction.Commit();

    //            return Ok(new { Success = "Produto removido com sucesso" });

    //        } catch (Exception error)
    //        {

    //            dataTransaction.RollBack();
    //            return BadRequest(new { Error = error.Message.ToString() });

    //        }

    //    }
    }
}
