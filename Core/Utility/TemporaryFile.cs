﻿using System;
using System.IO;

namespace NuGetPe
{
    public sealed class TemporaryFile : IDisposable
    {
        public TemporaryFile(Stream stream, string? extension = null)
        {
            ArgumentNullException.ThrowIfNull(stream);

            if (string.IsNullOrWhiteSpace(extension) || extension[0] != '.')
            {
                FileName = Path.GetTempFileName();
            }
            else
            {
                FileName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + extension);
            }

            using var fstream = File.Open(FileName, FileMode.Create);
            stream.CopyTo(fstream);
            fstream.Flush();
        }

        public TemporaryFile(string path)
        {
            ArgumentNullException.ThrowIfNull(path);

            FileName = path;
        }

        public string FileName { get; }

        public long Length => new FileInfo(FileName).Length;

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                try
                {
                    File.Delete(FileName);
                }
                catch // best effort
                {
                }
            }
        }

    }
}
