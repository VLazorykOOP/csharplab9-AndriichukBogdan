using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace Lab6Charp
{
    // Класси для 3 завдання(1 частина)
    class DescendingComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((int)y).CompareTo((int)x);
        }
    }

    class NumberList : ICloneable, IEnumerable
    {
        private ArrayList numbers = new ArrayList();

        public void Add(int number)
        {
            numbers.Add(number);
        }

        public void Sort(IComparer comparer)
        {
            numbers.Sort(comparer);
        }

        public object Clone()
        {
            NumberList clone = new NumberList();
            foreach (int n in numbers)
                clone.Add(n);
            return clone;
        }

        public IEnumerator GetEnumerator()
        {
            return numbers.GetEnumerator();
        }
    }

    //Класс для 3 завдання (2 частина)
    class CharList : ICloneable, IEnumerable
    {
        private ArrayList chars = new ArrayList();

        public void Add(char c)
        {
            chars.Add(c);
        }

        public object Clone()
        {
            CharList clone = new CharList();
            foreach (char c in chars)
                clone.Add(c);
            return clone;
        }

        public IEnumerator GetEnumerator()
        {
            return chars.GetEnumerator();
        }
    }
    //Класси для 4 завдання
    class Song
    {
        private string title;
        private string artist;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }

        public Song(string t, string a)
        {
            title = t;
            artist = a;
        }

        public override string ToString()
        {
            return "Name: " + Title + ", Artist: " + Artist;
        }
    }

    class MusicDisc
    {
        private string name;
        private ArrayList songs;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public MusicDisc(string n)
        {
            name = n;
            songs = new ArrayList();
        }
        public void AddSong(Song song)
        {
            songs.Add(song);
        }

        public void RemoveSong(string title)
        {
            for (int i = 0; i < songs.Count; i++)
            {
                Song song = (Song)songs[i];
                if (song.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    songs.RemoveAt(i);
                    i--;
                }
            }
        }

        public ArrayList GetSongs()
        {
            return songs;
        }

        public override string ToString()
        {
            string result = ("Disc: "+Name+"\n");
            foreach (Song song in songs)
            {
                result += "  - " + song + "\n";
            }
            return result;
        }
    }

    class MusicCatalog
    {
        private Hashtable discs = new Hashtable();

        public void AddDisc(string name)
        {
            if (!discs.ContainsKey(name))
                discs.Add(name, new MusicDisc(name));
        }

        public void RemoveDisc(string name)
        {
            if (discs.ContainsKey(name))
                discs.Remove(name);
        }

        public void AddSongToDisc(string discName, Song song)
        {
            if (discs.ContainsKey(discName))
            {
                MusicDisc disc = (MusicDisc)discs[discName];
                disc.AddSong(song);
            }
        }

        public void RemoveSongFromDisc(string discName, string songTitle)
        {
            if (discs.ContainsKey(discName))
            {
                MusicDisc disc = (MusicDisc)discs[discName];
                disc.RemoveSong(songTitle);
            }
        }

        public void ViewAllCatalog()
        {
            foreach (DictionaryEntry entry in discs)
            {
                Console.WriteLine(entry.Value);
            }
        }

        public void ViewDisc(string discName)
        {
            if (discs.ContainsKey(discName))
                Console.WriteLine(discs[discName]);
            else
                Console.WriteLine("Disc is missed.");
        }

        public void SearchByArtist(string artist)
        {
            Console.WriteLine("Searching sorg by artists: {0}", artist);
            foreach (DictionaryEntry entry in discs)
            {
                MusicDisc disc = (MusicDisc)entry.Value;
                ArrayList songs = disc.GetSongs();

                foreach (object obj in songs)
                {
                    Song song = (Song)obj;
                    if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("[{0}] {1}", disc.Name, song);
                    }
                }
            }
        }
    }



    internal class Program
    {

        static void task1()
        {
            string inputFile = "input_1.txt";
            string outputFile = "output_1.txt";

            Stack<int> numbers = new Stack<int>();


            string[] lines = File.ReadAllLines(inputFile);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int number))
                    {
                        numbers.Push(number);
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                while (numbers.Count > 0)
                {
                    writer.Write(numbers.Pop() + " ");
                }
            }

            Console.WriteLine("Complete!");
        }

        static void task2()
        {
            string inputFile = "input_2.txt";

            Queue<char> nonDigits = new Queue<char>();
            Queue<char> digits = new Queue<char>();


            using (StreamReader reader = new StreamReader(inputFile))
            {
                int ch;
                while ((ch = reader.Read()) != -1)
                {
                    char c = (char)ch;
                    if (char.IsDigit(c))
                        digits.Enqueue(c);
                    else
                        nonDigits.Enqueue(c);
                }
            }

            Console.WriteLine("Result:");
            foreach (char c in nonDigits)
                Console.Write(c);
            foreach (char c in digits)
                Console.Write(c);

            Console.WriteLine();
        }

        static void task3()
        {
            string inputFile = "input_1.txt";
            string outputFile = "output_1_1.txt";

            NumberList numberList = new NumberList();

            foreach (string line in File.ReadAllLines(inputFile))
            {
                foreach (string part in line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (int.TryParse(part, out int num))
                        numberList.Add(num);
                }
            }

            numberList.Sort(new DescendingComparer());

            NumberList clonedList = (NumberList)numberList.Clone();

            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (int n in clonedList)
                    writer.Write(n + " ");
            }
        }

        static void task4()
        {
            string inputFile = "input_2.txt";

            CharList digits = new CharList();
            CharList nonDigits = new CharList();

            using (StreamReader reader = new StreamReader(inputFile))
            {
                int ch;
                while ((ch = reader.Read()) != -1)
                {
                    char c = (char)ch;
                    if (char.IsDigit(c))
                        digits.Add(c);
                    else
                        nonDigits.Add(c);
                }
            }

            CharList clonedNonDigits = (CharList)nonDigits.Clone();
            CharList clonedDigits = (CharList)digits.Clone();

            Console.WriteLine("Result:");
            foreach (char c in clonedNonDigits)
                Console.Write(c);
            foreach (char c in clonedDigits)
                Console.Write(c);
        }

        static void task5()
        {
            MusicCatalog catalog = new MusicCatalog();

            catalog.AddDisc("Rock Hits");
            catalog.AddDisc("Pop Mix");

            catalog.AddSongToDisc("Rock Hits", new Song("Highway to Hell", "AC/DC"));
            catalog.AddSongToDisc("Rock Hits", new Song("Smells Like Teen Spirit", "Nirvana"));
            catalog.AddSongToDisc("Pop Mix", new Song("Johny", "Varto"));
            catalog.AddSongToDisc("Pop Mix", new Song("Bad", "Billie"));

            Console.WriteLine("\n-- Full catalog --");
            catalog.ViewAllCatalog();

            Console.WriteLine("\n-- Pop music --");
            catalog.ViewDisc("Pop Mix");

            catalog.SearchByArtist("Varto");

            catalog.RemoveSongFromDisc("Rock Hits", "Highway to Hell");

            catalog.RemoveDisc("Pop Mix");

            Console.WriteLine("\n-- Catalog after delete --");
            catalog.ViewAllCatalog();
        }


        static void choose_task()
        {
            Console.Write("1.Stack \n2.Queue \n3.ArrayList(1)\n4.ArrayList(2)\n5.HashTable\n");
            int answer = Convert.ToInt16(Console.ReadLine());

            switch (answer)
            {
                case 1:
                    {
                        task1();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                case 2:
                    {
                        task2();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                case 3:
                    {
                        task3();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                case 4:
                    {
                        task4();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                case 5:
                    {
                        task5();
                        Console.Write("\n\n\n\n\n\n\n");
                        choose_task();
                        break;
                    }
                default:
                    {
                        choose_task();
                        break;
                    }

            }
        }
        public static void Main(string[] args)
        {
            choose_task();
        }
    }
}