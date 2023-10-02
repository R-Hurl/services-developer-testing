using Banking.Domain;

namespace Banking.UnitTests.BankAccountTests;
public class OverdraftNotifiesFraudDetection
{
    private readonly INotifyOfFraudDetection _fraudDetectorStub;
    private readonly Account _account;

    public OverdraftNotifiesFraudDetection()
    {
        var testsHelpers = new BankAccountTestsHelpers();
        _account = testsHelpers.CreateTestAccount();
        _fraudDetectorStub = testsHelpers.FraudDetectorStub;
    }

    [Fact]
    public void NofifiedOnOverdraft()
    {
        var openingBalance = _account.GetBalance();
        var amountToWithDraw = openingBalance + 10m;

        try
        {
            _account.Withdraw(amountToWithDraw);
        }
        catch (OverdraftException)
        {
            // Swallow. Don't care
        }

        _fraudDetectorStub.Received().NotifyOfOverdraft(amountToWithDraw);
    }

    [Fact]
    public void NotNotifiedIfNoOverdraft()
    {
        var openingBalance = _account.GetBalance();
        var amountToWithDraw = openingBalance;

        _account.Withdraw(amountToWithDraw);

        _fraudDetectorStub.DidNotReceive().NotifyOfOverdraft(Arg.Any<decimal>());
    }
}
