using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorApp.Models; 

public class Person {
  [Key]
  [JsonPropertyName("personId")]
  public int Id { get; set; }
  [Required]
  [JsonPropertyName("firstName")]
  public required string FirstName { get; set; } = string.Empty;
  [Required]
  [JsonPropertyName("lastName")]
  public required string LastName { get; set; } = string.Empty;
}