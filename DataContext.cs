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

            // Add genres to the context
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

            Users.AddRange(new[]
        {
            new User("admin",UserType.Admin, "1234"),
            new User("user", UserType.User, "1234"),
            new User("Yaqub", UserType.Admin, "1234")

        }); ;
        }

    }
}
