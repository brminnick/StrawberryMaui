using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace StrawberryMaui;

class App : Microsoft.Maui.Controls.Application
{
	public App(ContactsPage contactsPage)
	{
		var navigationPage = new Microsoft.Maui.Controls.NavigationPage(contactsPage)
		{
			BarBackgroundColor = ColorConstants.NavigationBarBackgroundColor,
			BarTextColor = ColorConstants.NavigationBarTextColor
		};

		navigationPage.On<iOS>().SetPrefersLargeTitles(true);

		MainPage = navigationPage;
	}
}

