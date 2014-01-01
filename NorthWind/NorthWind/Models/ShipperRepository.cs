using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class ShipperRepository
    {
        private Entities db;

        public ShipperRepository(Entities entities) { this.db = entities; }

        public IQueryable<Shipper> FindAllShippers() { return db.Shippers; }

        public Shipper GetShipper(int id)
        {
            return db.Shippers.SingleOrDefault(shipper => shipper.ShipperID == id);
        }

        public void Add(Shipper shipper) { db.Shippers.Add(shipper); }

        public void Delete(Shipper shipper)
        {
            db.Shippers.Remove(shipper);
        }
        public void Save() { db.SaveChanges(); }
    }
}