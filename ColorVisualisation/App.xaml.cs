using ColorVisualisation.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;

namespace ColorVisualisation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var app = new ApplicationView();
                var context = new VisualisationViewModel();
                app.Show();
                app.DataContext = context;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
