﻿using Microsoft.EntityFrameworkCore;
using Pathmaker.Persistence;

namespace Pathmaker.UnitTests.Factories;

public static class DbContextFactory {
    public static ApplicationDbContext Create() {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
        return new ApplicationDbContext(options.Options);
    }
}