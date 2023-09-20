using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StrawberryMaui;

partial class ContactsViewModel : BaseViewModel
{
	readonly GraphQLService _graphQLService;

	[ObservableProperty]
	bool _isRefreshing;

	public ContactsViewModel(GraphQLService graphQLService)
	{
		_graphQLService = graphQLService;
	}

	public ObservableCollection<Person> ContactList { get; } = new();

	[RelayCommand(IncludeCancelCommand = true)]
	async Task GetContacts(CancellationToken token)
	{
		IsRefreshing = true;

		try
		{
			ContactList.Clear();

			var contacts = await _graphQLService.GetContacts(token).ConfigureAwait(false);

			foreach (var contact in contacts.OrderBy(x => x.Name))
				ContactList.Add(contact);
		}
		finally
		{
			IsRefreshing = false;
		}
	}
}