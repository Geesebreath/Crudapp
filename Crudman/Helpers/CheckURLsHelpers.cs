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
    public static async Task<CheckedURLStatus> Check(URLModel model, IHttpClientFactory clientFactory, int timeoutSec)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, model.URL);
        HttpResponseMessage response;

        var client = clientFactory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(timeoutSec);

        CheckedURLStatus status = new CheckedURLStatus(model);
        
        try
        {
            // TODO: Timeout causes full cancellation of the task, cannot update the database as nothing will be returned by this Task.
            response = await client.SendAsync(request);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"{model.URL} HttpRequest error with code {e.StatusCode}.");
            status.Response = ConnectionType.HostNotFound;
            return status;
        }
        catch (WebException w)
        {
            Console.WriteLine(w.Status.ToString());
            if (w.Status == WebExceptionStatus.Timeout)
            {
                // Currently not being reached if HttpClient timeout time is reached. 
                status.Response = ConnectionType.Timeout;
            }
            else
            {
                Console.WriteLine($"{model.URL} Web error with status {w.Status}.");
                status.Response = ConnectionType.Error;
            }
            return status;
        }

        if (response.IsSuccessStatusCode)
        {
            status.Response = ConnectionType.Connected;
        }

        status.Code = response.StatusCode;
        response.Dispose();
        return status;
    }

    public static ValidationResult IsStringProperURI(String str)
    {
        if (Uri.IsWellFormedUriString(str, UriKind.Absolute))
        {
#pragma warning disable CS8603 // Possible null reference return.
            return ValidationResult.Success;
#pragma warning restore CS8603 // Possible null reference return.
        }

        return new ValidationResult("String is not a valid URI");
    }
}

public enum ConnectionType 
{ 
    Connected, 
    Timeout, 
    Unsuccessful, 
    HostNotFound, 
    Error
}

public class CheckedURLStatus
{
    public URLModel Model { get; set; }
    public ConnectionType Response { get; set; }
    public HttpStatusCode Code { get; set; }

    public CheckedURLStatus(URLModel m)
    {
        Model = m;
        Code = HttpStatusCode.Ambiguous;
        Response = ConnectionType.Unsuccessful;
    }

    public CheckedURLStatus(URLModel m, HttpStatusCode c, ConnectionType r)
    {
        Model = m;
        Code = c;
        Response = r;
    }
}