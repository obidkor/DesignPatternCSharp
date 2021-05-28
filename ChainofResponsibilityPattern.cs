using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // 책임연쇄 패턴은 요청을 처리하는 핸들러를 체이닝하여 요청을 처리하는 패턴.
    // 체인의 구성은 런타임에서 동적으로 구성이 가능하다.(표준 핸들러 인터페이스의 범위 내에서)
    // Chain of Responsibility pattern allows passing request along the chain of potential handlers until one of them handles request.
    // The Pattern allows multiple objects to handle the request without coupling sender class to the concrete classes of the receivers.
    // The Chain can be composed dynamically at runtime with any handler that follows a standard handler interface.

    // 패턴은 다른 개체에서 동일한 메서드를 간접적으로 호출하는 개체 그룹의 동작 메서드로 인식 할 수 있으며 모든 개체는 공통 인터페이스를 따릅니다.
    // The Chain of Responsibility pattern is only relevant when code operates with chains of objects.
    // Thre pattern is recognized by behavioral methods of one group of objects that indirectly call the same methods in other objects,
    // while all the objects follow the common interface.

    // Handler 인터페이스는 체이닝을 구성하고, 요청을 수행(핸들)하는 메서드를 명시함.
    // The Handler Interface declare a method for building the chain of handler.
    // It also declares a method for executing a request.
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }


    // The default chaining behavior can be implemented inside a base handler class
    public abstract class AbstractHandler : IHandler
    {

        private IHandler _nextHandler;

        
        // 체이닝을 위해 반환값을 IHandler 형태의 nextHandler를 반환해준다.
        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            // Returning a handler from here will let us link handlers in a convenient way like this.
            // ex) monkey.SetNext(squirrel).Setnext(dog);
            return handler;
        }

        public virtual object Handle(object request)
        {

            if(this._nextHandler != null)
            {
                // 다음 Handler의 Handle 소환
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }

    // 핸들러는 같은 인터페이스를 공유해야함.(IHandler -> AbstractHandler -> each Handler)
    public class MonkeyHandler : AbstractHandler
    {
        // 구현체로 내려오면서 Banana를 체크하고 아니면 다음 Handler로 넘어감.
        public override object Handle(object request)
        {
            if((request as string) == "Banana")
            {
                return $"Monkey: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    // 핸들러는 같은 인터페이스를 공유해야함.(IHandler -> AbstractHandler -> each Handler)
    public class SquirrelHandler : AbstractHandler
    {
        // 구현체로 내려오면서 Banana를 체크하고 아니면 다음 Handler로 넘어감.
        public override object Handle(object request)
        {
            if (request.ToString()  == "Nut")
            {
                return $"Squirrel: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    // 핸들러는 같은 인터페이스를 공유해야함.(IHandler -> AbstractHandler -> each Handler)
    public class DogHandler : AbstractHandler
    {
        // 구현체로 내려오면서 Banana를 체크하고 아니면 다음 Handler로 넘어감.
        public override object Handle(object request)
        {
            if (request.ToString() == "MeetBall")
            {
                return $"Dog: I'll eat the {request.ToString()}.\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    public class ChainClient
    {
        // The client code is usually suited to work with a single handler.
        // In most cases, it is not even aware that the handler is part of a chain.
        public static void ClientCode(AbstractHandler handler)
        {
            foreach(var food in new List<string> { "Nut", "Banana", "Cup of Coffee" })
            {
                Console.WriteLine($"Client: Who wants a {food}");

                var result = handler.Handle(food);

                if(result != null)
                {
                    Console.WriteLine($"    {result}");
                }
                else
                {
                    Console.WriteLine($"    {food} was left untouched");
                }
            }
        }
    }


    public class ChainofResponsibilityPattern
    {
        public void Main()
        {
            //  클라이언트 쪽에서 체인 핸들러를 생성하고 체인을 세팅함.
            // The other part of the client code constructs the actual chain.
            var monkey = new MonkeyHandler();
            var squirrel = new SquirrelHandler();
            var dog = new DogHandler();

            monkey.SetNext(squirrel).SetNext(dog);

            // 요청을 보냄.
            // The client should be able to send a request to any handler
            // not just the first one in the chain.
            Console.WriteLine("Chain: Monkey > Squirrel > Dog\n");
            ChainClient.ClientCode(monkey);
            Console.WriteLine();

            Console.WriteLine("Chain: Squirrel > Dog\n");
            ChainClient.ClientCode(squirrel);
            Console.WriteLine();
        }
    }
}
