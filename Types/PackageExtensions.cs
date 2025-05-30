﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using NuGetPe;

namespace NuGetPackageExplorer.Types
{
    public static class PackageExtensions
    {
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "InFolder")]
        public static IEnumerable<string> GetFilesInFolder(this IPackage package, string folder)
        {
            ArgumentNullException.ThrowIfNull(package);
            ArgumentNullException.ThrowIfNull(folder);

            if (string.IsNullOrEmpty(folder))
            {
                // return files at the root
                return from s in package.GetFiles()
                       where !s.Path.Contains(Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)
                       select s.Path;
            }
            else
            {
                var prefix = folder + Path.DirectorySeparatorChar;
                return from s in package.GetFiles()
                       where s.Path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                       select s.Path;
            }
        }

        public static IEnumerable<string> GetFilesUnderRoot(this IPackage package)
        {
            return GetFilesInFolder(package, string.Empty);
        }
    }
}
