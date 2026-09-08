using System;

namespace OOP_Practice
{
    public class BankAccount
    {
        private static long _nextAccountNumber = 1000000001;
        private decimal _balance;
        private string _accountHolder;
        public long AccountNumber { get; private set; }

        public string AccountHolder
        {
            get => _accountHolder;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên chủ tài khoản không được để trống hoặc null.");
                _accountHolder = value;
            }
        }

        public decimal Balance => _balance;

        public BankAccount(string accountHolder, decimal initialBalance)
        {
            // C# 7.3: Toán tử so sánh tiêu chuẩn
            if (initialBalance < 50_000m)
                throw new ArgumentException("Số dư ban đầu tối thiểu phải từ 50,000 VNĐ.");

            AccountHolder = accountHolder;
            _balance = initialBalance;
            AccountNumber = _nextAccountNumber++;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0m)
                throw new ArgumentException("Số tiền nạp phải lớn hơn 0 VNĐ.");

            _balance += amount;
            Console.WriteLine($"[+] Nạp thành công {amount:N0} VNĐ vào STK {AccountNumber}. Số dư mới: {Balance:N0} VNĐ");
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0m)
            {
                Console.WriteLine("[-] Rút tiền thất bại: Số tiền rút phải lớn hơn 0 VNĐ.");
                return false;
            }

            const decimal MIN_BALANCE = 50_000m;
            if (_balance - amount < MIN_BALANCE)
            {
                Console.WriteLine($"[-] Rút tiền thất bại: Số dư còn lại sau khi rút không được dưới hạn mức duy trì {MIN_BALANCE:N0} VNĐ.");
                return false;
            }

            _balance -= amount;
            Console.WriteLine($"[-] Rút thành công {amount:N0} VNĐ từ STK {AccountNumber}. Số dư còn lại: {Balance:N0} VNĐ");
            return true;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"• STK: {AccountNumber} | Chủ TK: {AccountHolder} | Số dư: {Balance:N0} VNĐ");
        }
    }
}