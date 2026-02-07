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
    public static async Task<CheckedURLStatus?> Check(URLModel model, IHttpClientFactory clientFactory, int timeoutSec)
    {
        HttpResponseMessage response;
        using var request = new HttpRequestMessage(HttpMethod.Get, model.URL);
        CheckedURLStatus status = new CheckedURLStatus(model);

        var client = clientFactory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(timeoutSec);
        
        try
        {
            response = await client.SendAsync(request);

        }
        catch (HttpRequestException)
        {
            // General Request Erroring
            Console.WriteLine("System.Net.Http.HttpRequestException caught");
            Console.WriteLine("HTTPEXCEPTION CUAGHT EXCEPTION CAUGHT LOOK AT ME I DID IT");
            status.Response = ConnectionType.Error;
            return status;
        }
        catch (WebException w)
        {
            Console.WriteLine(w.Status.ToString());
            Console.WriteLine("HTTPEXCEPTION CUAGHT EXCEPTION CAUGHT LOOK AT ME I DID IT");
            if (w.Status == WebExceptionStatus.Timeout)
            {
                status.Response = ConnectionType.Timeout;
            }
            else
            {
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

public enum ConnectionType { Connected, Timeout, Unsuccessfull, Error };
public class CheckedURLStatus
{
    public URLModel? Model { get; set; }
    public ConnectionType? Response { get; set; }
    public HttpStatusCode? Code { get; set; }

    public CheckedURLStatus(URLModel m)
    {
        Model = m;
        Code = null;
        Response = ConnectionType.Unsuccessfull;
    }

    public CheckedURLStatus(URLModel m, HttpStatusCode c, ConnectionType r)
    {
        Model = m;
        Code = c;
        Response = r;
    }
}