using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions
        {
            get { return _subscriptions.ToArray(); }
        }

        public void AddSubscripton(Subscription subscription)
        {
            // se ja tiver uma assinatura ativa, cancela

            //  Cancela todas as outras assinaturas, e coloca esta
            //  como principal
            var hasSubscriptionActive = false;
            foreach (var sub in Subscriptions)
            {
                sub.Inactivate();
            }
            AddNotifications(
                new Contract<Student>()
                    .Requires()
                    .IsFalse(
                        hasSubscriptionActive,
                        "Student.Subscriptions",
                        "Voce ja tem uma inscrição cadastrada"
                    )
                    .AreEquals(
                        0,
                        subscription.Payments.Count,
                        "Student.Subscription.Payments",
                        "Esta assinatura nao possui pagamentos"
                    )
            );

            _subscriptions.Add(subscription);
        }
    }
}
