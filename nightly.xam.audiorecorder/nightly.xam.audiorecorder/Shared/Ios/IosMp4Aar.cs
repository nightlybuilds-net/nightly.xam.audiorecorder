namespace nightly.xam.audiorecorder.Shared.Ios
{
    public class IosMp4Aar : IIosRecorderSettings
    {
        public IosAudioFormat AudioFormat { get; } = IosAudioFormat.MPEG4AAC;
        public double? SampleRate { get; private set; }
        public int? NumberChannels { get; } = 1;
        public int? LinearPcmBitDepth { get; } = 16;
        public bool? LinearPcmBigEndian { get; }
        public bool? LinearPcmFloat { get; }
        public bool? LinearPcmNonInterleaved { get; }
        public IosAudioQuality? AudioQuality { get; }
        public IosAudioQuality? SampleRateConverterAudioQuality { get; } 
        public int? EncoderBitRate { get; }
        public int? EncoderBitRatePerChannel { get; }
        public int? EncoderBitDepthHint { get; set; }

        
        /// <summary>
        /// Customize sample rate
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public IosMp4Aar WithSampleRate(double rate)
        {
            this.SampleRate = rate;
            return this;
        }
    }
}