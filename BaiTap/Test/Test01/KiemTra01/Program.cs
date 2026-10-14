using KiemTra01;
using System.Globalization;

CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");

var guest = new GuestCustomer("C01", "Guest Customer", "0900000001", LicenseType.None);
var motorcycleGuest = new GuestCustomer("C02", "Motorcycle Customer", "0900000002", LicenseType.Motorcycle);
var carGuest = new GuestCustomer("C03", "Car Customer", "0900000003", LicenseType.Car);
var goldCustomer = new GoldCustomer("C04", "Gold Customer", "0900000004", LicenseType.Motorcycle);
var silverCustomer = new SilverCustomer("C05", "Silver Customer", "0900000005", LicenseType.Car);
var start = new DateTime(2026, 10, 1, 8, 0, 0);

RunFeeCase("TC01", 45_000, guest, new Bicycle("B01", "Giant", 2024), start.AddHours(3), start.AddHours(3));
RunFeeCase("TC02", 145_000, guest, new Bicycle("B02", "Giant", 2024), start.AddHours(27), start.AddHours(27));
RunFeeCase("TC03", 390_000, motorcycleGuest, new Motorcycle("M01", "Honda", 2024, "66-AA-001"), start.AddDays(2), start.AddDays(2).AddHours(2).AddMinutes(10));
RunFeeCase("TC04", 1_100_000, carGuest, new ElectricCar("E01", "VinFast", 2025, "66A-001"), start.AddDays(1), start.AddDays(1), 350);
RunFeeCase("TC05", 351_000, goldCustomer, new Motorcycle("M02", "Honda", 2024, "66-AA-002"), start.AddDays(2), start.AddDays(2).AddHours(2).AddMinutes(10));
RunFeeCase("TC06", 1_520_000, silverCustomer, new ElectricCar("E02", "VinFast", 2025, "66A-002"), start.AddDays(2), start.AddDays(2), 380);
RunRejectedCase("TC07", RentalError.License, motorcycleGuest, new ElectricCar("E03", "VinFast", 2025, "66A-003"));
RunRejectedCase("TC08", RentalError.Status, guest, new Bicycle("B03", "Giant", 2024) { Status = VehicleStatus.Maintenance });

void RunFeeCase(string code, decimal expected, Customer customer, Vehicle vehicle, DateTime due, DateTime returned, int kilometers = 0)
{
    try
    {
        var ticket = RentalTicket.Create(code, customer, vehicle, start, due);
        var actual = ticket.CalculateAmount(returned, kilometers);
        Console.WriteLine($"{code} | Actual: {actual:N0} đ | Expected: {expected:N0} đ | {(actual == expected ? "ĐẠT" : "KHÔNG ĐẠT")}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{code} | Actual: {ex.Message} | Expected: {expected:N0} đ | KHÔNG ĐẠT");
    }
}

void RunRejectedCase(string code, RentalError expected, Customer customer, Vehicle vehicle)
{
    try
    {
        RentalTicket.Create(code, customer, vehicle, start, start.AddDays(1));
        Console.WriteLine($"{code} | Actual: accepted | Expected: rejected ({expected}) | KHÔNG ĐẠT");
    }
    catch (RentalNotAllowedException ex)
    {
        Console.WriteLine($"{code} | Actual: rejected ({ex.Reason}: {ex.Message}) | Expected: rejected ({expected}) | {(ex.Reason == expected ? "ĐẠT" : "KHÔNG ĐẠT")}");
    }
}
