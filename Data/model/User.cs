using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ATM_Replica.Data_folder.model_folder
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public List<Transaction>? Transactions { get; set; } = new List<Transaction>();
        public decimal Balance { get; set; }
        public AccountTypeEnum AccountType { get; set; } = AccountTypeEnum.CURRENT;

        public enum AccountTypeEnum
        {
            CURRENT, SAVINGS, CREDIT
        }

        public User()
        {
            
        }

        //public User(string firstName, string lastName, string email, string password, AccountTypeEnum accountType, decimal balance)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    Password = password;
        //    AccountType = accountType;
        //    Balance = balance;
           
        //}
    }
}
