using System;

namespace nightly.xam.audiorecorder.Exceptions
{
    /// <summary>
    /// Exception that incapsulate NativeException
    /// </summary>
    public class RecorderException : Exception
    {
        public object NativeException { get; }

        public RecorderException(object nativeException) : base("See NativeException property")
        {
            this.NativeException = nativeException;
        }
        
        public RecorderException(object nativeException, string message) : base(message)
        {
            this.NativeException = nativeException;
        }
    }
}