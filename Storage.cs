using System;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;

namespace oop_7 {
    public class Storage<T> where T : Shape {
        public class Node {
            public bool isDistinguishVertex = false;
            public Node next;
            public Node Next {
                get { return next; }
                set { next = value; }
            }
            public Node prev;
            public Node Prev {
                get { return prev; }
                set { prev = value; }
            }
            public T data;
            public T Data {
                get { return data; }
                set { data = value; }
            }
            public Node() {
                next = prev = null;
            }
            public Node(T tdata) {
                data = tdata;
                next = null;
            }
        }

        private int capacity;
        private Node head;
        private Node current;
        private Node iterator;
        bool iFlag;
        public int GetCapacity() {
            return capacity;
        }
        public Node Current {
            get {
                return current;
            }
            set {
                current = value;
            }
        }
        public void IFirst() {
            iterator = head;
            iFlag = false;
        }
        public Node IObject {
            get {
                return iterator;
            }
            set {
                iterator = value;
            }
        }
        public void INext() {
            iFlag = true;
            iterator = iterator.next;
        }
        public bool IIsEOL() {
            return iterator == head && iFlag;
        }       


        public Node GetDistinguished() {
            if (capacity == 0)
                return null;
            Node tnode = head;
            int count = 0;
            bool flag = head.isDistinguishVertex;
            while (!tnode.isDistinguishVertex && count != (capacity + 1)) {
                tnode = tnode.next;
                count++;
                if (tnode.isDistinguishVertex) {
                    flag = true;
                    break;
                }
            }
            if (flag)
                return tnode;
            else
                return null;
        }

        public void DeleteDistinguished() {
            bool hasDistinguished = false;
            Node tempNode = head;
            for (int i = 0; i < capacity; i++) {
                if (tempNode.isDistinguishVertex) {
                    hasDistinguished = true;
                    break;
                }
                tempNode = tempNode.next;
            }
            if (!hasDistinguished) {
                DeleteCurrent();
            }

            if (capacity == 0)
                return;
            tempNode = head.next;
            Node newCurrent = current;
            int count = 0;
            while (newCurrent.isDistinguishVertex && (count++) < (capacity + 1))
                newCurrent = newCurrent.prev;

            while ((tempNode = GetDistinguished()) != null) {
                current = tempNode;
                DeleteCurrent();
            }
            current = newCurrent;            
        }


        public Storage() {
            current = head = null;
        }       
        
        
        public void DeleteCurrent() {

            if (capacity == 1)
            {
                capacity = 0;
                head = current = null;
            }
            if (capacity > 0)
            {
                capacity--;
                Node tnode = current.prev;
                if (current == head)
                    head = tnode;
                current.prev.next = current.next;
                current.next.prev = current.prev;
                current = tnode;
            }
        }

        public void Clear() {
            Node t1 = head;
            Node t2;
            for (int i = 0; i < capacity; i++) {
                t2 = t1.next;
                t1.next = null;
                t1.prev = null;
                t1 = t2;
            }
            head = current = null;
            capacity = 0;
        }
        public void goFront() {
            if (capacity > 0)
                current = current.next;
        }
        public void goBack() {
            if (capacity > 0)
                current = current.prev;
        }
        public void goFirst() {
            current = head;
        }
        public void goTail() {
            if (head != null)
                current = head.prev;
        }
        public Node Tail {
            get {
                if (capacity == 0)
                    return null;
                return head.prev;
            }
        }
        public void Add(T tdata) {
            Node tnode = new Node(tdata);
            capacity++;
            if (head == null) {
                head = current = tnode;
                head.next = head;
                head.prev = head;
            }
            else {
                tnode.prev = head.prev;
                tnode.next = head;
                head.prev.next = tnode;
                head.prev = tnode;
            }
        }

        public void PrintAll()
        {
            Node tnode = head;
            for (int i = 0; i < capacity; i++)
            {
                Console.Write(String.Format("{0,2:0}", 1 + i));

                if (tnode == current)
                    Console.Write(" >");
                else
                    Console.Write("  ");
                tnode.Data.Print();
                tnode = tnode.next;
            }
            if (capacity == 0)
                Console.WriteLine("Empty");
            Console.WriteLine("Capacity: " + capacity);
        }

        public Node GetTail()
        {
            return head.prev;
        }
        public void Save(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);
            Node tnode = head;
            for (int i = 0; i < capacity; i++)
            {
                writer.Write(tnode.data.GetData() + "," + tnode.isDistinguishVertex + "," + (tnode == current));              
                
                writer.WriteLine();
                tnode = tnode.next;
            }
            writer.Close();
        }
        public bool Load(string filename, string namesp)
        {
            StreamReader s;
            try
            {
                s = new StreamReader(filename);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не найден");
                return false;
            }
            using (s)
            {
                while (!s.EndOfStream)
                {
                    var arrStr = s.ReadLine().Split(',');
                    var ttype = Type.GetType(namesp + "." + arrStr[0], false, true);

                    var types = new object[arrStr.Length - 3];
                    for (int l = 0; l < types.Length; l++)
                        types[l] = int.Parse(arrStr[l + 1]);

                    object obj = new Shape();
                    var constructors = ttype.GetConstructors();
                    foreach (var t in constructors)
                    {
                        try
                        {
                            obj = t.Invoke(types);
                        }
                        catch (Exception)
                        {
                            //continue; 
                        }
                    }
                    Add((T)obj);
                    GetTail().isDistinguishVertex = bool.Parse(arrStr[arrStr.Length - 2]);
                    if (bool.Parse(arrStr[arrStr.Length - 1]))
                        current = GetTail();
                }
            }
            return true;
        }
    }
}