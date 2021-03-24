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
        private readonly RecordFormat _recordFormat;
        
        private MediaRecorder _recorder;
        private TaskCompletionSource<Stream> _recordTask;
        private readonly string _filePath;
        public bool IsRecording { get; private set; }
        
        public NightlyRecorderService(RecordFormat recordFormat)
        {
            this._recordFormat = recordFormat;
            this._filePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        }

        private void StartRecorder(int sampleRate)
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
                this.SetupRecorderFor(this._recordFormat, sampleRate);
                this._recorder.SetOutputFile(this._filePath);
                this._recorder.Prepare();
                this._recorder.Start();
            }
            catch (JavaException e)
            {
                throw new RecorderNativeException(e, e.Message);
            }
        }

        private void SetupRecorderFor(RecordFormat format, int sampleRate)
        {
            switch (format)
            {
                // case RecordFormat.Aac:
                //     this._recorder.SetOutputFormat(OutputFormat.Ogg);
                //     this._recorder.SetAudioEncoder(AudioEncoder.Default);
                //     break;
                case RecordFormat.Mp4Aac:
                    this._recorder.SetOutputFormat(OutputFormat.Mpeg4);
                    this._recorder.SetAudioEncoder(AudioEncoder.Aac);
                    this._recorder.SetAudioSamplingRate(sampleRate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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

        public Task<Stream> RecordAsync(int sampleRate = 44100)
        {
            this.IsRecording = true;
            this.StartRecorder(sampleRate);

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