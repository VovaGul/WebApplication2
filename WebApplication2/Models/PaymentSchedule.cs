namespace WebApplication2.Models;

public class PaymentSchedule
{
    private static readonly int FirstPaymentNumber = 1;

    public IReadOnlyList<Payment> Payments { get; }
    public double TotalAmountOfAllOverpaymentsOnTheCredit { get; }

    public PaymentSchedule(IReadOnlyList<Payment> payments)
    {
        Payments = payments;
        TotalAmountOfAllOverpaymentsOnTheCredit = Math.Round(payments.Sum(payment => payment.AmountByInterest), Payment.NumberOfSimbolsAfterComma);
    }

    public static PaymentSchedule Calculate(CalculatorForm calculatorForm, DateTime today)
    {
        var payments = new List<Payment>();
        var countOfPayments = GetCountOfPayments(calculatorForm);
        for (var paymentNumber = FirstPaymentNumber; paymentNumber <= countOfPayments; paymentNumber++)
        {
            var payment = Payment.CreatePayment(paymentNumber, calculatorForm, today, payments);
            payments.Add(payment);
        }

        return new PaymentSchedule(payments);
    }

    private static int GetCountOfPayments(CalculatorForm calculatorForm)
    {
        return calculatorForm.CreditTermInMonths / Payment.PaymentPeriodInMonths;
    }
}