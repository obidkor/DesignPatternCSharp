using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class AbstractFactoryPattern
    {

        //팩토리 메서드 패턴
        // 조건에 따른 객체 생성을 팩토리 클래스로 위임하여, 팩토르 클래스에서 객체를 생성하는 패턴(링크 )
        //추상 팩토리 패턴
        //서로 관련이 있는 객체들을 통째로 묶어서 팩토리 클래스로 만들고, 이들 팩토리를 조건에 따라 생성하도록 다시 팩토리를 만들어서 객체를 생성하는 패턴

        //https://whereami80.tistory.com/211
    }


    // 추상팩토리 패턴은 팩토리가 여러개 있으면(서로 연관된 도메인) 이걸 인터페이스로 하나로 묶는다.
    // 팩토리 패턴은 팩토리가 하나..
    // Abstract Factory -> Abstract Product(family / co-related theme or concept)
    // product B -> collarborate product A 
    // product A -> cannot collarborate product A 
    public interface IAbstractFactory
    {
        // 요렇게 하나로 묶는 이유는 팩토리끼리 연동 시키기 위해서이다.
        // 팩토리 1
        IAbstractProductA CreateProductA();
        // 팩토리 2
        IAbstractProductB CreateProductB();
    }

    // 추상팩토리패턴의 ConcreteFactory는 팩토리 패턴의 Creator와 같다.
    // Abstract product형태의 ConcreteProduct 인스턴스를 리턴한다.
    // Concrete Factory -> produce a family of products
    // Products are compatible as they are in same variant
    // return an abstract product 
    // on the other hand, inside of the method a concrete product is instantiated
    class ConcreteFactory1 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA1();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB1();
        }
    }
    //Each Concrete Factory has a corresponding product variant.
    class ConcreteFactory2 : IAbstractFactory
    {
        public IAbstractProductA CreateProductA()
        {
            return new ConcreteProductA2();
        }

        public IAbstractProductB CreateProductB()
        {
            return new ConcreteProductB2();
        }


    }
    
    // Product들도 각자의 인터페이스가 존재한다. 팩토리 패턴은 product인터페이스가 하나이다.
    // Product 인터페이스가 두개이므로 서로의 인터페이스 간의 교류도 가능하다.(B->A)
    // Each distinct product of a product family should have a base interface 
    // All variants of the product should implement this interface
    public interface IAbstractProductA
    {
        string UsefulFuctionA();
    }

    // Product 객체들은 ConcreteFacrory로 인핸 생성된다.
    // Concrete products are created by corresponding Concrete Factories.
    internal class ConcreteProductA1 : IAbstractProductA
    {
        public string UsefulFuctionA()
        {
            return "The result of the product A1.";
        }
    }


    internal class ConcreteProductA2 : IAbstractProductA
    {
        public string UsefulFuctionA()
        {
            return "The result of the product A2.";
        }
    }


    // ProductB 인스턴스에서는 ProductA의 메서드도 사용가능하다.(같은 Factory 인스턴스 내에서만)
    // All products in ProductsB can interact with each other,
    // only within condtion that same concrete factory(variant)
    public interface IAbstractProductB
    {
        // product B is able to do its own thing
        string UsefulFunctionB();

        // IAbstractProductA를 매개변수로 넣어준다.
        // it also can collarborate with productA
        // the Abract Factory(variant) makes sure that products in its own  variant  are compatible
        string AnotherUsefulFuctionB(IAbstractProductA collaborator);
    }

    // Concrete products are created by corresponding concrete factory
    internal class ConcreteProductB1 : IAbstractProductB
    {

        public string UsefulFunctionB()
        {
            return "The result of the product B1.";
        }

        // IAbstractProductA 형태의 인스턴스를 사용할 수 있다.(인스턴스는 팩토리의 인스턴스에 따라 달라진다)
        // in detail the variant of Product B1, is only able to work with Product A1
        // but it can accepts any instance of AbstractProdcutA as an argument.
        public string AnotherUsefulFuctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFuctionA();
            return $"The result of the B1 collaborating with the {result}";
        }
    }




    internal class ConcreteProductB2 : IAbstractProductB
    {
        public string UsefulFunctionB()
        {
            return "The result of the product B2.";
        }
        // IAbstractProductA 형태의 인스턴스를 사용할 수 있다.(인스턴스는 팩토리의 인스턴스에 따라 달라진다)
        // in detail the variant of Product B2, is only able to work with Product A2
        // but it can accepts any instance of AbstractProdcutA as an argument.
        public string AnotherUsefulFuctionB(IAbstractProductA collaborator)
        {
            var result = collaborator.UsefulFuctionA();
            return $"The result of the B2 collaborating with the {result}";
        }
    }

    // 클라이언트는 factory와 product에 인터페이스(추상클래스)를 통해서만 접근이 가능한다.
    // the Client code works with factories and products only through 
    // the each abstract types which are AbstractFactory and AbstractProduct
    // client dont need to pass any factory or product subclass without breaking it.
    public class Client
    {
        public void Main()
        {
            // The client code can work with any concrete factory class.
            Console.WriteLine("Client : Testing client code with the first factory type...");
            ClientMethod(new ConcreteFactory1());
            Console.WriteLine();

            Console.WriteLine("Client : Testing the same client code with the secont factory type...");
            ClientMethod(new ConcreteFactory2());
        }

        public void ClientMethod(IAbstractFactory factory)
        {
            var productA = factory.CreateProductA();
            var productB = factory.CreateProductB();

            Console.WriteLine(productB.UsefulFunctionB());
            Console.WriteLine(productB.AnotherUsefulFuctionB(productA));
        }
    }

}
