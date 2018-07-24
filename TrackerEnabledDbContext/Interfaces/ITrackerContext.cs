﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackerEnabledDbContext.Common.EventArgs;
using TrackerEnabledDbContext.Common.Models;

namespace TrackerEnabledDbContext.Core.Common.Interfaces
{
    public interface ITrackerContext : IDbContext
    {
        DbSet<AuditLog> AuditLogs { get; set; }
        DbSet<AuditLogDetail> AuditLogDetails { get; set; }
        bool TrackingEnabled { get; set; }
        bool AdditionTrackingEnabled { get; set; }
        bool ModificationTrackingEnabled { get; set; }
        bool DeletionTrackingEnabled { get; set; }

        event EventHandler<AuditLogGeneratedEventArgs> OnAuditLogGenerated;

        void ConfigureUsername(Func<string> usernameFactory);
        void ConfigureUsername(string defaultUsername);
        void ConfigureMetadata(Action<dynamic> metadataConfiguration);

        IQueryable<AuditLog> GetLogs(string entityFullName);
        IQueryable<AuditLog> GetLogs(string entityFullName, object primaryKey);
        IQueryable<AuditLog> GetLogs<TEntity>();
        IQueryable<AuditLog> GetLogs<TEntity>(object primaryKey);

        int SaveChanges(object userName);

        //async
        Task<int> SaveChangesAsync(object userName, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(int userId);
        Task<int> SaveChangesAsync(string userName);
    }
}