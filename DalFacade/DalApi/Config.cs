
using System.Xml.Linq;
namespace DalApi;
static class Config
{
    /// <summary>
    /// internal PDS class
    /// </summary>
    internal record DalImplementation
    (string Package,   // package/dll name
     string Namespace, // namespace where DAL implementation class is contained in
     string Class   // DAL implementation class name
    );

    internal static string s_dalName;
    internal static Dictionary<string, DalImplementation> s_dalPackages;

    /// <summary>
    /// A helper class that knows how to read at runtime the data from the configuration file dal-config.xml.
    /// The XElement.Load method loads an xml file, performs its analysis, builds a tree and bytes of type XElement
    /// and returns the object of the root element of the xml file.
    /// An object of type XElement contains parsed information about some xml element
    /// Element("dal") method returns an object of an element with the name "dal" from the DOM tree, and the Value attribute returns the value inside the element.
    /// Calling Element("dal-packages")?.Elements returns a collection of all elements that are "children" of an element named "dal-packages" in the DOM tree.
    /// The ToDictionary method builds and returns a hashed table whose sub-element name is a hash key,
    /// and the element value is the value attached to the hash key.
    /// </summary>
    /// <exception cref="DalConfigException"></exception>
    static Config()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml") ??//
            throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig.Element("dal")?.Value ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig.Element("dal-packages")?.Elements() ??
            throw new DalConfigException("<dal-packages> element is missing");
        s_dalPackages = (from item in packages
                         let pkg = item.Value
                         let ns = item.Attribute("namespace")?.Value ?? "Dal"
                         let cls = item.Attribute("class")?.Value ?? pkg
                         select (item.Name, new DalImplementation(pkg, ns, cls))
                        ).ToDictionary(p => "" + p.Name, p => p.Item2);//A 
    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
