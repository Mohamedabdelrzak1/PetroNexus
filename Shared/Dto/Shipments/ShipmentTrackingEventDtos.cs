using System;

namespace Shared.Dto.Shipments
{
    public class ShipmentTrackingEventCreateDto
    {
        public int ShipmentId { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; } = null!;
        public string? Location { get; set; }
    }

    public class ShipmentTrackingEventUpdateDto
    {
        public int ShipmentId { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; } = null!;
        public string? Location { get; set; }
    }

    public class ShipmentTrackingEventResponseDto
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; } = null!;
        public string? Location { get; set; }
    }
}
