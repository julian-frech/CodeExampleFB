
########################################
############# Libraries ################
########################################

# pip install elementpath

import xml.etree.ElementTree as ET
import os
import csv
import configparser
import errno
import shutil
from difflib import SequenceMatcher
import logging
import logging.handlers
from datetime import datetime
import sys

class Helper:
    """Helper class to evaluate configuration strings, clear target location and create target folders.
    """

    def __init__(self):
        """Constructor
        """
        self.data = []

    def EvalToList(self, input):
        """Evaluation function

        Args:
            input ([string]): [string representation of a list, or list of tuples]

        Returns:
            [List[],List[tuples()]]: [applies formatting based on the string input]
        """
        return eval("[%s]" % input)

    def ClearLocation(self, Target):
        """Clears location based on provided target variable

        Args:
            Target ([string]): [string representation of folder location i.e. 'DeployXMLs/FOLDER/FOLDER_CONCAT/']
        """
        """ param <path> could either be relative or absolute. """
        if os.path.exists(Target):
            if os.path.isfile(Target) or os.path.islink(Target):
                os.remove(Target)  # remove the file
            elif os.path.isdir(Target):
                shutil.rmtree(Target)  # remove dir and all contains
            else:
                raise ValueError(
                    "file {} is not a file or dir.".format(Target))

    def CreatePathFile(self, path_file):
        """[Checks if the path + filename already exists]

        Args:
            filename ([string]): [path/filename]
        """
        if not os.path.exists(os.path.dirname(path_file)):
            try:
                os.makedirs(os.path.dirname(path_file))
            except OSError as exc:
                if exc.errno != errno.EEXIST:
                    raise

    def similar(self, a, b):
        return SequenceMatcher(None, a, b).ratio()


class CompareXMLs:

    helper = Helper()

    APPLY_RULES_FOLDER = ('FOLDER', 'SMART_FOLDER')
    APPLY_RULES_JOB = 'JOB'

    def __init__(self, configFile, environment):
        """Constructor of the CompareXMLs class --> initializes the class with provided input arguments

        Args:
            configFile ([string]): [name of the configuration file, relative to the current location of this script.]
            compareNew ([string]): [name of the new xml file, relative to the current location of this script.]
            compareOld ([string]): [name of the old xml file, relative to the current location of this script.]
            environment ([string]): [dev/sim/prod]
        """
        self.data = []
        self.WorkDir = os.path.abspath(os.getcwd())
        self.configFilePath = self.WorkDir + "/" + configFile
        self.Environment = environment
        self.FirstLvlTuple = 'FOLDER_NAME'
        self.SecondLvlTuple = ['PARENT_FOLDER',
                               'APPLICATION', 'SUB_APPLICATION', 'JOBNAME']
        self.ListFolderOld = []
        self.ListFolderNew = []
        self.ListUniqueTupleNew = []
        self.ListUniqueTupleOld = []

    def CreateCompareFile(self):
        JobDeltaReports = open("JobDeltaReports.csv", "a")
        with JobDeltaReports:
            writer = csv.writer(JobDeltaReports, quoting=csv.QUOTE_MINIMAL)
            writer.writerow(['Status', 'Smart/Folder', 'Folder_name', 'Sub_application',
                             'Job_name', 'Property', self.Environment + ' FUTURE', self.Environment + ' NOW'])

    def ConfigurationParser(self):
        """Parser for the configuration file defined in variable self.configFilePath."""
        configParser = configparser.RawConfigParser()
        configParser.read(self.configFilePath)
        self.JOB_PARAMS = self.helper.EvalToList(
            (configParser.get('UsedProperties', 'JOB_PARAMS')))

    def CreateUniqueTuples(self, root, arg):
        """[For a provided xml root it will create unique tuples for iteration over input variable args, that is based on public variables FirstLvlTuple and SecondLvlTuple.]

        Args:
            root ([Element]): [root Element of xlm ElementTree]
            arg ([string]): [description]

        Returns:
            [List]: [List of tuples of first and second level xml depth, as provided on the atomic property in FirstLvlTuple and SecondLvlTuple.]
        """
        TupleList = []
        for firstLevel in root.iter(arg):
            for secondLevel in firstLevel.iter(self.APPLY_RULES_JOB):
                tempTuple = (firstLevel.get(self.FirstLvlTuple), secondLevel.get(self.SecondLvlTuple[1]), secondLevel.get(
                    self.SecondLvlTuple[2]), secondLevel.get(self.SecondLvlTuple[3]))
                TupleList.append(tempTuple)
        return TupleList

    def DifferenceMethod(self, List1, List2):

        setList1 = set(List1)
        setList2 = set(List2)
        # List1 not in List2 ---> Added
        Additional = [x for x in setList1 if x not in setList2]
        # List2 not in List1 ---> Removed
        Removed = [x for x in setList2 if x not in setList1]

        return (Additional, Removed)

    def elements_equal_OLD(self, e1, e2, intersect, JOBNAME):
        keysSet = set(e2.attrib.keys())
        if type(intersect) != type(None):
            intersectSet = set(intersect)
            consideredKeys = [x for x in keysSet if x in intersectSet]
        else:
            consideredKeys = keysSet
        for keys in consideredKeys:
            try:
                if e2.attrib[keys] != e1.attrib[keys]:
                    return (keys, e1.attrib[keys], e2.attrib[keys])
            except:
                print("Fail at: " + keys )

    def list_diff(self, list1, list2):
        out = []
        for ele in list1:
            if not ele in list2:
                out.append(ele)
        for ele in list2:
            if not ele in list1:
                out.append(ele)
        return out

    def elements_equal(self, e1, e2, intersect, JOBNAME):

        ListFeedback = []

        count = 0
        ListNew = []
        ListOld = []
        # print(JOBNAME)
        for item1 in e1:
            tmp = list(item1.items())
            tmp.append(item1.tag)
            ListNew.append(tmp)
        for item2 in e2:
            tmp = list(item2.items())
            tmp.append(item2.tag)
            ListOld.append(tmp)

        list_differenceNew = [item for item in ListNew if item not in ListOld]
        list_differenceOld = [item for item in ListOld if item not in ListNew]

        if(len(list_differenceNew) > 0):
            for item1 in list_differenceNew:
                count += 1
                CompareList = []
                for item2 in list_differenceOld:
                    if item1[-1] == item2[-1]:
                        if (self.helper.similar(str(item1), str(item2))/len(set(item1) ^ set(item2)) > 0.2):
                            CompareList.append((self.helper.similar(str(item1), str(
                                item2))/len(set(item1) ^ set(item2)), (item1, item2)))
                            if (self.helper.similar(str(item1), str(item2))/len(set(item1) ^ set(item2)) > 0.4):
                                list_differenceOld.remove(item2)
                CompareList.sort(key=lambda x: x[0], reverse=True)
                if(len(CompareList) == 0 or type(CompareList) == type(None)):
                    ListFeedback.append(
                        ('ADDED', JOBNAME, str(item1[-1]), str(item1), ""))
                else:
                    try:
                        print('--------------NEW TRY------------------' + JOBNAME)
                        tmpPerc = str(list(list(CompareList)[0])[0])
                        tmpNew = str(list(list(CompareList)[0][1])[0])
                        tmpOld = str(list(list(CompareList)[0][1])[1])
                        #tmpJoin = str.join(' ', (tmpPerc, tmpNew))
                        tmpJoin = tmpNew
                        ListFeedback.append(
                            ('CHANGED', JOBNAME, str(item1[-1]), tmpJoin, tmpOld))
                    except:
                        print('--------------NEW EXCEPTION------------------')
                        print(item1)
                        print(
                            '--------------NEW FINISH------------------ end item: ' + str(count))

            for item1 in list_differenceOld:
                count += 1
                CompareList = []
                for item2 in list_differenceNew:
                    if item1[-1] == item2[-1]:
                        CompareList.append(set(item1) ^ set(item2))
                CompareList.sort(key=lambda x: len(x))
                if(len(CompareList) == 0 or type(CompareList) == type(None)):
                    # print('------------OLD--------------------')
                    ListFeedback.append(
                        ('REMOVED', JOBNAME, str(item1[-1]), str(item1), ""))
                    # print('------------OLD FINISH-------------------- end item: ' + str(count))
        return ListFeedback

    def CompareOldNew(self, CompareNew, CompareOld):
        

        
        self.ConfigurationParser()
        JobDeltaReports = open("JobDeltaReports.csv", "a")
        
        # Read XMLS
        treeNew = ET.parse(CompareNew)  # new
        rootNew = treeNew.getroot()
        treeOld = ET.parse(CompareOld)  # old
        rootOld = treeOld.getroot()

        
        with JobDeltaReports:
            writer = csv.writer(JobDeltaReports, quoting=csv.QUOTE_MINIMAL)
            for FolderLevelKind in self.APPLY_RULES_FOLDER:
                print('################ ' +
                      FolderLevelKind + ' ################ ')

                ########################################################
                ############# Jobs removed and added  ##################
                ########################################################
                """[Create unique tuples of (Application,Sub_Application,Job)]"""
                TupleListOld = self.CreateUniqueTuples(
                    rootOld, FolderLevelKind)
                TupleListNew = self.CreateUniqueTuples(
                    rootNew, FolderLevelKind)

                Added_Removed = self.DifferenceMethod(
                    TupleListNew, TupleListOld)

                for item in Added_Removed[1]:
                    writer.writerow(['Removed', FolderLevelKind, item[0], item[2],
                                     item[3], '---', '---', '---'])
                for item in Added_Removed[0]:
                    writer.writerow(['Added', FolderLevelKind, item[0], item[2],
                                     item[3], '---', '---', '---'])

                ########################################################
                ############# Job Level Delta  #########################
                ########################################################

                JobNew = rootNew.findall('./' + FolderLevelKind+"/JOB")

                for JOB in JobNew:
                    JobOld = rootOld.find('./' + FolderLevelKind+'/JOB' + '[@'+self.SecondLvlTuple[3]+'=' + "'" + JOB.get(self.SecondLvlTuple[3]) + "'" + ']' + '[@'+self.SecondLvlTuple[2]+'=' + "'" + JOB.get(
                        self.SecondLvlTuple[2]) + "'" + ']' + '[@'+self.SecondLvlTuple[1]+'=' + "'" + JOB.get(self.SecondLvlTuple[1]) + "'" + ']' + '[@'+self.SecondLvlTuple[0]+'=' + "'" + JOB.get(self.SecondLvlTuple[0]) + "'" + ']')
                    if type(JobOld) != type(None):
                        difference = self.elements_equal_OLD(
                            JOB, JobOld, self.JOB_PARAMS, JOB.get('JOBNAME'))
                        if type(difference) != type(None):
                            writer.writerow(['Changed', FolderLevelKind, JobOld.get(self.SecondLvlTuple[0]), JobOld.get(self.SecondLvlTuple[2]),
                                             JobOld.get(self.SecondLvlTuple[3]), difference[0], difference[1], difference[2]])

                ########################################################
                ############# Variable Level Delta  ####################
                ########################################################
                for thirdLvl in JobNew:
                    fourthLvlNew = rootNew.findall('./' + FolderLevelKind+'/JOB' + '[@'+self.SecondLvlTuple[3]+'=' + "'" + thirdLvl.get(self.SecondLvlTuple[3]) + "'" + ']' + '[@'+self.SecondLvlTuple[2]+'=' + "'" + thirdLvl.get(
                        self.SecondLvlTuple[2]) + "'" + ']' + '[@'+self.SecondLvlTuple[1]+'=' + "'" + thirdLvl.get(self.SecondLvlTuple[1]) + "'" + ']' + '[@'+self.SecondLvlTuple[0]+'=' + "'" + thirdLvl.get(self.SecondLvlTuple[0]) + "'" + ']/*')
                    fourthLvlOld = rootOld.findall('./' + FolderLevelKind+'/JOB' + '[@'+self.SecondLvlTuple[3]+'=' + "'" + thirdLvl.get(self.SecondLvlTuple[3]) + "'" + ']' + '[@'+self.SecondLvlTuple[2]+'=' + "'" + thirdLvl.get(
                        self.SecondLvlTuple[2]) + "'" + ']' + '[@'+self.SecondLvlTuple[1]+'=' + "'" + thirdLvl.get(self.SecondLvlTuple[1]) + "'" + ']' + '[@'+self.SecondLvlTuple[0]+'=' + "'" + thirdLvl.get(self.SecondLvlTuple[0]) + "'" + ']/*')
                    if len(fourthLvlOld) > 0 and type(fourthLvlOld) != type(None):
                        difference = self.elements_equal(
                            fourthLvlNew, fourthLvlOld, None, thirdLvl.get('JOBNAME'))
                        if(type(difference) != type(None) and len(difference) > 0):
                            for item in difference:
                                writer.writerow([item[0], FolderLevelKind, thirdLvl.get(self.SecondLvlTuple[0]), thirdLvl.get(self.SecondLvlTuple[2]),
                                                 thirdLvl.get(self.SecondLvlTuple[3]), item[2], item[3], item[4]])


log = logging.getLogger("CompareTool")

handler = logging.handlers.SysLogHandler(address = '/var/run/syslog')

formatter = logging.Formatter(logging.BASIC_FORMAT)

handler.setFormatter(formatter)

log.addHandler(handler)

logging.basicConfig(level=logging.DEBUG)


# try:
#     start_time = datetime.now()

#     log.info(str(start_time) + " :" + " CompareTool started...." )

#     _helper = Helper()
    
#     _helper.ClearLocation("JobDeltaReports.csv")

#     x.CompareOldNew()
    
#     end_time = datetime.now()

#     log.info(str(end_time) + " :" + " CompareTool finished...." )

#     log.info("Execution time --- %s ---" % (end_time - start_time))

#     sys.exit(0)

# except Exception as e:
#     log.exception("Exception triggered: " + str(e))
    
#     end_time = datetime.now()

#     log.info(str(end_time) + " :" + " CompareTool finished...." )

#     log.info("Execution time --- %s ---" % (end_time - start_time))

#     sys.exit(1)
