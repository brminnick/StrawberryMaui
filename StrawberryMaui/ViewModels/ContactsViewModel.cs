using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StrawberryMaui;

partial class ContactsViewModel(GraphQLService graphQLService, IDispatcher dispatcher) : BaseViewModel(dispatcher)
{
	readonly GraphQLService _graphQLService = graphQLService;

	[ObservableProperty]
	bool _isRefreshing;

	public ObservableCollection<Person> ContactList { get; } = new();

	[RelayCommand(IncludeCancelCommand = true)]
	async Task GetContacts(CancellationToken token)
	{
		IsRefreshing = true;

		var minimumRefreshTimeTask = Task.Delay(TimeSpan.FromSeconds(2), token);

		try
		{
			ContactList.Clear();

			var contacts = await _graphQLService.GetContacts(token).ConfigureAwait(false);

			foreach (var contact in contacts.OrderBy(x => x.Name))
				ContactList.Add(contact);
		}
		finally
		{
			await minimumRefreshTimeTask.ConfigureAwait(false);
			IsRefreshing = false;
		}
	}

	[RelayCommand]
	async Task HandleSelectionChanged(CollectionView collectionView, CancellationToken token)
	{
#if ANDROID
		var delay = TimeSpan.FromSeconds(1);
#else
		var delay = TimeSpan.FromMilliseconds(100);
#endif

		try
		{
			await Task.Delay(delay, token)
				.ConfigureAwait(ConfigureAwaitOptions.ForceYielding | ConfigureAwaitOptions.ContinueOnCapturedContext);
		}
		catch (TaskCanceledException)
		{
			// Do nothing
		}
		finally
		{
			collectionView.SelectedItem = null;
		}
	}
}