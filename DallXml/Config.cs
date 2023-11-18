﻿namespace DallXml
{
    static internal  class Config
    {
        static string s_data_config_xml = "data-config";
        internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }
        internal static int nextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "nextDependencyId"); }

    }
}