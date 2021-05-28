using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // 전략패턴은 여러 동작 기능들을 한 객체에 세팅하고 이것을 오리지날 context에서 계속 교체할수 있게끔한다.
    // Original Context 는 Strategy object의 인스턴스(인터페이스,추상클래스)를 가지고 있으며 여기에 동작을 위임한다. 
    // 작업 방식을 교체하기 위해 Strategy object는 계속 다른 개체로 바뀔수 있다,
    // Strategy pattern turns a set of behaviors into object and makes them interchangalbe inside original context object.
    // The Original object, called context, holds a reference to a strategy object and delegate it executing the behavior.
    // In order to change the way the context performs it works, other objects may replace the currently linked strategy object with another one

    // 전략패턴으로 상속없이 기능을 추가하고 빼고가 가능.
    // 중첩된 object가 실제 기능을 수행하도록 하는 메서드(다형성)가 있으며. Strategy 인터페이스의 setter가 있다.
    // The Strategy pattern provides users a way to change the behavior of a class without extending it
    // Strategy pattern has a method that lets nested object do the actual work, as well as the setter that allows replacing that object with different one.

    // The Context defines the interface of interest to clients.
    public class StrategyContext
    {
        // Context 클래스는 Strategy object를 가지고 이걸로 동작을 꼇다뺏다하는디
        // 여기서 Strategy는 인터페이스이다.
        // The Context maintain a reference to one of the Strategy objects.
        // The Context doesn't know the concrete class of a strategy.
        // It should work with all strategy via the Strategy interface.
        private IStrategy _strategy;

        public StrategyContext() { }

        // Usually, the Context accepts a strategy through the constructor,
        // but also provides a setter to change it at runtime.
        public StrategyContext(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // the Context allows replace
        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        // Strategy 구현체를 갈아 끼워주면 각기다른 DoAlgorithm이 시행됨. 메서드 이름을 똑같이 해야함(다형성).
        // The Context delegates some work to the Strategy object instead of implementing multiple version of the algorithm on its own.
        public void DoSomeBusinessLogic()
        {
            Console.WriteLine("Context: Sorting data using the strategy(not sure how it'll dot it)");
            var result = this._strategy.DoAlgorithm(new List<string> { "a", "b", "c", "d", "e" });

            string resultStr = string.Empty;
            foreach(var element in result as List<string>)
            {
                resultStr += element + ",";
            }

            Console.WriteLine(resultStr);
        }

    }

    // 알고리즘 수만금 IStrategy의 구현체를 만든다. 인터페이스를 통해 호출 -> 알고리즘 구현체
    // the Strategy interface declares operations common to all supported version of some algorithm
    // The Context uses this interface to call the algorithm defined by Concrete Startegies.
    public interface IStrategy
    {
        object DoAlgorithm(object data);
    }

    // Concrete Strategies implement the algorithm while following the base
    // Strategy interface. The interface makes them interchangeable in the Context.
    public class ConcreteStarategyA : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();

            return list;
        }
    }

    public class ConcreteStarategyB : IStrategy
    {
        public object DoAlgorithm(object data)
        {
            var list = data as List<string>;
            list.Sort();
            list.Reverse();

            return list;
        }
    }

    public class StrategyPattern
    {
        public void Main()
        {
            // The client code picks a concrete strategy ans passes it to the context.
            // The client should be awre of the differences between strategies in order to make the right choice.
            var context = new StrategyContext();

            Console.WriteLine("Client: Strategy is set to normal sorting");
            context.SetStrategy(new ConcreteStarategyA());
            context.DoSomeBusinessLogic();

            Console.WriteLine();

            Console.WriteLine("Client: Strategy is set to reverse sorting.");
            context.SetStrategy(new ConcreteStarategyB());
            context.DoSomeBusinessLogic();
        }
    }
}
