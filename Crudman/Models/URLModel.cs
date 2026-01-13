using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWebAppMovies.Models;

public class URLModel
{
    public int Id { get; set; }

    public int Order {get;set;}

    public string URL { get; set; }


}