using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseWatcher.Model
{
    [Table("FlowControl", Schema = "configuration")]
    public class configuration_FlowControl 
    {

        public int? FlowID { get; set; }
        public int AppID { get; set; }
        public string AppName { get; set; }
        public string Parameter { get; set; }
        public string SourceTable1 { get; set; }
        public string SourceTable2 { get; set; }
        public string TargetTable1 { get; set; }
        public string TargetTable2 { get; set; }
        public int TaskSuccess { get; set; }
        public string TaskPlanned { get; set; }
        public int WatchId { get; set; }
        public int AnalysisiId { get; set; }
        public configuration_FlowControl(int? flowID, int appID, string appName, string parameter, string sourceTable1, string sourceTable2, string targetTable1, string targetTable2, int taskSuccess, string taskPlanned, int watchId, int analysisId)
        {
            this.FlowID = flowID;
            this.AppID = appID;
            this.AppName = appName;
            this.Parameter = parameter;
            this.SourceTable1 = sourceTable1;
            this.SourceTable2 = sourceTable2;
            this.TargetTable1 = targetTable1;
            this.TargetTable2 = targetTable2;
            this.TaskSuccess = taskSuccess;
            this.TaskPlanned = taskPlanned;
            this.WatchId = watchId;
            this.AnalysisiId = analysisId;
        } 
    }
}
