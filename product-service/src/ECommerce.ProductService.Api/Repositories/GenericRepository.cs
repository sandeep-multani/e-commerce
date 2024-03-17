using System.Linq.Expressions;
using Ardalis.GuardClauses;
using ECommerce.ProductService.Api.Attributes;
using ECommerce.ProductService.Api.Configurations;
using ECommerce.ProductService.Api.Documents;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerce.ProductService.Api.Repositories;

public class Repository<TDocument> : IRepository<TDocument>
    where TDocument : IDocument
{
    private readonly ILogger<Repository<TDocument>> _logger;
    private readonly IMongoCollection<TDocument> _collection;

    public Repository(ILogger<Repository<TDocument>> logger, MongoDbConfiguration configuration)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        Guard.Against.Null(configuration, nameof(configuration));

        var database = new MongoClient(configuration.ConnectionString)
                            .GetDatabase(configuration.DatabaseName)
                            .WithReadPreference(ReadPreference.SecondaryPreferred)
                            .WithWriteConcern(WriteConcern.WMajority);

        _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    private protected string GetCollectionName(Type documentType)
    {
        return (documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault() as BsonCollectionAttribute)?.CollectionName
                ?? documentType.Name.ToLower();
    }

    public virtual IQueryable<TDocument> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
    {
        return _collection.Find(filterExpression).ToEnumerable();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(Expression<Func<TDocument, bool>> filterExpression, Expression<Func<TDocument, TProjected>> projectionExpression)
    {
        return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual Task DeleteByIdAsync(string id)
    {
        return Task.Run(() =>
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            _collection.FindOneAndDeleteAsync(filter);
        });
    }

    public virtual Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
    }

    public virtual Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
    }

    public virtual Task<TDocument> FindByIdAsync(string id)
    {
        return Task.Run(() =>
       {
           var objectId = new ObjectId(id);
           var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
           return _collection.Find(filter).SingleOrDefaultAsync();
       });
    }

    public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
    }

    public virtual Task InsertManyAsync(ICollection<TDocument> documents)
    {
        return Task.Run(() => _collection.InsertManyAsync(documents));

    }

    public virtual Task InsertOneAsync(TDocument document)
    {
        return Task.Run(() => _collection.InsertOneAsync(document)); ;
    }

    public virtual Task ReplaceOneAsync(TDocument document)
    {
        return Task.Run(() =>
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            return _collection.FindOneAndReplaceAsync(filter, document);
        });
    }
}