namespace OrderList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // --- Ejemplos con tipos nativos ---
            Console.WriteLine("--- Ordenando números enteros ---");
            List<int> numbers = new List<int> { 5, 1, 9, 2, 8, 3, 7, 4, 6 };
            Console.WriteLine("Lista original: " + string.Join(", ", numbers));
            List<int> sortedNumbers = ListManager.OrderDescending(new List<int>(numbers)); // Pasamos una copia
            Console.WriteLine("Lista ordenada: " + string.Join(", ", sortedNumbers));
            Console.WriteLine("---------------------------------\n");

            Console.WriteLine("--- Ordenando cadenas de texto ---");
            List<string> fruits = new List<string> { "banana", "apple", "grape", "cherry", "kiwi" };
            Console.WriteLine("Lista original: " + string.Join(", ", fruits));
            List<string> sortedFruits = ListManager.OrderDescending(new List<string>(fruits)); // Pasamos una copia
            Console.WriteLine("Lista ordenada: " + string.Join(", ", sortedFruits));
            Console.WriteLine("---------------------------------\n");

            // --- Ejemplo con la clase Persona ---
            Console.WriteLine("--- Ordenando Personas por Edad ---");
            List<Person> people = new List<Person>
            {
                new Person("Alice", 30),
                new Person("Bob", 25),
                new Person("Charlie", 35),
                new Person("David", 25), 
                new Person("Eve", 28)
            };

            Console.WriteLine("Lista original de personas:");
            foreach (var person in people)
            {
                Console.WriteLine($"- {person.Name} ({person.Age})");
            }

            // Usamos una copia de la lista para no modificar la original
            List<Person> sortedPeople = ListManager.OrderDescending(new List<Person>(people));

            Console.WriteLine("\nLista de personas ordenada (por Edad, luego por Nombre):");
            foreach (var person in sortedPeople)
            {
                Console.WriteLine($"- {person.Name} ({person.Age})");
            }
            Console.WriteLine("---------------------------------\n");
        }
    }

    public static class ListManager
    {
        public static List<T> OrderAscending<T>(List<T> list) where T : IComparable<T>
        {
            // Manejo de caso borde: si la lista está vacía o tiene un solo elemento, ya está "ordenada".
            if (list == null || list.Count <= 1)
            {
                return new List<T>();
            }

            int prev;
            int actual;

            for (int i = 0; i < list.Count; i++)
            {
                prev = i;

                for (int j = i + 1; j < list.Count; j++)
                {
                    actual = j;

                    if (list[prev].CompareTo(list[actual]) > 0)
                    {
                        T temporal = list[prev];
                        list[prev] = list[actual];
                        list[actual] = temporal;
                    }

                    prev = actual;
                }
            }
            return list;
        }
        public static List<T> OrderAscending2<T>(List<T> list) where T : IComparable<T>
        {
            // Manejo de caso borde: si la lista está vacía o tiene un solo elemento, ya está "ordenada".
            if (list == null || list.Count <= 1)
            {
                return new List<T>();
            }


            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j].CompareTo(list[minIndex]) < 0)
                        minIndex = j;
                }

                if (minIndex != i)
                {
                    T temp = list[i];
                    list[i] = list[minIndex];
                    list[minIndex] = temp;
                }
            }
            return list;
        }

        public static List<T> OrderDescending<T>(List<T> list) where T : IComparable<T>
        {
            if (list == null || list.Count <= 1)
            { return new List<T>(); }

            for (int i = 0; i < list.Count; i++)
            {
                int maxIndex = i;

                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[maxIndex].CompareTo(list[j]) < 0)
                        maxIndex = j;
                }

                if (maxIndex != i)
                {
                    T temp = list[i];
                    list[i] = list[maxIndex];
                    list[maxIndex] = temp;
                }
            }
            return list;
        }
        
    }
    public class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public int CompareTo(Person other)
        {
            if (other == null) return 1;

            int ageComparison = this.Age.CompareTo(other.Age);
            if (ageComparison != 0)
            {
                return ageComparison;
            }

            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return $"{Name} ({Age})";
        }
    }
}
