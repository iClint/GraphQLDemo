using GraphQlDemo.Exceptions;
using MongoDB.Driver;

namespace GraphQlDemo.Services;

public class DatabaseService<T>
{
    private readonly IMongoCollection<T> _collection;

    public DatabaseService(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public void InsertOne(T document)
    {
        try
        {
            _collection.InsertOne(document);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting document: {ex.Message}");
            throw;
        }
    }
    
    public List<T> GetAll(string sortByProperty = "_id", bool ascending = true)
    {
        try
        {
            var sortDirection = ascending ? SortDirection.Ascending : SortDirection.Descending;
            var sortDefinition = Builders<T>.Sort.Ascending(sortByProperty);

            return _collection.Find(_ => true).Sort(sortDefinition).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting all documents: {ex.Message}");
            throw;
        }
    }

    public T GetByFilter(FilterDefinition<T> filter)
    {
        try
        {
            var result = _collection.Find(filter).SingleOrDefault();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting document: {ex.GetType().Name} - {ex.Message}");
            throw;
        }
    }

    public T FindAndUpdateByFilter(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options)
    {
        try
        {
            if (_collection == null)
            {
                Console.WriteLine("Collection is null");
                throw new InvalidOperationException("Collection is null");
            }
            
            
            if (filter == null || update == null || options == null)
            {
                Console.WriteLine("Filter, update, or options is null");
                throw new InvalidOperationException("Filter, update, or options is null");
            }
            
            var result = _collection.FindOneAndUpdate(filter, update, options);
            if (result != null)
            {
                return result;
            }
            else
            {
                Console.WriteLine("Document not found");
                throw new NotFoundException("Document not found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating document: {ex.Message}");
            throw;
        }
    }

    public bool DeleteByFilter(FilterDefinition<T> filter)
    {
        try
        {
            var result = _collection.DeleteOne(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting document: {ex.Message}");
            throw;
        }
    }
}