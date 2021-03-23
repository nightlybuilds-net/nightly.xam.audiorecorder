using System;
using System.IO;
using System.Threading.Tasks;
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
        private NSDictionary _settings;
        private string _path;
        private TaskCompletionSource<Stream> _recordTask;

        public bool IsRecording => this._recorder?.Recording ?? false;

        public NightlyRecorderService(RecordFormat recordFormat)
        {
            this._recordFormat = recordFormat;
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
            if (!this.IsRecording || this._recorder == null)
                return;

            this._recorder.Stop();

            using (var streamReader = new StreamReader(this._path))
            {
                var memstream = new MemoryStream();
                streamReader.BaseStream.CopyTo(memstream);
                memstream.Seek(0, SeekOrigin.Begin);
                this._recordTask.TrySetResult(memstream);
            }

            File.Delete(this._path);
        }

        private void InitializeRecordService()
        {
            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
                throw new RecorderNativeException(err, err.LocalizedFailureReason);

            err = audioSession.SetActive(true);

            if (err != null)
                throw new RecorderNativeException(err, err.LocalizedFailureReason);

            var path = Path.GetTempPath();

            var fileName = this.GetFileName();
            var audioFilePath = Path.Combine(path, fileName);
            this._path = audioFilePath;

            this._url = NSUrl.FromFilename(audioFilePath);
            var values = this.GetAudioSettings();

            NSObject[] keys =
            {
                AVAudioSettings.AVSampleRateKey,
                AVAudioSettings.AVFormatIDKey,
                AVAudioSettings.AVNumberOfChannelsKey,
                AVAudioSettings.AVLinearPCMBitDepthKey,
                AVAudioSettings.AVLinearPCMIsBigEndianKey,
                AVAudioSettings.AVLinearPCMIsFloatKey
            };

            this._settings = NSDictionary.FromObjectsAndKeys(values, keys);
            this._recorder = AVAudioRecorder.Create(this._url, new AudioSettings(this._settings), out var error);

            if (error != null)
                throw new RecorderNativeException(error, error.LocalizedFailureReason);

            var ready = this._recorder.PrepareToRecord();
            if (!ready)
                throw new Exception("PrepareToRecord() returned false");
        }

        /// <summary>
        /// Retrieve audio settins for used audio format type
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private NSObject[] GetAudioSettings()
        {
            switch (this._recordFormat)
            {
                case RecordFormat.Wav:
                    NSObject[] wavSettings =
                    {
                        NSNumber.FromFloat(44100.0f),
                        NSNumber.FromInt32 ((int)AudioToolbox.AudioFormatType.LinearPCM),//AVFormat
                        NSNumber.FromInt32 (2), //Channels
                        NSNumber.FromInt32 (16), //PCMBitDepth 
                        NSNumber.FromBoolean (false), //IsBigEndianKey 
                        NSNumber.FromBoolean (false) //IsFloatKey
                    };
                    return wavSettings;
                case RecordFormat.Mp4:
                    NSObject[] mp4Settings =
                    {
                        NSNumber.FromFloat(12000.0f), //Sample Rate
                        NSNumber.FromInt32((int) AudioToolbox.AudioFormatType.MPEG4AAC), //AVFormat
                        NSNumber.FromInt32(1), //Channels
                        NSNumber.FromInt32(16), //PCMBitDepth
                        NSNumber.FromBoolean(false), //IsBigEndianKey
                        NSNumber.FromBoolean(false) //IsFloatKey
                    };
                    return mp4Settings;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private string GetFileName()
        {
            switch (this._recordFormat)
            {
                case RecordFormat.Wav:
                    return $"audio{DateTime.Now:yyyyMMddHHmmss}.wav";
                case RecordFormat.Mp4:
                    return $"audio{DateTime.Now:yyyyMMddHHmmss}.mp4";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}