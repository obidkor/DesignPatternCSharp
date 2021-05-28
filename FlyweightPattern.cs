using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Flyweight의 목적은 메모리 줄이기임. 객체 생성시 객체 재사용을 목적으로 객체 풀을 운영함.
    // Flyweight Pattern allows programs to support vast quantities of objects by keeping their memory consumption low
    // The pattern achieves it by sharing parts of object state between multiple objects. 
    // In other words, the Flyweight saves RAM by caching the same data used by different objects.


    // The Flyweight pattern has a single purpose: minimizing memory intake. Flyweight can be recognized by
    // a creation method that returns cached object instead of creating new.

    
    // 풀에 없는 키가 등록될 경우 객체생성 / 키가 있을 경우 있는 객체 재사용
    // creates and manages flyweight objects
    // and ensure that flyweight are shared properly. When a client request a flyweight,
    // the FlyweightFactory objects assets an existing instance or creates one, if none exist.
    public class FlyweightFactory
    {
        // object pool
        private Hashtable flyweights = new Hashtable();
        
        //constructor
        public FlyweightFactory()
        {
            flyweights.Add("X", new ConcreteFlyweight());
            flyweights.Add("Y", new ConcreteFlyweight());
            flyweights.Add("Z", new ConcreteFlyweight());
        }

        // if flyweight[key] exists return existing flyweight
        // else create new flyweight add to pool of flyweights return new flyweight
        public Flyweight GetFlyweight(string key)
        {
            if(flyweights[key] == null)
            {
                var rtn = new ConcreteFlyweight();
                flyweights.Add(key, new ConcreteFlyweight());
                return rtn;
            }
            else
            {
                return ((Flyweight)flyweights[key]);
            }
        }

        
    }

    // 외부 상태를 체크할 인터페이스
    // declare an interface through which flyweight can receive and act on extrinsic state
    public abstract class Flyweight
    {
        public abstract void Operation(int extrinsicstate);
    }

    // 외부 상태에서 받아온 값을 내부 값으로 저장할 구현객체
    // implements the Flyweight interface and adds storage for intrinsic state, if any.
    // A ConcreteFlyweight object must be sharable. Any state it stores must be intrinsic. that is,
    // it must be independent of the ConcreteFlyweight objects's context.
    public class ConcreteFlyweight : Flyweight
    {
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine("ConcreteFlyweight: " + extrinsicstate);
        }
    }

    // not all Flyweight subclasses need to be shared. The Flyweight interface enables sharing, but it doesn't enforce it.
    // It is common for UnsharedConcreteFlyweight objects to have ConcreteFlyweight objects as children at some level in the flyweight object structure
    // (as the Row and Column classes have)
    public class UnsharedConcreteFlyweight : Flyweight
    {
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine("UnsharedConcreteFlyweight: " + extrinsicstate);
        }
    }

    // 고객을 외부 레퍼런스 값을 제공
    // Client maintains a reference to flyweights and computes or stores the extrinsic state of flyweights.
    public class FlyweightPattern
    {
        public void Main()
        {
            int extrinsicstate = 22;

            FlyweightFactory factory = new FlyweightFactory();

            // 기준
            Flyweight fx = factory.GetFlyweight("X");
            fx.Operation(--extrinsicstate);

            // 공유 및 재사용
            Flyweight fy = factory.GetFlyweight("X");
            fy.Operation(--extrinsicstate);

            // 새로 생성함.
            Flyweight fz = factory.GetFlyweight("A");
            fz.Operation(--extrinsicstate);

            // 상관없는 거
            UnsharedConcreteFlyweight fu = new UnsharedConcreteFlyweight();

            fu.Operation(--extrinsicstate);

           
        }
    }
}
