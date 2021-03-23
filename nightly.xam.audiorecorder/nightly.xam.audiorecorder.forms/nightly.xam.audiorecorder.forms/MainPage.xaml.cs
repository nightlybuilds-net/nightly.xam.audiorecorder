﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nightly.xam.audiorecorder.Shared;
using Xamarin.Forms;

namespace nightly.xam.audiorecorder.forms
{
    public partial class MainPage : ContentPage
    {
        private readonly NightlyRecorderService _recordService;
        private Stream _stream;

        public MainPage()
        {
            this.InitializeComponent();
            this._recordService = new NightlyRecorderService(RecordFormat.Mp4);
        }

        private async void RecordButton_OnClicked(object sender, EventArgs e)
        {
            this.PlayBtn.IsEnabled = false;
            this.RecordBtn.IsEnabled = false;
            this.SizeLabel.IsVisible = false;
            this.PlayBtn.IsVisible = false;

            var streamFile = await this._recordService.RecordAsync();
            this._stream = streamFile;
        }

        private void StopButton_OnClicked(object sender, EventArgs e)
        {
            this._recordService.Stop();
            this.PlayBtn.IsEnabled = true;
            this.RecordBtn.IsEnabled = true;
            this.SizeSpan.Text = (this._stream.Length/1000).ToString();
            this.SizeLabel.IsVisible = true;
            this.PlayBtn.IsVisible = true;

        }

        private void PlayButton_OnClicked(object sender, EventArgs e)
        {
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(this._stream);
            player.Play();
        }
    }
}