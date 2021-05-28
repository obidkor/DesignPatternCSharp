using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{

    // State  패턴은 내부 값이 바뀔때 동작이 변경되도록 하는 패턴.
    // State 패턴은 내부값과 관련된 기능을 다른 클래스로 분리하고 원래 객체가 이러한 동작을 분리한 클래스에서 동작시킨다.
    // context <---> state 서로의 인스턴스를 가지고 있으며 context는 state를 바꾸는 메서드를 만듦. state는 context의 동작을 위임받아 구현함.
    // State pattern allows an object to change the behavior when its internal state changes.
    // State pattern extracts state-related behaviors into separate state classes and forces the original object
    // to delegate the work to an instance of these classes, instead of acting on its own.

    // state 패턴은 일반적으로 대규모 기본 스위치상태 시스템을 개체로 변환하는데 사용
    // 외부에서 제어되는 객체의 상태에 따라 동작을 변경한다.
    // The state Pattern is commonly used in c# to convert massive switch-base state machines into object.
    // State Pattern can be recognized by methods that change their behavior depending on the object's state controlled externally.

    // Context 클래스는 크라이언트가 관심있는 인터페이스를 정의함.
    // 또 State 구현체 클래스 인스턴스를 가지고있는데 이걸로 현재 Context의 state를 관리함.
    // The Context defines the interface of interest to clients. 
    // It also maintains a reference to an instance of a State subclass, which represent the current state of the Context.
    public class Context
    {
        // 현재 state(형태는 추상클래스, 인터페이스이다)
        // A reference to the current state of the Context
        private State _state = null;

        // 파라미터로 넣어주는 거는 추상 State 클래스의 구현체이다.
        public Context(State state)
        {
            this.TransitionTo(state);
        }

        // 런타임에 state바꾸는 메서드 . 파라미터로 넣어주는 거는 추상 State 클래스의 구현체이다.
        // The Context allows changing the State object at runtime
        public void TransitionTo(State state)
        {
            Console.WriteLine($"Context : Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
        }

        // 외부에서 어떤 state가 구현되어 들어오느냐에따라 동작이 달라진다.
        // The Context delegates part of its behavior to the current State object.
        public void Request1()
        {
            this._state.Handle1();
        }

        public void Request2()
        {
            this._state.Handle2();
        }
    }

    // State 클래스는 Context object의 인스턴스를 가지고있다.(역참조)
    // 역참조가 걸린 Context로 state를 변환시킬수 있다.(Context.Transitionto)
    // The base State class declares methods that all Concrete State should implement 
    // and also provides a backreference to the Context object, associated with the State
    // This backreference can be used by States to transition the Context to another State.
    public abstract class State
    {
        protected Context _context;

        // State에 context 역참조 세팅을 해줘야한다.
        public void SetContext(Context context)
        {
            this._context = context;
        }

        public abstract void Handle1();

        public abstract void Handle2();
    }

    // 추상클래스 State의 구현체는 관련 State에서 Context가 수행할 다양한 동작과 기능을 구현한다!
    // Concrete State implement various behaviors, associated with a state of the Context.
    public class ConcreteStateA : State
    {
        //동작내에 상태를 변경하는 코드를 넣는다. state를 상속하므로 Context에 접근가능.
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateA handles request1.");
            Console.WriteLine("ConcreteStateA wants to change the state of the context.");
            // Context의 상태변경!
            this._context.TransitionTo(new ConcreteStateB());
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateA handles request2");
        }
    }

    public class ConcreteStateB : State
    {
        public override void Handle1()
        {
            Console.WriteLine("ConcreteStateB handles request1");
        }

        public override void Handle2()
        {
            Console.WriteLine("ConcreteStateB handles request2");
            Console.WriteLine("ConcreteStateB wnats to change the state of the context");
            this._context.TransitionTo(new ConcreteStateA());

        }
    }

    public class StatePattern
    {
        public void Main()
        {
            var context = new Context(new ConcreteStateA());
            context.Request1();
            context.Request2();
        }
    }
}
