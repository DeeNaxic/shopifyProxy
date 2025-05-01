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
        var url          = "https://httpbin.org/get";
        var client       = new HttpClient();
        var httpResponse = await client.GetAsync(url);
        var content      = await httpResponse.Content.ReadAsStringAsync();
        var response     = request.CreateResponse(HttpStatusCode.OK);

        response.Headers.Add("Content-Type", "application/json");
        response.WriteString(content);

        return response;
    }
}