using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Core;
using Core.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly RestService restService;
        private readonly SettingsService settingsService;
        private readonly LocalContentService localContentService;

        private UserSettings settings;

        private string userMessage;

        public string UserMessage
        {
            get => userMessage;
            set => base.SetProperty(ref userMessage, value);
        }

        private string textContent;

        public string TextContent
        {
            get => textContent;
            set => base.SetProperty(ref textContent, value);
        }

        public ICommand OpenWebCommand { get; }

        public ICommand SaveChangesCommand { get; }

        public AboutViewModel()
        {
            this.restService = new RestService();
            this.settingsService = new SettingsService();
            this.localContentService = new LocalContentService();

            this.settings = this.settingsService.ReadSettings();

            base.Title = "MiniNotes";
            this.OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            this.SaveChangesCommand = new Command(this.SaveChangesCommandExecute);

            if (this.settings.SynchronizationEnabled)
            {
                try
                {
                    this.restService
                        .SetRemote(this.settings.RemoteAddress)
                        .SetId(this.settings.Identifier)
                        .SetSecret(this.settings.Secret);

                    this.TextContent = this.restService.GetContent();
                }
                catch (Exception e)
                {
                    this.UserMessage = "Could not fetch remote data!";
                    this.TextContent = this.localContentService.GetContent();
                    Trace.WriteLine($"Fetch data: {e.Message}");
                }
            }
            else
            {
                this.TextContent = this.localContentService.GetContent();
            }
        }

        public void SaveChangesCommandExecute()
        {
            this.SaveChanges();
        }

        private void SaveChanges()
        {
            this.localContentService.StoreContent(this.TextContent);

            if (this.settings.SynchronizationEnabled)
            {
                this.restService.StoreContent(this.TextContent);
            }

            this.UserMessage = "Manually saved!";
        }
    }
}