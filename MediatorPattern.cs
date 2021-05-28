using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Mediator 패턴은 두 object의 통신을 mediator를 통함으로써 프로그램 요소간의 결합도를 줄인다.
    // 결합도를 낮춤으로써 재사용성, 확장성을 늘린다.
    // Mediator reduces couping between components of a program by making them communicate indirectly, through a special mediator object
    // The Mediaotr makes it easy to modify, extend and reuse individual components because they're no longer dependent on the dozen of other classes
     
    // Mediator pattern is facilitating communications between GUI components of an app. The synonym of the Mediator is the Controller part of MVC pattern.

    // Mediator 인터페이스는 컴포넌트의 이벤트를 감지해서 이를 다른 컴포넌트에 통보하는 메서드를 가진다.
    // The Mediator interface declares a method used by components to notify the mediator  about various events.
    // The Mediator may react to these events and pass the execution to other components. 
    public interface IMediator
    {
        // 통보 메서드
        void Notify(object sender, string ev);
    }

    
    // Concrete Mediators implements cooperative behavior by coordinating several components.
    public class ConcreteMediator : IMediator
    {
        // Mediator 구현 클래스는 이벤트를 주고받을 컴포넌트의 객체를 가지고 있다. 그래야지 이벤트에 맞춰 콜백해줄꺼아녀..
        private ComponentA _componentA;
        private ComponentB _componentB;

        public ConcreteMediator(ComponentA componentA, ComponentB componentB)
        {
            // Mediator 생성시에 주고받을  컴포넌트를 넣어준다.
            this._componentA = componentA;
            this._componentB = componentB;
            // component의 mediator도 지정해준다.
            this._componentA.SetMediator(this);
            this._componentB.SetMediator(this);
        }

        // 어캐반응할지 다른 컴포넌트의 메서드를 지정해준다.
        public void Notify(object sender, string ev)
        {
            if(ev == "A")
            {
                Console.WriteLine("Mediator reacts on A and triggers following operations:");
                this._componentB.DoC();
            }
            if (ev == "D")
            {
                Console.WriteLine("Mediator reacts on D and triggers following operations:");
                this._componentA.DoB();
                this._componentB.DoC();
            }
        }
    }

    // 컴포넌트의 기본 부모 객체는 Mediator 인스턴스를 가지고 있다.
    // The Base Component provides the basic functionality of storing a mediator's instance inside component objects.
    public class BaseComponent
    {
        protected IMediator _mediator;

        public BaseComponent(IMediator mediator = null)
        {
            this._mediator = mediator;
        }

        // 컴포넌트의 mediator를 지정한다.
        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    // 컴포넌트 구현체는 각종 기능을 메서드로 구현한다. 정확히 이 구현체는 다른 컴포넌트나 mediator와는 의존성이 없다.
    // 단지 의존있는 것을 상속할뿐..
    // Concrete Components implement various functionality. They dont depend on
    // other componet. They also dont depend on any concrete mediator classes.
    public class ComponentA : BaseComponent
    {
        public void DoA()
        {
            // 어떤 이벤트가 감지되면
            Console.WriteLine("Component A does A");
            // mediator의 notify로 콜백 호출~
            this._mediator.Notify(this, "A");
        }

        public void DoB()
        {
            Console.WriteLine("Component A does B");

            this._mediator.Notify(this, "B");
        }
    }

    public class ComponentB : BaseComponent
    {
        public void DoC()
        {
            Console.WriteLine("Component B does C");

            this._mediator.Notify(this, "C");
        }

        public void DoD()
        {
            Console.WriteLine("Component B does D");

            this._mediator.Notify(this, "D");
        }
    }


    public class MediatorPattern
    {
        public void Main()
        {
            // The client code
            ComponentA componentA = new ComponentA();
            ComponentB componentB = new ComponentB();

            // mediator 세팅
            new ConcreteMediator(componentA, componentB);

            Console.WriteLine("Client triggers operation : A");
            componentA.DoA();

            Console.WriteLine();

            Console.WriteLine("Client triggers operation : D");
            componentB.DoD();

        }
    }
}
