using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreMessagesFn.Model
{
    public class LogTQ : TableEntity
    {
        public LogTQ(string skey, string srow)
        {
            this.PartitionKey = skey;
            this.RowKey = srow;
        }

        public LogTQ() { }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
