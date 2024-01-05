using ATM_Replica.Data_folder.model_folder;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ATM_Replica.Data.model
{
    public class TransactionHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public User? receiverEmail { get; set; }
        public decimal withdraw { get; set; }
        public decimal debit { get; set; }
        public decimal transfer { get; set; }
        public string? description { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal amount { get; set; }
    }
}
