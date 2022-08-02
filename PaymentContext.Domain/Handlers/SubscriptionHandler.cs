using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Command;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler
        : Notifiable<Notification>,
            IHandler<CreateBoletoSubscriptionCommand>,
            IHandler<CreatePayPalSubscriptionCommand>,
            IHandler<CreateCreditCardSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail fast validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "nao foi possivel realizar sua assinatura");
            }

            // Verificar se Documento ja esta cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotifications(new Contract<Document>());

            // Verificar se E-mail ja esta cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotifications(new Contract<Document>());

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode
            );
            var email = new Email(command.Email);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                document,
                address,
                email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscripton(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificacoes
            if (!IsValid)
                return new CommandResult(false, "nao foi possivel realizar sua assinatura");

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(
                student.Name.ToString(),
                student.Email.Address,
                "Bem vindo ao balta.io",
                "Sua assinatura foi criada"
            );

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult handle(CreatePayPalSubscriptionCommand command)
        {
            // Fail fast validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "nao foi possivel realizar sua assinatura");
            }

            // Verificar se Documento ja esta cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotifications(new Contract<Document>());

            // Verificar se E-mail ja esta cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotifications(new Contract<Document>());

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode
            );
            var email = new Email(command.Email);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(
                command.TransactionCode,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                document,
                address,
                email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscripton(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificacoes
            if (!IsValid)
                return new CommandResult(false, "nao foi possivel realizar sua assinatura");

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(
                student.Name.ToString(),
                student.Email.Address,
                "Bem vindo ao balta.io",
                "Sua assinatura foi criada"
            );

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }

        public ICommandResult handle(CreateCreditCardSubscriptionCommand command)
        {
            // Fail fast validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "nao foi possivel realizar sua assinatura");
            }

            // Verificar se Documento ja esta cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotifications(new Contract<Document>());

            // Verificar se E-mail ja esta cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotifications(new Contract<Document>());

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var address = new Address(
                command.Street,
                command.Number,
                command.Neighborhood,
                command.City,
                command.State,
                command.Country,
                command.ZipCode
            );
            var email = new Email(command.Email);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                command.CardHolderName,
                command.CardNumber,
                command.LastTransactionNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                document,
                address,
                email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscripton(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificacoes
            if (!IsValid)
                return new CommandResult(false, "nao foi possivel realizar sua assinatura");

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(
                student.Name.ToString(),
                student.Email.Address,
                "Bem vindo ao balta.io",
                "Sua assinatura foi criada"
            );

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}
