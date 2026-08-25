using System;

namespace Shared.Dto.PurchaseOrders
{
    public class PurchaseOrderItemCreateDto
    {
        public int PurchaseOrderId { get; set; }
        public int PrincipalProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
    }

    public class PurchaseOrderItemUpdateDto
    {
        public int PurchaseOrderId { get; set; }
        public int PrincipalProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
    }

    public class PurchaseOrderItemResponseDto
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int PrincipalProductId { get; set; }
        public string PrincipalProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
    }
}
