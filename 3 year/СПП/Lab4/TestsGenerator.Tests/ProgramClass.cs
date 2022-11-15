using System;
using System.Linq;
using System.Text;
using System.Collections;

namespace ProgramNamespace
{
    public class ProgramClass
    {
        private IDependency _dependency1;
        private IDependency _dependency2;

        public ProgramClass(IDependency dependency1, IDependency dependency2)
        {
            this._dependency1 = dependency1;
            this._dependency2 = dependency2;
        }

        private void DoUselessWork()
        {
            Console.WriteLine("First method");
        }

        public void DoUsefulWork()
        {
        }

        public int DoUsefulWork(int work, int work2)
        {
            return work;
        }
    }
}