using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Composite is structural design pattern that allows composing objects into a tree-like structure
    // and work with the it as if it was a singualr object.
    // building a tree structure. It has abililty to run methods recursively over the whole tree
    // structure and sum up the result.


    // It's often used to represent hierarchies of user interface component or the code that works with graphs.
    // if you have an object tree, ans each object of a tree is a part of the same class hierarchy,
    // this is most likely a composite. If methods of these classes delegate the work to child objects of the tree
    // and do it via the base class/interface of the hierarchy, this is definitely a composite

    // 모든 컴포넌트는 Component 추상클래스를 인터페이스로 공유한다.
    // The base Component class declares common operations for both simple ans complex objects of a composition.
    public abstract class Component
    {
        public Component() { }

        // 모든 Component는 각자의 operation은 상속하여 구현한다.
        // The base Component may implement some default behavior or leave it to
        // concrete classes (by declaring the method containing the behavior as "abstract").
        public abstract string Operation();

        // Composite에 해당하는 컴포넌트는 add/remove가 필요하고 말단의 component까지 구현할 필요가 없으므로 virtual로 구현한다.
        // In some cases, it would be beneficial to define the child-management
        // operations right in the base Component class. This way, you won't
        // need to expose any concrete component classes to the client cod,
        // even during the object tree assembly. The downside is  that these methods will be empty for the leaf-level components.
        public virtual void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        // Composite에 해당하는 컴포넌트의 IsComposite은 말단의 component의 IsComposite 비교하기 위해 virtual로 구현한다.
        // You can provide a method that lets the clients code figure out whether
        // a component can bear children.
        public virtual bool IsComposite()
        {
            return true;
        }
    }


    // The Leaf class represents the end objects of a composition. 
    // A leaf can't have any children.

    // 말단 Component
    // Usually, it's the Leaf objects that do the actual work,
    // whereas Composite objects only delegate to their sub-components.
    public class Leaf : Component
    {
        // 말단 component의 고유 operation으로 override로 구현
        public override string Operation()
        {
            return "Leaf";
        }

        // IsComposite은 false이므로 Component 인터페이스와 달리 override를 통해 말단에서 따로 구현.
        public override bool IsComposite()
        {
            return false;
        }
    }


    // The Composite class represent the complex components that may have
    // children. Ususally, the Composite objects delegate the actual work to
    // their children and then "sum-up" their result.
    public class Composite : Component
    {

        // Composite 클래스는 고유의 children 리스트가 있다. 형태는 Component이다.
        protected List<Component> _children = new List<Component>();

        // Composite 클래스이므로 Add/Remove를 구현해준다.
        public override void Add(Component component)
        {
            this._children.Add(component);
        }

        public override void Remove(Component component)
        {
            this._children.Remove(component);
        }

        // Composite의 operation은 composite부터 말단의 component까지의 operation을 반복적으로 실행한다.
        // The Composite executes its primary logic in a particular way.
        // It traverses recursively through all its children, collectiong and
        // summing their results. Since the Composite's  children pass these
        // calls to their children and so forth, the whole object tree is
        // traversed as a result.
        public override string Operation()
        {
            int i = 0;
            string result = "Branch(";

            foreach(Component component in this._children)
            {
                result += component.Operation();
                if(i != this._children.Count - 1)
                {
                    result += "+";
                }
                i++;
            }
            return result + ")";
        }

        // IsComposite는 추상인터페이스에서 구현된 것을 쓴다.
    }


    // client코드
    public class Client2
    {
        // 파라미터 Composite 혹은 Component의 operation을 실행
        // The client code works whti all of the components via the base interface.
        public void ClientCode(Component leaf)
        {
            Console.WriteLine($"REsult: {leaf.Operation()}\n");
        }

        // 파라미터로 받은 composite과 component를 iscomposite를 통해 검사 후 composite의 component list에 add해주는 로직
        // Thannks to the fact that the the child-management operations are declared
        // in the base Component class, the client code can work with any component,
        // simple or complex, without depending on their concrete classes.
        public void ClientCode2(Component component1, Component component2)
        {
            if (component1.IsComposite())
            {
                component1.Add(component2);
            }
            Console.WriteLine($"Result: {component1.Operation()}");
        }
    }


    public class CompositePattern
    {
        public void Main()
        {
            Client2 client = new Client2();

            // This way the client code can support the simple leaf components...
            Leaf leaf = new Leaf();
            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(leaf);

            // 직접 이렇게 add 해줄수도 있고
            // ... as well as the complex composites.
            Composite tree = new Composite();
            Composite branch1 = new Composite();
            branch1.Add(new Leaf());
            branch1.Add(new Leaf());
            Composite branch2 = new Composite();
            branch2.Add(new Leaf());
            tree.Add(branch1);
            tree.Add(branch2);
            Console.WriteLine("Client: Now I've got a composite tree:");
            client.ClientCode(tree);

            // iscomposite를 통해 add를 시켜줄수도있다.
            Console.Write("Client: I don't need to check the components classes even when managing the tree:\n");
            client.ClientCode2(tree, leaf);
        }
    }




    // override 시 virtual 유무에 따른 차이
    // 결과적으로 다형성 측면에서 자식 클래스의 인스턴스를 부모클래스로 캐스팅해서 사용 할 경우 차이가 발생한다. 
    // virtual 키워드는 override키워드와 함께 사용시 부모클래스로 자식클래스의 인스턴스를 캐스팅해서 사용 할 지라도 
    // 부모의 메서드가 아닌 실제로 가장 마지막에 생성된 인스턴스의 메소드를 호출하도록 지시한다.
    // virtual
    class Parent
    {
        public string Some() => "parent some";
        public virtual string Other() => "parent other";
        public virtual string Another() => "parent another";
    }


    // 아래의 예제에서 Other 메소드는 명시적으로 오버라이드를 선언하지 않은 
    // 메소드가, 부모 클래스로 캐스팅 할 경우 부모 클래스 타입의 메소드가 호출된다.
    // 반면 Another 메소드는 virtual 과 override 를 명시적으로 지정하여 부모 클래스로
    // 캐스팅 시에도 최종적으로 오버라이드 된 메소드가 호출됨을 볼 수 있다.
    class Child : Parent
    {
        public string Some() => "child some";

        // [Error] 'override' keyword is acceptable for abstract and virtual methods
        //public override string Some() => "child some";

        public string Other() => "child other";

        public override string Another() => "child another";
    }

}
