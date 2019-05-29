public class UsersApiModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int Role { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime TCAccept { get; set; }
        public DateTime SignUpDate { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isTCAccept { get; set; }

        public string LocationId { get; set; }

        public List<Location> Location { get; set; }
        public StripeApiModel CreditCardId { get; set; }
    }
    
    public class Location 
    {
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string Postal { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int UserId { get; set; }
    }
