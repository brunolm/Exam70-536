using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Specialized;

namespace Exam70_536
{
    /// <summary>
    /// You should pay attention to:
    /// - Constructors
    /// - Methods to Add/Remove/Find/Get... (non-linq methods)
    /// - How data structures work. Stack: FILO; Queue: FIFO; LinkedLists...
    /// - IEqualityComparer and IComparer interfaces, what it does, what it is used for
    /// </summary>
    [TestClass]
    public class CollectionsTest
    {
        /// <summary>
        /// Implements the IList interface using an array whose size is dynamically increased as required.
        /// Namespace:  System.Collections
        /// Assembly:  mscorlib (in mscorlib.dll)
        /// </summary>
        /// <remarks>
        /// The ArrayList is not guaranteed to be sorted. You must sort the ArrayList prior to performing
        /// operations (such as BinarySearch) that require the ArrayList to be sorted.
        /// Allows duplicate elements.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.arraylist.aspx"/>
        [TestMethod]
        public void ArrayListTest()
        {
            // Summary:
            //     Implements the System.Collections.IList interface using an array whose size
            //     is dynamically increased as required.
            var arrayList = new ArrayList(); //  IList, ICollection, IEnumerable, ICloneable

            arrayList.Add(/*object*/ value: 1);
            arrayList.Add(/*object*/ value: "xx");

            arrayList.AddRange(new StringCollection { "A", "B", "C" });

            Assert.AreEqual(5, arrayList.Count);

            Assert.IsTrue(arrayList.Contains("B"));
            Assert.IsTrue(arrayList.IndexOf("B") >= 0);

            arrayList.Remove("xx");

            Assert.IsFalse(arrayList.Contains("xx"));

            Assert.AreEqual(2, arrayList.GetRange(1, 2).Count);

            ArrayList sync = ArrayList.Synchronized(arrayList); // thread safe
        }

        /// <summary>
        /// Manages a compact array of bit values, which are represented as Booleans,
        /// where true indicates that the bit is on (1) and false indicates the bit is off (0).
        /// </summary>
        /// <remarks>
        /// The size of a BitArray is controlled by the client; indexing past the end of the BitArray throws an ArgumentException.
        /// Elements in this collection can be accessed using an integer index. Indexes in this collection are zero-based.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.bitarray.aspx"/>
        [TestMethod]
        public void BitArrayTest()
        {
            bool[] bitArrayValues1 = new bool[] { true, false, true, false, true };
            bool[] bitArrayValues2 = new bool[] { false, true, false, true, false };

            var bitArrayExpected = new BitArray(bitArrayValues1);
            var bitArray1 = new BitArray(bitArrayValues1);
            var bitArray2 = new BitArray(bitArrayValues2);

            bitArrayExpected.SetAll(true);

            Assert.AreNotEqual(bitArrayExpected, bitArray1.Or(bitArray2));
        }

        /// <summary>
        /// Provides a simple structure that stores Boolean values and small integers in 32 bits of memory.
        /// </summary>
        /// <remarks>
        /// BitVector32 is more efficient than BitArray for Boolean values and small integers that are used internally.
        /// A BitArray can grow indefinitely as needed, but it has the memory and performance overhead that
        /// a class instance requires. In contrast, a BitVector32 uses only 32 bits.
        /// A BitVector32 structure can be set up to contain either sections for small integers or
        /// bit flags for Booleans, but not both. 
        /// A BitVector32.Section is a window into the BitVector32 and is composed of the smallest number of
        /// consecutive bits that can contain the maximum value specified in CreateSection.
        /// CreateMask creates a series of masks that can be used to access individual bits in a BitVector32 that is set up as bit flags.
        /// Using a mask on a BitVector32 that is set up as sections might cause unexpected results.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.specialized.bitvector32.aspx"/>
        [TestMethod]
        public void BitVector32Test()
        {
            // Summary:
            //     Provides a simple structure that stores Boolean values and small integers
            //     in 32 bits of memory.
            var bitVector32 = new BitVector32(-1);

            Assert.AreEqual(-1, bitVector32.Data);

            bitVector32 = new BitVector32();

            // masks are used for bit flags
            int bitVector32Mask1 = BitVector32.CreateMask(); // Find the bit mask
            int bitVector32Mask2 = BitVector32.CreateMask(previous: bitVector32Mask1); // 2
            int bitVector32Mask3 = BitVector32.CreateMask(bitVector32Mask2); // 4
            int bitVector32Mask4 = BitVector32.CreateMask(bitVector32Mask3); // 8

            Assert.AreEqual(1, bitVector32Mask1);
            Assert.AreEqual(2, bitVector32Mask2);
            Assert.AreEqual(4, bitVector32Mask3);
            Assert.AreEqual(8, bitVector32Mask4);

            // sections are used to store numbers which size depends on number of bits
            BitVector32.Section bitVector32Section1 = BitVector32.CreateSection(4); // Create a section that is a range of bits
            BitVector32.Section bitVector32Section2 = BitVector32.CreateSection(4, bitVector32Section1);

            Assert.AreEqual(7, bitVector32Section1.Mask);
            Assert.AreEqual(7, bitVector32Section1.Mask);

            bitVector32[bitVector32Section1] = 1; // at the range change the binary to hold the number 1

            Assert.AreEqual(1, bitVector32.Data);
        }

        /// <summary>
        /// Represents a collection of keys and values.
        /// 
        /// You should pay attention to:
        /// - Inner type, which is KeyValuePair(TKey, TObject)
        /// </summary>
        /// <remarks>
        /// Keys are case sensitive by default, but can be changed.
        /// The Dictionary(Of TKey, TValue) generic class provides a mapping from a set of keys to a set of values.
        /// Each addition to the dictionary consists of a value and its associated key.
        /// Retrieving a value by using its key is very fast, close to O(1), because the Dictionary(Of TKey, TValue)
        /// class is implemented as a hash table.
        /// Every key in a Dictionary(Of TKey, TValue) must be unique according to the dictionary's equality comparer.
        /// A key cannot be null, but a value can be, if the value type TValue is a reference type.
        /// The element type is a KeyValuePair(Of TKey, TValue) of the key type and the value type.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/xfhwa508.aspx"/>
        [TestMethod]
        public void DictionaryTest()
        {
            var dictionary = new Dictionary<string, string>();

            dictionary.Add("a", "b");
            dictionary.Add("X", "b");

            Assert.IsTrue(dictionary.ContainsKey("a"));
            Assert.IsFalse(dictionary.ContainsKey("A"));
            Assert.IsFalse(dictionary.ContainsKey("b"));

            Assert.IsTrue(dictionary.ContainsValue("b"));
            Assert.IsFalse(dictionary.ContainsValue("x"));

            Assert.IsTrue(dictionary.Remove("a"));

            string dictionaryValue;
            dictionary.TryGetValue("X", out dictionaryValue);

            Assert.AreEqual("b", dictionaryValue);

            try
            {
                dictionaryValue = dictionary["ff"]; // KeyNotFoundException
                Assert.IsTrue(false);
            }
            catch (KeyNotFoundException)
            {
                Assert.IsTrue(true);
            }
            dictionary.Clear();

            Assert.AreEqual(0, dictionary.Count);

            if (dictionary != null)
            {
                dictionary.Add("KeyXX", "ValueXX");
                foreach (KeyValuePair<string, string> item in dictionary) { }
            }
            
            // Dictionaries can be canse insensitive if the comparer is changed
            var caseInsensitiveDictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            caseInsensitiveDictionary.Add("A", "A");

            try
            {
                caseInsensitiveDictionary.Add("a", "val");
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Represents a collection of key/value pairs that are organized based on the hash code of the key.
        /// 
        /// Uses GetHashCode and Equals (IEqualityComparer)
        /// </summary>
        /// <remarks>
        /// Each element is a key/value pair stored in a DictionaryEntry object. A key cannot be null, but a value can be.
        /// The objects used as keys by a Hashtable are required to override the Object.GetHashCode method
        /// (or the IHashCodeProvider interface) and the Object.Equals method (or the IComparer interface).
        /// The implementation of both methods and interfaces must handle case sensitivity the same way;
        /// otherwise, the Hashtable might behave incorrectly.
        /// The capacity of a Hashtable is the number of elements the Hashtable can hold. As elements are added
        /// to a Hashtable, the capacity is automatically increased as required through reallocation.
        /// Elements stored are of type DictionaryEntry.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.hashtable.aspx"/>
        [TestMethod]
        public void HashtableTest()
        {
            var hashtable = new Hashtable();

            hashtable.Add("a", "b");
            hashtable.Add("X", "b");

            Assert.IsTrue(hashtable.ContainsKey("a"));
            Assert.IsFalse(hashtable.ContainsKey("b"));

            Assert.IsTrue(hashtable.ContainsValue("b"));
            Assert.IsFalse(hashtable.ContainsValue("x"));

            hashtable.Remove("a");

            Assert.IsFalse(hashtable.Contains("a"));

            string dictionaryValue;

            try
            {
                dictionaryValue = hashtable["ff"] as string;
                Assert.IsTrue(true);
            }
            catch (KeyNotFoundException)
            {
                Assert.IsTrue(false);
            }
            hashtable.Clear();

            Assert.AreEqual(0, hashtable.Count);

            if (hashtable != null)
            {
                hashtable.Add("KeyXX", "ValueXX");
                foreach (DictionaryEntry item in hashtable) { }
            }
        }

        /// <summary>
        /// Implements IDictionary by using a ListDictionary while the collection is small,
        /// and then switching to a Hashtable when the collection gets large.
        /// </summary>
        /// <remarks>
        /// This class is recommended for cases where the number of elements in a dictionary is unknown.
        /// It takes advantage of the improved performance of a ListDictionary with small collections,
        /// and offers the flexibility of switching to a Hashtable which handles larger collections better than ListDictionary.
        /// The constructor accepts a Boolean parameter that allows the user to specify whether the
        /// collection ignores the case when comparing strings.
        /// Elements stored are of type DictionaryEntry.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.specialized.hybriddictionary.aspx"/>
        [TestMethod]
        public void HybridDictionaryTest()
        {
            /**
             * Uses ListDictionary for small collections (10 elements)
             * and HashTable for large collections (internally)
             */
            var hybridDictionary = new HybridDictionary(caseInsensitive: false);

            Assert.AreEqual(0, hybridDictionary.Keys.Count);
            Assert.AreEqual(0, hybridDictionary.Values.Count);

            for (int i = 0; i < 50; ++i)
            {
                hybridDictionary.Add(i, i + 100);
            }

            Assert.AreEqual(hybridDictionary.Keys.Count, hybridDictionary.Values.Count);
            Assert.AreNotEqual(0, hybridDictionary.Keys.Count);

            try
            {
                hybridDictionary.Add(0, 0);
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Implements IDictionary using a singly linked list.
        /// Recommended for collections that typically contain 10 items or less.
        /// </summary>
        /// <remarks>
        /// Items in a ListDictionary are not in any guaranteed order; code should not depend on the current order.
        /// Can be case-insensitive if a comparer is specified.
        /// Elements stored are of type DictionaryEntry.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.specialized.listdictionary.aspx"/>
        [TestMethod]
        public void ListDictionaryTest()
        {
            var listDictionary = new ListDictionary(StringComparer.InvariantCultureIgnoreCase);

            Assert.AreEqual(0, listDictionary.Keys.Count);
            Assert.AreEqual(0, listDictionary.Values.Count);

            for (int i = 0; i < 50; ++i)
            {
                listDictionary.Add(i, i + 100);
            }

            Assert.AreEqual(listDictionary.Keys.Count, listDictionary.Values.Count);
            Assert.AreNotEqual(0, listDictionary.Keys.Count);

            try
            {
                listDictionary.Add(0, 0);
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// Represents a collection of associated String keys and String values that can be
        /// accessed either with the key or with the index.
        /// </summary>
        /// <remarks>
        /// This collection is based on the NameObjectCollectionBase class.
        /// However, unlike the NameObjectCollectionBase, this class stores multiple string values under a single key.
        /// This class can be used for headers, query strings and form data.
        /// The hash code provider dispenses hash codes for keys in the NameValueCollection.
        /// The default hash code provider is the CaseInsensitiveHashCodeProvider.
        /// The comparer determines whether two keys are equal. The default comparer is the CaseInsensitiveComparer.
        /// Collections of this type do not preserve the ordering of element, and no particular ordering is guaranteed when enumerating the collection.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.specialized.namevaluecollection.aspx"/>
        /// <see cref="http://msdn.microsoft.com/en-us/library/610xe886(v=vs.100).aspx"/>
        [TestMethod]
        public void NameValueCollectionTest()
        {
            var nameValueCollection = new NameValueCollection();

            // "equivalent" to Dictionary<string, string[]>

            nameValueCollection.Add("a", "a");
            nameValueCollection.Add("a", "a");
            nameValueCollection.Add("a", "a");
            nameValueCollection.Add("a", "a");

            nameValueCollection.Add("b", "a");
            nameValueCollection.Add("b", "b");

            Assert.AreEqual("a", nameValueCollection.GetKey(index: 0));
            Assert.AreEqual(4, nameValueCollection.GetValues(nameValueCollection.GetKey(index: 0)).Length);

            nameValueCollection.Add("c", "F");
            nameValueCollection.Add("c", "F");

            // set the key to a single value
            nameValueCollection.Set("c", "F");

            Assert.AreEqual("F", nameValueCollection["c"]);

            // if there is multiple values get all in CSV
            Assert.AreEqual("a,b", nameValueCollection.Get("b"));
        }

        /// <summary>
        /// Represents a collection of key/value pairs that are accessible by the key or index.
        /// </summary>
        /// <remarks>
        /// Each element is a key/value pair stored in a DictionaryEntry object.
        /// A key cannot be null, but a value can be.
        /// The elements of an OrderedDictionary are not sorted by the key, unlike the elements of
        /// a SortedDictionary(Of TKey, TValue) class.
        /// You can access elements either by the key or by the index.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.specialized.ordereddictionary.aspx"/>
        [TestMethod]
        public void OrderedDictionaryTest()
        {
            // Allows to access values by index
            var orderedDictionary = new OrderedDictionary();

            orderedDictionary.Add("A", "A");
            orderedDictionary.Add("B", "B");
            orderedDictionary.Add("C", "C");

            Assert.AreEqual("A", orderedDictionary[0]);
        }

        /// <summary>
        /// Represents a first-in, first-out collection of objects.
        /// </summary>
        /// <remarks>
        /// The capacity of a Queue(Of T) is the number of elements the Queue(Of T) can hold.
        /// As elements are added to a Queue(Of T), the capacity is automatically increased
        /// as required by reallocating the internal array.
        /// The capacity can be decreased by calling TrimExcess.
        /// Queue(Of T) accepts Nothing as a valid value for reference types and allows duplicate elements.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/7977ey2c.aspx"/>
        [TestMethod]
        public void QueueTest()
        {
            var queue = new Queue();

            queue.Enqueue(1);

            Assert.AreEqual(1, queue.Peek());

            queue.Enqueue(2);
            queue.Enqueue(3);

            Assert.AreEqual(3, queue.Count);

            while (queue.Count > 0)
                queue.Dequeue();

            Assert.AreEqual(0, queue.Count);

            queue.Clear();
        }

        /// <summary>
        /// Represents a collection of key/value pairs that are sorted on the key.
        /// </summary>
        /// <remarks>
        /// The SortedDictionary(Of TKey, TValue) generic class is a binary search tree
        /// with O(log n) retrieval, where n is the number of elements in the dictionary.
        /// In this respect, it is similar to the SortedList(Of TKey, TValue) generic class.
        /// The two classes have similar object models, and both have O(log n) retrieval.
        /// Where the two classes differ is in memory use and speed of insertion and removal:
        ///   - SortedList(Of TKey, TValue) uses less memory than SortedDictionary(Of TKey, TValue).
        ///   - SortedDictionary(Of TKey, TValue) has faster insertion and removal operations for
        ///     unsorted data: O(log n) as opposed to O(n) for SortedList(Of TKey, TValue).
        ///   - If the list is populated all at once from sorted data, SortedList(Of TKey, TValue)
        ///     is faster than SortedDictionary(Of TKey, TValue).
        /// Keys must be immutable as long as they are used as keys in the SortedDictionary(Of TKey, TValue).
        /// Every key in a SortedDictionary(Of TKey, TValue) must be unique.
        /// A key cannot be null, but a value can be, if the value type TValue is a reference type.
        /// SortedDictionary(Of TKey, TValue) requires a comparer implementation to perform key comparisons.
        /// You can specify an implementation of the IComparer(Of T) generic interface by using a constructor
        /// that accepts a comparer parameter; if you do not specify an implementation, the default
        /// generic comparer Comparer(Of T).Default is used. If type TKey implements the System.IComparable(Of T)
        /// generic interface, the default comparer uses that implementation.
        /// Stored elements are KeyValuePair<TKey, TValue>
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/f7fta44c.aspx"/>
        [TestMethod]
        public void SortedDictionaryTest()
        {
            /**
             * Faster adding 1 by 1
             * Slower adding mass of content
             */
            var sortedDictionary = new SortedDictionary<string, string>();

            sortedDictionary.Add("i", "A");
            sortedDictionary.Add("A", "A");
            sortedDictionary.Add("Z", "A");
            sortedDictionary.Add("X", "A");

            Assert.AreEqual("A", sortedDictionary.ElementAt(0).Key);
        }

        /// <summary>
        /// Represents a collection of key/value pairs that are sorted by the keys and are accessible by key and by index.
        /// </summary>
        /// <remarks>
        /// A SortedList element can be accessed by its key, like an element in any IDictionary implementation,
        /// or by its index, like an element in any IList implementation.
        /// A SortedList object internally maintains two arrays to store the elements of the list;
        /// that is, one array for the keys and another array for the associated values.
        /// Each element is a key/value pair that can be accessed as a DictionaryEntry object.
        /// Each element on the generic SortedList is a KeyValuePair<TKey, TValue>
        /// A key cannot be null, but a value can be.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.sortedlist.aspx"/>
        /// <see cref="http://msdn.microsoft.com/en-us/library/ms132319.aspx"/>
        [TestMethod]
        public void SortedListTest()
        {
            /**
             * Sort elements by Key
             * Slower adding 1 by 1
             * Faster adding mass of content
             */
            var sortedList = new SortedList(1000);

            for (int i = 1000; 0 < i; --i)
            {
                sortedList.Add(i, i);
            }

            Assert.AreEqual(1, sortedList.GetByIndex(0));

            var sortedListGeneric = new SortedList<string, int>(StringComparer.InvariantCulture);

            var range1 = Enumerable.Range('A', 'Z' - 'Z');
            var range2 = Enumerable.Range('a', 'z' - 'a');
            
            foreach (var item in range1)
            {
                sortedListGeneric.Add(((char)item).ToString(), item);
            }
            foreach (var item in range2)
            {
                sortedListGeneric.Add(((char)item).ToString(), item + 1000);
            }

            Assert.AreEqual("a", sortedListGeneric.Keys[0]);
        }

        /// <summary>
        /// Represents a simple last-in-first-out (LIFO) non-generic collection of objects.
        /// </summary>
        /// <remarks>
        /// For the generic version of this collection, see System.Collections.Generic.Stack(Of T).
        /// Stack is implemented as a circular buffer.
        /// If Count is less than the capacity of the stack, Push is an O(1) operation.
        /// If the capacity needs to be increased to accommodate the new element,
        /// Push becomes an O(n) operation, where n is Count. Pop is an O(1) operation.
        /// Stack accepts Nothing as a valid value and allows duplicate elements.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/3278tedw.aspx"/>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.stack.aspx"/>
        [TestMethod]
        public void StackTest()
        {
            var stack = new Stack<int>();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            int comp = 3;

            Assert.AreEqual(3, stack.Count);

            while (stack.Count > 0)
            {
                Assert.AreEqual(comp--, stack.Pop());
            }

            stack.Clear();

            Assert.AreEqual(0, stack.Count);
        }

        /// <summary>
        /// Represents a collection of strings.
        /// </summary>
        /// <remarks>
        /// StringCollection accepts Nothing as a valid value and allows duplicate elements.
        /// String comparisons are case-sensitive.
        /// Elements in this collection can be accessed using an integer index. Indexes in this collection are zero-based.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.specialized.stringcollection.aspx"/>
        [TestMethod]
        public void StringCollectionTest()
        {
            var stringCollection = new StringCollection();

            stringCollection.Add("a");
            stringCollection.AddRange(new string[] { "b", "c", "abc" });

            Assert.IsTrue(stringCollection.Contains("a"));
            stringCollection.Remove("a");
            Assert.IsFalse(stringCollection.Contains("a"));

            stringCollection.Insert(0, "Z");
            Assert.AreEqual("Z", stringCollection[0]);

            int count = stringCollection.Count;
            stringCollection.RemoveAt(1);

            Assert.AreEqual(count - 1, stringCollection.Count);
        }

        /// <summary>
        /// Implements a hash table with the key and the value strongly typed to be strings rather than objects.
        /// </summary>
        /// <remarks>
        /// A key cannot be null, but a value can.
        /// The key is handled in a case-insensitive manner; it is translated to lowercase before it is used
        /// with the string dictionary.
        /// </remarks>
        /// <see cref="http://msdn.microsoft.com/en-us/library/system.collections.specialized.stringdictionary.aspx"/>
        [TestMethod]
        public void StringDictionaryTest()
        {
            var stringDictionary = new StringDictionary();

            stringDictionary.Add("a", "a");
            try
            {
                stringDictionary.Add("A", "a"); // exception
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(true);
            }

            Assert.IsTrue(stringDictionary.ContainsKey("a"));
            Assert.IsTrue(stringDictionary.ContainsValue("a"));
            Assert.IsFalse(stringDictionary.ContainsValue("A"));
        }

        [TestMethod]
        public void LinkedListTest()
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            LinkedListNode<int> first = linkedList.AddFirst(0);
            LinkedListNode<int> last = linkedList.AddLast(3);

            linkedList.AddAfter(first, 1);
            linkedList.AddBefore(last, 2);

            int i = 0;
            foreach (var item in linkedList)
            {
                Assert.AreEqual(i++, item);
            }
        }

        [TestMethod]
        public void CollectionsUtilTest()
        {
            Hashtable insensitiveHT = CollectionsUtil.CreateCaseInsensitiveHashtable();
            SortedList insensitiveSL = CollectionsUtil.CreateCaseInsensitiveSortedList();

            insensitiveHT.Add("A", 1);
            try
            {
                insensitiveHT.Add("a", 1);
                Assert.Fail();
            }
            catch
            {
            }

            insensitiveSL.Add("A", 1);
            try
            {
                insensitiveSL.Add("a", 1);
                Assert.Fail();
            }
            catch
            {
            }
        }

        [TestMethod]
        public void ComparerTest()
        {
            List<int> list = new List<int>();

            list.Add(10);
            list.Add(1);
            list.Add(100);
            list.Add(50);

            list.Sort(new CustomIntComparer());

            Assert.AreEqual(100, list.ElementAt(0));
        }

        class CustomIntComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y - x;
            }
        }
    }
}
