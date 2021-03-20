using System;
using System.IO;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;
using nightly.xam.audiorecorder.Exceptions;

namespace nightly.xam.audiorecorder
{
    public partial class NightlyRecorderService : IRecorder 
    {
        private AVAudioRecorder _recorder;
        private NSUrl _url;
        private NSDictionary _settings;
        private string _path;
        private TaskCompletionSource<Stream> _recordTask;

        public bool IsRecording => this._recorder?.Recording ?? false; 


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
                this._recordTask.TrySetResult(memstream);
            }

            File.Delete(this._path);
        }

        private void InitializeRecordService()
        {
            var audioSession = AVAudioSession.SharedInstance ();
            var err = audioSession.SetCategory (AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
                throw new RecorderNativeException(err, err.LocalizedFailureReason);
            
            err = audioSession.SetActive(true);
            
            if (err != null)
                throw new RecorderNativeException(err, err.LocalizedFailureReason);

            var path = Path.GetTempPath();

            var fileName = $"audio{DateTime.Now:yyyyMMddHHmmss}.mp4";
            var audioFilePath = Path.Combine (path, fileName);

            Console.WriteLine("Audio File Path: " + audioFilePath);
            this._path = audioFilePath;

            this._url = NSUrl.FromFilename(audioFilePath);
            NSObject[] values = 
            {
                NSNumber.FromFloat (12000.0f), //Sample Rate
                NSNumber.FromInt32 ((int)AudioToolbox.AudioFormatType.MPEG4AAC), //AVFormat
                NSNumber.FromInt32 (1), //Channels
                NSNumber.FromInt32 (16), //PCMBitDepth
                NSNumber.FromBoolean (false), //IsBigEndianKey
                NSNumber.FromBoolean (false) //IsFloatKey
            };

            NSObject[] keys = 
            {
                AVAudioSettings.AVSampleRateKey,
                AVAudioSettings.AVFormatIDKey,
                AVAudioSettings.AVNumberOfChannelsKey,
                AVAudioSettings.AVLinearPCMBitDepthKey,
                AVAudioSettings.AVLinearPCMIsBigEndianKey,
                AVAudioSettings.AVLinearPCMIsFloatKey
            };

            this._settings = NSDictionary.FromObjectsAndKeys (values, keys);
            this._recorder = AVAudioRecorder.Create(this._url, new AudioSettings(this._settings), out var error);
            
            if(error != null)
                throw new RecorderNativeException(error, error.LocalizedFailureReason);

            var ready = this._recorder.PrepareToRecord();
            if (!ready)
                throw new Exception("PrepareToRecord() returned false");
        }
    }
}