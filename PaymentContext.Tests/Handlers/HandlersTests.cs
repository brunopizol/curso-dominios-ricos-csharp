using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using PaymentContext.Domain.Command;

namespace PaymentContext.Tests;

[TestClass]
public class HandlersTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Email _email;
    private readonly Subscription _subscription;

    public HandlersTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("12345678901", EDocumentType.CPF);
        _address = new Address("rua 1", "1234", "Bairro 3", "Gotham", "SP", "BR", "12345678");
        _email = new Email("batman@dc.com");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenDocumentsExists()
    {
        var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
        var command = new CreateBoletoSubscriptionCommand();
        command.BarCode="123456789";
        command.BoletoNumber="1234654987";
        command.City="as";
        command.Country="as";
        command.Document="99999999999";
        command.Email="hello@balta.io2";
        command.ExpireDate= DateTime.Now.AddMonths(1);
        command.FirstName="Bruce";
        command.LastName="Wayne";
        command.Neighborhood="";
        command.Number="dasd";
        command.PaidDate= DateTime.Now;
        command.Payer="WAYNE CORP";
        command.PayerDocument="12345678911";
        command.PayerDocumentType = EDocumentType.CPF;
        command.PayerEmail="batman@dc.com";
        command.PaymentNumber="123121";
        command.State="as";
        command.Street="adasda";
        command.Total=60;
        command.TotalPaid=60;
        command.ZipCode="12345678";

        handler.handle(command);
        Assert.AreEqual(false,command.IsValid);


    }

    
}
