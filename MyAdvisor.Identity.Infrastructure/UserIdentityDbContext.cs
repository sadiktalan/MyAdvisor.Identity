using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyAdvisor.Identity.Domain.CommonModels;
using MyAdvisor.Identity.Domain.Entities;
using MyAdvisor.Identity.Domain.Enums;

namespace MyAdvisor.Identity.Infrastructure;

public class UserIdentityDbContext : IdentityDbContext<User, Role, Guid, UserClaim, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, RoleClaim, IdentityUserToken<Guid>>
{
    public DbSet<UserSession> UserSession { get; set; }
    public DbSet<Screen> Screen { get; set; }
    public DbSet<ScreenClaim> ScreenClaim { get; set; }
    
    //private readonly IContextProvider _contextProvider;
    
    public UserIdentityDbContext(
        //IContextProvider contextProvider,
        DbContextOptions<UserIdentityDbContext> options
        ) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
        CreateDefaultMappings(builder);
        ConvertEnumColumnsToString(builder);
    }
    private static void CreateDefaultMappings(ModelBuilder builder)
    {
        var schema = "core";
        builder.Entity<User>().ToTable("user", schema); 
        builder.Entity<Role>().ToTable("role", schema); 
        builder.Entity<RoleClaim>().ToTable("role_claim", schema);
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_token", schema);
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_role", schema);
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_login", schema);
        
        builder.Entity<UserSession>().ToTable("user_session", schema);
       
        builder.Entity<Screen>().ToTable("screen", schema);
        builder.Entity<ScreenClaim>().ToTable("screen_claim", schema);
    }
    private static void CreateMsSqlMappings(ModelBuilder builder)
    {
        var schema = "Core";
        builder.Entity<User>().ToTable("User", schema); ;
        builder.Entity<Role>().ToTable("Role", schema); ;
        builder.Entity<RoleClaim>().ToTable("RoleClaim", schema);
    
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken", schema);
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole", schema);
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin", schema);

        builder.Entity<UserSession>().ToTable("UserRefreshToken", schema);
        builder.Entity<Screen>().ToTable("Screen", schema);
        builder.Entity<ScreenClaim>().ToTable("ScreenClaim", schema);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (string.IsNullOrWhiteSpace(entry.Entity.CreatedBy))
                    {
                        // if (string.IsNullOrWhiteSpace(_contextProvider.CurrentContext.UserId))
                        // {
                        //     throw new AudititableInfoMissingException();
                        // }
                        // entry.Entity.CreatedBy = _contextProvider.CurrentContext.UserId;
                    }
                    entry.Entity.CreateDate = DateTime.Now;
                    entry.Entity.RecordStatus = RecordStatus.Active;
                    break;

                case EntityState.Modified:
                    // entry.Entity.LastModifiedBy = !string.IsNullOrWhiteSpace(entry.Entity.LastModifiedBy)
                    //     ? entry.Entity.LastModifiedBy
                    //     : _contextProvider.CurrentContext.UserId;
                    entry.Entity.UpdateDate = DateTime.Now;
                    break;
            }
        }
        

        // var entityChangeLog = PrepareChangeTrackLogs(_contextProvider.CurrentContext.UserId);

        var result = await base.SaveChangesAsync(cancellationToken);

        // await DispatchEvents(events);

        // await DispatchChangeTrackLogsAsync(entityChangeLog);

        return result;
    }
    
    // private async Task DispatchChangeTrackLogsAsync(List<EntityChangeLogModel> entityChangeLogList)
    // {
    //     foreach (var item in entityChangeLogList)
    //     {
    //         var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    //         var busEndpoint = await _bus.GetSendEndpoint(new Uri("exchange:Log.ChangeTracker"));
    //         await busEndpoint.Send(item, cancellationToken.Token);
    //     }
    // }
    //
    // private List<EntityChangeLogModel> PrepareChangeTrackLogs(string userId)
    // {
    //     var entries = new List<EntityChangeLogModel>();
    //
    //     return ApplyChangeTracker(userId, entries);
    // }
    // private List<EntityChangeLogModel> ApplyChangeTracker(string userId, List<EntityChangeLogModel> entries)
    // {
    //     foreach (var entry in base.ChangeTracker.Entries<ITrackChange>())
    //     {
    //         var schemaName = GetSchema(entry);
    //
    //         var entityChangeLog = new EntityChangeLogModel
    //         {
    //             ServiceName = base.Database.GetDbConnection().Database,
    //             ShemaName = schemaName,
    //             TableName = entry.Entity.GetType().Name,
    //             UserId = userId,
    //             ClientIpAddress = _contextProvider.CurrentContext?.ClientIpAddress,
    //             LogDate = DateTime.Now,
    //             CorrelationId = _contextProvider.CurrentContext?.CorrelationId,
    //         };
    //
    //         ApplyEntityState(entries, entry, entityChangeLog);
    //     }
    //     return entries;
    // }
    // private static void ApplyEntityState(List<EntityChangeLogModel> entries, EntityEntry<ITrackChange> entry, EntityChangeLogModel entityChangeLog)
    // {
    //     if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
    //     {
    //         entries.Add(entityChangeLog);
    //
    //         var databaseValues = entry.GetDatabaseValues();
    //
    //         foreach (var property in entry.Properties)
    //         {
    //             string propertyName = property.Metadata.Name;
    //             if (property.Metadata.IsPrimaryKey())
    //             {
    //                 entityChangeLog.KeyValues[propertyName] = property.CurrentValue?.ToString();
    //                 continue;
    //             }
    //             switch (entry.State)
    //             {
    //                 case EntityState.Added:
    //                     entityChangeLog.CrudOperationType = CrudOperationType.Create;
    //                     entityChangeLog.NewValues[propertyName] = property.CurrentValue?.ToString();
    //                     break;
    //
    //                 case EntityState.Deleted:
    //                     entityChangeLog.CrudOperationType = CrudOperationType.Delete;
    //                     entityChangeLog.OldValues[propertyName] = property.OriginalValue?.ToString();
    //                     break;
    //
    //                 case EntityState.Modified:
    //                     if (property.IsModified)
    //                     {
    //                         if (databaseValues[propertyName]?.ToString() != property.CurrentValue?.ToString())
    //                         {
    //                             entityChangeLog.AffectedColumns.Add(propertyName);
    //                             entityChangeLog.NewValues[propertyName] = property.CurrentValue?.ToString();
    //                         }
    //                         entityChangeLog.OldValues[propertyName] = databaseValues[propertyName]?.ToString();
    //                         entityChangeLog.CrudOperationType = CrudOperationType.Update;
    //                     }
    //                     break;
    //             }
    //         }
    //     }
    // }
    // private string GetSchema(EntityEntry entry)
    // {
    //     var entity = entry.Entity;
    //     var schemaAnnotation = base.Model.FindEntityType(entity.GetType()).GetAnnotations()
    //     .FirstOrDefault(a => a.Name == "Relational:Schema");
    //     return schemaAnnotation == null ? "dbo" : schemaAnnotation.Value.ToString();
    // }
    private static void ConvertEnumColumnsToString(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType.IsEnum)
                {
                    var type = typeof(EnumToStringConverter<>).MakeGenericType(property.ClrType);
                    var converter = Activator.CreateInstance(type, new ConverterMappingHints()) as ValueConverter;

                    property.SetValueConverter(converter);
                    property.SetMaxLength(50);
                }
            }
        }
    }
    // private async Task DispatchEvents(DomainEvent[] events)
    // {
    //     foreach (var @event in events)
    //     {
    //         @event.IsPublished = true;
    //         await _domainEventService.PublishAsync(@event);
    //     }
    // }
}