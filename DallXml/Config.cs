namespace DallXml;

using Dal;
using DalApi;
using DallXml;
using DO;
/// <summary>
/// General configuration data
/// </summary>
internal static  class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextTaskId { get =>XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }//A datum that stores the last running number for Task
    internal static int nextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }//A datum that stores the last running number for Dependency
}