using System;
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
        private readonly RecordFormat _recordFormat;
        private AVAudioRecorder _recorder;
        private NSUrl _url;
        private readonly string _path;
        private TaskCompletionSource<Stream> _recordTask;

        public bool IsRecording => this._recorder?.Recording ?? false;
        

        public NightlyRecorderService(RecordFormat recordFormat)
        {
            this._recordFormat = recordFormat;

            // init a temp file
            var path = Path.GetTempPath();
            var audioFilePath = Path.Combine(path, Guid.NewGuid().ToString("N"));
            this._path = audioFilePath;
        }

        public Task<Stream> RecordAsync(int sampleRate = 44100)
        {
            if (this.IsRecording)
                throw new Exception("Service already recording.");

            this.InitializeRecordService(sampleRate);
            this._recorder.Record();
            this._recordTask = new TaskCompletionSource<Stream>();
            return this._recordTask.Task;
        }


        public void Stop()
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

        private void InitializeRecordService(int bitRate)
        {
            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
                throw new RecorderNativeException(err, err.LocalizedFailureReason);

            err = audioSession.SetActive(true);

            if (err != null)
                throw new RecorderNativeException(err, err.LocalizedFailureReason);

            this._url = NSUrl.FromFilename(this._path);
            var settings = this.GetAudioSettings(bitRate);

            if (this._recorder == null)
            {
                this._recorder = AVAudioRecorder.Create(this._url, settings, out var error);

                if (error != null)
                    throw new RecorderNativeException(error, error.LocalizedFailureReason);
            }

            var ready = this._recorder.PrepareToRecord();
            if (!ready)
                throw new Exception("PrepareToRecord() returned false");
        }

        /// <summary>
        /// Retrieve audio settins for used audio format type
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private AudioSettings GetAudioSettings(int sampleRate)
        {
            if (this._recordFormat == RecordFormat.Mp4Aac)
            {
                return new AudioSettings
                {
                    Format = AudioFormatType.MPEG4AAC,
                    NumberChannels = 1,
                    SampleRate = sampleRate,
                };
            }

            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this._recorder?.Dispose();
            this._url?.Dispose();
        }
    }
}