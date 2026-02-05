using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Headers;
using System.Net;
using Crudman.Models;
using System.Runtime.CompilerServices;
namespace Crudman.Helpers;

public static class CheckURLsHelper
{
    //private bool urlResponseError;

    public static async Task<HttpStatusCode?> Check(string urlString,IHttpClientFactory clientFactory, int timeoutSec )
    {
        Console.WriteLine("Check Service Started");
        using var request = new HttpRequestMessage(HttpMethod.Get,
            urlString);
        //request.Headers.Add("Accept", "application/vnd.github.v3+json");
        //request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

        var client = clientFactory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(timeoutSec);
        HttpStatusCode? statusCode = null;
        HttpResponseMessage response;
        try
        {
             response = await client.SendAsync(request);
 
        }
        catch(HttpRequestException)
        {
            // check for timeout from HttpClient
            Console.WriteLine("System.Net.Http.HttpRequestException caught");
            Console.WriteLine("EXCEPTION CUAGHT EXCEPTION CAUGHT LOOK AT ME I DID IT");
            return null;
        }

        if (response.IsSuccessStatusCode)
        {
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
        statusCode = response.StatusCode;
        response.Dispose();
        return statusCode;
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

public class StatusReturn
{
    URLModel Model {get; set;}

    HttpStatusCode Code {get; set;}

     public StatusReturn( URLModel m, HttpStatusCode c)
    {
        Model = m;
        Code = c;
    }
}