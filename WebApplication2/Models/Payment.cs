namespace WebApplication2.Models;

public class Payment
{
    private static readonly int CountOfMonthsInAYear = 12;
    public static readonly int PaymentPeriodInMonths = 1;
    public static readonly int NumberOfSimbolsAfterComma = 2;

    public int Number { get; }
    public DateTime Date { get; }
    public double Amount { get; }
    public double AmountByBody { get; }
    public double AmountByInterest { get; }
    public double ResidueCreditAmount { get; }

    public Payment(int number, DateTime date, double amount, double amountByBody, double amountByInterest, double residueCreditAmount)
    {
        Number = number;
        Date = date;
        Amount = Math.Round(amount, NumberOfSimbolsAfterComma);
        AmountByBody = Math.Round(amountByBody, NumberOfSimbolsAfterComma);
        AmountByInterest = Math.Round(amountByInterest, NumberOfSimbolsAfterComma);
        ResidueCreditAmount = Math.Round(residueCreditAmount, NumberOfSimbolsAfterComma);
    }

    public static Payment CreatePayment(int paymentNumber, CalculatorForm calculatorForm, DateTime today, IReadOnlyList<Payment> payments)
    {
        var paymentDate = GetPaymentDate(today, paymentNumber);
        var paymentAmount = GetPaymentAmount(calculatorForm);
        var paymentAmountByBody = GetPaymentAmountByBody(payments, calculatorForm);
        var paymentAmountByInterest = GetPaymentAmountByInterest(payments, calculatorForm);
        var paymentResidueCreditAmount = GetPaymentResidueCreditAmount(payments, calculatorForm);
        var payment = new Payment(
            paymentNumber,
            paymentDate,
            paymentAmount,
            paymentAmountByBody,
            paymentAmountByInterest,
            paymentResidueCreditAmount);
        return payment;
    }
    private static double GetPaymentResidueCreditAmount(IReadOnlyList<Payment> payments, CalculatorForm calculatorForm)
    {
        var creditAmountForThePeriod = ResidueCreditAmountForThePeriod(payments, calculatorForm);
        var paymentAmountByBody = GetPaymentAmountByBody(payments, calculatorForm);
        return creditAmountForThePeriod - paymentAmountByBody;
    }

    private static double GetPaymentAmountByBody(IReadOnlyList<Payment> payments, CalculatorForm calculatorForm)
    {
        var paymentAmount = GetPaymentAmount(calculatorForm);
        var paymentAmountByInterest = GetPaymentAmountByInterest(payments, calculatorForm);
        return paymentAmount - paymentAmountByInterest;
    }

    private static double GetPaymentAmount(CalculatorForm calculatorForm)
    {
        var annuityCoefficient = GetAnnuityCoefficient(calculatorForm);
        return annuityCoefficient * calculatorForm.CreditAmount;
    }

    private static DateTime GetPaymentDate(DateTime today, int paymentNumber)
    {
        return today.AddMonths(PaymentPeriodInMonths * paymentNumber);
    }

    private static double GetPaymentAmountByInterest(IReadOnlyList<Payment> payments, CalculatorForm calculatorForm)
    {
        var creditAmountForThePeriod = ResidueCreditAmountForThePeriod(payments, calculatorForm);
        var monthCreditRate = GetMonthCreditRate(calculatorForm);
        return creditAmountForThePeriod * monthCreditRate;
    }

    private static double ResidueCreditAmountForThePeriod(IReadOnlyList<Payment> payments, CalculatorForm calculatorForm)
    {
        return payments.Count > 0
            ? payments[^1].ResidueCreditAmount
            : calculatorForm.CreditAmount;
    }

    private static double GetAnnuityCoefficient(CalculatorForm calculatorForm)
    {
        var monthCreditRate = GetMonthCreditRate(calculatorForm);
        var countOfPayments = GetCountOfPayments(calculatorForm);

        return
            monthCreditRate * Math.Pow(1 + monthCreditRate, countOfPayments)
            /
            (Math.Pow(1 + monthCreditRate, countOfPayments) - 1);
    }

    private static double GetMonthCreditRate(CalculatorForm calculatorForm)
    {
        return calculatorForm.YearCreditRate / CountOfMonthsInAYear;
    }

    private static int GetCountOfPayments(CalculatorForm calculatorForm)
    {
        return calculatorForm.CreditTermInMonths / PaymentPeriodInMonths;
    }
}