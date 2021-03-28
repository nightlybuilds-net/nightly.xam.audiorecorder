namespace nightly.xam.audiorecorder.Shared.Ios
{
    public interface IIosRecorderSettings
    {
        IosAudioFormat AudioFormat { get;  }
        double? SampleRate { get; }

        int? NumberChannels { get; }

        int? LinearPcmBitDepth { get; }

        bool? LinearPcmBigEndian { get; }

        bool? LinearPcmFloat { get; }

        bool? LinearPcmNonInterleaved { get;  }

        IosAudioQuality? AudioQuality { get; }

        IosAudioQuality? SampleRateConverterAudioQuality { get; }

        int? EncoderBitRate { get; }

        int? EncoderBitRatePerChannel { get; }

        int? EncoderBitDepthHint { get; }

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