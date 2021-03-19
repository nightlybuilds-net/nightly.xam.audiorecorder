using System;
using System.IO;
using System.Threading.Tasks;

namespace nightly.xam.audiorecorder
{
    public partial class NightlyAudioService : IRecorder 
    {
        public Task<Stream> StartAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public bool IsRecording => throw new NotImplementedException();
        public Stream GetAudioFileStream()
        {
            throw new System.NotImplementedException();
        }
    }
}