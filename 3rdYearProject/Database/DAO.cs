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
        public void Save(string key, string value, int jumps, int minutes, int seconds,int levelID)
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
                if (levelID == 1)
                {
                    j.L1Minutes = minutes;
                    j.L1Seconds = seconds;
                }
                else if (levelID == 2)
                {
                    j.L2Minutes = minutes;
                    j.L2Seconds = seconds;
                }
                else if (levelID == 3)
                {
                    j.L3Minutes = minutes;
                    j.L3Seconds = seconds;
                }
                else
                {
                    j.L1Minutes = 60;
                    j.L1Seconds = 60;
                    j.L2Minutes = 60;
                    j.L2Seconds = 60;
                    j.L3Minutes = 60;
                    j.L3Seconds = 60;
                }

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
                if (levelID == 1)
                {
                    if (minutes <= i.L1Minutes && seconds < i.L1Seconds)
                    {
                        var UpdateDocument2 = new UpdateDocument
                        {
                            { "$set", i.L1Seconds = seconds }

                        };
                        var UpdateDocument3 = new UpdateDocument
                        {
                            { "$set", i.L1Minutes = minutes }

                        };
                    }
                }
                if (levelID == 2)
                {
                    if (minutes <= i.L2Minutes && seconds < i.L2Seconds)
                    {
                        var UpdateDocument2 = new UpdateDocument
                    {
                        { "$set", i.L2Seconds = seconds }

                    };
                        var UpdateDocument3 = new UpdateDocument
                    {
                        { "$set", i.L2Minutes = minutes }

                    };
                    }
                }
                if (levelID == 3)
                {
                    if (minutes <= i.L3Minutes && seconds < i.L3Seconds)
                    {
                        var UpdateDocument2 = new UpdateDocument
                    {
                        { "$set", i.L3Seconds = seconds }

                    };
                        var UpdateDocument3 = new UpdateDocument
                    {
                        { "$set", i.L3Minutes = minutes }

                    };
                    }
                }
               

                _collection.Save(i);
            }
        }

        public Entity[] getHighScores(int levelID)
        {
            //Querey Database on Minutes and Seconds
            //Order by Minutes first, then seconds
            //Return as array of Entities
            Entity[] entities = new Entity[_collection.Count()];
            string levelMin="", levelSec="";
            

            if (levelID == 1)
            {
                levelMin = "L1Minutes";
                levelSec = "L1Seconds";
            }
            if (levelID == 2)
            {
                levelMin = "L2Minutes";
                levelSec = "L2Seconds";
            }
            if (levelID == 3)
            {
                levelMin = "L3Minutes";
                levelSec = "L3Seconds";
            }

            var query = Query.Exists(levelMin);
            var cursor = _collection.FindAs<Entity>(query).SetSortOrder(SortBy.Ascending(levelMin, levelSec));

            int i=0;

            foreach (Entity e in cursor)
            {
                entities[i] = e;
                i++;
            }

            return entities;
        }

    }
}
