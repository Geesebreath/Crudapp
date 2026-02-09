using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Headers;
using System.Net;
using Crudman.Models;
using System.Runtime.CompilerServices;
namespace Crudman.Helpers;

public static class UrlStatusHelper
{
    public static async Task<CheckedUrlStatus> CheckStatus(UrlModel model, IHttpClientFactory clientFactory, int timeoutSec)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, model.URL);

        var client = clientFactory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(timeoutSec);
        
        try
        {
            using var response = await client.SendAsync(request);
            return new CheckedUrlStatus(model, response.StatusCode, ConnectionType.Connected); 
        }
        catch (HttpRequestException)
        {
            return new CheckedUrlStatus(model, null, ConnectionType.HostNotFound);
        }
        catch (WebException)
        {
            return new CheckedUrlStatus(model, null, ConnectionType.Error);
        }
        catch (TaskCanceledException)
        {
            return new CheckedUrlStatus(model, null, ConnectionType.Timeout);
        }    
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

public class CheckedUrlStatus
{
    public UrlModel Model { get; }
    public ConnectionType Response { get; }
    public HttpStatusCode? Code { get; set; }

    public CheckedUrlStatus(UrlModel m, HttpStatusCode? c, ConnectionType r)
    {
        Model = m;
        Code = c;
        Response = r;
    }
}