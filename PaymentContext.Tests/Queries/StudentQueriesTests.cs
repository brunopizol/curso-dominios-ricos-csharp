using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using PaymentContext.Domain.Command;
using PaymentContext.Shared.ValueObjects;
using PaymentContext.Domain.Queries;

namespace PaymentContext.Domain.ValueObjects;

[TestClass]
public class StudentQueriesTests
{
    private IList<Student> _students;
    public StudentQueriesTests()    
    {
        for(var i=0; i<=10; i++){
            _students.Add(new Student(
                new Name("Aluno", i.ToString()),
                new Document("11111111111"+ i.ToString(),EDocumentType.CPF),
                new Email(i.ToString()+ "@balta.io")
            ));
        }
    }

    [TestMethod]
    public void ShouldReturnNullWhenDocumentNotExists()
    {
        var exp = StudentQueries.GetStudentInfo("12345678911");
        var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

        Assert.AreEqual(null, studn);


    }

    [TestMethod]
    public void ShouldReturnStudentWhenDocumentExists()
    {
        var exp = StudentQueries.GetStudentInfo("11111111111");
        var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

        Assert.AreEqual(null, studn);


    }

    
}
