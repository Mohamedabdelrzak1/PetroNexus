using System;

namespace Shared.Dto.CostCenters
{
    public class CostCenterCreateDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Budget { get; set; }
    }

    public class CostCenterUpdateDto
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Budget { get; set; }
    }

    public class CostCenterResponseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Budget { get; set; }
    }
}
