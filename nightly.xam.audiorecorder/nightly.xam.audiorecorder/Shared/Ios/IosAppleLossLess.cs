namespace nightly.xam.audiorecorder.Shared.Ios
{
    public class IosAppleLossLess : IIosRecorderSettings
    {
        public IosAudioFormat AudioFormat { get; set; } = IosAudioFormat.AppleLossless;
        public double? SampleRate { get; set; }
        public int? NumberChannels { get; set; } = 1;
        public int? LinearPcmBitDepth { get; set; } = 16;
        public bool? LinearPcmBigEndian { get; set; }
        public bool? LinearPcmFloat { get; set; }
        public bool? LinearPcmNonInterleaved { get; set; }
        public IosAudioQuality? AudioQuality { get; set; }
        public IosAudioQuality? SampleRateConverterAudioQuality { get; set; } 
        public int? EncoderBitRate { get; set; }
        public int? EncoderBitRatePerChannel { get; set; }
        public int? EncoderBitDepthHint { get; set; }
    }
}