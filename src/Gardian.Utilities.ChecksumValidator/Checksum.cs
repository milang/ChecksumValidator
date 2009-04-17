using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Gardian.Utilities.ChecksumValidator
{

    /// <summary>
    /// Checksum computation implementation.
    /// </summary>
    internal static class Checksum
    {

        //**************************************************
        //* Public interface
        //**************************************************

        //-------------------------------------------------
        /// <summary>
        /// Compute checksum for the specified source file,
        /// return it as hex string ("F3BA87...").
        /// </summary>
        /// <remarks>
        /// This method will be run on a worker thread
        /// (from thread pool) via async invocation.
        /// </remarks>
        public static string ComputeChecksum(string sourceFile, ChecksumMethod method, Action<decimal> progressNotifier)
        {
            System.Threading.Thread.Sleep(100);
            using (var hashAlgorithm = CreateHashAlgorithm(method))
            using (var source = new TrackingStream(File.OpenRead(sourceFile), progressNotifier))
            {
                var hash = hashAlgorithm.ComputeHash(source);

                var msg = new StringBuilder(128);
                foreach (var byteValue in hash)
                {
                    msg.AppendFormat(byteValue.ToString("X2", CultureInfo.InvariantCulture));
                }
                return msg.ToString();
            }
        }


        //-------------------------------------------------
        /// <summary>
        /// Flag that requests cancellation of the current computation
        /// (if set to true and a computation is ongoing).
        /// </summary>
        /// <remarks>
        /// Field marked as volatile because it will be accessed
        /// by multiple threads (UI thread to set it, worker
        /// thread to check it).
        /// </remarks>
        public static volatile bool Cancel;




        //**************************************************
        //* Private
        //**************************************************

        //-------------------------------------------------
        /// <summary>
        /// Find hash algorithm that corresponds to the
        /// requested method
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown
        /// when requested method is not recognized.</exception>
        private static HashAlgorithm CreateHashAlgorithm(ChecksumMethod method)
        {
            switch (method)
            {
                case ChecksumMethod.SHA1: return new SHA1CryptoServiceProvider();
                case ChecksumMethod.MD5: return new MD5CryptoServiceProvider();
                case ChecksumMethod.CRC32: return new CRC32();
                default: throw new NotSupportedException(string.Concat("Requested checksum method ", method, " is not supported"));
            }
        }


        //-------------------------------------------------
        /// <summary>
        /// Custom stream for hash algorithm that only implements
        /// Read &amp; Dispose methods (every other method will
        /// throw NotSupportedException). This class implements
        /// a bridge pattern (both Read and Dispose are forwarded
        /// to the underlying "tracked" stream). The only extra
        /// functionality is that if Read operation read another
        /// megabyte of data since last progress notifier invocation,
        /// progress percentage is recomputed and progress notifier
        /// is called with this value. This allows us to track
        /// progress of hash computation even though HashAlgorithm
        /// does not support this.
        /// </summary>
        private sealed class TrackingStream : Stream
        {
            public TrackingStream(Stream trackedStream, Action<decimal> progressNotifier)
            {
                if (trackedStream == null) { throw new ArgumentNullException("trackedStream"); }
                this._trackedStream = trackedStream;
                this._progressNotifier = progressNotifier;
            }
            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                if (disposing)
                {
                    this._trackedStream.Dispose();
                }
            }
            public override void Flush()
            {
                throw new NotImplementedException();
            }
            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }
            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }
            public override int Read(byte[] buffer, int offset, int count)
            {
                var ret = this._trackedStream.Read(buffer, offset, count);

                if (Checksum.Cancel)
                {
                    throw new ThreadInterruptedException();
                }
                if (this._progressNotifier != null)
                {
                    var position = this._trackedStream.Position;
                    const long mb = 2L << 19;
                    var megabyte = position / mb;
                    if (megabyte > this._lastMegabyte)
                    {
                        this._lastMegabyte = megabyte;
                        var percentage = ((decimal)position / this._trackedStream.Length);
                        this._progressNotifier(percentage);
                    }
                }
                return ret;
            }
            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }
            public override bool CanRead
            {
                get { throw new NotImplementedException(); }
            }
            public override bool CanSeek
            {
                get { throw new NotImplementedException(); }
            }
            public override bool CanWrite
            {
                get { throw new NotImplementedException(); }
            }
            public override long Length
            {
                get { throw new NotImplementedException(); }
            }
            public override long Position
            {
                get { throw new NotImplementedException(); }
                set { throw new NotImplementedException(); }
            }

            private long _lastMegabyte;
            private readonly Action<decimal> _progressNotifier;
            private readonly Stream _trackedStream;

        }

    }

}