using movieTicket;

MovieList movieList = new MovieList();
movieList.AddMovie(new Movie("Dunky", "Comedy", "Hindi", 8.8, new string[] { "10:00 AM", "3:00 PM", "7:00 PM" }));
movieList.AddMovie(new Movie("Wednesday", "Thriller", "English", 8.6, new string[] { "12:00 PM", "6:00 PM" }));
movieList.AddMovie(new Movie("Amaron", "Action", "Tamil", 9.2, new string[] { "9:00 AM", "1:00 PM", "5:00 PM" }));

// Creating a Binary Search Tree for theaters
TheaterBST theaters = new TheaterBST();
theaters.Insert("Galaxy", 50);
theaters.Insert("Sunset", 100);

Theater[] theaterArray = theaters.GetTheatersAsArray();

// Creating a Theater Graph for showtimes
TheaterGraph theaterGraph = new TheaterGraph();
theaterGraph.AddTheater("Galaxy");
theaterGraph.AddTheater("Sunset");
theaterGraph.AddShowtime("Galaxy", "10:00 AM");
theaterGraph.AddShowtime("Sunset", "12:00 PM");

bool running = true;
while (running)
{
    Console.WriteLine("\n--- Movie Ticket Booking System ---");
    Console.WriteLine("1. Display Movies");
    Console.WriteLine("2. Sort Movies by Rating");
    Console.WriteLine("3. Select a Movie");
    Console.WriteLine("4. Exit");
    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            // Display all movies
            movieList.DisplayMovies();
            break;

        case "2":
            // Sort movies by rating
            movieList.MergeSortByRating();
            Console.WriteLine("\nMovies sorted by rating.");
            movieList.DisplayMovies();
            break;

        case "3":
            // Select a movie and then book a seat
            Console.Write("\nEnter movie name: ");
            string movieName = Console.ReadLine();
            Movie? selectedMovie = movieList.SelectMovieByName(movieName); 

            if (selectedMovie == null)
            {
                Console.WriteLine("Movie not found.");
                break;
            }

            // Display available showtimes for the selected movie
            Console.WriteLine("\nAvailable Showtimes:");
            foreach (var show in selectedMovie.ShowTimes)
            {
                Console.WriteLine(show);
            }

            Console.Write("\nSelect a showtime: ");
            string showtime = Console.ReadLine();

            Console.WriteLine("\nAvailable Theaters:");
            foreach (var theater in theaterArray)
            {
                Console.WriteLine(theater.Name);
            }

            Console.Write("\nSelect a theater: ");
            string theaterName = Console.ReadLine();

            // Search for the selected theater
            Theater selectedTheater = theaters.Search(theaterName);
            if (selectedTheater == null)
            {
                Console.WriteLine("Invalid theater.");
                break;
            }

            // Display available seats for the selected theater
            selectedTheater.DisplaySeats();
            Console.Write("\nSelect a seat number: ");
            int seatNumber = int.Parse(Console.ReadLine());

            // Check if the seat is already booked 
            if (selectedTheater.Seats[seatNumber] != null)
            {
                Console.WriteLine("\nSeat already booked.");
            }
            else
            {
                // Proceed to ask for user details if the seat is available
                Console.Write("\nEnter your name: ");
                string userName = Console.ReadLine();
                Console.Write("Enter your contact number: ");
                string contactNo = Console.ReadLine();

                User user = new User(userName, contactNo);

                // Attempt to book the seat
                if (selectedTheater.BookSeat(seatNumber, user))
                {
                    Console.WriteLine($"\nBooking confirmed for {userName} at {theaterName} for {selectedMovie.Title} at {showtime}.");
                }
                else
                {
                    Console.WriteLine("\nSeat could not be booked.");
                }
            }
            break;

        case "4":
            // Exit the program
            running = false;
            break;

        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

Console.WriteLine("Thank you for using the Movie Ticket Booking System!");
            