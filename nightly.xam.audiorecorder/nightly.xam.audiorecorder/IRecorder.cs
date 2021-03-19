using System.IO;
using System.Threading.Tasks;

namespace nightly.xam.audiorecorder
{
    public interface IRecorder
    {
        /// <summary>
        /// Start registration
        /// </summary>
        /// <returns></returns>
        Task<Stream> StartAsync ();

        /// <summary>
        /// Stop registration
        /// </summary>
        void Stop ();
        
        /// <summary>
        /// Is recording
        /// </summary>
        bool IsRecording { get; }

        /// <summary>
        /// Get recorded file stream
        /// </summary>
        /// <returns></returns>
        Stream GetAudioFileStream();
    }
}