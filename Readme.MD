# Developer tests


There are three types in this project:
	* SingleList
	* DoubleList
	* Dict

I would like you to complete their implementation.

## SingleList

This is a singly-linked list.  The Add method is already implemented.  Fill in 
the rest of the implementation.

Do not derive from IEnumerable<T>, IComparable<T> or IEquatable<T> and do not 
create an IEnumerator<T> derived enumerator class for GetEnumerator().  This
might seem like an arbitrary requirement, and it is, it's to see if you have 
a deeper understanding of not just fundamental data-structures, but also some
of the quirks of .NET.

## DoubleList

This is a doubly-linked list.  Nothing has been implemented.  Fill in 
the rest of the implementation.

Do not derive from IEnumerable<T>, IComparable<T> or IEquatable<T> and do not 
create an IEnumerator<T> derived enumerator class for GetEnumerator()


## Dict

This is a dictionary type: a key-value store.  Items added are stored in-order.

Do not derive from IDictionary<K,V>, IEnumerable<T>, IComparable<T> or IEquatable<T> 
and do not create an IEnumerator<T> derived enumerator class for GetEnumerator()


## LINQ

There is a static class Extensions in Extensions.cs - I'd like you to implement some 
basic LINQ operators for the types listed above.

Implement Select, SelectMany and Where extension methods for DoubleList and SingleList
Implement Select and Where extension methods for DoubleList and SingleList

## Important

Write your code as though it was for a real project.  So provide comments, don't take 
short-cuts, follow my instruction, and provide tests to prove your code works.  The 
tests can just be a set of Debug.Asserts in Program.cs - you don't need to use a 
unit-testing framework.  Note, there's no trick questions here, there may however be
bugs in what I have wrote.  Also make sure the methods are the most efficient they can
be for the specified type.

Also, if you're ever unsure of what to do, just email me - in the real world I'll be 
your boss and you'll be able to ask me questions, so don't feel like this situation 
is any different.  If you find this is taking you too long, drop me an email, if it's 
too onerous then we can work something out.

Good luck!


# Implementation Notes

## IEnumerator 
The instruction to avoid inheriting from IEnumerable<T> or IEnumerator, 
yet the method signatures provided explicitly return the IEnumerator. The intent here is not clear to me, 
so have implemented IEnumerable myself.  
So I have deleted the interfaces, and amazingly ForEach still works! I never realised it searches for an actual method name.. 

## Issues not dealt with 
	* Modification of the array while enumerating is considered out of scope. There are no If-clauses or exceptions for that 
	* Concurrency is considered out of scope 

## Issues not tested


##  Performance 
I wanted to compare performance with that of system collections to make sure it's roughly on par. 
The performance numbers here are only a rough guide -> this "benchmark" does not have multiple iterations, and if accurate information is wanted I would reach for Benchmark.net

All operations are performed with an array for 5,000,000 random numbers, except Random Access, which uses a smaller dataset of 50,000

|Operation           | SingleList         | System.List  | System.LinkedList  | 
|--------------------|--------------------|--------------|--------------------|
| Add		         |    820 ms          |              |   780 ms          |   
| Enumerator         |   74 ms			  |              |    69 ms           |
| Indexer Sequential |   170 ms           |     24 ms    |     N/A            |
| Indexer Random	 |  15,126,076 Ticks  |  6,615 ticks |     N/A            |

