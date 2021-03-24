using System;
using System.IO;
using System.Threading.Tasks;

namespace nightly.xam.audiorecorder.Shared
{
    public interface IRecorder : IDisposable
    {
        /// <summary>
        /// Start registration
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        Task<Stream> RecordAsync(int sampleRate = 44100);

        /// <summary>
        /// Stop registration
        /// </summary>
        void Stop();

        /// <summary>
        /// Is recording
        /// </summary>
        bool IsRecording { get; }
    }
}