using ContactBook.Db.Connections;
namespace ContactBook.Db.Data
{
    public partial class ContactBookEdmContainer
    {
        public ContactBookEdmContainer(string connectionName = "ContactBookEdmContainer")
            : base(ConnectionString.EFConnectionString(connectionName))
        {
        }
    }
}