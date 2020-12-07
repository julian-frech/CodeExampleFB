using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FlowController.Interfaces;

namespace FlowController.Model
{
    [Table("Auditing", Schema = "loging")]
    public class loging_Auditing
    {
        [Key, Column(Order = 0)] public int AuditID { get; set; }
        public int AppID { get; set; }
        public string AppName { get; set; }
        public string Parameter { get; set; }
        public string SourceTable1 { get; set; }
        public string SourceTable2 { get; set; }
        public string TargetTable1 { get; set; }
        public string TargetTable2 { get; set; }
        public int TaskSuccess { get; set; }
        public DateTime TaskPlanned { get; set; }
        public DateTime TaskStarted { get; set; }
        public DateTime TaskEnded { get; set; }
        public string AuditLog { get; set; }
    }
}
