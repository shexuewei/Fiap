using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Common.Entity;
using Eiap.Framework;
using Eiap.Framework.AppBase.UnitOfWork;
using Eiap.Framework.Base.Dependency.SXW;
using Eiap.Framework.Base.AssemblyService.SXW;
using System.Reflection;
using Eiap.Framework.AppBase.Repository.SXW;
using Eiap.Framework.AppBase.ApplicationService.SXW;
using Eiap.Framework.Base.Dependency;
using Eiap.Framework.AppBase.Repository;
using Eiap.Framework.Common.DataMapping.SQL;
using Eiap.Framework.Common.DataAccess.SQL;

namespace Eiap.Framework.Common.DataMapping.SQLServer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.RegisterAssembly(@"C:\MyWork\EiapV3.0\Eiap.Framework\Eiap.Framework.Common.DataMapping.SQLServer.Test\bin\Debug").Register(DependencyManager.Instance.Register);
            IUnitOfWorkApplicationTest test = (IUnitOfWorkApplicationTest)DependencyManager.Instance.Resolver(typeof(IUnitOfWorkApplicationTest));
            var school = test.InsertSchoolTest("shoolTest001");
            var classtest = test.InsertClassTest("class001", school.Id);
            test.InsertStudentTest("101", 10, DateTime.Now, classtest.Id, school.Id);
            test.InsertStudentTest("102", 10, DateTime.Now, classtest.Id, school.Id);
            test.InsertStudentTest("103", 10, DateTime.Now, classtest.Id, school.Id);
            test.InsertStudentTest("104", 10, DateTime.Now, classtest.Id, school.Id);
            IUnitOfWork _CurrentUnitOfWork = DependencyManager.Instance.Resolver<IUnitOfWork>(ObjectLifeCycle.Context);
            _CurrentUnitOfWork.Commit();

            //List<StudentTest> list = test.GetStudentTestList();
            //foreach (StudentTest stu in list)
            //{
            //    Console.WriteLine(stu.ToString());
            //}

            //test.UpdateClassTest(Guid.Parse("AEAC857C-FEDE-4239-B628-004A7D39BF9D"), "class002");
            //IUnitOfWork _CurrentUnitOfWork = DependencyManager.Instance.Resolver<IUnitOfWork>(ObjectLifeCycle.Context);
            //_CurrentUnitOfWork.Commit();
            Console.ReadLine();
        }
    }

    public class StudentTest : IEntity<Guid>
    {

        public Guid Id
        {
            get;
            set;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime? Birthday { get; set; }

        public Guid ClassTestId { get; set; }

        public ClassTest ClassTest { get; set; }

        public Guid SchoolTestId { get; set; }

        public SchoolTest SchoolTest { get; set; }

        public override string ToString()
        {
            return "Name:" + Name + "   Age:" + Age + " Birthday:" + (Birthday.HasValue ? Birthday.ToString() : "Null") + " ClassTestId:" + ClassTestId + " SchoolTestId " + SchoolTestId;//+ SchoolTestId;
        }
    }

    public class ClassTest : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string ClassName { get; set; }

        public Guid SchoolTestId { get; set; }

        public SchoolTest SchoolTest { get; set; }
    }

    public class SchoolTest:IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string SchoolName { get; set; }
    }

    public interface IUnitOfWorkApplicationTest : IRealtimeDependency
    {
        void InsertStudentTest(string name, int age, DateTime? birthday, Guid classid, Guid schoolid);

        void UpdateClassTest(Guid id, string name);

        ClassTest InsertClassTest(string name, Guid schoolid);

        SchoolTest InsertSchoolTest(string name);

        List<StudentTest> GetStudentTestList();
    }



    public class UnitOfWorkApplicationTest : AppService, IUnitOfWorkApplicationTest
    {
        private readonly IRepository<StudentTest, Guid> _studentTestRepository;
        private readonly IRepository<ClassTest, Guid> _classTestRepository;
        private readonly IRepository<SchoolTest, Guid> _schoolTestRepository;


        public UnitOfWorkApplicationTest(IRepository<StudentTest, Guid> studentTestRepository,
            IRepository<ClassTest, Guid> classTestRepository,
            IRepository<SchoolTest, Guid> schoolTestRepository)
        {
            _studentTestRepository = studentTestRepository;
            _classTestRepository = classTestRepository;
            _schoolTestRepository = schoolTestRepository;
        }

        public void InsertStudentTest(string name, int age, DateTime? birthday, Guid classid, Guid schoolid)
        {
            StudentTest stu = new StudentTest { Name = name, Age = age, Id = Guid.NewGuid(), Birthday = birthday, ClassTestId = classid, SchoolTestId = schoolid }; //SchoolTestId = schoolid
            Log.Info(stu.ToString(), "InsertStudentTest");
            _studentTestRepository.Add(stu);
        }

        public List<StudentTest> GetStudentTestList()
        {
            DateTime dt = DateTime.Parse("2016-05-22 22:48:05.503");
            Guid classid = Guid.Parse("AEAC857C-FEDE-4239-B628-004A7D39BF9D");
            return _studentTestRepository.Query().Where(m => m.Birthday.Value >= dt && m.ClassTestId == classid)
                .OrderByDesc(m => m.Birthday).GetEntityList();
        }

        public ClassTest InsertClassTest(string name, Guid schoolid)
        {
            return _classTestRepository.Add(new ClassTest { ClassName = name, Id = Guid.NewGuid()});//, SchoolTestId = schoolid 
        }

        public SchoolTest InsertSchoolTest(string name)
        {
            return _schoolTestRepository.Add(new SchoolTest { SchoolName = name, Id = Guid.NewGuid() });
        }

        public void UpdateClassTest(Guid id, string name)
        {
            var entity = _classTestRepository.Query().GetEntity(id);
            entity.ClassName = name;
            _classTestRepository.Update(entity);
        }
    }
}
