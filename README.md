# nightly.xam.audiorecorder
## _Xamarin Forms Audio Recorder Library_

## Features

- Ultra easy to use
- Task-based asynchronous pattern
- Register compressed audio
- Highly customizable
- Not enought??

## Installation

Add nighlty.xam.audiorecorder nuget package to forms and native (iOS and Droid) apps  [HERE]("https://www.nuget.org/packages/nightly.xam.audiorecorder/")

```sh
dotnet add package nightly.xam.audiorecorder 
```

## Usage
[See Sample project in this repo.]("https://github.com/nightlybuilds-net/nightly.xam.audiorecorder/tree/main/nightly.xam.audiorecorder/nightly.xam.audiorecorder.forms")
Just create a record service:
```sh
this._recordService = new NightlyRecorderService();
```
Start record audio:
```sh
var streamFile = await this._recordService.RecordAsync();
this._stream = streamFile;
```
Stop Record
```sh
this._recordService.Stop();
```

**Isn't it easy?**

The default output stream is MP4 AAR.
You can easily customize SampleRate in this way:

```sh
this._recordService = new RecorderSettings.Default.WithSampleRate(SampleRate.Low);
```

or you can use others ready to use configurations in this way:
```sh
 this._recordService = new NightlyRecorderService(new RecorderSettings
  {
     IosRecorderSettings = new IosAppleLossLess(),
     DroidRecorderSettings = new DroidOggOpus()
 });
```

## API

You can customize iOS and Android record settings implementing the following interfaces

```sh
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
    }
	
	
	public interface IDroidRecorderSettings
    {
        DroidAudioEncoder AudioEncoder { get; }
        DroidOutPutFormat OutPutFormat { get; }
        int? SamplingRate { get; set; }
        int? AudioChannels { get; set; }
        int? CaptureRate { get; set; }
        int? AudioEncodingBitRate { get; set; }
    }
```

After implementing those interfaces you can create a record service:

```sh
this._recordService = new NightlyRecorderService(new RecorderSettings
  {
     IosRecorderSettings = new YourIosImplementation(),
     DroidRecorderSettings = new YourDroidImplementation()
 });
```

## OOB Record Configuration

There are some ready to use implementation:

**iOS**
- IosAppleLossLess
- IosFlac
- IosMp4Aar

**Droid**
- Droid3GppAar
- DroidOggOpus
- DroidMp4Aar


## License

MIT

**Free Software, Hell Yeah!**

