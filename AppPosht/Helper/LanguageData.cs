using System.Collections.Generic;

namespace AppPosht.Helper
{
    /// <summary>
    /// Reference data and utility methods for working with country data codes, language and names.
    /// </summary>
    public struct LanguageData
    {
        public LanguageData(string iso2, string language, string name)
        {
            Iso2 = iso2;
            Language = language;
            Name = name;
        }

        /// <summary>Returns data for countries that have flag images.</summary>
        public static IEnumerable<LanguageData> AllLanguage => new[]
        {
            new LanguageData("RU", "ru-RU", "Руский язык"),
            new LanguageData("UA", "uk-UA", "Українська мова"),
            new LanguageData("GB", "en-US", "English")
        };

        /// <summary>
        /// The country's identifier, according to ISO 3166-1 alpha-2.
        /// </summary>
        public string Iso2 { get; }

        /// <summary>
        /// The language name.
        /// </summary>
        public string Language { get; }

        /// <summary>The country's common name.</summary>
        public string Name { get; }
    }
}