using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{


    // The Singleton class defines the "GetInstance" method that serves
    // as an alternative to constructor ans lets clients access the same instance of this class over and over
    public class Singleton
    {
        //The Singleton's constructor should always be private to prevent
        // direct construction calls with the "new" operator.
        private Singleton() { }

        // The Singleton's instance is stored in a static field. There are
        // multiple ways to initialize this field, all of them have various pros and cons.
        // In this example we'll show the simplest of these ways,
        // which doesn't work really weel in multithreaded program.
        private static Singleton _instance;

        // This is the static method that controls the access to the singleton instance.
        // On the first run, it creates a singleton object and places it into the static filed.
        // On subsequent runs, it returns the client existing object in the static field.
        public static Singleton GetInstacne()
        {
            if(_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        // Finally, any singleton should define some business logic,
        // which can be excuted on its instance.
        public static void someBusinessLogic()
        {
            // ...
        }

    }

    public class SingletonPatternTest
    {
        public void Main()
        {
            // The client code.
            Singleton s1 = Singleton.GetInstacne();
            Singleton s2 = Singleton.GetInstacne();

            if(s1 == s2)
            {
                Console.WriteLine("Singleton works, both variables contain the same instacne");
            }
            else
            {
                Console.WriteLine("Singleton failed, variables contain different instances.");
            }
        }
    }

    // This Singleton implementation is called "double check lock".
    // It is safe in multithreaded environment and provides lazy initialization for the
    // Singleton object.
    public class ThreadSafeSingleton
    {
        private ThreadSafeSingleton() { }

        private static ThreadSafeSingleton _instance;

        // We now have a lock object that will be used to synchronize threads
        // during first access to the Singleton
        private static readonly object _lock = new object();

        public static ThreadSafeSingleton GetInstance(string value)
        {
            // This conditional is needed to prevent threads stumbling over the lock
            // once the instacne is ready.
            if(_instance == null)
            {
                // Now, imagine that the program has jus been launched.
                // Since there's no Singleton instance yet, multiple threads can 
                // simultaneously pass the previous conditional and reach this point
                // almost at the same time. The first of them will acquire lock and
                // will proceed further while the rest will wait here.
                lock (_lock)
                {
                    // The first thread to acquire the lock, reaches this conditional,
                    // goes inside and creates the Singleton instance.
                    // Once it leaves the lock block, a thread that might have been
                    // waiting for the lock release may then enter this section.
                    // But since the Singleton field is already initialized, the thread
                    // won't create a new object
                    if(_instance == null)
                    {
                        _instance = new ThreadSafeSingleton();
                        _instance.Value = value;
                    }
                }
            }
            return _instance;
        }

        // We'll use this property to prove that our Singleton really works.
        public string Value { get; set; }
    }

    public class SingletonThreadTest
    {
        public void Main()
        {
            // The Client code.

            Console.WriteLine(
                "{0}\n{1}\n\n{2}\n",
                "If you see the same value, then singleton was reused (yay!)",
                "If you see different values, then 2 singletons were created (booo!!)",
                "RESULT:"
            );

            Thread process1 = new Thread(() =>
            {
                TestThreadSafeSingleton("Foo");
            });
            Thread process2 = new Thread(() =>
            {
                TestThreadSafeSingleton("Bar");
            });


            process1.Start();
            process2.Start();

            process1.Join();
            process2.Join();
        }

        public void TestThreadSafeSingleton(string value)
        {
            ThreadSafeSingleton singleton = ThreadSafeSingleton.GetInstance(value);
            Console.WriteLine(singleton.Value);
        }
    }
}
