namespace nightly.xam.audiorecorder.Shared.Ios
{
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

        int? EncoderBitRatePerChannel { get; set;}

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
}