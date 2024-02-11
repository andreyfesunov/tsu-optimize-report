using MongoDB.Bson;

namespace web.Infrastructure.Models;

public class Base
{
    protected Base(string id)
    {
        Id = ObjectId.Parse(id);
    }

    public ObjectId Id;
}