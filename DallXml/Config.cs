namespace DallXml
{
    static internal  class Config
    {
        static string s_data_config_xml = "data-config";
        internal static int NextTaskId { get => XmlTools.XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
        internal static int nextDependencyId { get => XmlTools.XMLTools.GetAndIncreaseNextId(s_data_config_xml, "nextDependencyId"); }

    }
}