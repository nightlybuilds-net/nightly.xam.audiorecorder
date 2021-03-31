namespace nightly.xam.audiorecorder.Shared.Droid
{
    public static class DroidExtensions
    {
        /// <summary>
        /// Setup sample rate
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public static IDroidRecorderSettings WithSamplingRate(this IDroidRecorderSettings setting, int sampleRate)
        {
            setting.SamplingRate = sampleRate;
            return setting;
        }

        /// <summary>
        /// Setup sample rate
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static IDroidRecorderSettings WithSamplingRate(this IDroidRecorderSettings setting, SampleRate rate)
        {
            setting.SamplingRate = (int)rate;
            return setting;
        }
    }
}