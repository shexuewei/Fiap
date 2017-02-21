using Eiap.Framework.Base.AssemblyService;
using Eiap.Framework.Base.Dependency.SXW;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Base.Serialization.SXW.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.AssemblyInitialize().Register(DependencyManager.Instance.Register);
            Students student1 = new Students { Age = 10, Birthday = DateTime.Now, Height = null, Name = "Student1" };
            Students student2 = new Students { Age = 20, Birthday = DateTime.Now, Height = 160, Name = "student2" };
            Students student3 = new Students { Age = 30, Birthday = DateTime.Now, Height = null, Name = "student3" };

            Classes class1 = new Classes { ClassName = "Class1", StudentList = new List<Students> { student1 } };
            Classes class2 = new Classes { ClassName = "class2", StudentList = new List<Students> { student3, student2, student1 } };

            Schools school1 = new Schools
            {
                SchoolName = "school1",
                SchoolAge = 10,
                IsPubSchool = false,
                Amt = 1999.12345m,
                ClassList = new List<Classes> { class1, class2 },
                IsPriSchool = true,
                Building = new string[] { "1", "2", "3" },
                ClassList2 = new ArrayList { class1, 2 }
            };
            //List<Schools> schoolList = new List<Schools>();
            //schoolList.Add(school1);
            ISerializationManager serliz = DependencyManager.Instance.Resolver<ISerializationManager>();
            string value = serliz.SerializeObject(school1);
            Console.WriteLine(value);
            string value2 = JsonConvert.SerializeObject(school1);
            Console.ReadLine();
        }
    }

    public class Schools
    {
        public string SchoolName { get; set; }
        public int SchoolAge { get; set; }
        public bool? IsPubSchool { get; set; }
        public bool? IsPriSchool { get; set; }
        public string[] Building { get; set; }
        public decimal Amt { get; set; }
        public IEnumerable<Classes> ClassList { get; set; }
        public ArrayList ClassList2 { get; set; }
    }

    public class Classes
    {
        public string ClassName { get; set; }
        public IList<Students> StudentList { get; set; }
    }

    public class Students
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public int? Height { get; set; }
    }
}
