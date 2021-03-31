namespace nightly.xam.audiorecorder.Shared.Droid
{
    public class Droid3GppAar : IDroidRecorderSettings
    {
        public DroidAudioEncoder AudioEncoder { get; } = DroidAudioEncoder.Aac;
        public DroidOutPutFormat OutPutFormat { get; } = DroidOutPutFormat.ThreeGpp;
        public int? SamplingRate { get; set; }
        public int? AudioChannels { get; set; }
        public int? CaptureRate { get; set; }
        public int? AudioEncodingBitRate { get; set; }
    }
}