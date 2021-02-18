using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataOperator.Models
{//
    [Table("LOGS", Schema = "logging")]
    public class Logs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Log_Id {get;set;}
        public string Service_Name { get; set; }
        public string Logging_Level { get; set; }
        public DateTime Log_Time { get; set; }
        public int Thread_Id { get; set; }
        public string Machine_Name { get; set; }
        public int Service_Step_Num { get; set; }
        public string Message { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool Solved { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ITS { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UTS { get; set; }

        //public Logs() { }

        public Logs(string service_Name, string logging_Level, DateTime log_Time, int thread_Id, string machine_Name, int service_Step_Num, string message)
        {
            Service_Name = service_Name;
            Logging_Level = logging_Level;
            Log_Time = log_Time;
            Thread_Id = thread_Id;
            Machine_Name = machine_Name;
            Service_Step_Num = service_Step_Num;
            Message = message;
        }
    }

    //public class ApplyToDbContext_LOGS : IEntityTypeConfiguration<Logs>
    //{
    //    public virtual void Configure(EntityTypeBuilder<Logs> builder)
    //    {
    //        builder.HasKey(x => new { x.Log_Id });
    //    }
    //}
}
