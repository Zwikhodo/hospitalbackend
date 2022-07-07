using HospitalSystem.Debugging;

namespace HospitalSystem
{
    public class HospitalSystemConsts
    {
        public const string LocalizationSourceName = "HospitalSystem";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "4dad837773724ac8b7abf7fe48958ef1";
    }
}
