using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using FasterScale.Services;
using FasterScale.Pages;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace FasterScale
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            // Komut satırı parametreleri kontrol et
            string[] cmdArgs = Environment.GetCommandLineArgs();
            bool skipUpdate = cmdArgs.Any(arg => arg.Equals("--skip-update", StringComparison.OrdinalIgnoreCase));

            if (!skipUpdate)
            {
                // Güncelleme kontrolü yap
                var updateService = new UpdateService();
                var (hasUpdate, currentVersion, latestVersion) = await updateService.CheckForUpdateAvailability();

                if (hasUpdate)
                {
                    // Güncelleme varsa UpdateWindow'u aç
                    m_window = new UpdateWindow();
                    m_window.Activate();
                    return;
                }
            }

            // Güncelleme yoksa veya atlanırsa MainWindow'u aç
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
