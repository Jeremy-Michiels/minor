using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minor;
using NuGet.Common;
using System.Net.Http;

namespace minor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopifyController : ControllerBase
    {
        private readonly MyContext _context;

        public ShopifyController(MyContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("GetProducts")]
        public async Task<ActionResult<object>> GetProducts([FromBody] PostKey post){
            var key = post.Key;
                var apikey = await _context.aPIKeys.SingleOrDefaultAsync(x => x.Key == key);
                if(apikey == null){
                    return BadRequest("API Key doesn't exist");
                }
                ShopifyGet content = new ShopifyGet();
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", "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");
                HttpResponseMessage response = await client.GetAsync("https://f35ee4.myshopify.com/admin/api/2023-10/products.json");
                if(response.IsSuccessStatusCode){
                    content = await response.Content.ReadAsAsync<ShopifyGet>();
                    content.Products = content.Products.Where(x => x.vendor == apikey.Brand).ToList();
                    return content;
                }
                else{
                    return BadRequest("Error");
                }
        }

        [HttpPost]
        [Route("PostProduct")]
        public async Task<ActionResult> PostProduct(ShopifyPost post){
            var key = post.key;
                var apikey = await _context.aPIKeys.SingleOrDefaultAsync(x => x.Key == key);
                if(apikey == null){
                    return BadRequest("API Key doesn't exist");
                }

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Shopify-Access-Token", "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");
                var productj = new productj(){
                    product = post.body
                };
                HttpResponseMessage response = await client.PostAsJsonAsync("https://f35ee4.myshopify.com/admin/api/2023-10/products.json", productj);
                if(response.IsSuccessStatusCode){
                    ShopifyReturnPost? product = await response.Content.ReadAsAsync<ShopifyReturnPost>();
                    var shopifyPicture = new ShopifyPicture(){
                        image = new image(){
                            position = 1,
                            attachment = post.body.images[0].attachment
                        }
                    };
                    var response2 = await client.PostAsJsonAsync("https://f35ee4.myshopify.com/admin/api/2023-10/products/" +product.product.id + "/images.json", shopifyPicture);
                    if(response2.IsSuccessStatusCode){
                        return Ok("Item has been added to your collection!");
                    }
                    return Ok("Item has been added to your collection, but no picture could be added.");
                }
                else{
                    return BadRequest("Error");
                }
        }


        
    }
}
