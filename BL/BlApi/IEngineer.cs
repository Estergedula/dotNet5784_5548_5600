using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// 
/// </summary>
public interface IEngineer
{
    //public IEnumerable<Engineer> (string nameOfProp, string value);
    public int Create(BO.Engineer boEngineer);
    public BO.Engineer? Read(int id);
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer?, bool>? filter=null);
    public void Update(BO.Engineer item);
    public void Delete(int id);
    //public BO.TaskInEngineer ?GetCurrentTaskOfEngineer(int idOfEmgineer);

    
    //public BO.EngineerInTask GetDetailedCourseForStudent(int StudentId, int CourseId);
}
