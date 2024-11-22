namespace MovieReviewer.Shared.Domain.Interfaces;

public interface IGenericRepository<T>
{
    Task<T?> GetById(int id);
    IEnumerable<T> GetAll();
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<bool> Exists(int id);
}