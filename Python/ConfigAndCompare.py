"""
Compare two Control-M XMLs and print output to csv.
Option1: Compare master before development with afterwards --> Config missing
Option2: Compare backup prod with configured master after development --> Need to split prod to folder and configure first
"""

"""
Implementation of Option2:
1. Split Backup XML to Folder XML
2. Run XmlManager.py to create Config_Folder XMLs of master code
3. Loop to compare Folder.xml and Configured_Folder.xml
"""


"""Implements moduls: ConcatToFolder & XmlReplace\n
    the XmlManager is callable and applies the moduls ConcatToFolder as well as XmlReplace to all subapplications
    """


import xml.etree.ElementTree as ET
import os
import json
import csv
import configparser
import sys
import shutil
import errno
import datetime
import re
import time
import argparse
import logging
import logging.handlers
from datetime import datetime
import ConcatToFolder
import XmlReplace
import CompareTool
import SplitToFolder as Splitter
from AuxiliaryModule import Helper
parser = argparse.ArgumentParser()
parser.add_argument('--target', type=str)
""" ICOS1 or ICOS2 """
parser.add_argument('--env', type=str)
""" dev sim or prod """
parser.add_argument('--config', type=str)
""" SearchAndReplaceConfig.txt """

args = parser.parse_args()

target = args.target
env = args.env
env = env.upper()
relativeConfigFilePath = args.config

log = logging.getLogger("ConfigAndCompare")

handler = logging.handlers.SysLogHandler(address='/var/run/syslog')

formatter = logging.Formatter(logging.BASIC_FORMAT)

handler.setFormatter(formatter)

log.addHandler(handler)

logging.basicConfig(level=logging.DEBUG)

try:
    start_time = datetime.now()

    log.info(str(start_time) + " :" + " ConfigAndCompare started....")

    H = Helper()
    """ConcatToFolder: Creates data.json with all SubApplXmls, then concatenates them to FolderXmls and creates a list of all FolderXmls as FolderList.txt
    """
    log.info(str(datetime.now()) + " : ConcatToFolder.py started....")

    # H.create_JsonFile("data","./08_CtrlM/")

    x = ConcatToFolder.SubToFolder('data.json', './08_CtrlM/')

    x.CreateFolderXMLs()

    log.info(str(datetime.now()) + " : ConcatToFolder.py finished....")

    """Reading FolderList and define list of tuples for XmlReplace
    """

    H.CreatePathFile(r"./08_CtrlM/Configured/")

    TupleFolderList = []

    ConfiguredFileList = open("ConfiguredFiles.txt", "a")

    for _folderXml in x.FolderList:
        _fileName = _folderXml.split("/")[-1]
        _tuple = (_folderXml, r"./08_CtrlM/Configured/Configured_" + _fileName)
        TupleFolderList.append(tuple(_tuple))
        ConfiguredFileList.write(
            r"./08_CtrlM/Configured/Configured_" + _fileName + "\n")

    """XmlReplace: Takes list of tuples to apply configuration to and store as afterwards.
    """
    log.info(str(datetime.now()) + " : XmlReplace.py started....")
    y = XmlReplace.XmlReplace(relativeConfigFilePath, target, env)

    for item in TupleFolderList:
        log.info(str(datetime.now()) +
                 ": Applying configuration file to " + item[0] + ".")
        y.ExecConfig(item[0], item[1])

    log.info(str(datetime.now()) + " : XmlReplace.py finished....")

    log.info(str(datetime.now()) + " :" + " SplitToFolder started....")

    H.ClearLocation("./PROD_BACKUP")

    H.CreatePathFile("./PROD_BACKUP")

    s = Splitter.SplitToFolder("BACKUP-PROD.xml", "PROD_BACKUP")

    s.ApplySplit()

    log.info(str(datetime.now()) + " :" + " SplitToFolder finished....")

    log.info(str(datetime.now()) + " :" + " CompareTool started....")

    H.ClearLocation("JobDeltaReports.csv")

    C = CompareTool.CompareXMLs("CompareToolConfig.txt", env)

    C.CreateCompareFile()

    countCompareSucc=0

    numberOfFiles = len(x.FolderList)

    for _folderXml in x.FolderList:
        _fileName = _folderXml.split("/")[-1]
        newFile = r"./08_CtrlM/Configured/Configured_" + _fileName
        oldFile = "./PROD_BACKUP/" + _fileName
        log.info(str(datetime.now()) + " :" + " Comparing: " + newFile + " vs. " + oldFile)
        try:
            C.CompareOldNew(newFile, oldFile)
            countCompareSucc = countCompareSucc +1
        except Exception as e:
            log.critical(str(datetime.now()) + " :" + " Comparing: " + newFile + " vs. " + oldFile + " failed!!")
            log.critical(str(datetime.now()) + " :" + str(e))
            continue

    log.info(str(datetime.now()) + " : Compared:" + str(countCompareSucc) + " of " + str(numberOfFiles) + " files.")
    log.info(str(datetime.now()) + " :" + " CompareTool finished....")
    end_time = datetime.now()

    log.info(str(end_time) + " :" + " ConfigAndCompare finished....")

    log.info("--- %s seconds ---" % (end_time - start_time))

    sys.exit(0)

except Exception as e:

    log.exception("Exception triggered: " + str(e))

    end_time = datetime.now()

    log.info(str(end_time) + " :" + " ConfigAndCompare finished....")

    log.info("Execution time --- %s ---" % (end_time - start_time))

    sys.exit(1)
