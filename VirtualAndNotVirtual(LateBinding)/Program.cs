using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAndNotVirtual_LateBinding_
{

    class BaseObject
    {
        public string PropertyNotVirtual { get; set; }

        public virtual string VirtualProperty { get; set; }

    }

    class InheritObject: BaseObject
    {
        public override string VirtualProperty { get => base.VirtualProperty; set => base.VirtualProperty = value; }
    }


    class Program
    {
        static void Main(string[] args)
        {

            InheritObject inheritObject = new InheritObject();

            inheritObject.PropertyNotVirtual = "Раннее связывание";
            inheritObject.VirtualProperty = "Позднее связывание";
        }
    }
}
