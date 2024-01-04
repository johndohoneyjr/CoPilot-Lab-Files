# Lab Assignment

We want to use Github CoPilot to create a UI with Angular that displays two management reports as a SPA that accesses an API using C# and DotNet.  The API will access a simple SQL Lite DB.  The queries that the DotNet API will use are:

## Total Sales API query: 
```
select i.billingcountry, sum(total) as 'TotalSales'
from invoice as i
group by billingcountry
order by totalsales desc
```

## Invoices by Country
```
select billingcountry, count(billingcountry) as '# of invoices'
from invoice
group by billingcountry
```

Once the API have been constructed, we will use Postman to test the queries through the API plumbing, then construct a Unit Test with Newman to run the Postman Collection against the API.

# Lab Sequence

```
1. Setting up CoPilot on CodeSpaces
2. Copilot and Infrastructure as Code (IAC)
3. CoPilot Full Stack - Report Application
3. Copilot and Python, APIFlask, and OpenAPI
4. CoPilot and Machine Learning
```
