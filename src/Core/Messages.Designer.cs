﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Physics {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Physics.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Unit system {0} only supports {1} dimension(s)..
        /// </summary>
        internal static string DimensionUnknown {
            get {
                return ResourceManager.GetString("DimensionUnknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Inherent prefix for unit symbol &apos;{0}&apos; could not be determined..
        /// </summary>
        internal static string InherentPrefixInvalid {
            get {
                return ResourceManager.GetString("InherentPrefixInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; has the same dimension and factor as &apos;{1}&apos;..
        /// </summary>
        internal static string UnitAlreadyKnown {
            get {
                return ResourceManager.GetString("UnitAlreadyKnown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The unit expression &apos;{0}&apos; is ambiguous..
        /// </summary>
        internal static string UnitExpressionAmbiguous {
            get {
                return ResourceManager.GetString("UnitExpressionAmbiguous", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The unit expression &apos;{0}&apos; is not formatted correctly..
        /// </summary>
        internal static string UnitExpressionInvalid {
            get {
                return ResourceManager.GetString("UnitExpressionInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Units are not the same dimension..
        /// </summary>
        internal static string UnitsNotSameDimension {
            get {
                return ResourceManager.GetString("UnitsNotSameDimension", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Units are not from the same unit system..
        /// </summary>
        internal static string UnitsNotSameSystem {
            get {
                return ResourceManager.GetString("UnitsNotSameSystem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A unit with symbol &apos;{0}&apos; is already known in the system..
        /// </summary>
        internal static string UnitSymbolAlreadyKnown {
            get {
                return ResourceManager.GetString("UnitSymbolAlreadyKnown", resourceCulture);
            }
        }
    }
}
