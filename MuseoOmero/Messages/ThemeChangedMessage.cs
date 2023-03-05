using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MuseoOmero.Messages;

public class ThemeChangedMessage : ValueChangedMessage<string>
{
	public ThemeChangedMessage(string value) : base(value)
	{
	}
}
