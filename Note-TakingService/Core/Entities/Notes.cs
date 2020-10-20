using System;

namespace NoteTakingService.Core.Entities
{
    public class Notes
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
}