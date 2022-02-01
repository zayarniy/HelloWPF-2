using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAndNotVirtual_LateBinding_
{

    

    class BaseObject
    {
        public BaseObject PropertyNotVirtual { get; set; }

        public virtual BaseObject VirtualProperty { get; set; }

    }

    class InheritObject: BaseObject
    {
        public override BaseObject VirtualProperty { get => base.VirtualProperty; set => base.VirtualProperty = value; }
    }

    class InheritObject2 : BaseObject
    {
        public override BaseObject VirtualProperty { get => base.VirtualProperty; set => base.VirtualProperty = value; }
    }

    class Program 
    {
        static void Main(string[] args)
        {

            InheritObject inheritObject = new InheritObject();

            inheritObject.PropertyNotVirtual = new InheritObject();
            inheritObject.VirtualProperty = new InheritObject2();
        }
    }
}
