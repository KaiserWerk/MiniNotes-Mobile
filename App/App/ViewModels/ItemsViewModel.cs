using Core.Model;
using System.Diagnostics;
using Core;
using Xamarin.Forms;

namespace App.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly SettingsService settingsService;

        public UserSettings Settings { get; set; }

        public Command SaveChangesCommand { get; }

        public ItemsViewModel()
        {
            Title = "Settings";
            this.SaveChangesCommand = new Command(this.SaveChangesCommandExecute);

            this.settingsService = new SettingsService();

            this.Settings = this.settingsService.ReadSettings();
        }

        private void SaveChangesCommandExecute(object obj)
        {
            this.settingsService.StoreSettings(this.Settings);
            Trace.WriteLine("clicked on save: " + this.Settings.RemoteAddress);
        }

        
        public void OnAppearing()
        { }
    }
}