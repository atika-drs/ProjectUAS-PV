//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace uasmvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class reservasi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public reservasi()
        {
            this.pembayaran = new HashSet<pembayaran>();
        }

        public int ReservasiID { get; set; }
        public int KamarID { get; set; }
        public int PelangganID { get; set; }
        public DateTime? tgtl_reserv { get; set; }
        public DateTime? tgl_masuk_reserv { get; set; }
        public DateTime? tgl_keluar_reserv { get; set; }
        public decimal lama_reserv { get; set; }
        public string status_reserv { get; set; }
    
        public virtual kamar kamar { get; set; }
        public virtual pelanggan pelanggan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pembayaran> pembayaran { get; set; }
    }
}
