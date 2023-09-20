using System.Collections.Frozen;
using StrawberryMaui.GraphQL;
using StrawberryShake;

namespace StrawberryMaui;

class GraphQLService(IContactsClient contactsClient)
{
	public async Task<FrozenSet<Person>> GetContacts(CancellationToken token)
	{
		var response = await contactsClient.GetAllPeople.ExecuteAsync(token).ConfigureAwait(false);
		response.EnsureNoErrors();

		var contactList = response.Data?.ListPeople?.Items?.Where(x => x != null).Select(x =>
		{
			ArgumentNullException.ThrowIfNull(x);

			return new Person
			{
				Email = x.Email,
				BirthDate = Convert.ToDateTime(x.Birthdate),
				Id = x.Id,
				Name = x.Name
			};

		}).ToFrozenSet() ?? Array.Empty<Person>().ToFrozenSet();

		return contactList;
	}
}