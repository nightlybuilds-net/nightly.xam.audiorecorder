using System;
using System.IO;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;

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


        public Task<Stream> StartAsync()
        {
            if (this.IsRecording)
                throw new Exception("Service already recording.");
            
            this.InitializeRecordService();
            this._recorder.Record ();
            this._recordTask = new TaskCompletionSource<Stream>();
            return this._recordTask.Task;
        }

        public void Stop()
        {
            if (!this.IsRecording || this._recorder == null)
                return;
        
            this._recorder.Stop ();

            using (var streamReader = new StreamReader(this._path))
            {
                var memstream = new MemoryStream(); 
                streamReader.BaseStream.CopyTo (memstream);
                this._recordTask.TrySetResult(memstream);
            }

            //File.Delete(this._path);
        }

        private void InitializeRecordService()
        {
            var audioSession = AVAudioSession.SharedInstance ();
            var err = audioSession.SetCategory (AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
                throw new Exception(err.Code.ToString());
            
            err = audioSession.SetActive (true);
            
            if (err != null )
                throw new Exception(err.Code.ToString());


            //Declare string for application temp path and tack on the file extension
            var path = Path.GetTempPath();

            var fileName = string.Format ("audio{0}.mp4", DateTime.Now.ToString ("yyyyMMddHHmmss"));
            var audioFilePath = Path.Combine (path, fileName);

            Console.WriteLine("Audio File Path: " + audioFilePath);
            this._path = audioFilePath;

            this._url = NSUrl.FromFilename(audioFilePath);
            //set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
            NSObject[] values = 
            {
                NSNumber.FromFloat (12000.0f), //Sample Rate
                NSNumber.FromInt32 ((int)AudioToolbox.AudioFormatType.MPEG4AAC), //AVFormat
                NSNumber.FromInt32 (1), //Channels
                NSNumber.FromInt32 (16), //PCMBitDepth
                NSNumber.FromBoolean (false), //IsBigEndianKey
                NSNumber.FromBoolean (false) //IsFloatKey
            };

            //Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
            NSObject[] keys = 
            {
                AVAudioSettings.AVSampleRateKey,
                AVAudioSettings.AVFormatIDKey,
                AVAudioSettings.AVNumberOfChannelsKey,
                AVAudioSettings.AVLinearPCMBitDepthKey,
                AVAudioSettings.AVLinearPCMIsBigEndianKey,
                AVAudioSettings.AVLinearPCMIsFloatKey
            };

            //Set Settings with the Values and Keys to create the NSDictionary
            this._settings = NSDictionary.FromObjectsAndKeys (values, keys);

            //Set recorder parameters
            this._recorder = AVAudioRecorder.Create(this._url, new AudioSettings(this._settings), out var error);
            
            // todo check error

            //Set Recorder to Prepare To Record
            var ready = this._recorder.PrepareToRecord();
            if (!ready)
                throw new Exception("PrepareToRecord() returned false");
        }
    }
}