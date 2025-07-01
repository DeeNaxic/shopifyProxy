using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

public class Function1
{
    [Function("proxy")]
    public async Task<HttpResponseData> Run ([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestData request)
    {
        var url          = "https://cphmurano.com/pages/product-feed";
        var client       = new HttpClient();

        client.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
            "(KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36");
        

        var httpResponse = await client.GetAsync(url);
        var content      = await httpResponse.Content.ReadAsStringAsync();
        var response     = request.CreateResponse(HttpStatusCode.OK);

        response.Headers.Add("Content-Type", "application/xml");
        response.WriteString(content.Trim());

        return response;
    }
}
