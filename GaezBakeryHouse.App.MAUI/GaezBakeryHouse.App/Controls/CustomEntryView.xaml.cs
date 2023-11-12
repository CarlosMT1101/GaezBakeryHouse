using System.Runtime.CompilerServices;

namespace GaezBakeryHouse.App.Controls;

public partial class CustomEntryView : ContentView
{
    #region BindablePropertys
    public static readonly BindableProperty PlaceHolderProperty =
       BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(CustomEntryView), default(string), BindingMode.OneWay);

    public static readonly BindableProperty IsPasswordProperty =
        BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(CustomEntryView), default(bool), BindingMode.OneWay);

    public static readonly BindableProperty KeyboarProperty =
        BindableProperty.Create(nameof(KeyBoard), typeof(Keyboard), typeof(CustomEntryView), default(Keyboard), BindingMode.OneWay);

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEntryView), default(string), BindingMode.OneWayToSource);

    public static new readonly BindableProperty HeightRequestProperty =
        BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(CustomEntryView), default(double), BindingMode.OneWay);

    #endregion

    #region Fields
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public new double HeightRequest
    {
        get => (double)GetValue(HeightRequestProperty);
        set => SetValue(HeightRequestProperty, value);
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
    #endregion

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        if(propertyName == TextProperty.PropertyName)
            entryView.Text = Text;

        if (propertyName == KeyboarProperty.PropertyName)
            entryView.Keyboard = KeyBoard;

        if(propertyName == IsPasswordProperty.PropertyName)
            entryView.IsPassword = IsPassword;

        if(propertyName == PlaceHolderProperty.PropertyName)
            entryView.Placeholder = PlaceHolder;

        if(propertyName == HeightRequestProperty.PropertyName)
        {
            frameView.HeightRequest = HeightRequest;
            entryView.HeightRequest = HeightRequest;
        }
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

    private void OnTextChanged(object sender, TextChangedEventArgs e) =>
        Text = e.NewTextValue;
}