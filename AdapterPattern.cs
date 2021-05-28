using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Adapter is a structural design pattern, which allows imcompatible object to collaborate.
    // The Adapter acts as a wrapper.
    // It catches calls for one object ans transforms them to format and interface recoginzable by second object.
    // often used in legacy code.

    
    // Adapter is recognizable by a constructor which takes an instance of
    // a differernt abstract/interface type.
    // When the adapter receives a call to any of its methods, it translates 
    // parameters to the appropriate format and then directs the call
    // to one or serveral methods of the wrapped object.

    // 호환되는 domain의 interface
    // The Target defines the domain-specific interface used by the client code.
    public interface ITarget
    {
        string GetRequest();
    }

    // The Adaptee contains some useful behavior, but its interface is
    // incompatible with the existing client cod. The Adaptee needs some 
    // adaptation before the client code can use it.
    public class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Specific request";
        }
    }

    // Adapter는 호환대상이 되는 Target의 인터페이스에 새로 붙는 기능(Adaptee)의 interface를 호환시킨다.
    // 이때 Adapter는 호환대상이 되는 Target의 인터페이스를 상속해야한다.
    // The Adapter makes the Adaptee's interface compatible with the Target's interface.
    public class Adapter : ITarget
    {
        // Adaptee(새로운기능)을 필드변수로 선언해
        private readonly Adaptee _adaptee;

        // 생성자에 set해준다
        public Adapter(Adaptee adaptee)
        {
            this._adaptee = adaptee;
        }

        // 호환대상의 기능에 새로운 기능(Adaptee의 기능)을 구현한다.
        public string GetRequest()
        {
            return $"This is {this._adaptee.GetSpecificRequest()}";
        }
    }

    public class AdapterPattern
    {
        public void Main()
        {
            Adaptee adaptee = new Adaptee();
            //Adapter를 선언해서(Target의 인터페이스로 선언) Adapter의 필드에 있는 Adaptee를 세팅해준다.
            ITarget target = new Adapter(adaptee);

            Console.WriteLine("Adapter interface is incompatible with the client");
            Console.WriteLine("But with adapter client can call it's method");

            //Adapter에서 구현한 Adaptee의 기능을 사용
            Console.WriteLine(target.GetRequest());

        }
    }
}
