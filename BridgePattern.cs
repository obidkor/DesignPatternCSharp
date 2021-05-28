using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Bridge is a structural design pattern that devides business logic or hug class
    // into seperate class hierarchies that can be developed independently.

    // Once of these hierarchies(often called the Abstraction) 
    // will get a reference to an object of the second hierarchy(Implementation)
    // The abstraction will be able to delegate some(most) of its calls to the implementations object.
    // Since all implementations will have a common interface , they'd be interchangable inside the abstraction.

    // The Bridge pattern is especaially useful when dealing with cross-platform apps,
    // supporting multiple types of database servers or working with serveral API providers of
    // a certain kind(ex: cloud ,social network)

    // Brige can be recogized by a clear distinction between some controlling entity and
    // serveral differnt platforms that it relies on.

    // 서로 다른 Implementation을 연결하는 Control 클래스
    // The Abstraction defines the interface for the "Control" part of the two class hierarchies.
    // It maintains a reference to an object of the Implementation hierarchy
    // and delegates all the real work to this object
    public class Abstraction
    {
        // 서로 Implementation이 공유하는 interface를 필드에 선언한다.
        protected IImplementation _implementation;

        // Control Entity 생성시 받는 파라미터로 각각의 Implementation을 받는다.
        public Abstraction(IImplementation implementation)
        {
            this._implementation = implementation;
        }


        // Bride패턴에서 연결하고자하는 로직을 구현 + implementation의 로직을 더해준다.
        public virtual string Operation()
        {
            return "Abstract : Base Operation with : \n" +
                _implementation.OperationImplementation();
        }
    }

    // Control 객체(Bridge의 핵심 객체)는 확장이 가능하다.
    // You can extend the Abstraction without changing the Implementation classes.
    public class ExtendedAbstraction : Abstraction
    {
        // 생성자에 부모객체의 생성자도 넣어주고
        public ExtendedAbstraction(IImplementation implementation) : base(implementation)
        {
        }

        // operation 이걸 override해주면된다.
        public override string Operation()
        {
            return "ExtendedAbstraction: Extended operation with:\n" +
                 base._implementation.OperationImplementation();
        }
    }


    // 서로다른 impletation이 공유하는 interface
    // The implementation defines the interface for all implementation classes.
    // It doesn't have to match the Abstraction's interface. In fact, the two
    // interfaces can be entirly different. Typically the Implementation interface
    // provides only primitive operations, while the Abstraction defines higer-level
    // operations based on those primitives.
    public interface IImplementation
    {
        string OperationImplementation();
    }

    // 서로다른 implementation은 각자의 operation을 가지고있당.
    // Each Concrete Implementation corresponds to a specific platform and
    // implements the Implementatison interface using that platfrom's API.
    public class ConcreteImplementationA : IImplementation
    {
        public string OperationImplementation()
        {
            return "ConcreteImplementationA: The result in platform A.\n";
        }
    }

    public class ConcreteImplementationB : IImplementation
    {
        public string OperationImplementation()
        {
            return "ConcreteImplementationB: The result in platform B.\n";
        }
    }


    // Control 객체(Abstraction)은 서로다른 implementation을 연결하는 interface를 필드로 가지고있기때문에
    // client는 이 Control 객체를 가지고 operation을 지정해주면 된다.
    public class Client1
    {
        // Except for the initialization phase, where an Abstraction object gets
        // linked with a specific Implementation object, the client code should
        // only depend on the Abstract class.  This way the client code can
        // support any abstract-implementation combination
        public void ClientCode(Abstraction abstraction)
        {
            // Control 객체의 operation의 실행
            Console.WriteLine(abstraction.Operation());
        }
    }

    public class BridgePatterm
    {
        public void Main()
        {
            Client1 client = new Client1();

            // Bridge를 위해 Control 객체를 선언하고
            Abstraction abstraction;
            // The client code should be able to work with any pre-configured
            // abstraction-implementation combination 
            // control 객체 생성자안에 implementation을 넣어준다. 
            abstraction = new Abstraction(new ConcreteImplementationA());
            // Conrol.operation + implementation.operation
            client.ClientCode(abstraction);

            Console.WriteLine();

            abstraction = new Abstraction(new ConcreteImplementationB());
            client.ClientCode(abstraction);

        }


        
    }
}
