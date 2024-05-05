using System.Diagnostics;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace StrawberryMaui;

abstract class BasePage<TViewModel>(TViewModel viewModel, bool shouldUseSafeArea = true) : BasePage(viewModel, shouldUseSafeArea)
	where TViewModel : BaseViewModel
{
	public new TViewModel BindingContext => (TViewModel)base.BindingContext;
}

abstract class BasePage : ContentPage
{
	protected BasePage(object? viewModel = null, bool shouldUseSafeArea = true)
	{
		BindingContext = viewModel;
		Padding = 12;
		BackgroundColor = ColorConstants.PageBackgroundColor;

		if (string.IsNullOrWhiteSpace(Title))
		{
			Title = GetType().Name;
		}

		On<iOS>().SetUseSafeArea(shouldUseSafeArea);
		On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.FormSheet);
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		Debug.WriteLine($"OnAppearing: {Title}");
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();

		Debug.WriteLine($"OnDisappearing: {Title}");
	}
}