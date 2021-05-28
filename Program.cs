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

        // lock문에 사용될 객체
        private object lockObject = new object();

        public void Run()
        {
            // 10개의 쓰레드가 동일 메서드 실행
            for (int i = 0; i < 10; i++)
            {
                new Thread(UnsafeCalc).Start();
            }
        }

        // Thread-Safe 메서드 
        private void SafeCalc()
        {
            // 한번에 한 쓰레드만 lock블럭 실행
            lock (lockObject)
            {
                // 필드값 변경
                counter++;

                // 가정 : 다른 복잡한 일을 한다
                for (int i = 0; i < counter; i++)
                    for (int j = 0; j < counter; j++) ;

                // 필드값 읽기
                Console.WriteLine(counter);
            }
        }
        // Thread-Safe하지 않은 메서드 
        private void UnsafeCalc()
        {
            // 객체 필드를 모든 쓰레드가 
            // 자유롭게 변경
            counter++;

            // 가정 : 다른 복잡한 일을 한다
            for (int i = 0; i < counter; i++)
                for (int j = 0; j < counter; j++) ;

            // 필드값 읽기
            Console.WriteLine(counter);
        }


        //출력 예:
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
