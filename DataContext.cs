using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ConsoleNetflix
{
    public class DataContext
    {
        static DataContext _context = new DataContext();
        static User CurrentUser;

        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<User> Users { get; set; } = new List<User>();

        public DataContext()
        {
            var action = new Genre("Action");
            var drama = new Genre("Drama");
            var comedy = new Genre("Comedy");
            var sciFi = new Genre("Sci-Fi");
            var crime = new Genre("Crime");
            var thriller = new Genre("Thriller");
            var adventure = new Genre("Adventure");
            var animation = new Genre("Animation");

            Genres.AddRange(new[] {
        action,
        drama,
        comedy,
        sciFi,
        crime,
        thriller,
        adventure,
        animation
    });

            Movies.AddRange(new[]
            {
          new Movie("Inception", action, 100, "A mind-bending thriller about dreams within dreams, where a team of experts attempt to implant an idea into someone's subconscious.", 148.5m),
new Movie("The Godfather", drama, 200, "The saga of a powerful Italian-American crime family led by Don Vito Corleone, with themes of loyalty, betrayal, and family.", 175.0m),
new Movie("Superbad", comedy, 50, "Two high school friends try to enjoy their final days together before graduation, but their plans go awry as they face hilarious misadventures.", 113.0m),
new Movie("The Dark Knight", action, 300, "Batman faces the Joker, a criminal mastermind with anarchy in his heart, while trying to protect Gotham from falling into chaos.", 152.0m),
new Movie("The Shawshank Redemption", drama, 150, "A man is sentenced to life in Shawshank prison, where he forms a deep bond with a fellow inmate and dreams of freedom.", 142.0m),
new Movie("The Matrix", sciFi, 250, "A computer hacker learns that reality is an illusion controlled by machines, and he becomes part of a resistance to free humanity.", 136.0m),
new Movie("Avengers: Endgame", action, 500, "The Avengers must band together one last time to undo the damage caused by Thanos in a final battle for the fate of the universe.", 181.0m),
new Movie("Forrest Gump", drama, 180, "The life story of Forrest Gump, an unintelligent man with extraordinary luck and a unique perspective on historical events in the 20th century.", 142.0m),
new Movie("Pulp Fiction", crime, 130, "A series of interconnected stories involving crime, redemption, and fate in the gritty underworld of Los Angeles.", 154.0m),
new Movie("The Hangover", comedy, 120, "After a wild bachelor party in Las Vegas, three friends struggle to piece together what happened the night before and search for their missing friend.", 100.0m),
new Movie("Jurassic Park", adventure, 220, "Scientists create a theme park with cloned dinosaurs, but things go awry when the creatures escape, putting everyone in peril.", 127.0m),
new Movie("The Silence of the Lambs", thriller, 160, "A young FBI agent seeks the help of a cannibalistic serial killer to track down another dangerous murderer.", 118.0m),
new Movie("Gladiator", action, 210, "A betrayed Roman general seeks revenge against the corrupt emperor who murdered his family and sent him into slavery.", 155.0m),
new Movie("The Lion King", animation, 300, "The journey of Simba, a lion cub who must embrace his destiny to become king after the tragic death of his father.", 88.0m),
new Movie("Fight Club", drama, 140, "An insomniac office worker forms an underground fight club with a soap salesman, but things spiral into chaos as the club grows.", 139.0m),
new Movie("Guardians of the Galaxy", sciFi, 175, "A group of misfit space heroes form an unlikely alliance to stop a villain from destroying the galaxy.", 121.0m),
new Movie("Interstellar", sciFi, 190, "A group of astronauts travel through a wormhole in search of a new home for humanity as Earth faces environmental collapse.", 169.0m),
new Movie("Deadpool", action, 120, "A former special forces operative turned mercenary gains regenerative healing powers and seeks revenge on the man who disfigured him.", 108.0m),
new Movie("The Pursuit of Happyness", drama, 110, "Based on a true story, a struggling salesman fights against adversity to create a better life for his son.", 117.0m),
new Movie("The Big Lebowski", comedy, 110, "A laid-back slacker known as 'The Dude' gets caught up in a series of misadventures involving a millionaire, a kidnapping, and a bowling tournament.", 117.0m)
        });
        }
        internal static void Login()
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
                    Console.WriteLine("1. Add Movie\n2. Remove Movie\n3. Add Genre\n4. Remove Genre\n5. Most Viewed Movies \n 6. Logout \n0. Exit");
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
        static void PrintGenres()
        {
            foreach (var genre in _context.Genres)
            {
                Console.WriteLine($"***{genre.Id} Genre: {genre.Name}***");
            }
        }
    }
}
