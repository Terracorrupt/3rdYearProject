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
        public int L1Minutes { get; set; }
        public int L1Seconds { get; set; }
        public int L2Minutes { get; set; }
        public int L2Seconds { get; set; }
        public int L3Minutes { get; set; }
        public int L3Seconds { get; set; }
    }

}
