﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Extensions
{
    public interface IExtensionManifest
    {

        string Publisher { get; set; }
        string Version { get; set; }
        Guid UniqueID { get; set; }


        string Title { get; set; }
        string DisplayName { get; set; }
        string Abstract { get; set; }
        string IconUrl { get; set; }
        string ContentControl { get; set; }
        string AssemblyName { get; set; }
        bool IsExtEnabled { get; set; }
        bool CanUninstall { get; set; }
        ExtensionInToolbarPositions FoundInToolbarPositions { get; set; }
        ExtensionInToolbarPositions LaunchInDockPositions { get; set; }

    }
}