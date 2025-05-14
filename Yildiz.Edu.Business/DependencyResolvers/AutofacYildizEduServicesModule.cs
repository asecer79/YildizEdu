using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.Business.Concrete;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.DataAccess.Dal.Concrete;

namespace Yildiz.Edu.Business.DependencyResolvers
{
    public class AutofacYildizEduServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FacultyService>().As<IFacultyService>();
            builder.RegisterType<FacultyDal>().As<IFacultyDal>();
            builder.RegisterType<DepartmentService>().As<IDepartmentService>();
            builder.RegisterType<DepartmentDal>().As<IDepartmentDal>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserDal>().As<IUserDal>();
        }
    }
    
}
