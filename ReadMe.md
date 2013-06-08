# Building

[Builder.cs](https://raw.github.com/MrAntix/Building/master/antix-building/Antix.Building/Builder.cs)

This is a generic builder class which separates off the business of creation into a reusable component

Its a quick step to get into the builder pattern which is useful for building loads of things, 
hierarchical things, fluent interfaces for configuring things and all that

It was part of [Testing](http://mrantix.github.io/Testing/), but its so "awesome" for so many things, 
not just testing, its got its own project now

[On NuGet](https://nuget.org/packages/antix-building)

### Examples

    // create a builder for a mocked interface (using Moq)
    var builder = new Builder<IThingy>(Mock.Of<IThingy>)
                    .With(x => 
                                {
                                    x.Name = "Some Name";
                                    x.SetValue("Some Value");
                                });
 
    // create an instance
    var instance = builder.Build();

    // create an other instance, override the name
    var otherInstance = builder
	                    .Build(x => x.Name = "Other Name");

    // create 100 instances, then 10 overriding the name, the index is supplied too
    var instances = builder
                        .Build(100)
                        .Build(10, (x, i) => x.Name = "Other Name")
						.ToList();

### Notes on Usage

The builder implements IEnumerable<T> and is has a lazy iterator, which means everytime 
you enumerate it you will get a new set of objects

Call .ToArray() or .ToList() to fix a set to a variable (see above)

