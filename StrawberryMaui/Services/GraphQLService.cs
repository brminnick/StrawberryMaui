using System.Collections.Frozen;
using StrawberryMaui.GraphQL;
using StrawberryShake;

namespace StrawberryMaui;

class GraphQLService(IContactsClient contactsClient)
{
	readonly IContactsClient _contactsClient = contactsClient;

	public async Task<FrozenSet<Person>> GetContacts(CancellationToken token)
	{
		
	}
}