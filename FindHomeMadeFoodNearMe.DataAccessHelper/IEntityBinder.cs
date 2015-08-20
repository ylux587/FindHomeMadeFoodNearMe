namespace FindHomeMadeFoodNearMe.DataAccessHelper
{
    using System.Data.SqlClient;

    public interface IEntityBinder<out T> where T : class
    {
        T BindEntity(SqlDataReader reader);
    }
}
