﻿using CoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using X.Extensions.Popups;
using System.Reflection;
using WeakEvent;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppExtensions;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Media.Imaging;
using X.Services.Settings;
using Windows.Storage;
using Windows.UI.Core;

namespace X.Services.Extensions
{
    public class ExtensionManifest : IExtensionManifest
    {
        public string Abstract { get; set; }

        public string AssemblyName { get; set; }

        public bool CanUninstall { get; set; } = true;

        public string ContentControl { get; set; }

        public string DisplayName { get { return Title; } set { } }

        public ExtensionInToolbarPositions FoundInToolbarPositions { get; set; }

        public string IconUrl { get; set; }

        public BitmapImage IconBitmap { get; set; }

        public object Icon
        {
            get
            {
                if (IconUrl == "bitmap") return IconBitmap;
                else return IconUrl;
            }
            set { }
        }

        public bool IsExtEnabled { get; set; } = true;

        public bool IsUWPExtension { get; set; } = false;

        public ExtensionInToolbarPositions LaunchInDockPositions { get; set; }

        public string Publisher { get; set; }

        public string Title { get; set; }

        public string TitleHashed { get { return FlickrNet.UtilityMethods.MD5Hash(Title); } private set { } }

        public string Path { get { return "//"; } set { } }

        public Guid UniqueID { get; set; } = Guid.NewGuid();

        public string Version { get; set; }

        public string AppExtensionUniqueID { get; set; }

        public int Size { get; set; }

        public ExtensionManifest()
        { }

        public ExtensionManifest(IExtensionManifest data)
        {
            this.Abstract = data.Abstract;
            this.AssemblyName = data.AssemblyName;
            this.CanUninstall = data.CanUninstall;
            this.ContentControl = data.ContentControl;
            this.DisplayName = data.DisplayName;
            this.FoundInToolbarPositions = data.FoundInToolbarPositions;
            this.IconUrl = data.IconUrl;
            this.IsExtEnabled = data.IsExtEnabled;
            this.LaunchInDockPositions = data.LaunchInDockPositions;
            this.Publisher = data.Publisher;
            this.Title = data.Title;
            this.UniqueID = data.UniqueID;
            this.Version = data.Version;
            this.Path = data.Path;
            this.IsUWPExtension = data.IsUWPExtension;
            this.AppExtensionUniqueID = data.AppExtensionUniqueID;
            this.IconBitmap = data.IconBitmap; // <==== this could lead to a leak, investigate this
        }

        public ExtensionManifest(string title, string iconUrl, string publisher, string version, string description, ExtensionInToolbarPositions iconPosition, ExtensionInToolbarPositions panelPosition)
        {

            Title = title;
            IconUrl = iconUrl;
            Publisher = publisher;
            Version = version;
            Abstract = description;
            FoundInToolbarPositions = iconPosition;
            LaunchInDockPositions = panelPosition;

        }

    }
    
}
