using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Name>()
                        .Requires()
                        .IsGreaterThan(FirstName,2,"Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
                        .IsGreaterThan(LastName,2,"Name.LastName", "Nome deve conter pelo menos 3 caracteres")
                        .IsLowerThan(FirstName,40,"Name.FirstName", "Nome deve conter ate 40 caracteres")
                        .IsLowerThan(LastName,40,"Name.LastName", "Nome deve conter ate 40 caracteres")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
