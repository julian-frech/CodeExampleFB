using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataOperator.Models
{
   
    [Table("V_Report_Overview", Schema = "reporting")]
    public class ReportConfiguration
    {
        public string Report_Name { get; set; }

        public string Report_Sql { get; set; }

        public string Report_Separator { get; set; }

        public string Parameter { get; set; }

        public string Header_Row { get; set; }

    }

    public class ApplyToDbContext_ReportConfiguration : IEntityTypeConfiguration<ReportConfiguration>
    {
        public virtual void Configure(EntityTypeBuilder<ReportConfiguration> builder)
        {
            builder.HasKey(x => x.Report_Name);
        }
    }



}
