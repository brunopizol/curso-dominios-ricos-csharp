using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public class DocumentsTests
{
    //red green refactor
    [TestMethod]
    public void ShouldReturnErrorWhenCNPJIsInvalid()
    {
        var doc = new Document("123", EDocumentType.CNPJ);
        Assert.IsFalse(doc.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenCNPJIsValid()
    {
        var doc = new Document("12345678912345", EDocumentType.CNPJ);
        Assert.IsTrue(doc.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenCPFIsInvalid()
    {
        var doc = new Document("123", EDocumentType.CPF);
        Assert.IsFalse(doc.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenCPFIsValid()
    {
        var doc = new Document("12345678912", EDocumentType.CPF);
        Assert.IsTrue(doc.IsValid);
    }
}
