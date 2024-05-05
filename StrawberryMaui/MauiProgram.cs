using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using Microsoft.Extensions.Http.Resilience;
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
		builder.Services.AddTransientWithShellRoute<ContactsPage, ContactsViewModel>(nameof(ContactsPage));

		builder.Services.AddSingleton<GraphQLService>();

		builder.Services.AddContactsClient()
			.ConfigureHttpClient(
				client =>
				{
					client.BaseAddress = new Uri("https://lmplh3zfpza2xad4squve7d6ku.appsync-api.us-west-1.amazonaws.com/graphql");
					client.DefaultRequestHeaders.Add("x-api-key", "da2-nbfcafaez5g2ff3x74j3hdo5na");
				},
				clientBuilder =>
				{
					clientBuilder.AddStandardResilienceHandler(options => options.Retry = new MobileHttpRetryStrategyOptions());
				});


		return builder.Build();
	}
	
	sealed class MobileHttpRetryStrategyOptions : HttpRetryStrategyOptions
	{
		public MobileHttpRetryStrategyOptions()
		{
			BackoffType = DelayBackoffType.Exponential;
			MaxRetryAttempts = 3;
			UseJitter = true;
			Delay = TimeSpan.FromSeconds(2);
		}
	}
}