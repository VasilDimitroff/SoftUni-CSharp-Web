namespace SharedTrip.Data
{
    public class UserTrip
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int TripId { get; set; }

        public Trip Trip { get; set; }
    }
}