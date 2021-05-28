using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // command 패턴 : 사용자 요청이나 로직 -> 특정 object로 변환
    // Command converts requests or simple operation into objects.
    // The conversion allows deferred or remote execution of commands, storing command history, etc

    // 커맨드 패턴은 콜백메서드의 대안으로 쓰이며 UI의 요소와 action 메서드를 파라미터화 한다. Tast queue를 만들거나 operation history를 만들기도 한다.
    // The Command Pattern, Most often it's used as an alternative for callbacks to parameterizing UI elements with actions
    // It is also used for queuing tasks, tracking operation history, etc

    // 커맨드 패턴은 호출하는 sender interface와 요청받는 receiver interface가 존재한다. receiver interface는 command의 핵심로직을 캡슐화한다.
    // The Command Pattern is recoginzed by behavioral methods in an abstract/interface type(sender) 
    // which invokes a method in an implementation of a different abstract/interface type(receiver) which has been
    // encapsulated by the command implementation during its creation.
    // Command classes are usually limited to specific actions.

    
    // 커맨드의 인터페이스는 execute메서드를 가진다.
    // The Command interface declares a method for executing a command.
    public interface ICommand
    {
        void Execute();
    }

    // 간단한 커맨드는 Command에 object를 연결할 필요가 없다.
    // Some commands can implement simple operation on thier own
    public class SimpleCommand : ICommand
    {
        private string _payload = string.Empty;

        public SimpleCommand(string payload)
        {
            this._payload = payload;
        }
        public void Execute()
        {
            Console.WriteLine($"SimpleCommand: See,I can do simple things like printing ({this._payload})");
        }
    }

    // 복잡한 command는 Object를 캡슐화하여 이 object가 요청을 수행(execute)하게끔 한다. 파라미터도 넘길수 있다.
    // However, some commands can delegate more complex operations to other objects, called "receivers."
    public class ComplexCommand : ICommand
    {
        private Receiver _receiver;

        // 파라미터
        // Context data, required for launching the recevier's method
        private string _a;

        private string _b;

        // receiver object가 요청을 수행한다.
        // Complex commands can accept one or several recevier objects along with
        // any context data via the constructor
        public ComplexCommand(Receiver receiver, string a, string b)
        {
            this._receiver = receiver;
            this._a = a;
            this._b = b;
        }

        // requset(sender) => command => receiver(execute)
        // Commands can delegate to any methods of a receiver
        public void Execute()
        {
            Console.WriteLine("ComplexCommand: Complex stuff should be done by a receiver object.");
            this._receiver.DoSomething(this._a);
            this._receiver.DoSomethingElse(this._b);
        }
    }

    // Receiver를 통해 핵심 로직을 구현한다. 어느 클래스든 Receiver가 될수 있다.
    // The Receiver classes contain some important business logic. They know how to perform
    // all kind of operations, associated with carrying out a request.
    // In fact, any class may serve as a Receiver
    public class Receiver
    {
        public void DoSomething(string a)
        {
            Console.WriteLine($"Receiver: Working on ({a}.)");
        }

        public void DoSomethingElse(string b)
        {
            Console.WriteLine($"Receiver: Also working on ({b}.)");
        }
    }

    // 요청자(UI)는 여러개의 Command를 가질 수 있다. 
    // the invoker is associated with one or several commands. It sends a request to the command
    public class Invoker
    {
        private ICommand _onStart;
        private ICommand _onFinish;

        // Initialize commands.
        public void SetOnStart(ICommand command)
        {
            this._onStart = command;
        }

        public void SetOnFinish(ICommand command)
        {
            this._onFinish = command;
        }

        // UI(invoker)가 의존하는 Command는 ICommand이며 / 어느 구현된 Command나 클래스에 결합 X
        // command를 수행하는 Receiver class를 간접호출할 뿐이다.
        // The invoker doesn't depend on concrete command or receiver classes.
        // The inovker passes a request to a receiver indirectly, by executing a command.
        public void DoSomethingImportant()
        {
            Console.WriteLine("Invoker: Does anybody want somthing done before I begin?");
            if (this._onStart is ICommand) this._onStart.Execute();

            Console.WriteLine("Invoker:... doing something really important");

            Console.WriteLine("Invoker: Does anybody want something done after I finish?");
            if (this._onFinish is ICommand) this._onFinish.Execute();
        }
    }

    public class CommandPattern
    {
        public void main()
        {
            // The client code can parameterize an invoker wth any commands.
            Invoker invoker = new Invoker();
            invoker.SetOnStart(new SimpleCommand("Say hi!!"));
            Receiver receiver = new Receiver();
            invoker.SetOnFinish(new ComplexCommand(receiver, "Send Email", "Save Report"));

            invoker.DoSomethingImportant();

        }
        
    }
}
