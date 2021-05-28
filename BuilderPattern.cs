using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // The Builder pattern can be recognized in a class,
    // which has a single creation method and several methods 
    // to configure the resulting object. 
    // Builder methods often support chaining (for example, someBuilder->setValueA(1)->setValueB(2)->create()).

    
    // 빌더 패턴은 하나의 생성메서드를 가지고 구성요소를 붙여나가며 객체를 생성하는 패턴
    // Builder interface는 생성될 객체의 configureation을 결정하는 메서드들을 명시한다.
    // The Builder interface specifies methods form creating the differet
    // parts of the Product object
    public interface IBuilder
    {
        void BuildPartA();
        void BuildPartB();
        void BuildPartC();
    }

    // 실제 구현되는 Builder Class는 특정 configuration을 구성하는 메서드를 구현한다.
    // The Concrete Builder Class follows the builder interface and
    // provide specific implementation of the building steps.
    // Your program may have several variations of Builder, 
    // implemented differently
    public class ConcreteBuilder : IBuilder
    {
        // configuration을 추가할 product
        private Product _product = new Product();

        // 생성자에는 product 생성 + product configuration 초기화 
        // a fresh builder instance should contain a blank product object,
        // which is used in further Assembly
        public ConcreteBuilder()
        {
            this.Reset();
        }

        // configuration 초기화 메서드
        private void Reset()
        {
            this._product = new Product();
        }

        // configuration method들 같은 product instance내에서 이루어진다.
        // All production steps work with the same product instance.
        public void BuildPartA()
        {
            this._product.Add("PartA1");
        }

        public void BuildPartB()
        {
            this._product.Add("PartB1");
        }

        public void BuildPartC()
        {
            this._product.Add("PartC1");
        }

        // 빌더 클래스에는 GetProduct가 잇는데 인터페이스에는 선언하면 안됨. building 하는 product가 다를수있기때문.
        // Concrete Builder's thier own method for retrieving results.
        // it shouldn't be declared in the base Builder interface
        // builders may create entirely diffent products that dont follow the same interface
        // the reason why calls reset method is instance is expected to be
        // ready to start producing another product.(not mandatory)
        //
        // product 클래스가 리턴되고 난후에는 builder 인스턴스는 다른 product 인스턴스를 생성하는게 보통임.필수는 아님.
        public Product GetProduct()
        {
            Product result = this._product;
            this.Reset();
            return result;
        }
    }


    // It makes sense to use the Builder pattern only when your products
    // are quite complex ans require extensive configuration.

    // unlike in other creational patterns, different concrete builders can
    // produce unrelated products. In other words, results of various builders
    // may not always follow the same interface.
    // Builder 패턴은 단순히 생성뿐만 아니라 configuration 이 필요한 product생성시에 씀
    public class Product
    {
        // product의 configuration리스트
        private List<object> _parts = new List<object>();

        public void Add(string part)
        {
            this._parts.Add(part);
        }

        public string ListParts()
        {
            string str = string.Empty;

            for(int i = 0; i<this._parts.Count; i++)
            {
                str += this._parts[i] + ", ";
            }
            str = str.Remove(str.Length - 2); // removing last ",c"

            return "Product parts: " + str + "\n";
        }

    }

    // Director class는 configuration 의 순서를 정해놓은 특정 구현 로직이다.
    // 이를 통해 client는 빌더에 편히 접근이 가능하다.
    // The Director is only responsible for excuting the building steps in a particular sequence.
    // It is helpful when producing products accroding to a specific order or configuration.
    // The Director class is optional, since the client can control builder directly
    public class Director
    {
        // 빌더를 필드에 선언하여
        private IBuilder _builder;

        public IBuilder Builder
        {
            set { _builder = value; }
        }

        // 특정 building path를 로직으로 구현한다.
        // The Director can construct several product variations using the same
        // building steps.
        public void BuildMinimalViableProduct()
        {
            this._builder.BuildPartA();
        }

        public void BuildFullFeaturedProduct()
        {
            this._builder.BuildPartA();
            this._builder.BuildPartB();
            this._builder.BuildPartC();

        }
    }

    public class Builder
    {
        public void Main()
        {

            // builder creatation -> pass it to director -> director construction product
            // the result is retrieved from the builder object 
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Builder = builder;

            Console.WriteLine("Standard basic product : ");
            director.BuildMinimalViableProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Standard full featured product : ");
            director.BuildFullFeaturedProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            // 빌더패턴에서 director가 꼭 필요한건 아니다.
            // the Builder pattern can be used without a Director class
            Console.WriteLine("Custom product : ");
            builder.BuildPartA();
            builder.BuildPartC();
            Console.WriteLine(builder.GetProduct().ListParts());
        }
    }


}
