﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace compagniaAerea.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=DESKTOP-TQRPAA4\\SQLEXPRESS;Initial Catalog=uniboAirlines;Integrated S" +
            "ecurity=True")]
        public string uniboAirlinesConnectionString1 {
            get {
                return ((string)(this["uniboAirlinesConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=erikserver.database.windows.net;Initial Catalog=UniboAirlines;Persist" +
            " Security Info=True;User ID=erik_amministratore;Password=Cesenate_81")]
        public string UniboAirlinesConnectionString2 {
            get {
                return ((string)(this["UniboAirlinesConnectionString2"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=erikserver.database.windows.net,1433;Initial Catalog=UniboAirlines;Pe" +
            "rsist Security Info=True;User ID=erik_amministratore")]
        public string UniboAirlinesConnectionString3 {
            get {
                return ((string)(this["UniboAirlinesConnectionString3"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=erikserver.database.windows.net,1433;Initial Catalog=UniboAirlines;Pe" +
            "rsist Security Info=True;User ID=erik_amministratore")]
        public string UniboAirlinesConnectionString {
            get {
                return ((string)(this["UniboAirlinesConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=DESKTOP-TQRPAA4\\SQLEXPRESS;Initial Catalog=UniboAirlines;Integrated S" +
            "ecurity=True")]
        public string UniboAirlinesConnectionString4 {
            get {
                return ((string)(this["UniboAirlinesConnectionString4"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=erikserver.database.windows.net;Initial Catalog=myUniboAirlines;Persi" +
            "st Security Info=True;User ID=erik_amministratore;Password=Cesenate_81")]
        public string myUniboAirlinesConnectionString {
            get {
                return ((string)(this["myUniboAirlinesConnectionString"]));
            }
        }
    }
}
