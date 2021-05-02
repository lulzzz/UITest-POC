using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOF.Etimad.Monafasat.Data
{
    public static class DbContextExtensions
    {
        public static void LogTransaction(this DbContext context, IHttpContextAccessor httpContextAccessor = null, List<Type> excludedTypes = null)
        {


            var changes = context.ChangeTracker.Entries().Where(c => c.State == EntityState.Added || c.State == EntityState.Deleted || c.State == EntityState.Modified).ToList();

            if (excludedTypes != null)
            {
                changes = changes.Where(a => !excludedTypes.Any(c => c.Name == a.Entity.GetType().Name)).ToList();

            }
            var transactionId = Guid.NewGuid().ToString();


            foreach (var change in changes)
            {
                var jsonData = GetJsonData(change);
                AuditLog tran = new AuditLog
                {
                    TransactionId = transactionId,
                    AuditType = GetAuditType(change.State),
                    TableName = GetTableName(change),
                    TimeStamp = DateTime.Now,
                    UserName = GetCurrenUser(httpContextAccessor),
                    PrimaryKey = GetPrimaryKey(change),
                    OldData = jsonData.OldData,
                    NewData = jsonData.NewData,
                    TableSchema = GetSchemaName(change)
                };
                context.Set<AuditLog>().Add(tran);
            }
        }

        public static void PreserveAuditData(this DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            var entities = context.ChangeTracker.Entries()
                .Where(x => x.Entity is AuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entity in entities)
            {
                var now = DateTime.Now;
                var username = GetCurrenUser(httpContextAccessor);
                var baseEntity = (AuditableEntity)entity.Entity;

                if (entity.State == EntityState.Added)
                {
                    baseEntity.CreatedAt = now;
                    baseEntity.CreatedBy = username;
                }
                if (entity.State == EntityState.Modified)
                {
                    baseEntity.UpdatedAt = now;
                    baseEntity.UpdatedBy = username;
                }
            }
        }

        private static string GetCurrenUser(IHttpContextAccessor httpContextAccessor)
        {
            string username = null;
            if (httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "fullname") != null)
            {
                username = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "fullname").Value;
            }
            if (httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "preferred_username") != null)
            {
                username += " | ";
                username += httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "preferred_username").Value;
            }
            return username;
        }

        private static string GetTableName(EntityEntry entry)
        {
            var tableName = entry.Metadata.GetTableName();
            return tableName;
        }


        private static string GetSchemaName(EntityEntry entry)
        {
            var tableName = entry.Metadata.GetSchema();
            return tableName;
        }


        private static string GetPrimaryKey(EntityEntry entry)
        {
            string primaryKeyValue = null;

            if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                var primaryKeyName = entry?.Metadata?.FindPrimaryKey()?.Properties[0]?.Name;
                primaryKeyValue = entry.OriginalValues[primaryKeyName].ToString();
            }

            return primaryKeyValue;
        }

        private static (string OldData, string NewData) GetJsonData(EntityEntry entry)
        {
            (string OldData, string NewData) data = (OldData: null, NewData: null);
            switch (entry.State)
            {
                case EntityState.Added:
                    data.NewData = JsonConvert.SerializeObject(entry.CurrentValues?.ToObject());
                    break;
                case EntityState.Deleted:
                    data.OldData = JsonConvert.SerializeObject(entry.OriginalValues?.ToObject());
                    break;
                case EntityState.Modified:
                    data = GetModifiedData(entry);
                    break;
            }

            return data;
        }

        private static (string OldData, string NewData) GetModifiedData(EntityEntry entry)
        {
            (string OldData, string NewData) data;
            var oldData = new Dictionary<string, object>();
            var newData = new Dictionary<string, object>();
            foreach (var property in entry.Properties)
            {
                var propertyName = property.Metadata.Name;
                if (object.Equals(property.OriginalValue, property.CurrentValue))
                    continue;
                oldData[propertyName] = property.OriginalValue;
                newData[propertyName] = property.CurrentValue;
            }
            data.OldData = oldData.Count > 0 ? JsonConvert.SerializeObject(oldData) : null;
            data.NewData = newData.Count > 0 ? JsonConvert.SerializeObject(newData) : null;
            return data;
        }


        private static string GetAuditType(EntityState state)
        {
            var auditType = "";
            switch (state)
            {

                case EntityState.Added:
                    auditType = "I";
                    break;
                case EntityState.Modified:
                    auditType = "U";
                    break;
                case EntityState.Deleted:
                    auditType = "D";
                    break;
            }
            return auditType;
        }



    }
}
