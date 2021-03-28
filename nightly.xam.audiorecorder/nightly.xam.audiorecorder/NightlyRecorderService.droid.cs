using System;
using System.IO;
using System.Threading.Tasks;
using Android.Media;
using Java.Interop;
using nightly.xam.audiorecorder.Exceptions;
using nightly.xam.audiorecorder.Shared;
using Stream = System.IO.Stream;

namespace nightly.xam.audiorecorder
{
    public partial class NightlyRecorderService : IRecorder
    {
        private readonly RecorderSettings _settings;

        private MediaRecorder _recorder;
        private TaskCompletionSource<Stream> _recordTask;
        private readonly string _filePath;
        public bool IsRecording { get; private set; }

        public NightlyRecorderService()
        {
            this._settings = RecorderSettings.Default;
            this._filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        }

        public NightlyRecorderService(RecorderSettings settings)
        {
            this._settings = settings;
            this._filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        }
        
      
        private void StartRecorder()
        {
            try
            {
                if(File.Exists(this._filePath))
                    File.Delete(this._filePath);
                
                var myFile = new Java.IO.File(this._filePath);
                myFile.CreateNewFile();

                if (this._recorder == null)
                    this._recorder = new MediaRecorder();
                else
                    this._recorder.Reset();

                this._recorder.SetAudioSource(AudioSource.Mic);
                this.SetupRecorder();
                this._recorder.SetOutputFile(this._filePath);
                this._recorder.Prepare();
                this._recorder.Start();
            }
            catch (JavaException e)
            {
                throw new RecorderNativeException(e, e.Message);
            }
        }

        private void SetupRecorder()
        {
            this._recorder.SetOutputFormat((OutputFormat) this._settings.DroidRecorderSettings.OutPutFormat);
            this._recorder.SetAudioEncoder((AudioEncoder) this._settings.DroidRecorderSettings.AudioEncoder);
            
            if(this._settings.DroidRecorderSettings.SamplingRate.HasValue)
                this._recorder.SetAudioSamplingRate(this._settings.DroidRecorderSettings.SamplingRate.Value);
            
            if(this._settings.DroidRecorderSettings.AudioChannels.HasValue)
                this._recorder.SetAudioChannels(this._settings.DroidRecorderSettings.AudioChannels.Value);
            
            if(this._settings.DroidRecorderSettings.CaptureRate.HasValue)
                this._recorder.SetCaptureRate(this._settings.DroidRecorderSettings.CaptureRate.Value);
            
            if(this._settings.DroidRecorderSettings.AudioEncodingBitRate.HasValue)
                this._recorder.SetAudioEncodingBitRate(this._settings.DroidRecorderSettings.AudioEncodingBitRate.Value);
            
            
            // switch (format)
            // {
            //     case DroidRecordFormat.ThreeGpAcc:
            //         this._recorder.SetOutputFormat(OutputFormat.ThreeGpp);
            //         this._recorder.SetAudioEncoder(AudioEncoder.Aac);
            //         // this._recorder.SetAudioSamplingRate(sampleRate);
            //         break;
            //     case DroidRecordFormat.Mp4Aac:
            //         this._recorder.SetOutputFormat(OutputFormat.Mpeg4);
            //         this._recorder.SetAudioEncoder(AudioEncoder.Aac);
            //         //this._recorder.SetAudioSamplingRate(sampleRate);
            //         break;
            //     case DroidRecordFormat.Mp4HeAac:
            //         this._recorder.SetOutputFormat(OutputFormat.Mpeg4);
            //         this._recorder.SetAudioEncoder(AudioEncoder.HeAac);
            //         //this._recorder.SetAudioSamplingRate(sampleRate);
            //         break;
            //     case DroidRecordFormat.Mp4Vorbis:
            //         this._recorder.SetOutputFormat(OutputFormat.Mpeg4);
            //         this._recorder.SetAudioEncoder(AudioEncoder.Vorbis);
            //         //this._recorder.SetAudioSamplingRate(sampleRate);
            //         break;
            //     case DroidRecordFormat.OggOpus:
            //         this._recorder.SetOutputFormat(OutputFormat.Ogg);
            //         this._recorder.SetAudioEncoder(AudioEncoder.Opus);
            //         // sample rate omitted because of weird results
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }

        }

        private void StopRecorder()
        {
            try
            {
                if (this._recorder != null)
                {
                    this._recorder.Stop();
                    this._recorder.Release();
                    this._recorder = null;
                }

                this.IsRecording = false;
            }
            catch (JavaException e)
            {
                throw new RecorderNativeException(e, e.Message);
            }
        }

        public Task<Stream> RecordAsync()
        {
            this.IsRecording = true;
            this.StartRecorder();

            this._recordTask = new TaskCompletionSource<Stream>();
            return this._recordTask.Task;
        }

        public void Stop()
        {
            this.StopRecorder();
            var stream = !File.Exists(this._filePath) ? null : File.OpenRead(this._filePath);
            stream?.Seek(0, SeekOrigin.Begin);
            this._recordTask.TrySetResult(stream);
            
            File.Delete(this._filePath);
        }

        public void Dispose()
        {
            this._recorder?.Dispose();
        }
    }
}