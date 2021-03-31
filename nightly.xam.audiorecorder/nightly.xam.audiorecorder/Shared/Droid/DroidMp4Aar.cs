namespace nightly.xam.audiorecorder.Shared.Droid
{
    public class DroidMp4Aar : IDroidRecorderSettings
    {
        public DroidAudioEncoder AudioEncoder { get; } = DroidAudioEncoder.Aac;
        public DroidOutPutFormat OutPutFormat { get; } = DroidOutPutFormat.Mpeg4;
        public int? SamplingRate { get; set; }
        public int? AudioChannels { get; set; }
        public int? CaptureRate { get; set; }
        public int? AudioEncodingBitRate { get; set; }
    }
}