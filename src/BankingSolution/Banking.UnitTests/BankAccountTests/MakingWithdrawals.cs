using Banking.Domain;

namespace Banking.UnitTests.BankAccountTests;
public class MakingWithdrawls
{
    private readonly Account _account;

    public MakingWithdrawls()
    {
        var testHelper = new BankAccountTestsHelpers();
        _account = testHelper.CreateTestAccount();
    }

    [Fact]
    public void MakingAWithdrawalDecreasesBalance()
    {
        var openingBalance = _account.GetBalance();
        var amountToWithdraw = 100M;

        // When
        _account.Withdraw(amountToWithdraw);

        // Then
        Assert.Equal(openingBalance - amountToWithdraw, _account.GetBalance());
    }

    [Fact]
    public void CanTakeFullBalance()
    {
        _account.Withdraw(_account.GetBalance());

        Assert.Equal(0, _account.GetBalance());
    }

    [Fact]
    public void OverdraftNotAllowed()
    {
        var openingBalance = _account.GetBalance();
        var amountToWithdraw = _account.GetBalance() + .01M;


        Assert.Throws<OverdraftException>(() => _account.Withdraw(amountToWithdraw));


        // Then
        Assert.Equal(openingBalance, _account.GetBalance());

    }
}