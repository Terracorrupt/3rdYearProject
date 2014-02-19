using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace _3rdYearProject
{
    public class Entity
    {
        public ObjectId Id { get; set; }
        public string _Name { get; set; }
    }

}
