using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movieTicket
{
    // User Class
    class User
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }

        public User(string name, string contactNo)
        {
            this.Name = name;
            this.ContactNo = contactNo;
        }
    }

    // Movie Class 
    class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public double Rating { get; set; }
        public string[] ShowTimes;

        public Movie(string title, string genre, string language, double rating, string[] showTimes)
        {
            Title = title;
            Genre = genre;
            Language = language;
            Rating = rating;
            ShowTimes = showTimes;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}, Genre: {Genre}, Language: {Language}, Rating: {Rating}");
        }
    }

    // Linked List for Movies
    class Node
    {
        public Movie Data;
        public Node Next;

        public Node(Movie data)
        {
            Data = data;
            Next = null;
        }
    }

    // Singly Linked List for Movies
    class MovieList
    {
        public Node Head;

        public void AddMovie(Movie movie)
        {
            Node newNode = new Node(movie);
            if (Head == null)
                Head = newNode;
            else
            {
                Node temp = Head;
                while (temp.Next != null)
                    temp = temp.Next;
                temp.Next = newNode;
            }
        }

        public void DisplayMovies()
        {
            Node temp = Head;
            while (temp != null)
            {
                temp.Data.DisplayInfo();
                temp = temp.Next;
            }
        }

        // Bubble Sort by Rating
        //public void BubbleSortByRating()
        //{
        //    if (Head == null || Head.Next == null) return; 

        //    bool swapped;
        //    do
        //    {
        //        swapped = false;
        //        Node current = Head;
        //        while (current.Next != null)
        //        {
        //            if (current.Data.Rating < current.Next.Data.Rating)
        //            {
        //                // Swap the movies
        //                Movie temp = current.Data;
        //                current.Data = current.Next.Data;
        //                current.Next.Data = temp;
        //                swapped = true;
        //            }
        //            current = current.Next;
        //        }
        //    }
        //    while (swapped);
        //}

        //public void InsertionSortByRating()
        //{
        //    if (Head == null || Head.Next == null) return; 

        //    Node sorted = null;  
        //    Node current = Head; 

        //    while (current != null)
        //    {
        //        Node next = current.Next; 

        //        
        //        if (sorted == null || sorted.Data.Rating <= current.Data.Rating)
        //        {
        //            
        //            current.Next = sorted;
        //            sorted = current;
        //        }
        //        else
        //        {
        //           
        //            Node temp = sorted;
        //            while (temp.Next != null && temp.Next.Data.Rating > current.Data.Rating)
        //            {
        //                temp = temp.Next;
        //            }

        //           
        //            current.Next = temp.Next;
        //            temp.Next = current;
        //        }

        //       
        //        current = next;
        //    }

        //    
        //    Head = sorted;
        //}

     

        public void MergeSortByRating()
        {
            Head = MergeSort(Head);
        }

        private Node MergeSort(Node head)
        {
            if (head == null || head.Next == null)
                return head;

            
            Node middle = GetMiddle(head);
            Node nextOfMiddle = middle.Next;
            middle.Next = null; 

            
            Node left = MergeSort(head);
            Node right = MergeSort(nextOfMiddle);

            
            return SortedMerge(left, right);
        }

        
        private Node SortedMerge(Node left, Node right)
        {
            if (left == null) return right;
            if (right == null) return left;

            Node result;
            if (left.Data.Rating >= right.Data.Rating) 
            {
                result = left;
                result.Next = SortedMerge(left.Next, right);
            }
            else
            {
                result = right;
                result.Next = SortedMerge(left, right.Next);
            }

            return result;
        }

        
        private Node GetMiddle(Node head)
        {
            if (head == null) return head;

            Node slow = head, fast = head;
            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }
            return slow;
        }


        public Movie? SelectMovieByName(string movieName)
        {
            Node temp = Head;
            while (temp != null)
            {
                if (temp.Data.Title == movieName) 
                {
                    return temp.Data;
                }
                temp = temp.Next;
            }
            return null;
        }

    }

    

    // Binary Search Tree Node for Theater
    class Theater
    {
        public string Name;
        public Theater Left, Right; 
        public int Capacity;
        public User[] Seats;

        
        public Theater(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            Seats = new User[capacity + 1]; 
        }

        
        public void DisplaySeats()
        {
            Console.Write("Available seats: ");
            for (int i = 1; i <= Capacity; i++)
            {
                if (Seats[i] == null) 
                    Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        // Method to book a seat
        public bool BookSeat(int seatNumber, User user)
        {
            if (seatNumber < 1 || seatNumber > Capacity || Seats[seatNumber] != null) 
                return false;

            Seats[seatNumber] = user;
            return true;
        }
    }

    
    // Binary Search Tree for Theaters
    class TheaterBST
    {
        public Theater Root; 

        
        public void Insert(string name, int capacity)
        {
            Root = InsertRec(Root, name, capacity);
        }

        private Theater InsertRec(Theater root, string name, int capacity)
        {
            if (root == null)
                return new Theater(name, capacity);

            if (string.Compare(name, root.Name) < 0)
                root.Left = InsertRec(root.Left, name, capacity);
            else
                root.Right = InsertRec(root.Right, name, capacity);

            return root;
        }

        // Search for a Theater in the BST by its name
        public Theater Search(string name)
        {
            return SearchRec(Root, name);
        }

        private Theater SearchRec(Theater root, string name)
        {
            if (root == null || root.Name == name)
                return root;

            if (string.Compare(name, root.Name) < 0)
                return SearchRec(root.Left, name);

            return SearchRec(root.Right, name);
        }

        
        private int CountTheaters(Theater node)
        {
            if (node == null)
                return 0;
            return 1 + CountTheaters(node.Left) + CountTheaters(node.Right);
        }

        // Return theaters in sorted order as an array
        public Theater[] GetTheatersAsArray()
        {
            int size = CountTheaters(Root);
            Theater[] theaters = new Theater[size];
            int index = 0;
            FillArrayInOrder(Root, theaters, ref index);
            return theaters;
        }

        
        private void FillArrayInOrder(Theater node, Theater[] theaters, ref int index)
        {
            if (node != null)
            {
                FillArrayInOrder(node.Left, theaters, ref index);
                theaters[index++] = node; // Store theater in array
                FillArrayInOrder(node.Right, theaters, ref index);
            }
        }
    }
    class TheaterGraph
    {
        private class TheaterNode
        {
            public string Name;
            public ShowtimeNode ShowtimesHead;
            public TheaterNode Next;

            public TheaterNode(string name)
            {
                Name = name;
                ShowtimesHead = null;
                Next = null;
            }
        }

        private class ShowtimeNode
        {
            public string Showtime;
            public ShowtimeNode Next;

            public ShowtimeNode(string showtime)
            {
                Showtime = showtime;
                Next = null;
            }
        }

        private TheaterNode Head;

        public void AddTheater(string theater)
        {
            TheaterNode newNode = new TheaterNode(theater);
            if (Head == null)
                Head = newNode;
            else
            {
                TheaterNode temp = Head;
                while (temp.Next != null)
                    temp = temp.Next;
                temp.Next = newNode;
            }
        }

        public void AddShowtime(string theater, string showtime)
        {
            TheaterNode temp = Head;
            while (temp != null && temp.Name != theater)
                temp = temp.Next;

            if (temp != null)
            {
                ShowtimeNode newShowtime = new ShowtimeNode(showtime);
                if (temp.ShowtimesHead == null)
                    temp.ShowtimesHead = newShowtime;
                else
                {
                    ShowtimeNode stTemp = temp.ShowtimesHead;
                    while (stTemp.Next != null)
                        stTemp = stTemp.Next;
                    stTemp.Next = newShowtime;
                }
            }
        }

        public void DisplayTheaterShowtimes(string theater)
        {
            TheaterNode temp = Head;
            while (temp != null && temp.Name != theater)
                temp = temp.Next;

            if (temp != null)
            {
                Console.Write($"{theater} has showtimes: ");
                ShowtimeNode stTemp = temp.ShowtimesHead;
                while (stTemp != null)
                {
                    Console.Write(stTemp.Showtime + " ");
                    stTemp = stTemp.Next;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Theater not found.");
            }
        }
    }

}
