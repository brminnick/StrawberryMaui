using System.Text.Json.Serialization;
using StrawberryMaui.GraphQL;

namespace StrawberryMaui;

public class Person : IGetAllPeople_ListPeople_Items_Person
{
	[JsonPropertyName(nameof(IGetAllPeople_ListPeople_Items_Person.Birthdate))]
	public DateTime? BirthDate { get; init; }
	public required string Email { get; init;  }
	public required string Id { get; init; }
	public required string Name { get; init; }
	
	string? IGetAllPeople_ListPeople_Items.Birthdate => BirthDate?.ToString("d MMMM yyyy");
}