namespace FindHomeMadeFoodNearMe.DataAccessHelper
{
    using System.Data.SqlClient;

    public interface IEntityBinder<T> where T : class
    {
        T BindEntity(SqlDataReader reader);
    }
}
