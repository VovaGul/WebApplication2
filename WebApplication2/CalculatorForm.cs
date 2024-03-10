using System.ComponentModel.DataAnnotations;

namespace WebApplication2;

public class CalculatorForm
{
    [Required]
    [Range(0.0, double.MaxValue)]
    public double CreditAmount { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int CreditTermInMonths { get; set; }

    [Required] 
    [Range(0.0, double.MaxValue)] 
    public double YearCreditRate { get; set; }
}