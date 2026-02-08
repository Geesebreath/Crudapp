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
        
        try
        {
            response = await client.SendAsync(request);
        }
        catch (HttpRequestException)
        {
            return new CheckedURLStatus(model, null, ConnectionType.HostNotFound);
        }
        catch (WebException)
        {
            return new CheckedURLStatus(model, null, ConnectionType.Error);
        }
        catch (TaskCanceledException)
        {
            return new CheckedURLStatus(model, null, ConnectionType.Timeout);
        }

        CheckedURLStatus status = new CheckedURLStatus(model, response.StatusCode, ConnectionType.Connected);
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
    public URLModel Model { get; }
    public ConnectionType Response  { get; }
    public HttpStatusCode? Code { get; set; }

    public CheckedURLStatus(URLModel m)
    {
        Model = m;
        Code = null;
        Response = ConnectionType.Unsuccessful;
    }

    public CheckedURLStatus(URLModel m, HttpStatusCode? c, ConnectionType r)
    {
        Model = m;
        Code = c;
        Response = r;
    }
}