using Banking.Domain;

namespace Banking.UnitTests.BankAccountTests;
internal class BankAccountTestsHelpers
{
    private readonly ICalculateBonuses _bonusCalculatorStub;
    private readonly INotifyOfFraudDetection _fraudDetectorStub;

    public BankAccountTestsHelpers()
    {
        _bonusCalculatorStub = Substitute.For<ICalculateBonuses>();
        _fraudDetectorStub = Substitute.For<INotifyOfFraudDetection>();
    }

    public ICalculateBonuses BonusCalculatorStub => _bonusCalculatorStub;
    public INotifyOfFraudDetection FraudDetectorStub => _fraudDetectorStub;
    public Account CreateTestAccount() => new(_bonusCalculatorStub, _fraudDetectorStub);
}
