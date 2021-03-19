using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace nightly.xam.audiorecorder.forms
{
    public partial class MainPage : ContentPage
    {
        private NightlyRecorderService _recordService;
        private Stream _stream;

        public MainPage()
        {
            this.InitializeComponent();
            this._recordService = new NightlyRecorderService();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var streamFile = await this._recordService.StartAsync();
            this._stream = streamFile;
        }

        private void StopButton_OnClicked(object sender, EventArgs e)
        {
            this._recordService.Stop();
        }

        private void PlayButton_OnClicked(object sender, EventArgs e)
        {
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(this._stream);
            player.Play();
        }
    }
}