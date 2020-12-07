"""Modul that contains ConcatToFolder class\n
based on a provided json file control-m folder.xmls are built.
"""

from lxml import etree as ET
import os
import json
import logging
import logging.handlers
from datetime import datetime
import sys
from AuxiliaryModule import Helper

class SubToFolder:
    """Class that concat subapplication level xmls to folder level xmls
    """
    APPLY_RULES_FOLDER = ('FOLDER','SMART_FOLDER')  # ('FOLDER','SMART_FOLDER')
    """Control-M knows folder and smart_folder
    """
    JsonName = 'name'
    """Based on the json file structure 
    """
    JsonBase = 'children'
    """Based on the json file structure 
    """
    JsonChild = 'children'
    """Based on the json file structure 
    """
    Encoding = 'utf-8'
    """Encoding to utf-8 can be adjusted if needed
    """
    FolderList = [] # Stores all FolderXml names
    """List of all folder xmls created. This list is used by the XmlManager to pass a list to the Control-M deployment
    """

    def __init__(self, jsonFile, target):
        """Constructor of class SubToFolder

        Args:
            jsonFile (string path): Path to file that contains the json based structure of Control-M.
            target (string): Name of the final xml output file.
        """
        self.JsonFile = jsonFile    # '04_CtrlM.json'
        self.Target = target  # '08_CtrlM/FOLDER/FOLDER_CONCAT/'
        self.data = []              #

    def JsonHandler(self):
        """Reads self.JsonFile and then loads its data.
        """
        self.File = open(self.JsonFile,)
        self.JsonData = json.load(self.File)

    def CreateFolderXMLs(self):
        """Loops through json hierachy and concats subapplication xmls to folder xmls.
        The first subapplication is used entirely for it's folder definition and then for all other subapplications only the jobs
        are added to the same xml. 
        """
        self.JsonHandler()

        try:
            for FolderLevelKind in self.JsonData[self.JsonBase]:
                """ FolderLevelKind element of (Folder,Smart_Folder) """
                for Folder in FolderLevelKind[self.JsonChild]:
                    """ Folder/Smart_Folder Level """
                    if Folder[self.JsonName] != '.DS_Store':
                        log.info('########################################')
                        log.info('Creating '+ FolderLevelKind[self.JsonName] + ': ' + Folder[self.JsonName] + '.xml')
                        treeBase = ET.parse('./08_CtrlM/' + FolderLevelKind[self.JsonName] + '/'+ Folder[self.JsonName] + '/' + Folder[self.JsonChild][0][self.JsonName])
                        rootBase = treeBase.getroot()
                        """ First subapplication xml defines the base for the folder xml """
                        filename = format(self.Target + Folder[self.JsonName] + '.xml')
                        H.CreatePathFile(filename)
                        self.FolderList.append(filename)
                        if(len(Folder[self.JsonChild][1:]))>0:
                            """ If there is more than one subapplication per folder, these are included into  $filename """
                            for SubApps in Folder[self.JsonChild][1:]:
                                log.info('Including: ' + SubApps[self.JsonName])
                                treeAdd = ET.parse('./08_CtrlM/' + FolderLevelKind[self.JsonName] + '/'+ Folder[self.JsonName] + '/' + SubApps[self.JsonName])
                                rootAdd = treeAdd.getroot()
                                for _first in rootBase.iter(FolderLevelKind[self.JsonName]):
                                    """ Only jobs are inserted into $filename, since the <Folder></Folder> part is distributed by the first subapplication """
                                    for _second in rootAdd.iterfind(FolderLevelKind[self.JsonName] +"/JOB"):                                     
                                        _first.insert(1,_second)
                                    treeBase.write(filename, pretty_print=True, xml_declaration=True,   encoding=self.Encoding)
                        else:
                            treeBase.write(filename, pretty_print=True, xml_declaration=True,   encoding=self.Encoding)
        except:
            log.info('########################################')
            log.critical("Error during concatination in CreateFolderXMLs definition!")
            log.info('########################################')
            raise



log = logging.getLogger("ConcatToFolderLogger")

handler = logging.handlers.SysLogHandler(address = '/var/run/syslog')

formatter = logging.Formatter(logging.BASIC_FORMAT)

handler.setFormatter(formatter)

log.addHandler(handler)

logging.basicConfig(level=logging.DEBUG)

H = Helper()
