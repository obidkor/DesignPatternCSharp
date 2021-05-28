using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{

    // Visitor allows adding new behavior to existing class hierarchy without altering any existing code.
    // Visitor 패턴은 잘 안쓰인다..

    // The Component interface declares an 'accept' method that should take the base visitor interface as an argument.
    public interface IComponent
    {
        void Accept(IVisitor visitor);
    }

    // Each Concrete Component must implement the 'Accept' method in such a way
    // that it calls the visitor's method corresponding to the component's class.
    public class ConcreteComponentA : IComponent
    {
        // Note that we're calling 'VisitorConcreteComponetntA', which matches the current class name.
        // This way we elt the visitor know the class of the component it work with.
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteCompoenetA(this);
        }

        // Concrete Components may have special methods that don't exsis in their base class or interface.
        // The Visitor is still able to use these mehtods since it's aware of the compenent's concrete class.
        public string ExclusiveMethodOfConcreteComponentA()
        {
            return "A";
        }
    }

    public class ConcreteComponentB : IComponent
    {
        // Same here : VisitConcreteCompenentB => ConcreteComponentB
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteCompoenetB(this);
        }

        public string SpecialMethodofConcreteComponentB()
        {
            return "B";
        }
    }

    // the Visitor Interface declares a set of visiting methods that correspond to component classes.
    // The signature of a visiting method allows the visitor to identify the exact class of the component that it's dealing with.
    public interface IVisitor
    {
        void VisitConcreteCompoenetA(ConcreteComponentA element);

        void VisitConcreteCompoenetB(ConcreteComponentB element);
    }


    // Concrete Visitors implement serveral version of the same algorithm,
    // which can work with all concrete component class.

    // You can experience the biggest benefit of the Visitor pattern when using it with a complex object structure, such as composite tree
    // In this case, it might be helpful to store some intermediate state of the algorithm while executing visitor's methods over various objects of the structure.
    public class ConcreteVisitor1 : IVisitor
    {
        public void VisitConcreteCompoenetA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor1");
        }

        public void VisitConcreteCompoenetB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodofConcreteComponentB() + " + ConcreteVisitor1");
        }
    }

    public class ConcreteVisitor2 : IVisitor
    {
        public void VisitConcreteCompoenetA(ConcreteComponentA element)
        {
            Console.WriteLine(element.ExclusiveMethodOfConcreteComponentA() + " + ConcreteVisitor2");
        }

        public void VisitConcreteCompoenetB(ConcreteComponentB element)
        {
            Console.WriteLine(element.SpecialMethodofConcreteComponentB() + " + ConcreteVisitor2");
        }
    }

    public class VisitorClient
    {
        // The client code can run visitor operations over any set of elements
        // without figuring out their concrete classes. the accept operation
        // directs a call to the aprropriate operation in the visitor object.
        public static void ClientCode(List<IComponent> compenents, IVisitor visitor)
        {
            foreach (var component in compenents)
            {
                component.Accept(visitor);
            }
        }
    }
    public class VisitorPattern
    {
        public void Main()
        {
            List<IComponent> components = new List<IComponent>
            {
                new ConcreteComponentA(),
                new ConcreteComponentB()
            };

            Console.WriteLine("The client code works with all visitors via the base Visitor interface:");
            var visitor1 = new ConcreteVisitor1();
            VisitorClient.ClientCode(components, visitor1);

            Console.WriteLine();

            Console.WriteLine("It allows the same client code to work with different types of visitors:");
            var visitor2 = new ConcreteVisitor2();
            VisitorClient.ClientCode(components, visitor2);
        }
    }
}
