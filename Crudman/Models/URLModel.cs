using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Crudman.Helpers;

namespace Crudman.Models;

public class UrlModel
{
    public int Id { get; set; }
    public int Order {get;set;}
    public HttpStatusCode? StatusCode {get;set;}
    public DateTime? TimeCode {get;set;}

    [Required]
    [CustomValidation(typeof(UrlStatusHelper),"IsStringProperURI")]
    public string? URL { get; set; }

}