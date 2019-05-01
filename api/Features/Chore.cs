using System;

namespace api
{
    public class Chore
    {
        public int Id { get; set; }
        public int Description { get; set; }
        public RoomMate Assignee { get; set; }
        public DateTime DueDate { get; set; }
    }
}