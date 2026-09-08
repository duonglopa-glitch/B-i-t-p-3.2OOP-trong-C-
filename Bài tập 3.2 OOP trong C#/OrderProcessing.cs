using System;

namespace OOP_Practice
{
    public class DiscountCalculator
    {
        public decimal ApplyDiscount(decimal totalAmount) => totalAmount * 0.95m;

        public decimal ApplyDiscount(decimal totalAmount, double percentage)
        {
            // C# 7.3: Biểu thức logic toán tử ||
            if (percentage < 0 || percentage > 100)
                throw new ArgumentOutOfRangeException(nameof(percentage), "Phần trăm giảm giá phải từ 0 - 100%.");

            return totalAmount - (totalAmount * (decimal)(percentage / 100.0));
        }

        public decimal ApplyDiscount(decimal totalAmount, decimal fixedVoucher, decimal minimumOrder)
        {
            if (totalAmount >= minimumOrder)
            {
                decimal result = totalAmount - fixedVoucher;
                return result < 0m ? 0m : result;
            }
            return totalAmount;
        }
    }

    public class DeliveryService
    {
        public string OrderId { get; set; }
        public double DistanceKm { get; set; }

        public DeliveryService(string orderId, double distanceKm)
        {
            OrderId = orderId;
            DistanceKm = distanceKm;
        }

        public virtual decimal CalculateShippingFee() => (decimal)DistanceKm * 5000m;
    }

    public class ExpressDelivery : DeliveryService
    {
        public ExpressDelivery(string orderId, double distanceKm) : base(orderId, distanceKm) { }

        public override decimal CalculateShippingFee() => (base.CalculateShippingFee() * 1.5m) + 20000m;
    }

    public class EcoDelivery : DeliveryService
    {
        public EcoDelivery(string orderId, double distanceKm) : base(orderId, distanceKm) { }

        public override decimal CalculateShippingFee()
        {
            decimal baseFee = base.CalculateShippingFee();
            return DistanceKm > 10 ? baseFee * 0.90m : baseFee;
        }
    }
}