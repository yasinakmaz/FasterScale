namespace FasterScale
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            // Ba�lang��ta varsay�lan sayfay� y�kle
            ContentFrame.Navigate(typeof(DashboardPage));
        }
        public NavigationView ViewPanel => NvView;
        private void NvView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked) // Ayarlar butonuna bas�ld�ysa
            {
                ContentFrame.Navigate(typeof(SettingsPage)); // Ayarlar sayfas�na git
                return;
            }

            // Se�ili ��eyi bul ve y�nlendir
            var selectedItem = sender.MenuItems.OfType<NavigationViewItem>()
                                .FirstOrDefault(x => (string)x.Content == (string)args.InvokedItem);

            if (selectedItem != null && selectedItem.Tag != null)
            {
                string pageTag = selectedItem.Tag.ToString();
                Type pageType = Type.GetType($"FasterScale.Pages.{pageTag}");

                if (pageType != null)
                {
                    ContentFrame.Navigate(pageType);
                }
            }
        }
    }
}
