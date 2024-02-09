using GraphQlDemo.Enums;
using GraphQlDemo.Exceptions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace GraphQlDemo.Services;

public class DatabaseService<T>
{
    private readonly IMongoCollection<T> _collection;

    public DatabaseService(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<ReturnResult> InsertOne(T document)
    {
        try
        {
            await _collection.InsertOneAsync(document);
            return ReturnResult.Successful;
        }
        catch (MongoWriteException writeEx)
        {
            Console.WriteLine($"MongoDB write error: {writeEx.WriteError}");
            return ReturnResult.Failed;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inserting document: {ex.Message}");
            return ReturnResult.Failed;
        }
    }
    
    public async Task<List<T>> GetAll(string sortByProperty = "_id", bool ascending = true)
    {
        try
        {
            var sortDefinition = ascending ? Builders<T>.Sort.Ascending(sortByProperty) : Builders<T>.Sort.Descending(sortByProperty);

            return await _collection.Find(_ => true).Sort(sortDefinition).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting all documents: {ex.Message}");
            throw;
        }
    }

    public async Task<T> GetByFilter(FilterDefinition<T> filter)
    {
        try
        {
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting document: {ex.Message}");
            throw;
        }
    }

    public async Task<T> FindAndUpdateByFilter(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options)
    {
        try
        {
            if (filter == null || update == null || options == null)
            {
                Console.WriteLine("Filter, update, or options is null");
                throw new InvalidOperationException("Filter, update, or options is null");
            }

            return await _collection.FindOneAndUpdateAsync(filter, update, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating document: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteByFilter(FilterDefinition<T> filter)
    {
        try
        {
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting document: {ex.Message}");
            throw;
        }
    }
}