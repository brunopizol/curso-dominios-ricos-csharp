using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Email _email;
    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("12345678901", EDocumentType.CPF);
        _address = new Address("rua 1", "1234", "Bairro 3", "Gotham", "SP", "BR", "12345678");
        _email = new Email("batman@dc.com");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment(
            "12345678",
            DateTime.Now,
            DateTime.Now.AddDays(5),
            10,
            10,
            "wayne corp",
            _document,
            _address,
            _email
        );
        _subscription.AddPayment(payment);
        _student.AddSubscripton(_subscription);
        _student.AddSubscripton(_subscription);
        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
    {
        _student.AddSubscripton(_subscription);
        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenAddSubscription()
    {
        
        var payment = new PayPalPayment(
            "12345678",
            DateTime.Now,
            DateTime.Now.AddDays(5),
            10,
            10,
            "wayne corp",
            _document,
            _address,
            _email
        );
        _subscription.AddPayment(payment);
        _student.AddSubscripton(_subscription);
        _student.AddSubscripton(_subscription);
        Assert.IsTrue(_student.IsValid);
    }
}
