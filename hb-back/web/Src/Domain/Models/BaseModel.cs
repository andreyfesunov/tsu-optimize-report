using MongoDB.Bson;

namespace web.Domain.Models;

public class BaseModel
{
    protected BaseModel(string? id)
    {
        Id = id ?? ObjectId.GenerateNewId().ToString();
    }

    public string Id;
}