namespace KiemTra01;

[Flags]
public enum LicenseType { None = 0, Motorcycle = 1, Car = 2 }

public abstract class Customer
{
    private string _id = "";
    private string _name = "";
    private string _phone = "";
    private LicenseType _licenses;

    protected Customer(string id, string name, string phone, LicenseType licenses)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Licenses = licenses;
    }

    public string Id
    {
        get => _id;
        set => _id = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Customer ID is required.");
    }
    public string Name
    {
        get => _name;
        set => _name = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Customer name is required.");
    }
    public string Phone
    {
        get => _phone;
        set => _phone = value is { Length: 10 } && value.All(c => c is >= '0' and <= '9')
            ? value : throw new ArgumentException("Phone must contain 10 digits.");
    }
    public LicenseType Licenses
    {
        get => _licenses;
        set => _licenses = (value & ~(LicenseType.Motorcycle | LicenseType.Car)) == 0
            ? value : throw new ArgumentOutOfRangeException(nameof(value));
    }

    public bool HasLicense(LicenseType required) => (Licenses & required) == required;
    public abstract decimal CalculateAmount(decimal rentalFee);
}

public class GuestCustomer : Customer
{
    public GuestCustomer(string id, string name, string phone, LicenseType licenses) : base(id, name, phone, licenses) { }
    public override decimal CalculateAmount(decimal rentalFee) => rentalFee;
}

public class SilverCustomer : Customer
{
    public SilverCustomer(string id, string name, string phone, LicenseType licenses) : base(id, name, phone, licenses) { }
    public override decimal CalculateAmount(decimal rentalFee) => rentalFee * 0.95m;
}

public class GoldCustomer : Customer
{
    public GoldCustomer(string id, string name, string phone, LicenseType licenses) : base(id, name, phone, licenses) { }
    public override decimal CalculateAmount(decimal rentalFee) => rentalFee * 0.90m;
}
