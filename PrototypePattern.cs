using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // pattern that allows cloning objects, 
    // even complex ones, without coupling to their specific classes.




    // All prototype classes should have a common interface that makes it possible to copy objects
    // even if thier concrete classes are unknown.
    // Prototype objects can produce full copies since objects of the same class can access each other's private fields.
    

    public class Person
    {
        public int Age;
        public DateTime BirthDate;
        public string Name;
        public IdInfo IdInfo;

        public Person ShallowCopy()
        {
            // memberwiseClone -> struct(stack)은 메모리 복사 / reference(heap)은 참조복사
            return (Person)this.MemberwiseClone();
        }

        public Person DeepCopy()
        {
            Person clone = (Person)this.MemberwiseClone();
            clone.IdInfo = new IdInfo(IdInfo.IdNumber);
            clone.Name = String.Copy(Name);
            return clone;
        }
    }

    public class IdInfo
    {
        public int IdNumber;

        public IdInfo(int idNumber)
        {
            this.IdNumber = idNumber;
        }
    }

    public class PrototypePattern
    {
        public void Main()
        {
            Person p1 = new Person();
            p1.Age = 42;
            p1.BirthDate = Convert.ToDateTime("1977-01-01");
            p1.Name = "Jack Daniels";
            p1.IdInfo = new IdInfo(666);

            // Person a shallow copy of p1 and assign it to p2.
            Person p2 = p1.ShallowCopy();
            // Make a deep copy of p1 and assign it to p3.
            Person p3 = p1.DeepCopy();

            // Display values of p1, p2, p3
            Console.WriteLine("Origin values of p1, p2, p3:");
            Console.WriteLine(" p1 instance values:");
            DisplayValues(p1);
            Console.WriteLine("Origin values of p1, p2, p3:");
            Console.WriteLine(" p2 instance values:");
            DisplayValues(p2);
            Console.WriteLine("Origin values of p1, p2, p3:");
            Console.WriteLine(" p3 instance values:");
            DisplayValues(p3);

            // Change the vlaue of p1 properties and display the values of p1,
            // p2 and p3.
            p1.Age = 32;
            p1.BirthDate = Convert.ToDateTime("1900-01-01");
            p1.Name = "Frank";
            p1.IdInfo.IdNumber = 7878;
            Console.WriteLine("\nVlaues of p1,p2 and p3 after Changes to p1");
            Console.WriteLine(" p1 instance values:");
            DisplayValues(p1);
            Console.WriteLine(" p2 instance values(reference values have changed):");
            DisplayValues(p2);
            Console.WriteLine(" p3 instance values(everything was kept the same):");
            DisplayValues(p3);


        }

        public static void DisplayValues(Person p)
        {
            Console.WriteLine($"    Name : {p.Name}, Age: {p.Age}, BirthDate: {p.BirthDate}");
            Console.WriteLine($"    ID#: {p.IdInfo.IdNumber}");
        }
    }
}
