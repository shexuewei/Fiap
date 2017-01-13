using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eiap.Framework.Base.AssemblyService;
using System.Reflection;
using System.Diagnostics;

namespace Eiap.Framework.Base.Dependency.SXW.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyManager.Instance.LoadAllAssembly().Register(DependencyManager.Instance.Register);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                ISchoolManager schoolManager = DependencyManager.Instance.Resolver<ISchoolManager>();
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }

    public class School
    {
        public string Name { get; set; }

        public int Age { get; set; }

        List<Student> StudentList { get; set; }
    }

    public class Student {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }
    }

    public interface ISchoolManager : IRealtimeDependency
    {
        School Create(string name, int age);
    }

    public class SchoolManager : ISchoolManager
    {
        public School Create(string name, int age)
        {
            return new School { Name = name, Age = age };
        }
    }
}
