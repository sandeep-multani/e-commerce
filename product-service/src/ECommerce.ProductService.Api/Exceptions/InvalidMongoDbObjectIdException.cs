namespace ECommerce.ProductService.Api.Exceptions;

[Serializable]
public class InvalidMongoDbObjectIdException : Exception
{
    public InvalidMongoDbObjectIdException() : base() { }
    public InvalidMongoDbObjectIdException(string message) : base(message) { }
    public InvalidMongoDbObjectIdException(string message, Exception inner) : base(message, inner) { }
}