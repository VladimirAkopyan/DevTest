using System;
using Xunit;

using Task;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using Xunit.Abstractions;
using System.Linq;
using System.Collections.Generic;
using static Tests.Helpers; 

namespace Tests
{
    public class LinqTests
    {
        //Test Data, dogs are structs, so we are comparing by value
        public List<Dog> _dogListSystem = new List<Dog> {
           new Dog {Name = "Caddy", Age = 4, Length = 12.6f },
           new Dog {Name = "Tommy", Age = 14, Length = 22.6f },
           new Dog {Name = "Boddy", Age = 6, Length = 44f },
           new Dog {Name = "Steve", Age = 3, Length = 119f },
           new Dog {Name = "whatever", Age = 2, Length = 2f },
           new Dog {Name = "Unnamed", Age = 1, Length = 3f }
        };
        SingleList<Dog> _dogListTest = new SingleList<Dog>();

        public LinqTests() {
            foreach (var dog in _dogListSystem) _dogListTest.Add(dog); 
        }

        [Fact]
        public void Select()
        {
            var dogNamesProduced = _dogListTest.Select(d => d.Name); 
            var dogNamesExpected = _dogListSystem.Select(d => d.Name);
            CompareEnumerables(dogNamesProduced, dogNamesExpected);
        }

        [Fact]
        public void SelectWithIndex()
        {
            var dogNamesProduced = _dogListTest.Select((d, i) => $"{i}: {d.Name}");
            var dogNamesExpected = _dogListSystem.Select((d, i) => $"{i}: {d.Name}");
            CompareEnumerables(dogNamesProduced, dogNamesExpected);
        }

        [Fact]
        public void Where()
        {
            //Dogs who'se age is even
            var dogsExpected = _dogListTest.Where(d => (d.Age % 2) == 0);
            var dogsProduced = _dogListSystem.Where(d => (d.Age % 2) == 0);
            CompareEnumerables(dogsExpected, dogsProduced);
        }

        [Fact]
        public void WhereWithIndex() {
            //Dogs who's age is lower than their index
            var dogsExpected = _dogListTest.Where((d, i) => (d.Age < i));
            var dogsProduced = _dogListSystem.Where((d, i) => (d.Age < i));
            CompareEnumerables(dogsProduced, dogsExpected);
        }

        [Fact]
        public void SelectMany() {
            //In this case we are returning a tuple, which is a value type and will allow us to do comparisons 
            //The tuple will contain a Dog, and a single letter from it's name -> totally useless, but the test works anyway
            var dogsExpected = _dogListSystem.SelectMany(d => d.Name, 
                (dog, dogNameLetter) => (dog, dogNameLetter));

            var dogsProduced = _dogListSystem.SelectMany(d => d.Name,
                (dog, dogNameLetter) => (dog, dogNameLetter));
            CompareEnumerables(dogsProduced, dogsExpected);
        }
    }
}
