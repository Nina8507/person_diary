using System.Text;
using System.Text.Json;
using BlazorApp.Models;

namespace BlazorApp.Service;

public class PersonService:IService<Person>{
  private readonly string uri = "https://localhost:5006";
  private readonly HttpClient httpClient;

  public PersonService(){
    httpClient = new HttpClient();
  }

  public async Task<IList<Person>> GetAllAsync(){
    HttpResponseMessage responseMessage = await httpClient.GetAsync(uri + "/Person");
      Console.WriteLine(responseMessage.Content);
    if (responseMessage.IsSuccessStatusCode)
    {
      string result = await responseMessage.Content.ReadAsStringAsync();
      Console.WriteLine(result);

      IList<Person> persons = JsonSerializer.Deserialize<IList<Person>>(result, new JsonSerializerOptions
      {
          PropertyNameCaseInsensitive = true
      });

      return persons;
    }
    
    throw new Exception("Error in uploading!");
  }

    public async Task<Person> GetByIdAsync(int personId) {
      HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:5006/Person/{personId}");
      if(response.IsSuccessStatusCode){
        Console.WriteLine(response.StatusCode);
      } 
      else {
        throw new Exception($@"Error: {response.StatusCode}, {response.StatusCode}");
    }
      string result = await response.Content.ReadAsStringAsync();
      Person person = JsonSerializer.Deserialize<Person>(result, new JsonSerializerOptions
      {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
      });
      
      return person;
    }

    public async Task AddAsync(Person person)
    {
      string personAsJson = JsonSerializer.Serialize(person);
      StringContent content = new StringContent(personAsJson, Encoding.UTF8, "application/json");
      
      Console.WriteLine(personAsJson);
      
      HttpResponseMessage response = await httpClient.PostAsync(uri + "/Person", content);
      if(response.IsSuccessStatusCode)
      {
        Console.WriteLine(response.StatusCode);
      }
      else
      {
        Console.WriteLine($@"Error: {response.StatusCode}, {response.ReasonPhrase}");
      }
    }

    public async Task RemoveAsync(int personId)
    {
      HttpResponseMessage response = await httpClient.DeleteAsync($"https://localhost:5006/Person/{personId}");
      if(response.IsSuccessStatusCode)
      {
        Console.WriteLine(response.StatusCode);
      }
      else 
      {
        throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
      }
    }

    public async Task UpdateAsync(Person person)
    {
      string personAsJson = JsonSerializer.Serialize(person);
      StringContent content = new StringContent(personAsJson, Encoding.UTF8, "application/json");
      HttpResponseMessage response = await httpClient.PatchAsync(uri + "/Person/{id}", content);

      if(response.IsSuccessStatusCode)
      {
        Console.WriteLine(response.StatusCode);
      }
      else 
      {
        Console.WriteLine($@"Error: {response.StatusCode}, {response.ReasonPhrase}");
      }

    }
}