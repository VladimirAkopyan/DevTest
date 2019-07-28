## Status:
[![CircleCI](https://circleci.com/gh/VladimirAkopyan/DevTest.svg?style=svg)](https://circleci.com/gh/VladimirAkopyan/DevTest)

# Instructions:

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
	
1. I have used .Net Core 2.2 and XUnit -> the amount of tests required to prove the collections really work without issues can be quite large. My udnerstanding of these instructions is that I should not focus on Unit testing too much, I've implemented what I consider resonable. 

2. The instruction to avoid inheriting from IEnumerable<T> or IEnumerator surprised me at first, and it took me a while to realise that ForEach and other array operations [work without it](https://blogs.msdn.microsoft.com/kcwalina/2007/07/18/duck-notation/). 
	
3. LINQ functions need to operate on a collection, and best I can tell, there is no DuckTyping for them -> I have implemented them explicitly using SingleList<T>, and it appears that the only way to make them work on multiple collections, without using IEnumerable or simular, is to basically create my own interface MyEnumerable. I am not sure what's the intent here. 
	
4. Modification of the array while enumerating is considered out of scope for this task. There are no exceptions for that 
	
5. Concurrency is considered out of scope 

6. There is an index-style accessor 


##  Performance 
I wanted to compare performance with that of system collections to make sure it's roughly on par. 
The performance numbers here are only a rough guide -> this "benchmark" does not have multiple iterations, and if accurate information is wanted I would reach for [Benchmark.net](https://github.com/dotnet/BenchmarkDotNet)

All operations are performed with an array for 5,000,000 random numbers, except Random Access, which uses a smaller dataset of 50,000

|Operation           | SingleList         | System.List  | System.LinkedList  | 
|--------------------|--------------------|--------------|--------------------|
| Add		         |    820 ms          |              |   780 ms          |   
| Enumerator         |   74 ms			  |              |    69 ms           |
| Indexer Sequential |   170 ms           |     24 ms    |     N/A            |
| Indexer Random	 |  15,126,076 Ticks  |  6,615 ticks |     N/A            |

