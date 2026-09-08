using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==================================================================");
                Console.WriteLine("          HỆ THỐNG QUẢN LÝ BÀI TẬP C# OOP (C# 7.3)                ");
                Console.WriteLine("==================================================================");
                Console.WriteLine("1. Bài tập 1: Quản lý tài khoản ngân hàng (BankAccount)");
                Console.WriteLine("2. Bài tập 2: Hệ thống phân cấp Nhân viên & Quản lý (Inheritance)");
                Console.WriteLine("3. Bài tập 3: Mô phỏng xử lý đơn hàng & Đa hình (Polymorphism)");
                Console.WriteLine("4. Bài tập 4: Cổng thanh toán đa phương thức (Abstract & Interface)");
                Console.WriteLine("0. Thoát chương trình");
                Console.WriteLine("==================================================================");
                Console.Write("Chọn bài tập thực thi (0-4): ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        RunBaiTap1();
                        break;
                    case "2":
                        RunBaiTap2();
                        break;
                    case "3":
                        RunBaiTap3();
                        break;
                    case "4":
                        RunBaiTap4();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn từ 0 đến 4.");
                        break;
                }

                Console.WriteLine("\nNhấn phím bất kỳ để quay lại menu chính...");
                Console.ReadKey();
            }
        }

        static void RunBaiTap1()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("           BÀI TẬP 1: QUẢN LÝ TÀI KHOẢN NGÂN HÀNG          ");
            Console.WriteLine("==========================================================");
            BankAccount acc1 = new BankAccount("Nguyen Van A", 500_000m);
            BankAccount acc2 = new BankAccount("Tran Thi B", 2_000_000m);

            Console.WriteLine("--- Danh sách tài khoản vừa tạo ---");
            acc1.DisplayInfo();
            acc2.DisplayInfo();

            Console.WriteLine("\n--- Thử tạo tài khoản lỗi (< 50,000 VNĐ) ---");
            try
            {
                BankAccount accInvalid = new BankAccount("Le Van C", 20_000m);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[Bắt lỗi exception]: {ex.Message}");
            }

            Console.WriteLine("\n--- Thực hiện giao dịch Nạp / Rút ---");
            acc1.Deposit(200_000m);
            acc1.Withdraw(100_000m);

            Console.WriteLine("\n--- Thử rút tiền vượt quá hạn mức duy trì 50k ---");
            acc1.Withdraw(600_000m);

            Console.WriteLine("\n--- Trạng thái cuối cùng tài khoản 1 ---");
            acc1.DisplayInfo();
        }

        static void RunBaiTap2()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("       BÀI TẬP 2: HỆ THỐNG PHÂN CẤP NHÂN VIÊN & QUẢN LÝ   ");
            Console.WriteLine("==========================================================");

            int currentYear = DateTime.Now.Year;

            Employee emp = new Employee("EMP001", "Pham Van D", 1998, 12_000_000m);
            Manager mgr = new Manager("MGR001", "Hoang Thi E", 1990, 25_000_000m, 5_000_000m);

            Console.WriteLine("--- PHIẾU LƯƠNG NHÂN VIÊN ---");
            Console.WriteLine($"Mã NV:        {emp.Id}");
            Console.WriteLine($"Họ tên:       {emp.FullName}");
            Console.WriteLine($"Tuổi:         {emp.GetAge(currentYear)}");
            Console.WriteLine($"Thực lĩnh:    {emp.CalculateIncome():N0} VNĐ");

            Console.WriteLine("\n--- PHIẾU LƯƠNG QUẢN LÝ ---");
            Console.WriteLine($"Mã QL:        {mgr.Id}");
            Console.WriteLine($"Họ tên:       {mgr.FullName}");
            Console.WriteLine($"Tuổi:         {mgr.GetAge(currentYear)}");
            Console.WriteLine($"Lương cơ bản: {mgr.BaseSalary:N0} VNĐ");
            Console.WriteLine($"Phụ cấp TN:   {mgr.ResponsibilityAllowance:N0} VNĐ");
            Console.WriteLine($"Thực lĩnh:    {mgr.CalculateIncome():N0} VNĐ");
        }

        static void RunBaiTap3()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("         BÀI TẬP 3: XỬ LÝ ĐƠN HÀNG VÀ TÍNH ĐA HÌNH        ");
            Console.WriteLine("==========================================================");

            Console.WriteLine("1. Nạp chồng phương thức (Method Overloading):");
            DiscountCalculator calc = new DiscountCalculator();
            decimal orderTotal = 1_000_000m;

            Console.WriteLine($"Đơn gốc:                     {orderTotal:N0} VNĐ");
            Console.WriteLine($"• Giảm 5% mặc định:         {calc.ApplyDiscount(orderTotal):N0} VNĐ");
            Console.WriteLine($"• Giảm 15% tùy chỉnh:       {calc.ApplyDiscount(orderTotal, 15.0):N0} VNĐ");
            Console.WriteLine($"• Voucher 100k (Đơn > 500k): {calc.ApplyDiscount(orderTotal, 100_000m, 500_000m):N0} VNĐ");

            Console.WriteLine("\n2. Ghi đè phương thức (Runtime Polymorphism):");
            List<DeliveryService> deliveries = new List<DeliveryService>
            {
                new DeliveryService("ORD_BASE", 12.5),
                new ExpressDelivery("ORD_EXPRESS", 12.5),
                new EcoDelivery("ORD_ECO_SHORT", 8.0),
                new EcoDelivery("ORD_ECO_LONG", 12.5)
            };

            foreach (var delivery in deliveries)
            {
                Console.WriteLine($"• Đơn [{delivery.OrderId}] - Quãng đường: {delivery.DistanceKm}km " +
                                  $"| Loai: {delivery.GetType().Name,-15} " +
                                  $"| Phí ship: {delivery.CalculateShippingFee():N0} VNĐ");
            }
        }

        static void RunBaiTap4()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("        BÀI TẬP 4: CỔNG THANH TOÁN ĐA PHƯƠNG THỨC         ");
            Console.WriteLine("==========================================================");

            MomoPayment momoApp = new MomoPayment("TXN_998877", "0987654321");
            momoApp.ValidateConnection();
            Console.WriteLine();

            IPayable paymentProcessor = momoApp;
            Console.WriteLine("--- Thực hiện thanh toán qua IPayable ---");
            paymentProcessor.ProcessPayment(450_000m);

            Console.WriteLine();

            IRefundable refundProcessor = momoApp;
            Console.WriteLine("--- Thực hiện hoàn tiền qua IRefundable ---");
            refundProcessor.ProcessRefund(150_000m, "Khách trả lại sản phẩm lỗi");
        }
    }
}