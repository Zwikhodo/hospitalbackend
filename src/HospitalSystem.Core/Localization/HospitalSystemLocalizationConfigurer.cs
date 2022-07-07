using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace HospitalSystem.Localization
{
    public static class HospitalSystemLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(HospitalSystemConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(HospitalSystemLocalizationConfigurer).GetAssembly(),
                        "HospitalSystem.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
