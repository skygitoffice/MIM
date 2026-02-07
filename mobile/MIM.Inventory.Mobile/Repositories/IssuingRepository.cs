using Microsoft.EntityFrameworkCore;
using MIM.Inventory.Mobile.Data;
using MIM.Inventory.Mobile.Models;

namespace MIM.Inventory.Mobile.Repositories
{
    public class IssuingRepository : IIssuingRepository
    {
        private readonly IDbContextFactory<MimDbContext> _dbContextFactory;

        public IssuingRepository(IDbContextFactory<MimDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IssueDocument> CreateIssueAsync(IssueCreateRequest request, CancellationToken cancellationToken = default)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var now = DateTime.Now;
            var issuingDate = request.IssuingDate.Date;
            var diffVoucherno = await GenerateIssueVouchernoAsync(dbContext, issuingDate, cancellationToken);

            var order = new IssuingOrder
            {
                DiffVoucherno = diffVoucherno,
                WarehouseId = request.WarehouseId,
                IssuingDate = issuingDate,
                Destination = request.Destination,
                MachineNo = request.MachineNo,
                IssuedBy = request.IssuedBy,
                IsValid = true,
                Remark = request.Remark,
                CreateBy = request.IssuedBy,
                CreateTime = now,
                UpdateBy = request.IssuedBy,
                UpdateTime = now
            };

            dbContext.IssuingOrders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            var detail = new IssuingDetail
            {
                IssuingId = order.Id,
                MaterialId = request.MaterialId,
                VendorId = request.VendorId,
                BucketRfid = request.MaterialOrRfid,
                IssuedWeight = request.IssuedWeight,
                Unit = request.Unit
            };

            dbContext.IssuingDetails.Add(detail);
            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new IssueDocument(order, new List<IssuingDetail> { detail });
        }

        public async Task<IReadOnlyList<IssueSummary>> GetRecentIssuesAsync(int limit, CancellationToken cancellationToken = default)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            return await dbContext.IssuingOrders
                .AsNoTracking()
                .OrderByDescending(order => order.CreateTime ?? DateTime.MinValue)
                .ThenByDescending(order => order.Id)
                .Take(limit)
                .Select(order => new IssueSummary
                {
                    Id = order.Id,
                    DiffVoucherno = order.DiffVoucherno,
                    IssuingDate = order.IssuingDate,
                    MachineNo = order.MachineNo,
                    IssuedBy = order.IssuedBy,
                    IsValid = order.IsValid
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IssueDocument?> GetIssueDetailAsync(long id, CancellationToken cancellationToken = default)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            var order = await dbContext.IssuingOrders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (order is null)
            {
                return null;
            }

            var details = await dbContext.IssuingDetails
                .AsNoTracking()
                .Where(detail => detail.IssuingId == order.Id)
                .OrderBy(detail => detail.Id)
                .ToListAsync(cancellationToken);

            return new IssueDocument(order, details);
        }

        public async Task<bool> VoidIssueAsync(long id, string updatedBy, CancellationToken cancellationToken = default)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            var order = await dbContext.IssuingOrders
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (order is null)
            {
                return false;
            }

            order.IsValid = false;
            order.UpdateBy = updatedBy;
            order.UpdateTime = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        }

        public async Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            return await dbContext.Database.CanConnectAsync(cancellationToken);
        }

        private static async Task<string> GenerateIssueVouchernoAsync(MimDbContext dbContext, DateTime issuingDate, CancellationToken cancellationToken)
        {
            var prefix = $"ISU-{issuingDate:yyyyMMdd}-";
            var latest = await dbContext.IssuingOrders
                .AsNoTracking()
                .Where(order => order.DiffVoucherno.StartsWith(prefix))
                .OrderByDescending(order => order.DiffVoucherno)
                .Select(order => order.DiffVoucherno)
                .FirstOrDefaultAsync(cancellationToken);

            var sequence = 1;
            if (!string.IsNullOrWhiteSpace(latest))
            {
                var parts = latest.Split('-');
                if (parts.Length > 0 && int.TryParse(parts[^1], out var parsed))
                {
                    sequence = parsed + 1;
                }
            }

            return $"{prefix}{sequence:000}";
        }
    }
}
