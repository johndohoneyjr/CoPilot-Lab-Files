using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using ReportsAPI.Models;


namespace ReportsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IDbConnection _db;
        public ReportsController(IDbConnection db)
        {
            _db = db;
        }

        // Add getSales and getInvoices methods here
        [HttpGet("getSales")]
        public ActionResult<IEnumerable<Sales>> GetSales()
        {
            var sql = @"select i.billingcountry, sum(total) as 'TotalSales'
                    from invoice as i
                    group by billingcountry
                    order by totalsales desc";

            var sales = _db.Query<Sales>(sql).ToList();

            return Ok(sales);

        }

        [HttpGet("getInvoices")]
        public ActionResult<IEnumerable<Invoices>> GetInvoices()
        {
            var sql = @"SELECT billingcountry, COUNT(billingcountry) as 'InvoiceCount'
                FROM invoice
                GROUP BY billingcountry";

            var invoices = _db.Query<Invoices>(sql).ToList();

            return Ok(invoices);
        }
    }

}
