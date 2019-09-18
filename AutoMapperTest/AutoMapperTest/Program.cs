using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperTest
{
    public class NestedCollectionA
    {
        public string Name { get; set; }
    }
    public class NestedCollectionB
    {
        public string Name { get; set; }
    }


    public class CollectionA
    {
        public string Key { get; set; }
        public int Value { get; set; }

        public List<NestedCollectionA> List { get; set; }

    }

    public class CollectionB
    {
        public string Key { get; set; }
        public int Value { get; set; }
        public List<NestedCollectionB> List { get; set; }
    }

    public class Model
    {
        public string FieldA { get; set; }
        public int FieldB { get; set; }
        public double FieldC { get; set; }

        public List<CollectionA> List { get; set; }
    }

    public class ViewModel
    {
        public string FieldA { get; set; }
        public int FieldB { get; set; }
        public double FieldC { get; set; }

        public List<CollectionB> List { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Model, ViewModel>();
                cfg.CreateMap<CollectionA, CollectionB>();
                cfg.CreateMap<NestedCollectionA, NestedCollectionB>();
            });

            var mapper = new Mapper(config);

            var listA = new List<CollectionA>();
            listA.Add(new CollectionA() { Key = "A", Value = 1, List = new List<NestedCollectionA>() { new NestedCollectionA() {Name="A1"}, new NestedCollectionA() { Name = "A2" } } });
            listA.Add(new CollectionA() { Key = "B", Value = 2, List = new List<NestedCollectionA>() { new NestedCollectionA() { Name = "B1" }, new NestedCollectionA() { Name = "B2" } } });
            listA.Add(new CollectionA() { Key = "C", Value = 3 });

            var model = new Model() {FieldA="A", FieldB=2, FieldC=2.2d, List= listA };
            var viewModel = mapper.Map<ViewModel>(model);
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
