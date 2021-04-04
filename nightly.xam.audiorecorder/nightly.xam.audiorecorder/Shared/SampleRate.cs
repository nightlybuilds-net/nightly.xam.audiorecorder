namespace nightly.xam.audiorecorder.Shared
{
    public enum SampleRate
    {
        /// <summary>
        /// 8000 Hz
        /// Telephone and encrypted walkie-talkie, wireless intercom and wireless microphone transmission; adequate for human speech but without sibilance
        /// </summary>
        _8000 = 8000,
        
        /// <summary>
        /// 11025
        /// One quarter the sampling rate of audio CDs; used for lower-quality PCM, MPEG audio and for audio analysis of subwoofer bandpasses
        /// </summary>
        _11025 = 11025,
        
        /// <summary>
        /// 16000 hz
        /// Wideband frequency extension over standard telephone narrowband 8,000 Hz. Used in most modern VoIP and VVoIP communication products
        /// </summary>
        _16000 = 16000,
        
        /// <summary>
        /// 22050 Hz
        /// One half the sampling rate of audio CDs; used for lower-quality PCM and MPEG audio and for audio analysis of low frequency energy. Suitable for digitizing early 20th century audio formats such as 78s and AM Radio.
        /// </summary>
        _22050 = 22050,
        
        /// <summary>
        /// 32000 Hz
        /// miniDV digital video camcorder, video tapes with extra channels of audio (e.g. DVCAM with four channels of audio), DAT (LP mode), Germany's Digitales Satellitenradio, NICAM digital audio, used alongside analogue television sound in some countries. High-quality digital wireless microphones.[16] Suitable for digitizing FM radio.[citation needed]
        /// </summary>
        _32000 = 32000,
        
        /// <summary>
        /// 37800 Hz
        /// CD-XA audio
        /// </summary>
        _37800 = 37800,
        
        /// <summary>
        /// 44100 Hz
        /// Audio CD, also most commonly used with MPEG-1 audio (VCD, SVCD, MP3). Originally chosen by Sony because it could be recorded on modified video equipment running at either 25 frames per second (PAL) or 30 frame/s (using an NTSC monochrome video recorder) and cover the 20 kHz bandwidth thought necessary to match professional analog recording equipment of the time. A PCM adaptor would fit digital audio samples into the analog video channel of, for example, PAL video tapes using 3 samples per line, 588 lines per frame, 25 frames per second.
        /// </summary>
        _44100 = 44100,
        
        /// <summary>
        /// 48000 Hz
        /// The standard audio sampling rate used by professional digital video equipment such as tape recorders, video servers, vision mixers and so on. This rate was chosen because it could reconstruct frequencies up to 22 kHz and work with 29.97 frames per second NTSC video – as well as 25 frame/s, 30 frame/s and 24 frame/s systems. With 29.97 frame/s systems it is necessary to handle 1601.6 audio samples per frame delivering an integer number of audio samples only every fifth video frame.[9]  Also used for sound with consumer video formats like DV, digital TV, DVD, and films. The professional Serial Digital Interface (SDI) and High-definition Serial Digital Interface (HD-SDI) used to connect broadcast television equipment together uses this audio sampling frequency. Most professional audio gear uses 48 kHz sampling, including mixing consoles, and digital recording devices.
        /// </summary>
        _48000 = 48000,
        
        /// <summary>
        /// 50000 Hz
        /// First commercial digital audio recorders from the late 70s from 3M and Soundstream.
        /// </summary>
        // _50000 = 50000
        
    }
}