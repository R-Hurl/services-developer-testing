using Banking.Domain;

namespace Banking.UnitTests.BankAccountTests;
public class MakingDeposits
{
    private readonly Account _account;

    public MakingDeposits()
    {
        var testHelper = new BankAccountTestsHelpers();
        _account = testHelper.CreateTestAccount();
    }

    [Fact]
    public void MakingADepositIncreasesTheBalance()
    {
        // Given
        var openingBalance = _account.GetBalance();
        var amountToDeposit = 100M;

        // When
        _account.Deposit(amountToDeposit);

        // Then
        Assert.Equal(openingBalance + amountToDeposit, _account.GetBalance());
    }
}