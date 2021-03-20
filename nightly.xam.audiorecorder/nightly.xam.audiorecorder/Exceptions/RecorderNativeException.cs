using System;

namespace nightly.xam.audiorecorder.Exceptions
{
    /// <summary>
    /// Exception that incapsulate NativeException
    /// </summary>
    public class RecorderNativeException : Exception
    {
        public object NativeException { get; }

        public RecorderNativeException(object nativeException)
        {
            this.NativeException = nativeException;
        }
        
        public RecorderNativeException(object nativeException, string message) : base(message)
        {
            this.NativeException = nativeException;
        }
    }
}