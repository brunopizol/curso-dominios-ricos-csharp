using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Command
{
    public class CreateCreditCardSubscriptionCommand : Notifiable<Notification>, ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PaymentNumber { get; set; }
        public string Document { get; set; }
        public string CardHolderName { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactionNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public EDocumentType PayerDocumentType { get; set; }
        public string PayerEmail { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Name>()
                    .Requires()
                    .IsGreaterThan(
                        FirstName,
                        2,
                        "Name.FirstName",
                        "Nome deve conter pelo menos 3 caracteres"
                    )
                    .IsGreaterThan(
                        LastName,
                        2,
                        "Name.LastName",
                        "Nome deve conter pelo menos 3 caracteres"
                    )
                    .IsLowerThan(
                        FirstName,
                        40,
                        "Name.FirstName",
                        "Nome deve conter ate 40 caracteres"
                    )
                    .IsLowerThan(
                        LastName,
                        40,
                        "Name.LastName",
                        "Nome deve conter ate 40 caracteres"
                    )
            );
        }
    }
}
