namespace nightly.xam.audiorecorder.Shared.Droid
{
    public interface IDroidRecorderSettings
    {
        DroidAudioEncoder AudioEncoder { get; }
        DroidOutPutFormat OutPutFormat { get; }
        int? SamplingRate { get; set; }
        int? AudioChannels { get; set; }
        int? CaptureRate { get; set; }
        int? AudioEncodingBitRate { get; set; }
    }
}