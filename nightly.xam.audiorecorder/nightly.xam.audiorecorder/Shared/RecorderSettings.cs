using nightly.xam.audiorecorder.Ios;

namespace nightly.xam.audiorecorder.Shared
{
    public class RecorderSettings
    {
        public IIosRecorderSettings IosRecorderSettings { get; set; }
        // public IIosRecorderSettings DroidRecorderSettings { get; set; }
    }

    public interface IIosRecorderSettings
    {
        IosAudioFormat AudioFormat { get; set; }
        double? SampleRate { get; set; }

        int? NumberChannels { get; set; }

        int? LinearPcmBitDepth { get; set; }

        bool? LinearPcmBigEndian { get; set; }

        bool? LinearPcmFloat { get; set; }

        bool? LinearPcmNonInterleaved { get; set; }

        IosAudioQuality? AudioQuality { get; set; }

        IosAudioQuality? SampleRateConverterAudioQuality { get; set; }

        int? EncoderBitRate { get; set; }

        int? EncoderBitRatePerChannel { get; set; }

        int? EncoderBitDepthHint { get; set; }

        // [iOS(7, 0)]
        // public AVAudioBitRateStrategy? BitRateStrategy
        //
        // [iOS(7, 0)]
        // public AVSampleRateConverterAlgorithm? SampleRateConverterAlgorithm
        //
        // [iOS(7, 0)]
        // public AVAudioQuality? EncoderAudioQualityForVBR
        //
    }

    public class Mp4Aar : IIosRecorderSettings
    {
        public IosAudioFormat AudioFormat { get; set; } = IosAudioFormat.MPEG4AAC;
        public double? SampleRate { get; set; }
        public int? NumberChannels { get; set; } = 1;
        public int? LinearPcmBitDepth { get; set; } = 16;
        public bool? LinearPcmBigEndian { get; set; }
        public bool? LinearPcmFloat { get; set; }
        public bool? LinearPcmNonInterleaved { get; set; }
        public IosAudioQuality? AudioQuality { get; set; }
        public IosAudioQuality? SampleRateConverterAudioQuality { get; set; } = IosAudioQuality.Min;
        public int? EncoderBitRate { get; set; }
        public int? EncoderBitRatePerChannel { get; set; }
        public int? EncoderBitDepthHint { get; set; }
    }
}