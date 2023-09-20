using CommunityToolkit.Maui.Markup;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace StrawberryMaui;

class ContactsListDataTemplate : DataTemplate
{
	public ContactsListDataTemplate() : base(() => CreateDataTemplate())
	{

	}

	static Grid CreateDataTemplate() => new()
	{
		RowSpacing = 2,

		RowDefinitions = Rows.Define(
			(Row.Text, Star),
			(Row.Detail, Star),
			(Row.Divider, 5)),

		Children =
		{
			new TextLabel(ColorConstants.TextColor, 16)
				.Row(Row.Text)
				.Bind(Label.TextProperty, static (Person person)  => person.Name),

			new TextLabel(ColorConstants.DetailColor, 13)
				.Row(Row.Detail)
				.Bind(Label.TextProperty, static (Person person) => person.BirthDate, convert: (DateTime? birthdate) => birthdate?.ToString("d MMMM yyyy") ?? "🤷‍♂️"),

			new BoxView { Color = Colors.DarkGray }
				.Row(Row.Divider)
				.Height(1)
				.Margin(0,2)
		}
	};

	enum Row { Text, Detail, Divider }

	class TextLabel : Label
	{
		public TextLabel(in Color textColor, in double fontSize)
		{
			FontSize = fontSize;
			TextColor = textColor;

			Margin = 0;
			Padding = new Thickness(10, 0);
		}
	}
}