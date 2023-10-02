using Banking.Domain;

namespace Banking.UnitTests.BankAccountTests;
public class NewAccount
{
    private Account _account;

    public NewAccount()
    {
        var testHelper = new BankAccountTestsHelpers();
        _account = testHelper.CreateTestAccount();
    }

    [Fact]
    public void NewAccountHasCorrectOpeningBalance()
    {
        // Given I have a new account
        // When I ask that account for the balance
        decimal balance = _account.GetBalance();

        // Then I am given the appropriate balance.
        Assert.Equal(5000M, balance);
    }
}