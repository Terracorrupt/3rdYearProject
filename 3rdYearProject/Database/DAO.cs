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

        public bool Insert(Entity e)
        {
            WriteConcernResult w = _collection.Insert(e);

            if (w.ErrorMessage==null)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public void findOne()
        {

        }

    }
}
