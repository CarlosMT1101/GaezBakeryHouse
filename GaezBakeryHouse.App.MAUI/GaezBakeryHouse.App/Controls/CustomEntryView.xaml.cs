namespace GaezBakeryHouse.App.Controls;

public partial class CustomEntryView : ContentView
{
    public static readonly BindableProperty PlaceHolderProperty =
        BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(CustomEntryView), default(string), BindingMode.OneWay, propertyChanged: OnPlaceHolderChanged);

    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomEntryView), default(bool), BindingMode.OneWay, propertyChanged: OnIsPasswordPropertyChanged);

    public static readonly BindableProperty KeyboarProperty =
        BindableProperty.Create(nameof(KeyBoard), typeof(Keyboard), typeof(CustomEntryView), default(Keyboard), BindingMode.OneWay, propertyChanged: OnKeyBoardPropertyChanged);

    private static void OnKeyBoardPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customEntryView = (CustomEntryView)bindable;

        customEntryView.entryView.Keyboard = customEntryView.KeyBoard;
    }

    public Keyboard KeyBoard
    {
        get => (Keyboard)GetValue(KeyboarProperty);
        set => SetValue(KeyboarProperty, value);
    }

    public string PlaceHolder
    {
        get => (string)GetValue(PlaceHolderProperty);
        set => SetValue(PlaceHolderProperty, value);
    }

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    private static void OnPlaceHolderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customEntryView = (CustomEntryView)bindable;

        customEntryView.entryView.Placeholder = customEntryView.PlaceHolder;
    }

    private static void OnIsPasswordPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var customEntryView = (CustomEntryView)bindable;

        customEntryView.entryView.IsPassword = customEntryView.IsPassword;
    }


    public CustomEntryView()
	{
		InitializeComponent();

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
#elif WINDOWS
#endif
        });
    }
}