using NuGet.Protocol.Plugins;
using Yildiz.Edu.WebUI.Entities;

namespace Yildiz.Edu.WebUI.DataAccess.Abstract
{
    public interface IFacultyDal
    {
        Faculty Get(int id);
        List<Faculty> GetAll();
        Faculty Add(Faculty faculty);
        Faculty Update(Faculty faculty);
        bool Delete(int id);
    }
}
