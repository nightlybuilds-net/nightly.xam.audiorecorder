using System;
using System.IO;
using System.Threading.Tasks;
using Android.Media;
using Stream = System.IO.Stream;

namespace nightly.xam.audiorecorder
{
    public partial class NightlyAudioService : IRecorder 
    {
        string _filePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "mic_record.mp4");

        MediaRecorder _recorder;
        private TaskCompletionSource<Stream> _recordTask;

        
        public bool IsRecording { get; private set; }

        private void StartRecorder()
        {
            if (File.Exists(this._filePath))
                File.Delete(this._filePath);

            Java.IO.File myFile = new Java.IO.File(this._filePath);
            myFile.CreateNewFile();

            if (this._recorder == null)
                this._recorder = new MediaRecorder(); 
            else
                this._recorder.Reset();

            this._recorder.SetAudioSource(AudioSource.Mic);
            this._recorder.SetOutputFormat(OutputFormat.Mpeg4);
            this._recorder.SetAudioEncoder(AudioEncoder.AmrNb);
            this._recorder.SetOutputFile(this._filePath);
            this._recorder.Prepare();
            this._recorder.Start();
        }

        private void StopRecorder()
        {
            if (this._recorder != null)
            {
                this._recorder.Stop();
                this._recorder.Release();
                this._recorder = null;
            }

            this.IsRecording = false;
        }

        public Task<Stream> StartAsync()
        {
            this.IsRecording = true;
            this.StartRecorder();

            this._recordTask = new TaskCompletionSource<Stream>();
            return this._recordTask.Task;
        }

        public void Stop()
        {
            this.StopRecorder();
            var stream = this.GetAudioFileStream();
            this._recordTask.TrySetResult(stream); 
        }


        public Stream GetAudioFileStream()
        {
            return !File.Exists(this._filePath) ? null : File.OpenRead(this._filePath);
        }
    }
}