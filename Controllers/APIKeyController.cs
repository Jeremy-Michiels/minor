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

namespace minor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIKeyController : ControllerBase
    {
        private readonly MyContext _context;

        public APIKeyController(MyContext context)
        {
            _context = context;
        }

        // GET: api/APIKey/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIKey>> GetAPIKey(string id)
        {
            var aPIKey = await _context.aPIKeys.FindAsync(id);

            if (aPIKey == null)
            {
                return NotFound("API key bestaat niet");
            }

            return aPIKey;
        }


        // POST: api/APIKey
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<APIKey>> PostAPIKey(PostAPIKey req)
        {
            if(req.MonthBilling == null || req.MonthBilling < 1 ){
                return BadRequest("Entity 'MonthBilling' cannot be lower than 1 or null");
            }
            else if(req.Email == null || !IsValid(req.Email)){
                return BadRequest("Entity 'Email' must contain a valid email address");
            }
            else if(req.Business == "" || req.Business == null){
                return BadRequest("Entity 'Business' Requires a non-empty value");
            }
            var nextDate = DateTime.Now.Date.AddMonths(req.MonthBilling);
            var apikey = new APIKey(){
                Key = RandomString(),
                Email = req.Email,
                Brand = req.Business,
                NextBillingDue = nextDate,
                signUpDate = DateTime.Now.Date,
            };
            while(true){
            if(_context.aPIKeys.Where(x => x.Key == apikey.Key).Count() > 0){
                apikey.Key = RandomString();
            }
            else{
                await _context.aPIKeys.AddAsync(apikey);
                break;
            }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (APIKeyExists(apikey.Key))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAPIKey", new { id = apikey.Key }, apikey);
        }

        private bool APIKeyExists(string id)
        {
            return _context.aPIKeys.Any(e => e.Key == id);
        }

        private static Random random = new Random();
        private static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#";
            return new string(Enumerable.Repeat(chars, 15)
                .Select(s => s[random.Next(15)]).ToArray());
        }

        private bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
