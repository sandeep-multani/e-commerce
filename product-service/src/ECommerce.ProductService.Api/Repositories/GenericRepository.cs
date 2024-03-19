using System.Linq.Expressions;
using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Attributes;
using ECommerce.ProductService.Api.Configurations;
using ECommerce.ProductService.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerce.ProductService.Api.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : IEntity
{
    private readonly ILogger<Repository<TEntity>> _logger;
    private readonly IMongoCollection<TEntity> _collection;

    public Repository(ILogger<Repository<TEntity>> logger, MongoDbConfiguration configuration)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        Guard.Against.Null(configuration, nameof(configuration));

        var database = new MongoClient(configuration.ConnectionString)
                            .GetDatabase(configuration.DatabaseName)
                            .WithReadPreference(ReadPreference.SecondaryPreferred)
                            .WithWriteConcern(WriteConcern.WMajority);

        _collection = database.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));
    }

    private protected string GetCollectionName(Type documentType)
    {
        return (documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault() as BsonCollectionAttribute)?.CollectionName
                ?? documentType.Name.ToLower();
    }

    public virtual IQueryable<TEntity> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public virtual IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).ToEnumerable();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TProjected>> projectionExpression)
    {
        return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual Task DeleteByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDeleteAsync(filter);
        });
    }

    public virtual Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression)
    {
        return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
    }

    public virtual Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression)
    {
        return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
    }

    public virtual Task<TEntity> FindByIdAsync(string id)
    {
        return Task.Run(() =>
       {
           var objectId = new ObjectId(id);
           var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, objectId);
           return _collection.Find(filter).SingleOrDefaultAsync();
       });
    }

    public virtual Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression)
    {
        return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
    }

    public virtual Task InsertManyAsync(ICollection<TEntity> documents)
    {
        return Task.Run(() => _collection.InsertManyAsync(documents));

    }

    public virtual Task InsertOneAsync(TEntity document)
    {
        return Task.Run(() => _collection.InsertOneAsync(document)); ;
    }

    public virtual Task ReplaceOneAsync(TEntity document)
    {
        return Task.Run(() =>
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, document.Id);
            return _collection.FindOneAndReplaceAsync(filter, document);
        });
    }
}