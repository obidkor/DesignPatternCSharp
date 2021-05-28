using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // 템플릿 패턴은 추상클래스 알고리즘의 골격을 정하고 전체 알고리즘의 구조를 변경하지 않고 하위클래스가 단계를 재정의하도록 허용하는 동적 디자인 패턴이다.
    // 상속을 사용해서 프레임워크의 표준기능을 확장하는 방법을 제공한다.
    // The Template method pattern allows you to defines a skeleton of an algorithm in a base class and let subclasses override the steps without changing the overall algorithm's structure.
    // The Template Method is used to providing framework users with a simple means of extending standard functionality using inheritance.

    // 템플릿 메서드는 제일 부모가 되는 추상클래스에 기본 동작 메서드가 있다. 이걸 상속 클래스가 확장하는 거임.
    // Template Method has a "default" behavior defined by the base class.
    
    // 제일 위에 추상클래스는 알고리즘 공격을 정한다(일반적으로 기본연산알고리즘만 구현)
    // The Abract Class defines a template method that contains a skeleton of some algorithm,
    // Composed of calls to (usually) abstract primitive operation.

    // 하위 사옥클래스는 확장작업을 구현해야하지만 템플릿 메서드자체는 그대로둠.
    // Concrete subclasses should implement these operation, but leave the template method it self intact.


    public abstract class AbstractClass
    {
        // 알고리즘 골격구조는 여기서 정한다.
        // The template method defines the skeleton of an algorithm
        public void TempalteMethod()
        {
            this.BaseOperation1();
            this.RequiredOperations1();
            this.BaseOperation2();
            this.Hook1();
            this.RequiredOperations2();
            this.BaseOperation3();
            this.Hook2();
        }

        // 기본 operation들은 최상위 추상클래스에서 구현
        // These operations already have implemnetations.
        protected void BaseOperation1()
        {
            Console.WriteLine("AbstractClass says: I am doing the bulk of the work");
        }

        protected void BaseOperation2()
        {
            Console.WriteLine("AbstractClass says: But I let subclasses override some operations");
        }

        protected void BaseOperation3()
        {
            Console.WriteLine("AbstractClass says: But I am doing the bulk of the work anyway");
        }

        // 서브클래스가 필수구현해야하는 목록
        //These operations have to be implemented in subclasses.
        protected abstract void RequiredOperations1();

        protected abstract void RequiredOperations2();

        // 서브클래스가 구현해도되고 안해도되는 목록. 
        // These are "hooks". Subclasses may override them, but it's not mandatory since the hooks already have default(but empty) implementation
        // Hooks provide additional extension point in some crucial places of the algorithm.
        protected virtual void Hook1() { }
        protected virtual void Hook2() { }
    }

    // 하위 클래스는 추상메서드들은 모두 구현해야함. 그런데 추상클래스에 이미 구현된 기본메서드들도 상속해서 구현해도됨.
    // Concrete classes have to implement all abstract operations of the base class.
    // They can also overrride some operations with a default implementaion.
    public class ConcreteClass1 : AbstractClass
    {
        protected override void RequiredOperations1()
        {
            Console.WriteLine("ConcreteClass1 says: Implemented Operation1");
        }
        
        protected override void RequiredOperations2()
        {
            Console.WriteLine("ConcreteClass1 says: Implemented Operation2");
        }
    }

    // Usually, concrete classes override only a fraction of base class' operations.
    public class ConcreteClass2 : AbstractClass
    {
        protected override void RequiredOperations1()
        {
            Console.WriteLine("ConcreteClass2 says: Implemented Operation1");
        }

        protected override void RequiredOperations2()
        {
            Console.WriteLine("ConcreteClass2 says: Implemented Operation2");
        }

        // hook을 구현해도됨.
        protected override void Hook1()
        {
            Console.WriteLine("ConcreteClass2 says: Overridden Hook1");
        }
    }

    public static class TemplateClient
    {
        // 클라이언트에서 템플릿의 알고리즘을 실행 => 추상클래스 통해서 호출하는거라 매개변수(어떤 구현체)에 따라 골격이 바뀐다(hook이 실행되고 말고)
        // The client code calls the template method to execute the algorithm.
        // Client code does not have to know the concrete class of an object it work with,
        // as long as it works with objects through the interface of their base classes.
        public static void ClientCode(AbstractClass abstractClass)
        {
            //...
            abstractClass.TempalteMethod();
            //...
        }
    }

    public class TemplatePattern
    {
        public void Main()
        {
            Console.WriteLine("Same client code can work with different subclassess:");
            TemplateClient.ClientCode(new ConcreteClass1());

            Console.Write("\n");
            Console.WriteLine("Same client code can work with different subclassess:");
            TemplateClient.ClientCode(new ConcreteClass2());
        }
    }
}
