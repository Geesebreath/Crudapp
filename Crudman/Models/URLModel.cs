using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using Crudman.Helpers;

namespace Crudman.Models;

public class UrlModel
{
    public int Id { get; set; }
    public int Order { get;set; }
    public ConnectionType? ConnectionType { get; set; }
    public HttpStatusCode? StatusCode { get;set; }
    public DateTime? TimeCode { get;set; }

    [Required]
    [CustomValidation(typeof(Validations),"IsStringProperURI")]
    public string? Url { get; set; }

    public void UpdateFromCheck(ConnectionType ct, HttpStatusCode? code)
    {
        this.ConnectionType = ct;
        this.StatusCode = code;
        this.TimeCode = DateTime.Now;
    }
}