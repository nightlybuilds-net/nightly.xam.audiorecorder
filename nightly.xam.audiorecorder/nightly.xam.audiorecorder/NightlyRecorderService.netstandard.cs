using System;
using System.IO;
using System.Threading.Tasks;

namespace nightly.xam.audiorecorder
{
    public partial class NightlyRecorderService : IRecorder 
    {
        public Task<Stream> RecordAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public bool IsRecording => throw new NotImplementedException();
       
    }
}