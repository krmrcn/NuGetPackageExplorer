﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;

namespace NuGetPe
{
    internal sealed class SimplePackage : IPackage
    {
        private readonly PackageBuilder _packageBuilder;

        public SimplePackage(PackageBuilder packageBuilder)
        {
            ArgumentNullException.ThrowIfNull(packageBuilder);

            Id = packageBuilder.Id;
            Version = packageBuilder.Version;
            Title = packageBuilder.Title;
            Authors = packageBuilder.Authors;
            Owners = packageBuilder.Owners;
            Icon = packageBuilder.Icon;
            IconUrl = packageBuilder.IconUrl;
            Readme = packageBuilder.Readme;
            LicenseUrl = packageBuilder.LicenseUrl;
            ProjectUrl = packageBuilder.ProjectUrl;
            RequireLicenseAcceptance = packageBuilder.RequireLicenseAcceptance;
            DevelopmentDependency = packageBuilder.DevelopmentDependency;
            Description = packageBuilder.Description;
            Summary = packageBuilder.Summary;
            ReleaseNotes = packageBuilder.ReleaseNotes;
            Language = packageBuilder.Language;
            Tags = string.Join(" ", packageBuilder.Tags);
            Serviceable = packageBuilder.Serviceable;
            FrameworkReferences = packageBuilder.FrameworkReferences;
            DependencyGroups = packageBuilder.DependencyGroups;
            PackageAssemblyReferences = packageBuilder.PackageAssemblyReferences;
            Copyright = packageBuilder.Copyright;
            Repository = packageBuilder.Repository;
            ContentFiles = packageBuilder.ContentFiles;
            PackageTypes = packageBuilder.PackageTypes;
            MinClientVersion = packageBuilder.MinClientVersion;
            LicenseMetadata = packageBuilder.LicenseMetadata;
            FrameworkReferenceGroups = packageBuilder.FrameworkReferenceGroups;

            _packageBuilder = packageBuilder;
        }
        public IEnumerable<IPackageFile> GetFiles()
        {
            return _packageBuilder.Files.Where(p => !PackageUtility.IsManifest(p.Path));
        }

        public Stream GetStream()
        {
            Stream memoryStream = new MemoryStream();
            _packageBuilder.Save(memoryStream);
            return memoryStream;
        }

        public string Id { get; private set; }

        public NuGetVersion Version { get; private set; }

        public string Title { get; private set; }

        public IEnumerable<string> Authors { get; private set; }

        public IEnumerable<string> Owners { get; private set; }

        public string Icon { get; private set; }

        public Uri IconUrl { get; private set; }

        public string Readme { get; private set; }

        public Uri LicenseUrl { get; private set; }

        public DateTimeOffset? Published
        {
            get { return DateTimeOffset.Now; }
        }

        public Uri ProjectUrl { get; private set; }

        public bool RequireLicenseAcceptance { get; private set; }

        public bool DevelopmentDependency { get; private set; }

        public string Description { get; private set; }

        public string Summary { get; private set; }

        public string ReleaseNotes { get; private set; }

        public string Copyright { get; private set; }

        public string Language { get; private set; }

        public string Tags { get; private set; }

        public bool Serviceable { get; private set; }

        public IEnumerable<FrameworkAssemblyReference> FrameworkReferences { get; private set; }

        public IEnumerable<PackageDependencyGroup> DependencyGroups { get; private set; }

        public IEnumerable<PackageReferenceSet> PackageAssemblyReferences { get; private set; }

        public bool IsPrerelease
        {
            get
            {
                return Version.IsPrerelease;
            }
        }

        public Uri? ReportAbuseUrl
        {
            get { return null; }
        }

        public long DownloadCount
        {
            get { return -1; }
        }

        public bool IsAbsoluteLatestVersion
        {
            get { return true; }
        }

        public bool IsLatestVersion
        {
            get { return true; }
        }

        public DateTimeOffset LastUpdated
        {
            get { return DateTimeOffset.UtcNow; }
        }

        public Version MinClientVersion
        {
            get; private set;
        }

        public IEnumerable<ManifestContentFiles> ContentFiles { get; private set; }

        public IEnumerable<PackageType> PackageTypes { get; private set; }

        public RepositoryMetadata Repository { get; private set; }

        public LicenseMetadata LicenseMetadata { get; private set; }

        public IEnumerable<FrameworkReferenceGroup> FrameworkReferenceGroups { get; private set; }

        public void Dispose()
        {
        }
    }
}
