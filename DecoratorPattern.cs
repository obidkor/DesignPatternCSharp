
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Decorator is a structural pattern that allows adding new behaviors to objects 
    // dynamically by placing them inside special wrapper objects.

    // Using decorators you can wrap objects countless number of times 
    // since both target objects and decorators follow the same interface.
    // The resulting object will get a stacking behavior of all wrappers.

    // Decorator can be recoginzed by creation methods or constructor that accept
    // objects of the same class or interface as a current class

    
    // 같은 인터페이스를 공유해야함.
    // The base Component interface defines operations that can be altered by decorators.
    public abstract class Component1
    {
        public abstract string Operation();
    }

    // 본래기능을 제공하는 클래스
    // Concrete Components provide default implementations of the operations.
    // There might be several variations of theses classes
    public class ConcreteComponent : Component1
    {
        public override string Operation()
        {
            return "ConcreteComponent";
        }
    }

    // Decorator
    // The base Decorator class follows the same interface as the other
    // components. The primary purpose of this class is to define the wrapping
    // interface for all concrete decorators. The default implementation of the
    // wrapping code might include a field for storing a wrapping component and
    // the means to initialize it.
    public abstract class Decorator : Component1
    {
        // 여기에 본래의 component를 배정한다.
        protected Component1 _component;

        public Decorator(Component1 component)
        {
            this._component = component;
        }

        public void SetComponent(Component1 component)
        {
            this._component = component;
        }

        // 본래 Component의 operation을 구현
        // The Decorator delegates all work to the wrapped component.
        public override string Operation()
        {
            if(this._component != null)
            {
                return this._component.Operation();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    // 추가될 decorator1(decorator 상속)
    // Concrete Decorators call the wrapped object and alter its result in some way.
    public class ConcreteDecoratorA : Decorator
    {
        // 생성자에 부모클래스(decorator)의 기본 component에 파라미터를 세팅해준다.
        public ConcreteDecoratorA(Component1 component) : base(component)
        {
        }

        // wrapping 되어 추가될 속성을 구현해주고 base.operation()을 통해 기본속성의 operation도 구현해준다.
        // Decorator may call parent implementation of the operation, instead of
        // calling the wrapped object directly. This approach simplifies extension of decorator classes.
        public override string Operation()
        {
            return $"ConcreteDecoratorA({base.Operation()})";
        }
    }

    // Decorators can execute their behavior either before or after the call to a wrapped object
    public class ConcreteDecoratorB : Decorator
    {
        public ConcreteDecoratorB(Component1 component) : base(component)
        {
        }

        public override string Operation()
        {
            return $"ConcreteDecoratorB({base.Operation()})";
        }
    }


    // 클라이언트에서 wrpping이 완료된 decorator + component의 operation을 실행해준다.
    public class Client3
    {
        // The client code works with all objects using the Component interface
        // This way it can stay independent of the concrete classes fo components it works with
        public void Clientcode(Component1 component)
        {
            Console.WriteLine("Result: " + component.Operation());
        }
    }

    public class DecoratorPattern
    {
        public void Main()
        {
            Client3 client = new Client3();
            var simple = new ConcreteComponent();
            Console.WriteLine("Client : I get a simple component:");
            client.Clientcode(simple);
            Console.WriteLine();

            // ... as well as decorated ones.

            // Note how decorators can wrap not only simple components but the other decorators as well
            ConcreteDecoratorA decorator1 = new ConcreteDecoratorA(simple);
            ConcreteDecoratorB decorator2 = new ConcreteDecoratorB(decorator1);
            ConcreteDecoratorB decorator3 = new ConcreteDecoratorB(decorator2);
            Console.WriteLine("Client: Now I've got a decorated component:");
            client.Clientcode(decorator3);
        }        
    }
}
