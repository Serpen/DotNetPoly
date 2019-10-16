namespace DotNetPolyForms
{
    namespace My
    {
        [global::System.Runtime.CompilerServices.CompilerGenerated()]
        [global::System.CodeDom.Compiler.GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal sealed partial class MySettings : global::System.Configuration.ApplicationSettingsBase
        {
            private static MySettings defaultInstance = (MySettings)global::System.Configuration.ApplicationSettingsBase.Synchronized(new MySettings());

            /* TODO ERROR: Skipped IfDirectiveTrivia */
            private static bool addedHandler;

            private static object addedHandlerLockObject = new object();

            [global::System.Diagnostics.DebuggerNonUserCode()]
            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
            private static void AutoSaveSettings(global::System.Object sender, global::System.EventArgs e)
            {
                
            }
            /* TODO ERROR: Skipped EndIfDirectiveTrivia */
            public static MySettings Default
            {
                get
                {

                    /* TODO ERROR: Skipped IfDirectiveTrivia */
                    if (!addedHandler)
                    {
                        lock (addedHandlerLockObject)
                        {
                            if (!addedHandler)
                            {
                                
                                addedHandler = true;
                            }
                        }
                    }
                    /* TODO ERROR: Skipped EndIfDirectiveTrivia */
                    return defaultInstance;
                }
            }
        }
    }

    namespace My
    {
        [global::Microsoft.VisualBasic.HideModuleName()]
        [global::System.Diagnostics.DebuggerNonUserCode()]
        [global::System.Runtime.CompilerServices.CompilerGenerated()]
        internal static class MySettingsProperty
        {
            [global::System.ComponentModel.Design.HelpKeyword("My.Settings")]
            internal static global::DotNetPolyForms.My.MySettings Settings
            {
                get
                {
                    return global::DotNetPolyForms.My.MySettings.Default;
                }
            }
        }
    }
}
