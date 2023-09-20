using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Microsoft.Extensions.Logging;
using Polly;

namespace StrawberryMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseMauiCommunityToolkitMarkup()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddTransient<App>();
		builder.Services.AddTransient<AppShell>();
		builder.Services.AddTransientWithShellRoute<ContactsPage, ContactsViewModel>(nameof(ContactsPage));

		builder.Services.AddSingleton<GraphQLService>();

		builder.Services.AddContactsClient()
			.ConfigureHttpClient(
				client =>
				{
					client.BaseAddress = new Uri("https://lmplh3zfpza2xad4squve7d6ku.appsync-api.us-west-1.amazonaws.com/graphql");
					client.DefaultRequestHeaders.Add("x-api-key", "da2-n3gvllxytndzhc6hpgatty4i5y");
				},
				clientBuilder =>
				{
					clientBuilder.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, SleepDurationProvider));
				});


		return builder.Build();

		static TimeSpan SleepDurationProvider(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
	}
}