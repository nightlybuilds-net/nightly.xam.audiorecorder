using System;
using System.IO;
using System.Threading.Tasks;
using nightly.xam.audiorecorder.Shared;

namespace nightly.xam.audiorecorder
{
    public partial class NightlyRecorderService : IRecorder 
    {

        public NightlyRecorderService()
        {
            
        }

        public NightlyRecorderService(RecorderSettings settings)
        {
        }
        
        public Task<Stream> RecordAsync()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public bool IsRecording => throw new NotImplementedException();

        public void Dispose()
        {
        }
    }
}