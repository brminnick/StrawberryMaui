using System.Diagnostics;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace StrawberryMaui;

abstract class BasePage<TViewModel> : BasePage where TViewModel : BaseViewModel
{
	protected BasePage(TViewModel viewModel, bool shouldUseSafeArea = true) : base(viewModel, shouldUseSafeArea)
	{
	}

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