using CommunityToolkit.Mvvm.ComponentModel;

namespace StrawberryMaui;

public abstract partial class BaseViewModel(IDispatcher dispatcher) : ObservableObject
{
	protected IDispatcher Dispatcher { get; } = dispatcher;
}