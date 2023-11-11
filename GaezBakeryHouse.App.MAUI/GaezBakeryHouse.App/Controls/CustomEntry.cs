namespace GaezBakeryHouse.App.Controls
{
    public class CustomEntry : Entry
    {
        public CustomEntry()
        {
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
}
