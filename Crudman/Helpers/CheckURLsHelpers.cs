using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Headers;
using System.Net;
namespace Crudman.Helpers;

public class CheckURLsHelper
{
    //private bool urlResponseError;

    public async Task<bool> Check(string urlString,IHttpClientFactory clientFactory )
    {
        Console.WriteLine("Check Service Started");
        using var request = new HttpRequestMessage(HttpMethod.Get,
            urlString);
        //request.Headers.Add("Accept", "application/vnd.github.v3+json");
        //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

        var client = clientFactory.CreateClient();
        HttpResponseMessage response;
        try
        {
             response = await client.SendAsync(request);
 
        }
        catch(HttpRequestException)
        {
            Console.WriteLine("System.Net.Http.HttpRequestException caught");
            Console.WriteLine("EXCEPTION CUAGHT EXCEPTION CAUGHT LOOK AT ME I DID IT");
            return false;
        }

        if (response.IsSuccessStatusCode)
        {
            HttpStatusCode statusCode = response.StatusCode;
            //using var responseStream = await response.Content.ReadAsStreamAsync();
            Console.WriteLine(urlString +" GET success");
            //branches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(responseStream);
        }
        else
        {
            // This is not triggering, massive error instead (443, no such host is known)
            //urlResponseError = true;
            Console.WriteLine(urlString +" GET failure");
        }
        Console.WriteLine("Check Service finished");
        response.Dispose();
        return true;
    }

    public static ValidationResult IsStringProperURI(String str)
    {
       if(Uri.IsWellFormedUriString(str, UriKind.Absolute))
        {
#pragma warning disable CS8603 // Possible null reference return.
            return ValidationResult.Success;
#pragma warning restore CS8603 // Possible null reference return.
        }
        else
        {
            return new ValidationResult("String is not a valid URI");
        }
    }

}