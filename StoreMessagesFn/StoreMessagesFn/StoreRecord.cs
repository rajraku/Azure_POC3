using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using StoreMessagesFn.Model;

namespace StoreMessagesFn
{
    public static class StoreRecord
    {
        [FunctionName("StoreRecord")]
        public static void Run([QueueTrigger("logtq")]string myQueueItem
            , [StorageAccount("AzureWebJobsStorage")]CloudStorageAccount storageAccount
            , ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

            LogTQ _logTQ = new LogTQ("logtq", Guid.NewGuid().ToString());
            _logTQ.Message = myQueueItem;
            _logTQ.CreatedDate = DateTime.Now;

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("LogMessages");

            table.CreateIfNotExistsAsync().GetAwaiter().GetResult();

            TableOperation insertOperation = TableOperation.Insert(_logTQ);

            table.ExecuteAsync(insertOperation).GetAwaiter().GetResult();

            log.LogInformation("Completed logging the message to the table");
        }
    }
}
