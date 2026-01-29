using System.Text.Json;
using System.Text.Json.Serialization;
namespace Crudman.Services;

public class CheckURLsService
{
    private bool urlResponseError;

    public async Task Check(string urlString,IHttpClientFactory clientFactory )
    {
        Console.WriteLine("Check Service Started");
        using var request = new HttpRequestMessage(HttpMethod.Get,
            urlString);
        //request.Headers.Add("Accept", "application/vnd.github.v3+json");
        //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

        var client = clientFactory.CreateClient();

        using var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            Console.WriteLine(urlString +" GET success");
            //branches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(responseStream);
        }
        else
        {
            // This is not triggering, massive error instead (443, no such host is known)
            urlResponseError = true;
            Console.WriteLine(urlString +" GET failure");
        }
        Console.WriteLine("Check Service finished");
    }


}
