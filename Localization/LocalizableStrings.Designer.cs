﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoProjectRenamer.Localization {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class LocalizableStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LocalizableStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AutoProjectRenamer.Localization.LocalizableStrings", typeof(LocalizableStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Closing section tag not found.
        /// </summary>
        internal static string ClosingSectionTagNotFoundError {
            get {
                return ResourceManager.GetString("ClosingSectionTagNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid format in line {0}: {1}.
        /// </summary>
        internal static string ErrorMessageFormatString {
            get {
                return ResourceManager.GetString("ErrorMessageFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected file header not found.
        /// </summary>
        internal static string FileHeaderMissingError {
            get {
                return ResourceManager.GetString("FileHeaderMissingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File header is missing version.
        /// </summary>
        internal static string FileHeaderMissingVersionError {
            get {
                return ResourceManager.GetString("FileHeaderMissingVersionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Global section specified more than once.
        /// </summary>
        internal static string GlobalSectionMoreThanOnceError {
            get {
                return ResourceManager.GetString("GlobalSectionMoreThanOnceError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Global section not closed.
        /// </summary>
        internal static string GlobalSectionNotClosedError {
            get {
                return ResourceManager.GetString("GlobalSectionNotClosedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property set is missing &apos;{0}&apos;.
        /// </summary>
        internal static string InvalidPropertySetFormatString {
            get {
                return ResourceManager.GetString("InvalidPropertySetFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid section type: {0}.
        /// </summary>
        internal static string InvalidSectionTypeError {
            get {
                return ResourceManager.GetString("InvalidSectionTypeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Project section is missing &apos;{0}&apos; when parsing the line starting at position {1}.
        /// </summary>
        internal static string ProjectParsingErrorFormatString {
            get {
                return ResourceManager.GetString("ProjectParsingErrorFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Project section not closed.
        /// </summary>
        internal static string ProjectSectionNotClosedError {
            get {
                return ResourceManager.GetString("ProjectSectionNotClosedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Section id missing.
        /// </summary>
        internal static string SectionIdMissingError {
            get {
                return ResourceManager.GetString("SectionIdMissingError", resourceCulture);
            }
        }
    }
}
