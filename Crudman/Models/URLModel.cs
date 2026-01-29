using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Crudman.Helpers;

namespace Crudman.Models;

public class URLModel
{
    public int Id { get; set; }

    public int Order {get;set;}

    [Required]
    [CustomValidation(typeof(CheckURLsHelper),"IsStringProperURI")]
    public string? URL { get; set; }

}