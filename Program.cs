using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Abstract FactoryPattern
            //new Client().Main();

            // Builder Pattern
            //new Builder().Main();

            // Factory Pattern
            //new FactoryPattern.Client().Main();

            // Prototype Pattern
            //new PrototypePattern().Main();

            //Singleton Pattern
            //new SingletonPatternTest().Main();
            //new SingletonThreadTest().Main();

            //Thread lock test
            //new MyClass().Run();

            //Adapter Pattern
            //new AdapterPattern().Main();

            //Bridge Pattern
            //new BridgePatterm().Main();

            //Composite Pattern
            //new CompositePattern().Main();

            //Decorator Pattern
            //new DecoratorPattern().Main();

            //Facade Pattern
            //new FacadePattern().Main();

            //Flyweight Pattern
            //new FlyweightPattern().Main();

            //Proxy Pattern
            //new ProxyPattern().Main();

            //Chain of Responsibility Pattern
            //new ChainofResponsibilityPattern().Main();

            //Command Pattern
            //new CommandPattern().main();

            //Iterator Pattern
            //new IteratorPattern().Main();

            // Mediator Pattern
            // new MediatorPattern().Main();

            // Memento Pattern
            // new MementoPattern().Main();

            // Observer Pattern
            // new ObserverPattern().Main();

            // State Pattern
            //new StatePattern().Main();

            // Strategy Pattern
            //new StrategyPattern().Main();

            // Template Pattern
            //new TemplatePattern().Main();

            // Visitor Pattern
            new VisitorPattern().Main();

        }
    }

    class MyClass
    {
        private int counter = 1000;

        // lock?????? ????????? ??????
        private object lockObject = new object();

        public void Run()
        {
            // 10?????? ???????????? ?????? ????????? ??????
            for (int i = 0; i < 10; i++)
            {
                new Thread(UnsafeCalc).Start();
            }
        }

        // Thread-Safe ????????? 
        private void SafeCalc()
        {
            // ????????? ??? ???????????? lock?????? ??????
            lock (lockObject)
            {
                // ????????? ??????
                counter++;

                // ?????? : ?????? ????????? ?????? ??????
                for (int i = 0; i < counter; i++)
                    for (int j = 0; j < counter; j++) ;

                // ????????? ??????
                Console.WriteLine(counter);
            }
        }
        // Thread-Safe?????? ?????? ????????? 
        private void UnsafeCalc()
        {
            // ?????? ????????? ?????? ???????????? 
            // ???????????? ??????
            counter++;

            // ?????? : ?????? ????????? ?????? ??????
            for (int i = 0; i < counter; i++)
                for (int j = 0; j < counter; j++) ;

            // ????????? ??????
            Console.WriteLine(counter);
        }


        //?????? ???:
        // 1001
        // 1002
        // 1003
        // 1004
        // 1005
        // 1006
        // 1007
        // 1008
        // 1009
        // 1010
    }
}
