using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace _3rdYearProject
{
    class DAO
    {
        MongoClient                         _client;
        MongoServer                         _server;
        MongoDatabase                       _database;
        MongoCollection                     _collection;

        public bool setup(string connectionString, string databaseString, string collectionString)
        {
            _client = new MongoClient(connectionString);
            _server = _client.GetServer();
            _database = _server.GetDatabase(databaseString);
            _collection = _database.GetCollection<Entity>(collectionString);

            //If Collection exists, then everything else must exist
            if (_collection.Exists())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Create
        public bool Insert(Entity e)
        {
            _collection.Insert(e);

            return true;
        }

        //Read
        public void findOne()
        {
            Entity i = new Entity();

            i = _collection.FindOneAs<Entity>();

            Console.WriteLine("Name: " + i.Name + ". Object_ID: " + i.Id);
        }

        public void find(string key, string value)
        {
            
            var query = new QueryDocument(key, value);
            //Query using passed in entity
            //Result is new Entity i

            foreach (Entity e in _collection.FindAs<Entity>(query))
            {
                Console.WriteLine("Name: " + e.Name + ". Jumps: " + e.Jumps);
            }

            //return i;
        }

        //Convert to arrays for multiple key values in future
        public void Save(string key, string value, int jumps, int minutes, int seconds)
        {
            //Construct Query
            var query = Query.EQ(key, value);

            //Get Entity
            Entity i = _collection.FindOneAs<Entity>(query);

            //If doesn't exist, create. Otherwise, Update
            if (i == null)
            {
                Entity j = new Entity();
                j.Name = value;
                j.Jumps = jumps;
                j.Minutes = minutes;
                j.Seconds = seconds;
                Insert(j);
            }
            else
            {
                if (jumps > i.Jumps)
                {
                    var UpdateDocument = new UpdateDocument
                    {
                        { "$set", i.Jumps = jumps },
      
                    };
                }

                if (minutes <= i.Minutes && seconds < i.Seconds)
                {
                    var UpdateDocument2 = new UpdateDocument
                    {
                        { "$set", i.Seconds = seconds }

                    };
                    var UpdateDocument3 = new UpdateDocument
                    {
                        { "$set", i.Minutes = minutes }

                    };
                }
               

                _collection.Save(i);
            }
        }

    }
}
