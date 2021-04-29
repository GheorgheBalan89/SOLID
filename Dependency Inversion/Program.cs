using System;
using System.Collections.Generic;
using System.Linq;

namespace Dependency_Inversion
{
    /// <summary>
    /// 5. Dependency inversion principle
    /// High-level modules should not depend upon low-level modules. Both should depend upon abstractions.
    /// This is to ensure a more modular approach to design that has fewer dependencies between modules
    /// </summary>
    ///
    /// 
    /// Model a genealogy relationship between people
    /// 
    public enum Relationship
    {
        Parent, Child, Sibling
    }
    
    public class Person
    {
        public string Name;

        public Person(string name)
        {
            Name = name;
        }
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    //low -level
    public class Relationships : IRelationshipBrowser
    {
       
        private readonly List<(Person, Relationship, Person)> relations =
            new List<(Person, Relationship, Person)>();

        //breaks the principle
        //public List<(Person, Relationship, Person)> Relations => relations;
        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(x =>
                                    x.Item1.Name == name
                                    && x.Item2 == Relationship.Parent)
                .Select(r => r.Item3);
        }
    }


    public class Research
    {
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John has a child called {p.Name}");
            }
        }


        //breaks the principle
        //public Research(Relationships relationships)
        //{
        //    var relations = relationships.Relations;
        //    foreach (var r in relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
        //    {
        //        Console.WriteLine($"John has a child called {r.Item3.Name}");
        //    }
        //}
        static void Main(string[] args)
        {
            var parent = new Person("John");
            var child1 = new Person("Chris");
            var child2 = new Person("Mary");

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
}
