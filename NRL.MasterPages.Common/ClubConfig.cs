using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRL.MasterPages.Common
{
    public class ClubConfig
    {
        private const string CONFIGURATION_SECTION_NAME = "ClubSetttings";

        private static readonly List<Club> instance = null;
        public static List<Club> Instance
        {
            get { return ClubConfig.instance; }
        }

        private ClubConfig() { }
        static ClubConfig()
        {
            var suffix = ConfigurationManager.AppSettings ["EnvironmentSuffix"];

            var clubConfig = ConfigurationManager.GetSection(CONFIGURATION_SECTION_NAME) as ClubConfigurationSection;
            instance = clubConfig.ConfiguredClubs.Select(clubEntry =>
                new Club
                {
                    Name = clubEntry.Name,
                    Host = string.Format ( "{0}{1}", clubEntry.Host, suffix),
                    Resources = string.Format ( "{0}{1}", clubEntry.Resources, suffix),
                }).ToList();
        }
    }

    public class ClubConfigurationSection : ConfigurationSection
    {
        private const string ELEMENT_NAME_CONFIGURED_BRANDS = "Clubs";

        [ConfigurationProperty(ELEMENT_NAME_CONFIGURED_BRANDS)]
        [ConfigurationCollection(typeof(ClubConfigEntry), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public ClubCollection ConfiguredClubs
        {
            get { return this[ELEMENT_NAME_CONFIGURED_BRANDS] as ClubCollection; }
        }
    }

    public class ClubCollection : ConfigurationElementCollection, IEnumerable<ClubConfigEntry>
    {
        private Dictionary<string, ClubConfigEntry> clubs = null;

        protected override ConfigurationElement CreateNewElement()
        {
            return new ClubConfigEntry();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.ToString();
        }

        public new IEnumerator<ClubConfigEntry> GetEnumerator()
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
            {
                yield return base.BaseGet(i) as ClubConfigEntry;
            }
        }
    }

    public class ClubConfigEntry : ConfigurationElement
    {
        private const string ATTRIBUTE_HOST = "host";
        private const string ATTRIBUTE_NAME = "name";
        private const string ATTRIBUTE_RESOURCES = "resources";

        [ConfigurationProperty(ATTRIBUTE_NAME)]
        public string Name
        {
            get { return this[ATTRIBUTE_NAME].ToString(); }
        }

        [ConfigurationProperty(ATTRIBUTE_HOST)]
        public string Host
        {
            get { return this[ATTRIBUTE_HOST].ToString(); }
        }

        [ConfigurationProperty(ATTRIBUTE_RESOURCES)]
        public string Resources
        {
            get { return this[ATTRIBUTE_RESOURCES].ToString(); }
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", this.Name, this.Host, this.Resources);
        }
    }

    public class Club
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Resources { get; set; }
    }
}
