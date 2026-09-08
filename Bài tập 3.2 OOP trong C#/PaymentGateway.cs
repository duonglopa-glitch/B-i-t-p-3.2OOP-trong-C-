using System;

namespace OOP_Practice
{
    public interface IPayable
    {
        bool ProcessPayment(decimal amount);
    }

    public interface IRefundable
    {
        bool ProcessRefund(decimal amount, string reason);
    }

    public abstract class PaymentGateway
    {
        // C# 7.3: private set
        public string TransactionId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Status { get; protected set; }

        protected PaymentGateway(string transactionId)
        {
            TransactionId = transactionId;
            CreationDate = DateTime.Now;
            Status = "Pending";
        }

        public abstract void ValidateConnection();

        public virtual void LogTransaction(string message)
        {
            Console.WriteLine($"[{CreationDate:yyyy-MM-dd HH:mm:ss}] [Mã GD: {TransactionId}] [Trạng thái: {Status}] {message}");
        }
    }

    public class MomoPayment : PaymentGateway, IPayable, IRefundable
    {
        public string PhoneNumber { get; set; }

        public MomoPayment(string transactionId, string phoneNumber) : base(transactionId)
        {
            PhoneNumber = phoneNumber;
        }

        public override void ValidateConnection()
        {
            Console.WriteLine($"[MoMo Gateway] Đang kiểm tra kết nối API MoMo cho SĐT: {PhoneNumber}... Kết nối thành công!");
        }

        public bool ProcessPayment(decimal amount)
        {
            if (amount <= 0m) return false;
            Status = "Success";
            LogTransaction($"Thanh toán thành công số tiền {amount:N0} VNĐ.");
            return true;
        }

        public bool ProcessRefund(decimal amount, string reason)
        {
            if (amount <= 0m) return false;
            Status = "Refunded";
            LogTransaction($"Hoàn tiền thành công {amount:N0} VNĐ. Lý do: {reason}");
            return true;
        }
    }
}