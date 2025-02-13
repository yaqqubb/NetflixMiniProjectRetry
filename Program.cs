using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace ConsoleNetflix
{
    internal class Program
    {
        static DataContext _context = new DataContext();
        static User CurrentUser;
        
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            Console.WriteLine("Welcome to Netflix!");
            Login();

        }


        static void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            User user = _context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user != null && user.Password == password)
            {
                
               CurrentUser = user;
               Console.WriteLine($"Logged in as {CurrentUser.Role}");
               ShowMenu();
            }
            else
            {
                Console.WriteLine("Incorrect credentials!");
                Login();
            }
        }


        static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nCommands:");
                if (CurrentUser.Role == UserType.Admin)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("1. Add Movie\n2. Remove Movie\n3. Add Genre\n4. Remove Genre\n5. Most Viewed Movies \n6. Logout \n0. Exit");
                }
                else
                {
                    Console.WriteLine("1. Watch Movie\n2. Filter by Genre\n3. Add to Watchlist\n4. Search Movie\n5. Show Watchlist\n6. Logout\n0. Exit");
                }
                Console.Write("Enter command: ");

                string command = Console.ReadLine();
                ExecuteCommand(command);
            }
        }



        static void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "1":
                    if (CurrentUser.Role == UserType.Admin) AddMovie();
                    else WatchMovie();
                    break;
                case "2":
                    if (CurrentUser.Role == UserType.Admin) RemoveMovie();
                    else FilterMovies();
                    break;
                case "3":
                    if (CurrentUser.Role == UserType.Admin) AddGenre();
                    else AddToWatchlist();
                    break;
                case "4":
                    if (CurrentUser.Role == UserType.Admin) RemoveGenre();
                    else SearchMovie();
                    break;
                case "5":
                    if (CurrentUser.Role == UserType.Admin) ShowMostViewed();
                    else PrintWatchlist();
                    break;
                case "6":
                    Logout();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nInvalid command. Try again.");
                    break;
            }
        }

        static void AddMovie()
        {
            Console.Write("Enter movie title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Available Genres:");
            PrintGenres();
            Console.Write("Enter genre ID: ");
            if (int.TryParse(Console.ReadLine(), out int genreId))
            {
                var genre = _context.Genres.FirstOrDefault(g => g.Id == genreId);
                if (genre != null)
                {
                    Console.Write("Enter the movie description: ");
                    string desc = Console.ReadLine();
                    Console.Write("Enter the movie runtime: ");
                    decimal runtime = decimal.Parse(Console.ReadLine());

                    var movie = new Movie(title, genre, 0, desc, runtime);
                    _context.Movies.Add(movie);
                    Console.WriteLine("Movie added successfully!");
                    PrintMovies();
                }
                else
                {
                    Console.WriteLine("Invalid genre ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid genre ID.");
            }
        }
        static void AddToWatchlist()
        {
            PrintMovies();
            Console.Write("Enter movie title to add to watchlist: ");
           
            string title = Console.ReadLine();
            var movie = _context.Movies.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (movie != null)
            {
                CurrentUser.Watchlist.Add(movie);
                Console.WriteLine($"{movie.Title} added to your watchlist.");
            }
            else
            {
                Console.WriteLine("Movie not found!");
            }
        }
        static void WatchMovie()
        {
            PrintMovies();
            Console.Write("Enter movie title to watch: ");
            string title = Console.ReadLine();
            var movie = _context.Movies.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (movie != null)
            {
                movie.ViewCount++;
                Console.WriteLine($"You are watching {movie.Title}");
            }
            else
            {
                Console.WriteLine("Movie not found!");
            }
        }
        static void RemoveMovie()
        {
            PrintMovies();
            Console.Write("Enter movie title to remove: ");
            string title = Console.ReadLine();
            var movie = _context.Movies.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                Console.WriteLine("Movie removed successfully!");
            }
            else
            {
                Console.WriteLine("Movie not found!");
            }
        }
        static void AddGenre()
        {
           
            Console.Write("Enter genre name: ");
            string name = Console.ReadLine();
            _context.Genres.Add(new Genre { Name = name });
            Console.WriteLine("Genre added successfully!");
        }
        static void RemoveGenre()
        {
            PrintGenres();
            Console.Write("Enter genre name to remove: ");
            string name = Console.ReadLine();
            var genre = _context.Genres.FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                Console.WriteLine("Genre removed successfully!");
            }
            else
            {
                Console.WriteLine("Genre not found!");
            }
        }
        static void FilterMovies()
        {
            PrintGenres();
            Console.Write("Enter genre name: ");
            string genreName = Console.ReadLine();
            var movies = _context.Movies.Where(m => m.Genre.Name.Equals(genreName, StringComparison.OrdinalIgnoreCase));
            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title);
            }
        }
        
        

        static void ShowMostViewed()
        {
            var mostViewed = _context.Movies.OrderByDescending(m => m.ViewCount).Take(5);
            Console.WriteLine("Most Viewed Movies:");
            foreach (var movie in mostViewed)
            {
                Console.WriteLine($"{movie.Title} - {movie.ViewCount} views");
            }
        }
        static void SearchMovie()
        {
            Console.Write("Enter movie title to search: ");
            string title = Console.ReadLine();
            var movie = _context.Movies.FirstOrDefault(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine(movie != null ? movie.Title : "Movie not found!");
        }

        static void Logout()
        {
            Console.WriteLine("Logging out...");
            CurrentUser = null;
            Console.ForegroundColor = ConsoleColor.White;
            Login();

        }

        static void PrintGenres()
        {
            foreach (var genre in _context.Genres)
            {
                Console.WriteLine($"***{genre.Id} Genre: {genre.Name}***");
            }
        }

        static void PrintMovies()
        {
            if (_context.Movies.Count == 0)
            {
                Console.WriteLine("\n╔════════════════════════════════════╗");
                Console.WriteLine("║        🎬 No Movies Available      ║");
                Console.WriteLine("╚════════════════════════════════════╝\n");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      🎬 Available Movies 🎬                     ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");

            foreach (var movie in _context.Movies)
            {
               Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"║ 🎥 Title       : {movie.Title,-40} ║");
                Console.WriteLine($"║ 📂 Genre       : {movie.Genre.Name,-40} ║");
                Console.WriteLine($"║ 👀 Views       : {movie.ViewCount,-40} ║");
                Console.WriteLine($"║ ⏳ RunTime     : {movie.RunTime,-40} ║");
                Console.WriteLine("║ 📝 Description:");
                int maxWidth = 40;
                string description = movie.Description;
                while (description.Length > 0)
                {
                    int length = description.Length > maxWidth ? maxWidth : description.Length;
                    string line = description.Substring(0, length);
                    description = description.Length > maxWidth ? description.Substring(length) : "";

                    Console.WriteLine($" {line,-40} ║");
                }
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            }
        }

        static void PrintWatchlist()
        {
            if (CurrentUser.Watchlist.Count == 0)
            {
                Console.WriteLine("\n╔════════════════════════════════════╗");
                Console.WriteLine("║        🎬 No Movies in Watchlist   ║");
                Console.WriteLine("╚════════════════════════════════════╝\n");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                   🎬 Your Watchlist 🎬                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");

            foreach (var movie in CurrentUser.Watchlist)
            {
                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine($"║ 🎥 Title       : {movie.Title,-40} ║");
                Console.WriteLine($"║ 📂 Genre       : {movie.Genre.Name,-40} ║");
                Console.WriteLine($"║ 👀 Views       : {movie.ViewCount,-40} ║");
                Console.WriteLine($"║ ⏳ RunTime     : {movie.RunTime,-40} ║");
                Console.WriteLine("║ 📝 Description:");
                int maxWidth = 40;
                string description = movie.Description;
                while (description.Length > 0)
                {
                    int length = description.Length > maxWidth ? maxWidth : description.Length;
                    string line = description.Substring(0, length);
                    description = description.Length > maxWidth ? description.Substring(length) : "";

                    Console.WriteLine($" {line,-40} ║");
                }
                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            }
        }


    }
}

