using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GetHashPassword
{
    class Program
    {
        static string hashPassStr = "";
        static bool Check(string password)
        {
            MD5 md5 = MD5.Create();//Используем алгоритм хеширования MD5
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));//получаем битовое представление хэша пароля
            string hashPass = BitConverter.ToString(hash);//получаем строковое представление хэша пароля
            return hashPass==hashPassStr;
        }
        static void Main(string[] args)
        {
            string password = "123";
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            hashPassStr = BitConverter.ToString(hash);
            string pass;
            do
            {
                Console.Write("Enter password:");
                pass = Console.ReadLine();
            }
            while (!Check(pass));
            Console.WriteLine("Verification passed");
            Console.ReadKey();

        }
    }
}
