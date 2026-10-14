namespace KiemTra01;

public enum RentalError { Status, License }

public class RentalNotAllowedException : Exception
{
    public RentalError Reason { get; }
    public RentalNotAllowedException(RentalError reason, string message) : base(message) => Reason = reason;
}

public class RentalTicket
{
    private RentalTicket(string id, Customer customer, Vehicle vehicle, DateTime pickup, DateTime due)
    {
        Id = id;
        Customer = customer;
        Vehicle = vehicle;
        Pickup = pickup;
        Due = due;
    }

    public string Id { get; }
    public Customer Customer { get; }
    public Vehicle Vehicle { get; }
    public DateTime Pickup { get; }
    public DateTime Due { get; }

    public static RentalTicket Create(string id, Customer customer, Vehicle vehicle, DateTime pickup, DateTime due)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Ticket ID is required.");
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(vehicle);
        if (due <= pickup) throw new ArgumentException("Due time must follow pickup time.");
        if (vehicle.Status != VehicleStatus.Available)
            throw new RentalNotAllowedException(RentalError.Status, "Vehicle status is not available.");
        if (!customer.HasLicense(vehicle.RequiredLicense))
            throw new RentalNotAllowedException(RentalError.License, "Driver's license (GPLX) is not suitable.");

        vehicle.Status = VehicleStatus.Rented;
        return new RentalTicket(id, customer, vehicle, pickup, due);
    }

    public decimal CalculateAmount(DateTime returned, int kilometers = 0)
        => Customer.CalculateAmount(Vehicle.CalculateFee(Pickup, Due, returned, kilometers));
}
