using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resorts.Domain.Entities;

public class Booking
{
    [Key]
    public int Id { get; set; }

    public required string UserId { get; set; }

    [ForeignKey("UserId")]
    public ApplicationUser? User { get; set; }

    public required int VillaId { get; set; }

    [ForeignKey("VillaId")]
    public Villa? Villa { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public string? Phone { get; set; }

    public required double TotalCost { get; set; }

    public int Nights { get; set; }

    public string? Status { get; set; }

    public required DateTime BookingDate { get; set; }

    public required DateOnly CheckInDate { get; set; }

    public required DateOnly CheckOutDate { get; set; }

    public bool IsPaymentSuccessful { get; set; } = false;

    public DateTime PaymentDate { get; set; }

    public string? StripeSessionId { get; set; }

    public string? StripePaymentIntentId { get; set; }

    public DateTime ActualCheckInDate { get; set; }

    public DateTime ActualCheckOutDate { get; set; }

    public int VillaNumber { get; set; }

    [NotMapped]
    public List<VillaNumber>? VillaNumbers { get; set; }
}