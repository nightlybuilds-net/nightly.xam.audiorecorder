using nightly.xam.audiorecorder.Shared.Droid;
using nightly.xam.audiorecorder.Shared.Ios;

namespace nightly.xam.audiorecorder.Shared
{
    public class RecorderSettings
    {
        public IIosRecorderSettings IosRecorderSettings { get; set; }
        public IDroidRecorderSettings DroidRecorderSettings { get; set; }

        /// <summary>
        /// Default settings is MP4AAR
        /// </summary>
        public static RecorderSettings Default => new RecorderSettings
        {
            IosRecorderSettings = new IosMp4Aar(),
            DroidRecorderSettings = new DroidMp4Aar()
        };

        /// <summary>
        /// Set samplerate o ios and droid
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public RecorderSettings WithSampleRate(SampleRate sampleRate)
        {
            this.IosRecorderSettings.WithSamplingRate(sampleRate);
            this.DroidRecorderSettings.WithSamplingRate(sampleRate);
            return this;
        }
        
        /// <summary>
        /// Set samplerate o ios and droid
        /// </summary>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public RecorderSettings WithSampleRate(int sampleRate)
        {
            this.IosRecorderSettings.WithSamplingRate(sampleRate);
            this.DroidRecorderSettings.WithSamplingRate(sampleRate);
            return this;
        }
    }
}