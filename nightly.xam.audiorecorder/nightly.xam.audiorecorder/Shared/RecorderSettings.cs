namespace nightly.xam.audiorecorder.Shared
{
    public class RecorderSettings
    {
        /// <summary>
        /// Format for Droid Platform
        /// </summary>
        public DroidRecordFormat DroidRecordFormat { get; set; }
        
        /// <summary>
        /// Format for IOs Platform
        /// </summary>
        public IosRecordFormat IosRecordFormat { get; set; }

        /// <summary>
        /// Sample rate quality
        /// </summary>
        public RecordQuality RecordQuality { get; set; }

        /// <summary>
        /// Default settings. MP4 AAC Medium sample rate
        /// </summary>
        public static RecorderSettings Default => new RecorderSettings
        {
            RecordQuality = RecordQuality.Medium,
            IosRecordFormat = IosRecordFormat.Mp4Aac,
            DroidRecordFormat = DroidRecordFormat.Mp4Aac
        };
    }
}