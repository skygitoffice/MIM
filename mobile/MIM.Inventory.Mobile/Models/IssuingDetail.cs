using System.ComponentModel.DataAnnotations.Schema;

namespace MIM.Inventory.Mobile.Models
{
    [Table("issuing_detail")]
    public class IssuingDetail
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("issuing_id")]
        public long IssuingId { get; set; }

        [Column("material_id")]
        public long? MaterialId { get; set; }

        [Column("vendor_id")]
        public long? VendorId { get; set; }

        [Column("bucket_rfid")]
        public string? BucketRfid { get; set; }

        [Column("issued_weight")]
        public decimal IssuedWeight { get; set; }

        [Column("unit")]
        public string Unit { get; set; } = "G";

        public IssuingOrder? IssuingOrder { get; set; }
    }
}
