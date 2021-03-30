namespace nightly.xam.audiorecorder.Shared.Ios
{
    public static class IosExtensions
    {
        /// <summary>
        /// Setup sample rate
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public static IIosRecorderSettings WithSamplingRate(this IIosRecorderSettings setting, int sampleRate)
        {
            setting.SampleRate = sampleRate;
            return setting;
        }

        /// <summary>
        /// Setup sample rate
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static IIosRecorderSettings WithSamplingRate(this IIosRecorderSettings setting, SampleRate rate)
        {
            setting.SampleRate = (int)rate;
            return setting;
        }
    }
}