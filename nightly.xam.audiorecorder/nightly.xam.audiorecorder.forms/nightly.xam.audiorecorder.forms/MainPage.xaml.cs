using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace nightly.xam.audiorecorder.forms
{
    public partial class MainPage : ContentPage
    {
        private NightlyAudioService _recordService;

        public MainPage()
        {
            this.InitializeComponent();
            this._recordService = new NightlyAudioService();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var streamFile = await this._recordService.StartAsync();
            var t = streamFile;
        }

        private void StopButton_OnClicked(object sender, EventArgs e)
        {
            this._recordService.Stop();
        }
    }
}