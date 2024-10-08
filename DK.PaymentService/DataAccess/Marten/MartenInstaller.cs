using Marten.Services;
using Marten;
using DK.PaymentService.Domain.Contracts;
using DK.PaymentService.Domain;
using Newtonsoft.Json;

namespace DK.PaymentService.DataAccess.Marten;

public static class MartenInstaller
{
    public static void AddMarten(this IServiceCollection services, string cnnString)
    {
        services.AddSingleton(CreateDocumentStore(cnnString));

        services.AddScoped<IDataStore, MartenDataStore>();
    }

    private static IDocumentStore CreateDocumentStore(string cn)
    {
        return DocumentStore.For(_ =>
        {
            _.Connection(cn);
            _.DatabaseSchemaName = "payment_service";
            _.Serializer(CustomizeJsonSerializer());
            _.Schema.For<PolicyAccount>()
                .Duplicate(t => t.PolicyNumber, "varchar(50)", configure: idx => idx.IsUnique = true);
        });
    }

    private static JsonNetSerializer CustomizeJsonSerializer()
    {
        var serializer = new JsonNetSerializer();

        serializer.Customize(_ =>
        {
            _.ContractResolver = new ProtectedSettersContractResolver();
            _.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        });

        return serializer;
    }
}
