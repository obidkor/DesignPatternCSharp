using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    // Iterator 패턴은 복잡한 데이터를 순차순회함 => 데이터구조를 노출하지 않음.
    // Iterator 덕분에 단일 iterator 인터페이스로 다른 컬렉션 요소를 탐색할 수 있음.
    // Iterator Pattern allows sequentail traversal throught a complex data structure without exposing its internal detail.
    // Clients can go over elements of different collenctions in a similar fashin using a single iterator interface

    // iterator를 쓴다고해서 순회되는 컬렉션에 직접 접근하는 것은 아닐 수도 있음.
    // Iterator has the navigation methods(such as next, previous and others). 
    // Client code that uses iterators might not have direct access to the collection being traversed

    public abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();

        // Return the key of the current element.
        public abstract int Key();
        // Return the current element
        public abstract object Current();
        // Move forward to next element
        public abstract bool MoveNext();
        // Rewind the Iterator to the first element
        public abstract void Reset();
    }

    public abstract class IteratorAggregate : IEnumerable
    {
        // Returns an Iterator or another IteratorAggregate for the implementing object.
        public abstract IEnumerator GetEnumerator();
    }

    // Concrete Iterators implement various traversal algorithms.
    // These classes store the current traversal position at all times.
    public class AlphabeticalOrderIterator : Iterator
    {
        private WordsCollection _collection;

        // Store the current traversal position. An iterator may have a lot of
        // other fields for storing iteration state, especially when it is
        // supposed to work with a particular kind of collection. 
        private int _position = -1;

        private bool _reverse = false;

        public AlphabeticalOrderIterator(WordsCollection collection, bool reverse = false)
        {
            this._collection = collection;
            this._reverse = reverse;

            if (reverse)
            {
                this._position = collection.getItems().Count;
            }
        }

        public override object Current()
        {
            return this._collection.getItems()[_position];
        }

        public override int Key()
        {
            return this._position;
        }

        public override bool MoveNext()
        {
            // reverse에 따라 key값 위치 +-1해주고
            int updatedPosition = this._position + (this._reverse ? -1 : +1);

            if (updatedPosition >=0 && updatedPosition < this._collection.getItems().Count)
            {
                // collection range안이면 세팅
                this._position = updatedPosition;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Reset()
        {
            this._position = this._reverse ? this._collection.getItems().Count - 1 : 0;
        }
    }


    // Concrete Collections provicde one or several methods for retrieving fresh iterator instacne
    // compatible with the collection class
    public class WordsCollection : IteratorAggregate
    {
        List<string> _collection = new List<string>();

        bool _direction = false;

        public void ReverseDirection()
        {
            _direction = !_direction;
        }

        public List<string> getItems()
        {
            return _collection;
        }

        public void AddItem(string item){
            this._collection.Add(item);
        }
        public override IEnumerator GetEnumerator()
        {
            return new AlphabeticalOrderIterator(this, _direction);
        }

    }

    public class IteratorPattern
    {
        public void Main()
        {
            // The client code may or may know about the Concrete Iterator or Collection class,
            // depending on the level of indirection you want to keep in your program.
            var collection = new WordsCollection();
            collection.AddItem("First");
            collection.AddItem("Second");
            collection.AddItem("Third");

            Console.WriteLine("Straight traversal:");

            foreach(var element in collection)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("\nReverse traversal:");

            collection.ReverseDirection();

            foreach(var element in collection)
            {
                Console.WriteLine(element);
            }
        }
    }

}
