using CommunityToolkit.Maui.Markup;

namespace StrawberryMaui;

class ContactsPage : BasePage<ContactsViewModel>
{
	public ContactsPage(ContactsViewModel contactsViewModel) : base(contactsViewModel, false)
	{
		Title = "Contacts";

		Content = new RefreshView
		{
			RefreshColor = ColorConstants.NavigationBarTextColor,
			Content = new CollectionView
			{
				ItemTemplate = new ContactsListDataTemplate(),
				BackgroundColor = Colors.Transparent,
				SelectionMode = SelectionMode.Single
			}.Bind(CollectionView.ItemsSourceProperty, 
				 getter: static (ContactsViewModel vm) => vm.ContactList, 
				 mode: BindingMode.OneWay)
			 .Bind(CollectionView.SelectionChangedCommandProperty, 
				 getter: static (ContactsViewModel vm) => vm.HandleSelectionChangedCommand, 
				 mode: BindingMode.OneTime)
			 .Bind(CollectionView.SelectionChangedCommandParameterProperty, 
				 source: RelativeBindingSource.Self)

		}.Bind(RefreshView.CommandProperty, static (ContactsViewModel vm) => vm.GetContactsCommand, mode: BindingMode.OneTime)
		 .Bind(RefreshView.IsRefreshingProperty, static (ContactsViewModel vm) => vm.IsRefreshing);
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		var refreshView = (RefreshView)Content;
		refreshView.IsRefreshing = true;
	}
}