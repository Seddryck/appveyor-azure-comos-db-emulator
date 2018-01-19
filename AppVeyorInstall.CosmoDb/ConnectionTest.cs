using NUnit.Framework;
using Microsoft.Azure.Documents;
using System;
using Microsoft.Azure.Documents.Client;
using System.Net;

namespace AppVeyor_CosmosDB
{
    public class ConnectionTest
    {
        private const string EndpointUrl = "https://localhost:8081";
        private const string AuthKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        [Test]
        public void CheckConnection()
        {
            var client = new DocumentClient(new Uri(EndpointUrl), AuthKey);
            var db = client.CreateDatabaseIfNotExistsAsync(new Database { Id = "FamilyDB" }).Result;
            var checkdb = client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri("FamilyDB")).Result;
            Assert.That(checkdb.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


        [Test]
        public void CheckNotExistingDatabase()
        {
            var client = new DocumentClient(new Uri(EndpointUrl), AuthKey);
            var ex = Assert.ThrowsAsync<DocumentClientException>(async () => await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri("NotExistingDB")));
            Assert.That(ex.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        }
    }
}
