using todobackend.models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace todobackend.services;
public class TodoService
{
    private readonly IMongoCollection<Todo> todoCollection;

    public TodoService(IOptions<todoDatabaseSettings> todoDatabaseSettings)
    {
        var settings = MongoClientSettings.FromConnectionString("mongodb+srv://ravisha23:ravisha@cluster0.pwmuhjt.mongodb.net/?retryWrites=true&w=majority");
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase("todo");
        todoCollection = database.GetCollection<Todo>("todo_collection");

        var indexOptions = new CreateIndexOptions();
        var indexKeys = Builders<Todo>.IndexKeys.Ascending(todo => todo.id);
        var indexModel = new CreateIndexModel<Todo>(indexKeys, indexOptions);
        todoCollection.Indexes.CreateOneAsync(indexModel);
        
        var indexes = database.GetCollection<Todo>("todo_collection").Indexes.List().ToList();

        foreach (var index in indexes)
        {
            Console.WriteLine(index);
        }
    }


    public async Task createTodo(Todo newTodo)
    {
        newTodo.id = null; // ID will be set by MongoDB
        await todoCollection.InsertOneAsync(newTodo);
        //todo.Add(newTodo);//changine in database
    }

    public async Task<List<Todo>> getTodo()
    {
        return await todoCollection.Find(_ => true).ToListAsync();
        //return todo;//getting it from database
    }

    public async Task<Todo> getTodo(string Id)
    {
        return await todoCollection.Find<Todo>(todo => todo.todo_id == Id).FirstOrDefaultAsync();
        //return order.Find(x => x.id == Id);
    }

    public async Task deleteTodo(string Id)
    {
        await todoCollection.DeleteOneAsync(todo => todo.id == Id);
    }
}