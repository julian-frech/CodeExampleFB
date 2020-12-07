using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseWatcher.Model
{


    [Table("DataBaseWatcher", Schema = "configuration")]
    public class DataBaseWatcherObj
    {

        [Key, Column(Order = 0)] public int WatchId { get; set; }
        public string TableNameSource { get; set; }
        public string TableNameTarget { get; set; }
        public string ColumnNamesSource { get; set; }
        public string ColumnNamesTarget { get; set; }
        public string FilterSource { get; set; }
        public string FilterTarget { get; set; }
        public string AggregationInterval { get; set; }
        public int AppID { get; set; }
        public string AppName { get; set; }
        public string Parameter { get; set; }
        public string SourceTable1 { get; set; }
        public string TargetTable1 { get; set; }
        public DateTime TaskPlanned { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime ITS { get; set; }
        public DateTime UTS { get; set; }
        public string Status { get; set; }
        public string QueryExceptCommand { get; set; }
        public int TaskId { get; set; }
        public int AnalysisId { get; set; }
        public int UniqueIdentifier { get; set; }
        public string UniqueColumnName { get; set; }
        public string TargetTableGroupe { get; set; }
        public string SourceTableGroupe { get; set; }


        public List<string> FlowDataParameter { get; set; }

        public DataBaseWatcherObj(int watchId, string tableNameSource, string tableNameTarget, string columnNamesSource, string columnNamesTarget, string filterSource, string filterTarget, string aggregationInterval,int appID, string appName, string parameter, string sourceTable1, string targetTable1,DateTime taskPlanned, DateTime validFrom, DateTime validTo, DateTime iTS, DateTime uTS, string status, string queryExceptCommand, List<string> flowDataParameter, int taskId, int analysisId, int uniqueIdentifier, string uniqueColumnName, string sourceTableGroupe, string targetTableGroupe)
        {
            this.WatchId = watchId;
            this.TableNameSource = tableNameSource;
            this.TableNameTarget = tableNameTarget;
            this.ColumnNamesSource = columnNamesSource;
            this.ColumnNamesTarget = columnNamesTarget;
            this.FilterSource = filterSource;
            this.FilterTarget = filterTarget;
            this.AggregationInterval = aggregationInterval;
            this.AppID = appID;
            this.AppName = appName;
            this.Parameter = parameter;
            this.SourceTable1 = sourceTable1;
            this.TargetTable1 = targetTable1;
            this.TaskPlanned = taskPlanned;
            this.ValidFrom = validFrom;
            this.ValidTo = validFrom;
            this.ITS = iTS;
            this.UTS = uTS;
            this.Status = status;
            this.QueryExceptCommand = queryExceptCommand;
            this.FlowDataParameter = flowDataParameter;
            this.TaskId = taskId;
            this.AnalysisId = analysisId;
            this.UniqueIdentifier = uniqueIdentifier;
            this.UniqueColumnName = uniqueColumnName;
            this.TargetTableGroupe = targetTableGroupe;
            this.SourceTableGroupe = sourceTableGroupe;
        }

    }
}
