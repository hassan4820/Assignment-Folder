using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZooManagement
{
    public interface ISoundBehaviour
    {
        void MakeSound();
    }
    // Declaring an abstract class Animal. This is a form of abstraction in OOP.
    // Abstraction allows complex real-world concepts to be represented in a simplified manner in a program.
    // Animal class represents general characteristics of all animals.
    public abstract class Animal
    {
        // Properties are an example of encapsulation where you can have data validation inside getters and setters 
        // Here, however, we are using auto-implemented properties with default getters and setters.

        // The Guid data type is used to generate a unique identifier (ID) for each Animal object.
        public Guid ID { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Species { get; set; }

        // This is a constructor method for the Animal class, encapsulating the creation of an animal.
        // Encapsulation protects the data from outside interference and misuse.
        public Animal(string name, int age, string species)
        {
            ID = Guid.NewGuid(); // Assign a unique ID to each animal
            Name = name;
            Age = age;
            Species = species;
        }

        // This is a virtual method, allowing for the method to be overridden in subclasses.
        // This is another form of Polymorphism. The code will call the appropriate method depending on the actual object type at runtime.
        public virtual void Eat()
        {
            Console.WriteLine($"{Name} the {Species} is eating.");
        }
    }
    public class Habitat
    {
        public string Name { get; set; }

        // The Animals property is a List<Animal>, which is a data structure that holds a sequence of elements. 
        // It is used to store the animals that live in the same habitat.
        private List<Animal> Animals { get; set; }

        public Habitat(string name)
        {
            Name = name;
            Animals = new List<Animal>();

            Console.WriteLine($"Added {name} Habitat to the Zoo.");
        }

        public void AddAnimal(Animal animal)
        {
            Animals.Add(animal);
            Console.WriteLine($"Added {animal.Name} the {animal.Species} to the {Name} habitat.");
        }

        public List<Animal> GetAnimals()
        {
            return Animals;
        }
    }
    public class Lion : Animal, ISoundBehaviour
    {
        // This is the constructor for the Lion class, which calls the base class constructor.
        // The "base" keyword is used to access members in the base class from within the derived class.
        public Lion(string name, int age, Habitat habitat) : base(name, age, "Lion")
        {
            habitat.AddAnimal(this);  // Interacting with another object (the habitat), this is an aspect of object-oriented programming.
            Console.WriteLine($"Added {Name} the {Species} to the {habitat.Name} habitat.");
        }

        // This method overrides the virtual method from the parent class.
        // This is another example of Polymorphism.
        public override void Eat()
        {
            Console.WriteLine($"{Name} the Lion is eating meat.");
        }

        // This method provides the implementation for the ISoundBehaviour interface.
        // This is an example of Polymorphism. At runtime, the appropriate method will be called depending on the actual object type.
        public void MakeSound()
        {
            Console.WriteLine($"{Name} the Lion is roaring!");
        }
    }
    public class Elephant : Animal, ISoundBehaviour
    {
        public Elephant(string name, int age, Habitat habitat) : base(name, age, "Elephant")
        {
            habitat.AddAnimal(this);
            Console.WriteLine($"Added {Name} the {Species} to the {habitat.Name} habitat.");
        }

        public override void Eat()
        {
            Console.WriteLine($"{Name} the Elephant is eating peanuts.");
        }

        public void MakeSound()
        {
            Console.WriteLine($"{Name} the Elephant is trumpeting!");
        }
    }
    public class Monkey : Animal, ISoundBehaviour
    {
        public Monkey(string name, int age, Habitat habitat) : base(name, age, "Monkey")
        {
            habitat.AddAnimal(this);
            Console.WriteLine($"Added {Name} the {Species} to the {habitat.Name} habitat.");
        }

        public override void Eat()
        {
            Console.WriteLine($"{Name} the Monkey is eating bananas.");
        }

        public void MakeSound()
        {
            Console.WriteLine($"{Name} the Monkey is chattering!");
        }
    }
    //The Fish Class doesn't implement the ISoundBehaviour interface as the Fish don't sounds
    public class Fish : Animal
    {
        public Fish(string name, int age, Habitat habitat) : base(name, age, "Fish")
        {
            habitat.AddAnimal(this);
            Console.WriteLine($"Added {Name} the {Species} to the {habitat.Name} habitat.");
        }

        public override void Eat()
        {
            Console.WriteLine($"{Name} the Fish is eating seaweed.");
        }
    }
    // The Zoo class represents a collection of Habitat objects, an example of Aggregation.
    // Aggregation is a type of Association where the aggregate (Zoo) can exist independently of the parts (Habitats).
    public class Zoo
    {
        // The Habitats property is a List<Habitat>, a collection of different habitats present in the zoo.
        private List<Habitat> Habitats { get; set; }
        // The AnimalsInventory property is a Dictionary<Guid, Animal>, a collection of all animals present in the zoo,
        // it maps the unique ID of an animal to its corresponding Animal object. This allows for fast lookup of animals.
        private Dictionary<string, int> AnimalCountByType { get; set; }

        public Zoo()
        {
            Habitats = new List<Habitat>();
            AnimalCountByType = new Dictionary<string, int>();

            Console.WriteLine("New Zoo Created...\n");
        }

        public void AddHabitat(Habitat habitat)
        {
            Habitats.Add(habitat);
        }

        public void UpdateAnimalCounts()
        {
            foreach (var habitat in Habitats)
            {
                foreach (var animal in habitat.GetAnimals())
                {
                    if (AnimalCountByType.ContainsKey(animal.Species))
                        AnimalCountByType[animal.Species]++;
                    else
                        AnimalCountByType.Add(animal.Species, 1);
                }
            }
        }

        // A method to get the count of animals by type
        public int GetAnimalCountByType(string species)
        {
            if (AnimalCountByType.ContainsKey(species))
                return AnimalCountByType[species];
            else
                return 0;
        }

        // The FeedAllAnimals method is used to feed all animals in the zoo.
        public void FeedAllAnimals()
        {
            Console.WriteLine("\nFeeding all animals...\n");
            foreach (var habitat in Habitats)
            {
                Console.WriteLine($"In {habitat.Name} habitat:");
                foreach (var animal in habitat.GetAnimals())
                {
                    animal.Eat();
                }
                Console.WriteLine();
            }
        }

        // The MakeAllAnimalsSound method uses the ISoundBehaviour interface.
        // This is another example of Polymorphism. At runtime, the appropriate method will be called depending on the actual object type.
        public void MakeAllAnimalsSound()
        {
            Console.WriteLine("\nMaking all animals sound...\n");
            foreach (var habitat in Habitats)
            {
                Console.WriteLine($"In {habitat.Name} habitat:");
                foreach (var animal in habitat.GetAnimals()) // We get List<Animal>
                {
                    if (animal is ISoundBehaviour soundMakingAnimal)
                    {
                        soundMakingAnimal.MakeSound();
                    }
                }
                Console.WriteLine();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Object creation: an instance of the Zoo class is created.
            // This is an example of the instantiation aspect of OOP where objects are instances of classes.
            Zoo zoo = new Zoo();

            // Object creation: instances of the Habitat class are created.
            // These are examples of abstraction where real-world concepts (habitats) are represented in code.
            Habitat savannah = new Habitat("Savannah");
            Habitat jungle = new Habitat("Jungle");
            Habitat pond = new Habitat("Pond");

            // Object interaction: the Zoo object interacts with Habitat objects using the AddHabitat method.
            // This is an example of method invocation and encapsulation where the implementation details are hidden and interaction occurs through methods.
            zoo.AddHabitat(savannah);
            zoo.AddHabitat(jungle);
            zoo.AddHabitat(pond);

            Console.WriteLine("");

            // Object creation and interaction: instances of various Animal-derived classes are created and added to the habitats.
            // This is an example of polymorphism (specifically subtype polymorphism) as each specific animal is treated as an Animal object.
            // Encapsulation is also demonstrated as the Animal objects interact with the Habitat objects through the AddAnimal method.
            Lion lion = new Lion("Simba", 5, savannah);
            lion = new Lion("Simba", 5, savannah);
            Elephant elephant = new Elephant("Dumbo", 8, savannah);
            Monkey monkey = new Monkey("George", 3, jungle);
            Fish fish = new Fish("Nemo", 2, pond);

            zoo.UpdateAnimalCounts();

            // Object interaction: the Zoo object interacts with the Animal and Habitat objects using the FeedAllAnimals and MakeAllAnimalsSound methods.
            // This is an example of method invocation, encapsulation, and polymorphism.
            // Polymorphism is displayed as the same method call results in different behaviors depending on the type of Animal.
            zoo.FeedAllAnimals();
            zoo.MakeAllAnimalsSound();

            // New line of code to demonstrate getting the count of animals by type
            Console.WriteLine($"\nNumber of Lions in the zoo: {zoo.GetAnimalCountByType("Lion")}");
            Console.WriteLine($"Number of Elephants in the zoo: {zoo.GetAnimalCountByType("Elephant")}");
            Console.WriteLine($"Number of Monkeys in the zoo: {zoo.GetAnimalCountByType("Monkey")}");
            Console.WriteLine($"Number of Fish in the zoo: {zoo.GetAnimalCountByType("Fish")}");
        }
    }
}
