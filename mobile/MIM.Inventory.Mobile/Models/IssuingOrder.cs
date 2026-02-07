using System.ComponentModel.DataAnnotations.Schema;

namespace MIM.Inventory.Mobile.Models
{
    [Table("issuing_order")]
    public class IssuingOrder
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("diff_voucherno")]
        public string DiffVoucherno { get; set; } = string.Empty;

        [Column("warehouse_id")]
        public int WarehouseId { get; set; }

        [Column("issuing_date")]
        public DateTime IssuingDate { get; set; }

        [Column("destination")]
        public string? Destination { get; set; }

        [Column("machine_no")]
        public string MachineNo { get; set; } = string.Empty;

        [Column("issued_by")]
        public string? IssuedBy { get; set; }

        [Column("is_valid")]
        public bool IsValid { get; set; }

        [Column("remark")]
        public string? Remark { get; set; }

        [Column("create_by")]
        public string? CreateBy { get; set; }

        [Column("create_time")]
        public DateTime? CreateTime { get; set; }

        [Column("update_by")]
        public string? UpdateBy { get; set; }

        [Column("update_time")]
        public DateTime? UpdateTime { get; set; }

        public ICollection<IssuingDetail> Details { get; set; } = new List<IssuingDetail>();
    }
}
