#########################################
######## Descriptive block ##############
#########################################
"""Contains classes: XmlReplace\n
    XmlReplace class: Takes xml file and configuration file as input and provides configured xml file as output
    """

########################################
############# Libraries ################
########################################
# pip install elementpath
# pip install python-csv
# pip install configparser

import xml.etree.ElementTree as ET
import os
import configparser
import datetime
import re
import logging
import logging.handlers
from datetime import datetime

from AuxiliaryModule import Helper

class XmlReplace:
    """Class for replacing strings in xml file based on configuration file
    """
    Encoding = "utf-8"
    """
    Encoding format of the output xml file.
    """
    helper = Helper()
    """
    Helper class of AuxiliaryModule is initiated.
    """
    APPLY_RULES_FOLDER = []
    """Options are FOLDER and/or SMART_FOLDER provided by the config file.
    """

    def __init__(self, configFile, target, env):
        """Constructor for XmlReplace class --> initializes the class based on input variables
        Args:
            fileSource (string): folder location of the source xml
            fileTarget (string): configured xml file name
            configFile (string): configuration file name
        """
        self.Target = target
        self.Env = env.upper()
        self.data = []
        self.WorkDir = os.path.abspath(os.getcwd())
        self.configFilePath = self.WorkDir + "/" + configFile

    def ConfigurationParser(self):
        """Parser for the configuration file defined in variable configFilePath.
        """

        ConfigString = self.Target+"_"+ self.Env
        FilterString = str("Filter_" + ConfigString)

        configParser = configparser.RawConfigParser()
        configParser.read(self.configFilePath)
        self.FILTER_FOLDER_MODUS = configParser.get(
            FilterString, "FILTER_FOLDER_MODUS")
        self.FOLDER_DATACENTER = configParser.get(
            FilterString, "FOLDER_DATACENTER")
        self.SMART_FOLDER_REMOVE = configParser.get(
            FilterString, "SMART_FOLDER_REMOVE")
        self.DUMMY_DEST_ALL = configParser.get(FilterString, "DUMMY_DEST_ALL")
        self.DUMMY_DEST = configParser.get(FilterString, "DUMMY_DEST")
        self.DUMMY_CC_DEST = configParser.get(FilterString, "DUMMY_CC_DEST")
        self.VARIABLE_NAME_FTP_HOSTS = configParser.get(
            FilterString, "VARIABLE_NAME_FTP_HOSTS")
        self.ENVIRONMENT_SUBJECT = configParser.get(
            FilterString, "ENVIRONMENT_SUBJECT")

        self.FILTER_FOLDER_ARTIFACT = self.helper.EvalToList(
            (configParser.get(FilterString, "FILTER_FOLDER_ARTIFACT")))
        self.DUMMY_MEMNAME = self.helper.EvalToList(
            (configParser.get(FilterString, "DUMMY_MEMNAME")))
        self.JOBNAME_TO_CONFIRM = self.helper.EvalToList(
            (configParser.get(FilterString, "JOBNAME_TO_CONFIRM")))
        self.DUMMY_FOLDER_NAME = self.helper.EvalToList(
            (configParser.get(FilterString, "DUMMY_FOLDER_NAME")))
        self.DUMMY_SUB_APPLICATION_NAME = self.helper.EvalToList(
            (configParser.get(FilterString, "DUMMY_SUB_APPLICATION_NAME")))
        self.REPLACE_WHOLE_WORD = self.helper.EvalToList(
            (configParser.get(FilterString, "REPLACE_WHOLE_WORD")))
        self.DUMMY_VARIABLE_SP = self.helper.EvalToList(
            (configParser.get(FilterString, "DUMMY_VARIABLE_SP")))
        self.DUMMY_SP_NAMES = self.helper.EvalToList(
            (configParser.get(FilterString, "DUMMY_SP_NAMES")))
        self.RUN_AS_REPLACE = self.helper.EvalToList(
            (configParser.get(FilterString, "RUN_AS_REPLACE")))
        self.JOBNAME_TO_DUMMY = self.helper.EvalToList(
            (configParser.get(FilterString, "JOBNAME_TO_DUMMY")))
        self.REMOVE_SUB_APPLICATION = self.helper.EvalToList(
            (configParser.get(FilterString, "REMOVE_SUB_APPLICATION")))

        self.FTP_HOST_SUBST = []
        self.DUMMY_SP_LIST = []
        for SP_DUMMIES in self.DUMMY_SP_NAMES:
            self.DUMMY_SP_LIST.append(
                ((self.DUMMY_VARIABLE_SP[0][0], SP_DUMMIES[0]), (self.DUMMY_VARIABLE_SP[0][1], SP_DUMMIES[1])))

    def ExecConfig(self, artifactXml, targetFileName):
        """Executes the configuration for the xml provided and outputs the configured xml with specified file name.
        Args:
            artifactXml (string): name of the xml file to be configured.
            targetFileName (string): name of the configured file.
        """
        log.info(str(datetime.now()) + " :" + " ExecConfig started....")
        
        self.ConfigurationParser()

       ########################################
       ####### Global Replacements ############
       ########################################
        with open(artifactXml, "r", encoding=self.Encoding) as file:
            Artifactory = file.read()

        for item in self.REPLACE_WHOLE_WORD:
            patternSearch = re.compile(item[0])
            patternReplace = item[1]
            Artifactory = re.sub(patternSearch, patternReplace, Artifactory)

        ########################################
        ############# Parse XML ################
        ########################################
        Artifactory = ET.XML(Artifactory)

        treeArtifactory = ET.ElementTree(Artifactory)
        rootArtifactory = treeArtifactory.getroot()

        #####################################################
        ############ SMART_FOLDER Folder Level ##############
        #####################################################
        if self.SMART_FOLDER_REMOVE == "1":
            self.APPLY_RULES_FOLDER.append("FOLDER")
            for FOLDER in rootArtifactory.iter("SMART_FOLDER"):
                rootArtifactory.remove(FOLDER)
        elif self.SMART_FOLDER_REMOVE != "1":
            APPLY_RULES_FOLDER = ("FOLDER", "SMART_FOLDER")

        for FolderLevelKind in APPLY_RULES_FOLDER:
            for FOLDER in rootArtifactory.iter(FolderLevelKind):
                FOLDER.set("DATACENTER", self.FOLDER_DATACENTER)

            #####################################################
            ########## Clean Artifact Folder Level ##############
            #####################################################

            FolderLvL = rootArtifactory.findall("./" + FolderLevelKind)

            if self.FILTER_FOLDER_MODUS == "REMOVE":
                for removeFolder in self.FILTER_FOLDER_ARTIFACT:
                    tmp = rootArtifactory.find("./" + FolderLevelKind + "[@FOLDER_NAME=" + "'" + removeFolder + "'" + "]")
                    if(type(tmp) != type(None)):
                        rootArtifactory.remove(tmp)
            elif self.FILTER_FOLDER_MODUS == "USE":
                for FOLDER in FolderLvL:
                    if FOLDER.get("FOLDER_NAME") not in self.FILTER_FOLDER_ARTIFACT:
                        rootArtifactory.remove(FOLDER)


            ################ Remove SubApplication #####################

            for FOLDER in FolderLvL:
                for JOB in FOLDER:
                    if (JOB.get("APPLICATION"), JOB.get("SUB_APPLICATION")) in self.REMOVE_SUB_APPLICATION:
                        FOLDER.remove(JOB)

        #########################################################################
        ################ Dummies,TFP,RUN_AS, SubApplication #####################
        #########################################################################
            for _run_AS in self.RUN_AS_REPLACE:
                for runAsReplace in rootArtifactory.findall("./" + FolderLevelKind+"/JOB" + "[@RUN_AS=" + "'" + _run_AS[0] + "'" + "]"):
                    runAsReplace.set("RUN_AS", _run_AS[1])

            for item in self.DUMMY_SUB_APPLICATION_NAME:
                for SubApp in rootArtifactory.findall("./" + FolderLevelKind+"/JOB" + "[@PARENT_FOLDER=" + "'" + item[0] + "'" + "]" + "[@SUB_APPLICATION=" + "'" + item[1] + "'" + "]"):
                    SubApp.set("TASKTYPE", "Dummy")

            for item in self.DUMMY_MEMNAME:
                for MemName in rootArtifactory.findall("./" + FolderLevelKind+"/JOB" + "[@MEMNAME=" + "'" + item + "'" + "]"):
                    MemName.set("TASKTYPE", "Dummy")

            for item in self.DUMMY_FOLDER_NAME:
                for FolderName in rootArtifactory.findall("./" + FolderLevelKind + "[@FOLDER_NAME=" + "'" + item + "'" + "]" + "/JOB"):
                    FolderName.set("TASKTYPE", "Dummy")

            for item in self.DUMMY_FOLDER_NAME:
                for FolderName in rootArtifactory.findall("./" + FolderLevelKind + "[@FOLDER_NAME=" + "'" + item + "'" + "]" + "/JOB"):
                    FolderName.set("TASKTYPE", "Dummy")

            for item in self.DUMMY_SP_NAMES:
                for FOLDER in FolderLvL:
                    for JOB in FOLDER:
                        for VARIABLE in JOB:
                            if(VARIABLE.get("VALUE") == item[1] and VARIABLE.get("NAME") == "%%DB-STP_NAME"):
                                JOB.set("TASKTYPE", "Dummy")


            if self.DUMMY_DEST_ALL == "1":
                for DOMAIL in rootArtifactory.findall("./" + FolderLevelKind+"/JOB/ON/DOMAIL"):
                    if type(DOMAIL.get("DEST"))!= type(None):
                        DOMAIL.set("DEST", self.DUMMY_DEST)
                        if type(DOMAIL.get("SUBJECT")) != type(None):
                            DOMAIL.set("SUBJECT", self.ENVIRONMENT_SUBJECT + " " + DOMAIL.get("SUBJECT"))
                        else:
                            DOMAIL.set("SUBJECT", self.ENVIRONMENT_SUBJECT + " " + "Missing Subject in ctrlm XML!")
                        if type(DOMAIL.get("CC_DEST")) != type(None):
                            DOMAIL.set(
                                "CC_DEST", self.DUMMY_CC_DEST)

            for VARIABLE in rootArtifactory.findall("./" + FolderLevelKind+"/JOB/VARIABLE" + "[@NAME=" + "'" + "%%FTP-LHOST" + "'" + "]"):
                VARIABLE.set("VALUE", self.VARIABLE_NAME_FTP_HOSTS)

            for VARIABLE in rootArtifactory.findall("./" + FolderLevelKind+"/JOB/VARIABLE" + "[@NAME=" + "'" + "%%FTP-RHOST" + "'" + "]"):
                VARIABLE.set("VALUE", self.VARIABLE_NAME_FTP_HOSTS)
                
            #####################################################
            ############ Set Job to Confirmation ################
            #####################################################
            for itemConfirm in self.JOBNAME_TO_CONFIRM:
                tmp1 = rootArtifactory.find("./" + FolderLevelKind+"/JOB" + "[@PARENT_FOLDER=" + "'" + itemConfirm[0] + "'" + "]" +
                                            "[@SUB_APPLICATION=" + "'" + itemConfirm[1] + "'" + "]" + "[@JOBNAME=" + "'" + itemConfirm[2] + "'" + "]")
                if(type(tmp1) != type(None)):
                    tmp1.set("CONFIRM", "1")
            for itemDummy in self.JOBNAME_TO_DUMMY:
                tmp2 = rootArtifactory.find("./" + FolderLevelKind+"/JOB" + "[@PARENT_FOLDER=" + "'" + itemDummy[0] + "'" + "]" +
                                            "[@SUB_APPLICATION=" + "'" + itemDummy[1] + "'" + "]" + "[@JOBNAME=" + "'" + itemDummy[2] + "'" + "]")
                if(type(tmp2) != type(None)):
                    tmp2.set("TASKTYPE", "Dummy")

        ############################################################
        ############### Create output file #########################
        ############################################################
            treeArtifactory.write(targetFileName, encoding=self.Encoding, xml_declaration=True)
        log.info(str(datetime.now()) + " :" + " ExecConfig finished....")

log = logging.getLogger("XmlReplaceLogger")

handler = logging.handlers.SysLogHandler(address = "/var/run/syslog")

formatter = logging.Formatter(logging.BASIC_FORMAT)

handler.setFormatter(formatter)

log.addHandler(handler)

logging.basicConfig(level=logging.DEBUG)

