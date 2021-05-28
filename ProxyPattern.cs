using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // 프록시 패턴은 실제 서비스 객체의 대체객체를 선언한다. / 이 프록시객체는 클라이언트의 요청을 받거나 요청을 수행하고 이 요청을 서비스객체에 전달한당.
    // Prpxy provides an object that acts as a substitute for a real service object used by a client.
    // A proxy receives client request, does some work(access control, caching, etc) and then passes the request to a service object.

    // 프록시 객체는 실제 서비스와 동일한 인터페이스를 상속하여 클라이언트에 전달될 때 실제 객체와 교환될 수 있다.
    // The proxy object has the same interface as a service, which makes it interchangeable with a real object when passed to a client

    // 프록시 패턴으로 클라이언트 코드를 건드리지 않고 기능을 추가할 수 있음.
    // While the Proxy pattern isn't a frequent in most c# applications, It's irreplaceable when yo want to add some additional behaviors to an object
    // of some existing class without changing the client code

    // 프록시는 real service 객체들을 참조할 수 밖에 없다.(프록시가 서비스 하위 클래스가 아니라면)
    // 프록시는 모든 실제작업을 다른 객체에 위임한다.
    // Proxies delegates all of the real work to some other object. Each proxy method should, intheend, refer to a service object
    // unless the proxy is a subclass of a service

    
    // Subject 인터페이스는 프록시와 실제 서비스 객체가 공유하는 인터페이스임.
    // 공유하기 때문에 프록시로 실제 서비스객체의 메서드를 소환가능한거임.
    // The Subject interface declares common operations for both RealSubject and the Proxy.
    // As long as the client works with RealSubject using this interface, you'll be able to pass it a proxy instead of a real subject.
    public interface ISubject
    {
        void Request();
    }

    // 서비스객체에서 어떠한 기능을 추가할 경우 프록시를 사용하면 서비스객체를 수정할 필요 없이 기능을 추가가능.
    // The RealSubject contains some core business logic. Usually, RealSubjects are capable of doing some useful work
    // which may also be very slow or sensitive for example, correcting input data. A Proxy can solve these issues without any changes to the RealSubject's code.
    public class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: handling Request.");
        }
    }

    // 프록시가 상속하는 인터페이스는 서비스객체의 인터페이스와 같다.
    // The Proxy has an interface indentical to the RealSubject.
    public class Proxy : ISubject
    {
        private RealSubject _realSubject;

        // 진짜 서비스객체를 넣어서 배정해준다.. 없어도 되긴하다.
        public Proxy(RealSubject realSubject)
        {
            this._realSubject = realSubject;
        }

        // 프록시 패턴을 사용하면 레이지로딩, 캐싱, Auth등 다양한 기능을 사용할 수 있고 
        // 그 결과를 실제 서비스 객체의 이름이 같은(프록시메서드이름 = 서비스객체메서드이름) 메서드에 전달할 수 있다.
        // The most common applications of the Proxy pattern  are lazy loading,
        // caching, controlling the access, logging, etc. A Proxy can perform one of these things and then,
        // depending on the result, pass the execution to the same method in a linked RealSubject object.
        public void Request()
        {
            if (this.CheckAccess())
            {
                this._realSubject.Request();
                this.LogAccess();
            }
        }
        public bool CheckAccess()
        {
            // Some real checks should go here.
            Console.WriteLine("Proxy : Checking access prior to firing a real request.");
            return true;
        }

        public void LogAccess()
        {
            Console.WriteLine("Proxy : Logging the time fo request");
        }

    }

    public class ProxyClient
    {
        // 클라이언트의 코드는 프록시를 쓰거나 실제 서비스객체를 쓰거나 둘중하나다. 둘은 인터페이스를 공유하기때문에 가능하다
        // 클라이언트 코드의 레거시에 서비스 객체를 대부분 접근해서 쓴다면, 이 경우 proxy를 좀더 쉽게쓰려면 실제 서비스객체를 프록시가 상속하면 좀더 쉽다.
        // The client code is supposed to work with all objects (both subjects and proxies)
        // via the Subject interface in order to support both real subjects and proxies.
        // In real life, however, clients mostly work with ther real subjects directly.
        // In this case , to implement the pattern more easily, you can extend your proxy from the real subject's class.
        public void ClientCode(ISubject subject)
        {
            // ...

            subject.Request();

            // ...
        }
    }



    public class ProxyPattern
    {
        public void Main()
        {
            ProxyClient client = new ProxyClient();

            Console.WriteLine("Client: Executing the client code with a real subject:");
            RealSubject realSubject = new RealSubject();
            client.ClientCode(realSubject);

            Console.WriteLine();

            Console.WriteLine("Client: Executing the same client code with a proxy:");
            Proxy proxy = new Proxy(realSubject);
            client.ClientCode(proxy);
        }
    }
}
