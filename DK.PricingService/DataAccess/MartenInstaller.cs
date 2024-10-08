using DK.PricingService.Domain.Contracts;
using Marten.Services;
using Marten;
using DK.PricingService.Domain;
using Weasel.Core;

namespace DK.PricingService.DataAccess;

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
            _.DatabaseSchemaName = "policy_service";
            _.Serializer(CustomizeJsonSerializer());
            _.AutoCreateSchemaObjects = AutoCreate.All;

            _.Schema.For<Tariff>().Duplicate(t => t.Code, "varchar(50)", configure: idx => idx.IsUnique = true);
        });
    }

    private static JsonNetSerializer CustomizeJsonSerializer()
    {
        var serializer = new JsonNetSerializer();

        serializer.Customize(_ => { _.ContractResolver = new ProtectedSettersContractResolver(); });

        return serializer;
    }
}
