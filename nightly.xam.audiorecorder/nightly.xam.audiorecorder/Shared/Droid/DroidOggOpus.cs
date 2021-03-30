namespace nightly.xam.audiorecorder.Shared.Droid
{
    public class DroidOggOpus : IDroidRecorderSettings
    {
        public DroidAudioEncoder AudioEncoder { get; } = DroidAudioEncoder.Opus;
        public DroidOutPutFormat OutPutFormat { get; } = DroidOutPutFormat.Ogg;
        public int? SamplingRate { get; set; }
        public int? AudioChannels { get; set; }
        public int? CaptureRate { get; set; }
        public int? AudioEncodingBitRate { get; set; }
    }
}