using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{

    // 요약 : creating product objects without specifying their concrete classes.

    //Factory Method defines a method, which should be used for creating objects
    //instead of direct constructor call(new operator). 
    //Subclasses can override this method to change the class of objects that will be created.

    // Factory Pattern can be recoginzed by creation methods, which creates
    // object from concrete classes but return them as objects of abstract type or interface


    
    // interface두개 1. product interface 2. creator interface : 이렇게 만드는 이유는 Factory와 product가 독립적으로 존재해야하기 때문이다.
    // The Creator class declares the factory method that is supposed to return
    // an object of a Product class. The Creator's subclasses usually provide
    // the implementation of this method
    public abstract class Creator
    {
        // Creator interface는 product interface를 생성하는 메서드를 상속시켜야하고.
        // Note that the Creator may also provide some default implementation of the factory method
        public abstract IProduct FactoryMethod();


        // Operation을 구현한 메서드를 가지고 있어야 한다. 
        // despite its name, the Creator's primary responsibility is not creating product.
        // Usually, it contains some core business logic that relies on Product object,
        // ,returned by the factory method. subClasses can indirectly change that business logic
        // by overriding the factory method and returning a different type of product from it.
        public string SomeOperation()
        {
            // 구현한 상세 operation 메서드에서는 product interface를 호출하여 그것의 operation을 호출한다.
            // 이걸 호출 시 생성된 product의 operation으로 내려간다.
            // Call the factory method to create a Product object
            var product = FactoryMethod();
            // use the product.
            var result = "Creator : The same creator's code has just worked with "
                + product.Operation();

            return result;
        }
    }

    // creator를 상속한 상세 Creator 클래스
    // Concrete Creators override the factory method in order to change the 
    // resulting product's type.
    public class ConcreteCreator1 : Creator
    {
        // 여기서 상세 product를 생성한다.
        // Note that the signature of the method still uses the abstract product type,
        // even though the concrete product is actually returned from the method.
        // This way the Creator can stay independent of concrete product classes.
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct1();
        }
    }
    
    public class ConcreteCreator2 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct2();
        }
    }
    
    // product 인터페이스 =>  product의 기능을 구현한다.
    // The product interface declares the operations that all concrete product must implement.
    public interface IProduct
    {
        string Operation();
    }

    // Concrete Products provide various implementations of the Product interface.
    public class ConcreteProduct1 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct1}";
        }
    }

    public class ConcreteProduct2 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct2}";
        }
    }
    public class FactoryPattern
    {

        public class Client
        {
            public void Main()
            {
                Console.WriteLine("App : Launched with the ConcreteCreator1.");
                // Client는 Factory(Creator)객체를 생성해서  product의 operation을 사용한다. 
                ClientCode(new ConcreteCreator1());

                Console.WriteLine("");

                Console.WriteLine("App : Launched with the ConcreteCreator2.");
                ClientCode(new ConcreteCreator2());

            }

            // Creator interface를 통해 같은 코드로 다른 Factory객체를 생성해 각 Product의 operation을 사용할 수 있다.
            // The client code works with an instance of a concrete creator, albeit
            // through its base interface. As long as the client keeps working with
            // the creator via the vase interface, you can pass it any creator's subclass.
            public void ClientCode(Creator creator)
            {
                //...
                Console.WriteLine("Client : I'm not aware of the creator's class," +
                    " but it still works.\n" + creator.SomeOperation());
                //...
            }
        }
        
    }
}
