using Banking.Domain;

namespace Banking.UnitTests.BankAccountTests;
public class BankAccountUsesBonusCalculator
{
    private readonly ICalculateBonuses _bonusCalculatorStub;
    private readonly Account _account;

    public BankAccountUsesBonusCalculator()
    {
        var testsHelpers = new BankAccountTestsHelpers();
        _account = testsHelpers.CreateTestAccount();
        _bonusCalculatorStub = testsHelpers.BonusCalculatorStub;
    }

    [Fact]
    public void BonusCalculatorUsedInDeposit()
    {
        var openingBalance = _account.GetBalance();
        var amountToDeposit = 100M;
        decimal bonusAmount = 42.23M;
        _bonusCalculatorStub.GetBonusForDepositOn(openingBalance, amountToDeposit).Returns(bonusAmount);

        // When
        _account.Deposit(amountToDeposit);

        Assert.Equal(amountToDeposit + openingBalance + bonusAmount, _account.GetBalance());
    }
}
