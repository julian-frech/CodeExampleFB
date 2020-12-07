import os
import csv
import sys
import json
import shutil
import errno
import re
import time
import argparse
import logging
import logging.handlers
from datetime import datetime

class Helper:
    """Helper class to evaluate configuration strings, clear target location and create target folders.
    """

    def __init__(self):
        """Constructor to initialize Helper class.
        """
        self.data = []

    def EvalToList(self, input):
        """Evaluation function
        Args:
            input (string): string representation of a list, or list of tuples
        Returns:
            List[],List[tuples(): applies formatting based on the string input
        """
        return eval("[%s]" % input)

    def ClearLocation(self, Target):
        """Clears location, removes file based on provided target variable
        Args:
            Target (string): string representation of folder location i.e. "08_CtrlM/FOLDER/FOLDER_CONCAT/"
        """
        if os.path.exists(Target):
            if os.path.isfile(Target) or os.path.islink(Target):
                os.remove(Target)  # remove the file
            elif os.path.isdir(Target):
                shutil.rmtree(Target)  # remove dir and all contains
            else:
                raise ValueError(
                    "file {} is not a file or dir.".format(Target))

    def CreatePathFile(self, path_file):
        """Checks if the path + filename already exists
        Args:
            filename (string): path/filename
        """
        if not os.path.exists(os.path.dirname(path_file)):
            try:
                os.makedirs(os.path.dirname(path_file))
            except OSError as exc:
                if exc.errno != errno.EEXIST:
                    raise
    
    def path_to_dict(self,path):
        """Creates a dictionary that represents the folder structure

        Args:
            path (string): Folder location

        Returns:
            [dictionary]: Returns dictionary of the folder structure
        """
        d = {"name": os.path.basename(path)}
        if os.path.isdir(path):
            d["type"] = "directory"
            d["children"] = [self.path_to_dict(os.path.join(path,x)) for x in os.listdir(path)]
        else:
            d["type"] = "file"
        return d

    def create_JsonFile(self,jsonName, location):
        """Creates json file in provided location

        Args:
            jsonName (string): Name of the json file without extension.
            location (string): Path where the file should be stored.
        """
        with open(jsonName + ".json", "w") as outfile:
            json.dump(self.path_to_dict(location), outfile)
