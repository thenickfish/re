using System.Collections.Generic;

namespace api.Features.House
{
    public class House
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public ICollection<RoomMate> RoomMates { get; set; }
        public Bill Rent { get; set; }
    }
}