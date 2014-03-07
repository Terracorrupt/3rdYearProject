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
        public string Name { get; set; }
        public int Jumps { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }

}
