using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;
internal class EngineerExperiencesCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> s_exper =
(Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => s_exper.GetEnumerator();
}

