

namespace WebApplication2;

public class PaymentSchedule
{
    public IReadOnlyList<Payment> Payments { get; }
    public decimal TotalValueOfAllOverpaymentsOnTheLoan { get; }

    public static PaymentSchedule Calculate(CalculatorForm calculatorForm)
    {

    }
}

public class Payment
{
    public int Number { get; }
    public DateTime Date { get; }
    public decimal PaymentAmountByBody { get; set; }
    public decimal InterestPaymentAmount { get; set; }
    public string PrincipalBalance { get; set; }
}