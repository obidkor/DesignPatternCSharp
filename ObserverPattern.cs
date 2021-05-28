using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // 옵저버 패턴은 객체의상태변경을 다른 객체에 알리도록 설계됨.
    // 구독자 인터페이스를 구현하는 모든 객체에 이런 이벤트 구독및 구독취소하는 방법을 제공
    // Observer pattern allows some objects to notify other objects about changes in their state.
    // The Observer pattern provides a way to subscribe and unsubscribe to and from these events
    // for any object that implements a subscriber interface.

    // GUI 컴포넌트가 다른 object와 결합되는 일 없이 이벤트의 콜백을 호출할수 있다.
    // The Observer pattern is pretty common in the GUI components. It provides a way to 
    // react to events happening in other objects without coupling to their classes

    // 옵저버패턴은 리스트에 observer를 보관하고 구독할때마다 여기에 observer를 추가한다.
    // 그리고 상태가 변화되면 옵저버리스트의 update 메서드를 호출한다.
    // The Observer Pattern provides a subscription method, that store objects in a list,
    // and calls to the update method issued to objects in that list.

    public interface IObserver
    {
        // 구독한 subject의 상태변화 감지
        // Receive update from subject
        void Update(ISubject1 subject);
    }

    public interface ISubject1
    {
        // 구독
        // Attach an observer to subject
        void Attach(IObserver observer);

        // 구독 해지
        // Detach an observer from subject
        void Detach(IObserver observer);

        // 상태변화 알림.
        // Notify all observer about an event.
        void Notify();
    }

    // Subject는 각 state를 가지고 있고 이것이 변할때마다 Observer에 통보함.
    // The Subject owns some important state and notifies observers when the state changes.
    public class Subject : ISubject1
    {
        // 간단히 int인 state가 중요한 정보라고 하자.
        // For the sake of simplicity, the Subject's state, essential to all subscribers, is stored in this value.
        public int State { get; set; } = -0;

        // 옵저버 리스트. 현실에서는 옵저버리스트가 카테고리화 되는등 여러개로 나뉠수도있다.
        // 리스트의 형태는 IObserver이다.
        // List of subscribers. In real life, the list of subscribers can be stored more comprehensively(catregorized by event type, etc..)
        private List<IObserver> _observers = new List<IObserver>();

        // 구독 메서드
        // The subscription management methods.
        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject : Attached an observer.");
            this._observers.Add(observer);
        }

        // 구독해지 메서드
        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Subject : Detached an observer.");
        }

        // 상태 변화 노티 메서드
        // Trigger an update in each subscriber.
        public void Notify()
        {
            Console.Write("Subject: Notifying observers...");

            foreach(var observer in _observers)
            {
                observer.Update(this);
            }
        }

        // 구독 로직은 Subject의 기능중 하나이며 비지니스로직은 따로 잇음.
        // 이러한 비지니스로직이일어날때마가 알림메서드를 트리거할수있음.
        // Usually, the subscription logic is only a fraction of what a Subject can really do.
        // Subjects commonly hold some important business logic, that triggers a notification method
        // whenever something important is about to happen(or after it).
        public void SomeBusinessLogic()
        {
            Console.WriteLine("\nSubject: I'm doing something important.");
            this.State = new Random().Next(0, 10);

            Thread.Sleep(15);

            Console.WriteLine("Subject: My state has just changed to: " + this.State);
            this.Notify();
        }
    }

    // ConcreteObserver는 상태변화 통보에 어캐 반응하는 것인지(React)를 구현함.
    //Concrete Observers react to the updates issued by the Subject they had been attached to.
    public class ConcreteObserverA : IObserver
    {
        public void Update(ISubject1 subject)
        {
            if((subject as Subject).State < 3)
            {
                Console.WriteLine("ConcreteObserverA: React to the event.");
            }
        }
    }

    public class ConcreteObserverB : IObserver
    {
        public void Update(ISubject1 subject)
        {
            if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
            {
                Console.WriteLine("ConcreteObserverB: React to the event.");
            }
        }
    }

    public class ObserverPattern
    {
        public void Main()
        {
            //Subject와 observer
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            var observerB = new ConcreteObserverB();

            // 구독
            subject.Attach(observerA);
            subject.Attach(observerB);

            // noti
            subject.SomeBusinessLogic();
            subject.SomeBusinessLogic();

            // 구독해지
            subject.Detach(observerB);

            // noti
            subject.SomeBusinessLogic();
        }
    }
}
