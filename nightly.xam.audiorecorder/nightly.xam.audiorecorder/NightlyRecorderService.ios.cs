using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AudioToolbox;
using AVFoundation;
using Foundation;
using nightly.xam.audiorecorder.Exceptions;
using nightly.xam.audiorecorder.Shared;

namespace nightly.xam.audiorecorder
{
    public partial class NightlyRecorderService : IRecorder
    {
        private AVAudioRecorder _recorder;
        private NSUrl _url;
        private string _path;
        private TaskCompletionSource<Stream> _recordTask;
        private readonly RecorderSettings _settings;

        public bool IsRecording => this._recorder?.Recording ?? false;

        public NightlyRecorderService()
        {
            this._settings = RecorderSettings.Default;
            this.InitTempFilePath();
        }

        public NightlyRecorderService(RecorderSettings settings)
        {
            this._settings = settings;
            this.InitTempFilePath();
        }

        public Task<Stream> RecordAsync()
        {
            if (this.IsRecording)
                throw new Exception("Service already recording.");

            this.InitializeRecordService();
            this._recorder.Record();
            this._recordTask = new TaskCompletionSource<Stream>();
            return this._recordTask.Task;
        }


        public void Stop()
        {
            try
            {
                if (!this.IsRecording || this._recorder == null)
                    return;

                this._recorder.Stop();

                using (var streamReader = new StreamReader(this._path))
                {
                    var memstream = new MemoryStream();
                    streamReader.BaseStream.CopyTo(memstream);
                    memstream.Seek(0, SeekOrigin.Begin);
                    this._recordTask.SetResult(memstream);
                }

                File.Delete(this._path);
            }
            catch (Exception e)
            {
                throw new RecorderException(e);
            }
            
        }

        private void InitializeRecordService()
        {
            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
                throw new RecorderException(err);

            err = audioSession.SetActive(true);

            if (err != null)
                throw new RecorderException(err);

            this._url = NSUrl.FromFilename(this._path);
            var settings = this.GetAudioSettings();

            if (this._recorder == null)
            {
                this._recorder = AVAudioRecorder.Create(this._url, settings, out var error);

                if (error != null)
                    throw new RecorderException(error);
            }

            var ready = this._recorder.PrepareToRecord();
            if (!ready)
                throw new RecorderException("PrepareToRecord() returned false");
        }

        /// <summary>
        /// Retrieve audio settins for used audio format type
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private AudioSettings GetAudioSettings()
        {
            return new AudioSettings
            {
                Format = (AudioFormatType?)this._settings.IosRecorderSettings.AudioFormat,
                NumberChannels = this._settings.IosRecorderSettings.NumberChannels,
                AudioQuality = (AVAudioQuality?)this._settings.IosRecorderSettings.AudioQuality,
                SampleRate = this._settings.IosRecorderSettings.SampleRate,
                // BitRateStrategy = ,
                EncoderBitRate = this._settings.IosRecorderSettings.EncoderBitRate,
                LinearPcmFloat = this._settings.IosRecorderSettings.LinearPcmFloat,
                EncoderBitDepthHint = this._settings.IosRecorderSettings.EncoderBitDepthHint,
                LinearPcmBigEndian = this._settings.IosRecorderSettings.LinearPcmBigEndian,
                LinearPcmBitDepth = this._settings.IosRecorderSettings.LinearPcmBitDepth,
                LinearPcmNonInterleaved = this._settings.IosRecorderSettings.LinearPcmNonInterleaved,
                // SampleRateConverterAlgorithm = (AVAudioQuality?)this._settingsNew.IosRecorderSettings.SampleRateConverterAudioQuality,
                EncoderBitRatePerChannel = this._settings.IosRecorderSettings.EncoderBitRatePerChannel,
                SampleRateConverterAudioQuality = (AVAudioQuality?)this._settings.IosRecorderSettings.SampleRateConverterAudioQuality,
                // EncoderAudioQualityForVBR = this._settingsNew.IosRecorderSettings.vb,
                //SampleRate = (int)this._settings.RecordQuality,
            };
        }
        
        private void InitTempFilePath()
        {
            // init a temp file
            var path = Path.GetTempPath();
            var audioFilePath = Path.Combine(path, Guid.NewGuid().ToString("N"));
            this._path = audioFilePath;
        }

        public void Dispose()
        {
            this._recorder?.Dispose();
            this._url?.Dispose();
        }
    }
}