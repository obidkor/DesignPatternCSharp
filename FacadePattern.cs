using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{

    // 퍼사드 패턴은 복잡한 라이브러리나 api를 간단한 인터페이스로 만들어준다.
    // Facade Pattern provides a simplified(but limited) interface to a complex system of classess, library or framework
    // Facade decrease the overall complexity of the application, and move unwanted dependencies to one place

    // Facade는 대개 작업을 다른 클래스에 위임하고 사용하는 객체의 전체 수명주기를 관리하는 역할을 한다.
    // Facade can be recognized in a class that has a simple interface, but delegates most of the work to other class
    // Usually, Facades manage the full life cycle of objects they use.

    // 클라이언트로부터의 요청사항을 알맞은 서브시스템의 object로 위임한다.
    // The Facade class prpvides a simple interface to the complex logic of one or serveral subsystems. 
    // The Facade delegates the client requests to the appropriate objects within the subsystem. 
    // The Facade is also responsible for managing their lifecycle. 
    // All of this shields the client from the undesired complexity of the subsystem.
    public class Facade
    {
        protected Subsystem1 _subsystem1;

        protected Subsystem2 _subsystem2;

        public Facade(Subsystem1 subsystem1, Subsystem2 subsystem2)
        {
            this._subsystem1 = subsystem1;
            this._subsystem2 = subsystem2;
        }

        // Facade 클래스의 메서드들은 복잡한 서브시스템의 기능들을 간단하게 쓸수 있도록 제공되어야한다.
        // 근데 이렇게 쓰면 서브시스템의 기능을 자유롭게 100%다 못쓴다.
        // The Facade's methods are convenient shortcuts to the sophiscated functionality of the subsystem.
        // However, clients get only to a fraction of a subsystem's capabilities.
        public string Operation()
        {
            string result = "Facade initializes subsystems:\n";
            result += this._subsystem1.operation1();
            result += this._subsystem2.operation1();
            result += "Facade orders subsystems to perform the action:\n";
            result += this._subsystem1.operationN();
            result += this._subsystem2.operationZ();
            return result;
        }


    }

    // 고객입장에서는 당연히 퍼사드를 쓰거나 서브시스템을 쓰거나 둘다 할 수 있다.
    // 서브시스템 입장에서는 퍼사드로 부터 요청받거나 클라이언트로 부터 요청받거나..
    // 퍼사드도 결국에는 서브시스템입장에서는 고객의 요청일 뿐이다.
    // The Subsystem can accept requests either from the facade or client directly
    // In any case, to the Subsystem, the Facade is yet another client, ans it's not a part of the Subsystem.
    public class Subsystem1
    {
        public string operation1()
        {
            return "Subsystem1: Ready!\n";
        }

        public string operationN()
        {
            return "Subsystem1: Go!\n";
        }

    }

    // 퍼사드 패턴은 멀티서브시스템을 한번에 처리할 수 있어야한다.
    // Some facades can work with multipel subsystems at the same time
    public class Subsystem2
    {
        public string operation1()
        {
            return "Subsystem2: Get ready!\n";
        }

        public string operationZ()
        {
            return "Subsystem2: Fire!\n";
        }

    }

    public static class FacadeClient
    {
        // 퍼사드의 장점은 고객이 복잡한 API나 프레임워크를 간단한 인터페이스를 통해 조작이 가능하다는 점이다.
        // The Client code works with complex subsystems through a simple interface provided by the Facade.
        // When a facade manages the lifecycle of the subsystem, the client might not even know about the existence of the subsystem.
        // This approach lets you keep the complexity under control.
        public static void ClientCode(Facade facade)
        {
            Console.Write(facade.Operation());
        }
    }

    public class FacadePattern
    {
        public void Main()
        {
            // 고객의 코드에 subsystem의 인스턴스가 있을수도 있다. 이 경우에는 이객체를 그대로 쓰는게 좋을 수도 있다.
            // The client code may have some of the subsystem's objects already created.
            // In this case, it might be worthwhile to initialize the Facade 
            // with these objects instead of letting the Facade create new instance.
            Subsystem1 subsystem1 = new Subsystem1();
            Subsystem2 subsystem2 = new Subsystem2();
            Facade facade = new Facade(subsystem1, subsystem2);
            FacadeClient.ClientCode(facade);
        }
    }
}
