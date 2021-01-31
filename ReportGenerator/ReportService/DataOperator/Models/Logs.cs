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
    [Table("F_SERVICE_LOGS", Schema = "logging")]
    public class Logs
    {
        public int ServiceId { get; set; }
        public string LogMessage { get; set; }
        public string ServiceMethod { get; set; }
        public DateTimeOffset LogTime { get; set; }

        public Logs() { }

        public Logs(string logString, int serviceId, string serviceMethod)
        {
            ServiceId = serviceId;
            LogMessage = logString;
            ServiceMethod = serviceMethod;
            LogTime = DateTimeOffset.Now;
        }
    }

    public class ApplyToDbContext_LOGS : IEntityTypeConfiguration<Logs>
    {
        public virtual void Configure(EntityTypeBuilder<Logs> builder)
        {
            builder.HasKey( x => new { x.LogMessage, x.LogTime, x.ServiceId, x.ServiceMethod});
        }
    }
}
