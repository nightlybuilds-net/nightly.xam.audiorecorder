using System.IO;
using System.Threading.Tasks;

namespace nightly.xam.audiorecorder.Shared
{
    public interface IRecorder
    {
        /// <summary>
        /// Start registration
        /// </summary>
        /// <returns></returns>
        Task<Stream> RecordAsync();

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