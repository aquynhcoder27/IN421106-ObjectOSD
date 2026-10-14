namespace KiemTra01;

public enum VehicleStatus { Available, Rented, Maintenance }

public interface IRentalFee
{
    decimal CalculateFee(DateTime pickup, DateTime due, DateTime returned, int kilometers);
}

public abstract class Vehicle : IRentalFee
{
    private string _id = "";
    private string _manufacturer = "";
    private int _year;
    private VehicleStatus _status;

    protected Vehicle(string id, string manufacturer, int year)
    {
        Id = id;
        Manufacturer = manufacturer;
        Year = year;
        Status = VehicleStatus.Available;
    }

    public string Id
    {
        get => _id;
        set => _id = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Vehicle ID is required.");
    }
    public string Manufacturer
    {
        get => _manufacturer;
        set => _manufacturer = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Manufacturer is required.");
    }
    public int Year
    {
        get => _year;
        set => _year = value is >= 1886 and <= 2100 ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }
    public VehicleStatus Status
    {
        get => _status;
        set => _status = Enum.IsDefined(value) ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public abstract LicenseType RequiredLicense { get; }
    public abstract decimal CalculateFee(DateTime pickup, DateTime due, DateTime returned, int kilometers);
    protected static int PlannedDays(DateTime pickup, DateTime due)
    {
        if (due <= pickup) throw new ArgumentException("Due time must follow pickup time.");
        var ticks = (due - pickup).Ticks;
        return checked((int)((ticks + TimeSpan.TicksPerDay - 1) / TimeSpan.TicksPerDay));
    }
}

public class Bicycle : Vehicle
{
    public Bicycle(string id, string manufacturer, int year) : base(id, manufacturer, year) { }
    public override LicenseType RequiredLicense => LicenseType.None;

    public override decimal CalculateFee(DateTime pickup, DateTime due, DateTime returned, int kilometers)
    {
        if (returned < pickup) throw new ArgumentException("Return time must follow pickup time.");
        // Mỗi khoảng đủ 24 giờ tính tối đa 100.000 đồng; phần còn lại tính theo giờ.
        var ticks = (returned - pickup).Ticks;
        var fullDays = ticks / TimeSpan.TicksPerDay;
        var remainingTicks = ticks % TimeSpan.TicksPerDay;
        var remainingHours = (remainingTicks + TimeSpan.TicksPerHour - 1) / TimeSpan.TicksPerHour;
        return fullDays * 100_000m + Math.Min(remainingHours * 15_000m, 100_000m);
    }
}

public class Motorcycle : Vehicle
{
    private string _licensePlate = "";
    public Motorcycle(string id, string manufacturer, int year, string licensePlate) : base(id, manufacturer, year)
        => LicensePlate = licensePlate;
    public string LicensePlate
    {
        get => _licensePlate;
        set => _licensePlate = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("License plate is required.");
    }
    public override LicenseType RequiredLicense => LicenseType.Motorcycle;

    public override decimal CalculateFee(DateTime pickup, DateTime due, DateTime returned, int kilometers)
    {
        if (returned < pickup) throw new ArgumentException("Return time must follow pickup time.");
        var lateTicks = Math.Max(0, (returned - due).Ticks);
        var lateHours = (lateTicks + TimeSpan.TicksPerHour - 1) / TimeSpan.TicksPerHour;
        return PlannedDays(pickup, due) * 150_000m + lateHours * 30_000m;
    }
}

public class ElectricCar : Vehicle
{
    private string _licensePlate = "";
    public ElectricCar(string id, string manufacturer, int year, string licensePlate) : base(id, manufacturer, year)
        => LicensePlate = licensePlate;
    public string LicensePlate
    {
        get => _licensePlate;
        set => _licensePlate = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("License plate is required.");
    }
    public override LicenseType RequiredLicense => LicenseType.Car;

    public override decimal CalculateFee(DateTime pickup, DateTime due, DateTime returned, int kilometers)
    {
        if (returned < pickup) throw new ArgumentException("Return time must follow pickup time.");
        if (kilometers < 0) throw new ArgumentOutOfRangeException(nameof(kilometers));
        var days = PlannedDays(pickup, due);
        var extraKilometers = Math.Max(0, kilometers - days * 200);
        return days * 800_000m + extraKilometers * 2_000m;
    }
}
