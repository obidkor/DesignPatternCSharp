using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{

    // 메멘토 패턴은 객체의 상태를 스냅샷으로 만들어 향후에 복원할 수있는 패턴
    // 객체의 내부구조와 스냅샷 내부에 보관된 데이터를 손상 시키지 않음.
    // 메멘토의 리턴형태는 모두 메멘토 인터페이스로 이루어진다.
    // Memento allows making snapshots of an object's state and restoring it in future.
    // The Memento doesn't compromise the internal structure of the object it works with, as well as data kept inside the snapshot

    // 메멘토 패턴은 직렬화 데이터를 씀.
    // The Memento's principle can be archieved using the serialization. While it's not the only and the most efficient way to 
    // make snapshot of an object's state, it still allows storing state backups while protecting the originator's structure from other objects.


    // Originator 클래스는 state를 가지고 있으며 이를 저장하고 복구하는 메서드를 가지고 있음.
    // The Originator holds some important state  that may change over time.
    // It also define a mehtod for saving the state inside a memento ans another method for restoring the state from it.
    public class Originator
    {
        // 그냥 간단히 state가 한 변수에 저장된다고 하자.
        // For the sake of simplicity, the originator's state is stored inside single variable
        private string _state;

        public Originator(string state)
        {
            this._state = state;
            Console.WriteLine("Originator : My initial state is " + state);
        }

        // Originator의 로직이 state를 변화시킨다고 하면 클라이언트 코드에서 save()로 state의 상태를 저장할꺼다.
        // The Originator's business logic may affect its internal state.
        // Therefor, the client should backup the state before launching mehod of the business logic via the save() method
        public void DoSomthing()
        {
            Console.WriteLine("Originator: I'm doing somthing important.");
            this._state = this.GenerateRandomString(30);
            Console.WriteLine($"Originator: and my state has changed to: {_state}");
        }

        public string GenerateRandomString(int length = 10)
        {
            string allowedSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = string.Empty;

            while (length > 0)
            {
                result += allowedSymbols[new Random().Next(0, allowedSymbols.Length)];
                Thread.Sleep(12);
                length--;
            }
            return result;
        }

        // 저장용 형태는 메멘토 인터페이스 생성은 new Concretememento
        // Saves the current state inside a memento
        public IMemento Save()
        {
            return new ConcreteMemento(this._state);
        }

        // 복구용 파라미터 메멘토로 복구
        // Restore the Originator's state from a memento object
        public void Restore(IMemento memento)
        {
            if(!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            this._state = memento.GetState();
            Console.WriteLine($"Originator : My state has changed to :  {_state}");
        }
    }

    // The Memento interface provides a way to retrieve the memento's metadata,
    // such as creation data or name. However, it does't expose the Originator's state.
    public interface IMemento
    {
        string GetName();
        string GetState();
        DateTime GetDate();
    }

    // 메멘토의 구체화 클래스는 Originator를 저장하는 구조를 가진다.
    // The Concrete Memento contains the infrastructure for storing the Originator's state.
    public class ConcreteMemento : IMemento
    {
        private string _state;

        private DateTime _date;

        public ConcreteMemento(string state)
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        // The Originator uses this method when restoring its state.
        public string GetState()
        {
            return this._state;
        }
        // The rest of the methods are used by the Caretaker to display metadata.
        public string GetName()
        {
            return $"{this._date} / ({this._state.Substring(0, 9)})...";
        }
        public DateTime GetDate()
        {
            return this._date;
        }
    }

    // Caretaker 클래스는 메멘토 인터페이스와 연결되어 있어 Memento에 저장된 Originator의 state에 접근할수 없다.(_mementos[i].state에 접근안된다. 당연히 _state가 private니깐 못한다.)
    // The Caretaker doesn't depend on the Concrete Memento class. Therefore,
    // it doesn't have access to the originator's state, stored inside the memento.
    // It work with all mementos via the base Memento interface
    public class Caretaker
    {
        // 백업용 메멘토 인터페이스 리스트
        private List<IMemento> _mementos = new List<IMemento>();

        // Concrete memento에 의존하지 않으려고 이거선언
        private Originator _originator = null;

        public Caretaker(Originator originator)
        {
            this._originator = originator;
        }

        // 백업은 그냥 addlist
        public void Backup()
        {
            // Concrete memento를 선언하는 Save메서드는 originator가 가지고잇음.
            Console.WriteLine("\nCaretaker: Saving Originator's state...");
            this._mementos.Add(this._originator.Save());
        }

        // 캔슬은 백업리스트 마지막 메멘토를 일단 지우고 복구
        public void Undo()
        {
            if (this._mementos.Count == 0) return;

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);

            Console.WriteLine("Caretaker: Restoring state to: " + memento.GetName());

            try
            {
                this._originator.Restore(memento);
            }
            catch (Exception ex)
            {
                this.Undo();
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine("Caretaker: Here's the list of mementos:");

            foreach(var memento in this._mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
       

    }

    public class MementoPattern
    {
        public void Main()
        {
            // Client code.
            Originator originator = new Originator("Super-duper-super-puper-super.");
            Caretaker caretaker = new Caretaker(originator);

            caretaker.Backup();
            originator.DoSomthing();

            caretaker.Backup();
            originator.DoSomthing();

            caretaker.Backup();
            originator.DoSomthing();

            Console.WriteLine();
            caretaker.ShowHistory();

            Console.WriteLine("\nClient: Now, let's rollback!\n");
            caretaker.Undo();

            Console.WriteLine("\n\nClient: Once more!\n");
            caretaker.Undo();

            Console.WriteLine();
        }
    }
}
