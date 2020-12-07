"""Implements moduls: ConcatToFolder & XmlReplace\n
    the XmlManager is callable and applies the moduls ConcatToFolder as well as XmlReplace to all subapplications 
    """
import sys
import argparse
import logging
import logging.handlers
from datetime import datetime

import ConcatToFolder
import XmlReplace
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

log = logging.getLogger("XmlManager")

handler = logging.handlers.SysLogHandler(address='/var/run/syslog')

formatter = logging.Formatter(logging.BASIC_FORMAT)

handler.setFormatter(formatter)

log.addHandler(handler)

logging.basicConfig(level=logging.DEBUG)

try:
    start_time = datetime.now()

    log.info(str(start_time) + " :" + " XmlManager started....")

    H = Helper()
    """ConcatToFolder: Creates data.json with all SubApplXmls, then concatenates them to FolderXmls and creates a list of all FolderXmls as FolderList.txt
    """
    log.info(str(datetime.now()) + " : ConcatToFolder.py started....")
    
    H.create_JsonFile("data","./08_CtrlM/")

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

    end_time = datetime.now()

    log.info(str(end_time) + " :" + " XmlManager finished....")

    log.info("--- %s seconds ---" % (end_time - start_time))

    sys.exit(0)

except Exception as e:

    log.exception("Exception triggered: " + str(e))

    end_time = datetime.now()

    log.info(str(end_time) + " :" + " XmlManager finished....")

    log.info("Execution time --- %s ---" % (end_time - start_time))

    sys.exit(1)
