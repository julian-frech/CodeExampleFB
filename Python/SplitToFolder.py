"""SplitToFolder module: \n
Takes a control-m xml as input and creates folder xmls named as $FOLDER_NAME.xml. 
The folder xmls are stored in the provided target folder $targetDir.
"""

from lxml import etree as ET
import errno
import shutil
import os
import logging
import logging.handlers
from datetime import datetime

from AuxiliaryModule import Helper

class SplitToFolder:

    APPLY_RULES_FOLDER = ("FOLDER", "SMART_FOLDER")

    def __init__(self, file, targetDir):
        """Constructor of the SplitToFolder class --> initializes the class with provided input arguments

        Args:
            file (string): Xml file to be split
        """
        self.data = []
        self.file = file
        self.targetDir = targetDir

    def ApplySplit(self):
        """Applies the split into folder xmls of the provided xml file and saves the folder.xmls in the target folder.
        """
        treeDeploy = ET.parse(self.file)
        rootDeploy = treeDeploy.getroot()
        xml = ("<DEFTABLE xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"Folder.xsd\">\n"+"</DEFTABLE>").encode()

        for FolderLevelKind in self.APPLY_RULES_FOLDER:
            for _first in rootDeploy.iter(FolderLevelKind):
                log.info(str(datetime.now()) + " :" + " Creating " + FolderLevelKind + " " + _first.get("FOLDER_NAME") + ".xml ....")
                parent = ET.fromstring(xml)
                filename = format(self.targetDir +"/" + _first.get("FOLDER_NAME") + ".xml")
                H.CreatePathFile(filename)
                parent.append(_first)
                tree = ET.ElementTree(parent)
                tree.write(filename, pretty_print=True, xml_declaration=True, encoding="utf-8")


log = logging.getLogger("SplitToFolder")

handler = logging.handlers.SysLogHandler(address="/var/run/syslog")

formatter = logging.Formatter(logging.BASIC_FORMAT)

handler.setFormatter(formatter)

log.addHandler(handler)

logging.basicConfig(level=logging.DEBUG)

H = Helper()

# s = SplitToFolder("BACKUP-PROD.xml","PROD_BACKUP")

# s.ApplySplit()
