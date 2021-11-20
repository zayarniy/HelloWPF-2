using System;

using CommonSnappableTypes;

namespace CSharpSnapIn
{
    [CompanyInfo(CompanyName = "FooBar", CompanyUrl = "www.FooBar.com")]
    public class CSharpModule : IAppFunctionality
    {
        void IAppFunctionality.DoIt()
        {
            Console.WriteLine("You have just used the C# snap in!");
        }
    }

    [CompanyInfo(CompanyName = "C#-3", CompanyUrl = "www.GeekBrains.ru")]
    public class CSharpModule2 : IAppFunctionality
    {
        void IAppFunctionality.DoIt()
        {
            Console.WriteLine("Вы используете оснастку на C#!");
        }
    }
} 
