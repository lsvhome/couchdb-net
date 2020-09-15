﻿using CouchDB.Driver.E2E.Models;
using CouchDB.Driver.Options;

namespace CouchDB.Driver.E2ETests
{
    public class MyDeathStarContext : CouchContext
    {
        public CouchDatabase<Rebel> Rebels { get; set; }

        protected override void OnConfiguring(CouchOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseEndpoint("http://localhost:5984/")
                .EnsureDatabaseExists()
                .UseBasicAuthentication(username: "admin", password: "admin");
        }

        protected override void OnDatabaseCreating(CouchDatabaseBuilder databaseBuilder)
        {
            databaseBuilder.Document<Rebel>()
                .HasIndex("surnames_index", builder => builder
                    .IndexBy(r => r.Surname)
                    .ThenBy(r => r.Name));
        }
    }
}
