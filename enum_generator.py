#!/usr/bin/env python

import sys
from json_generator import icons, colors, letters, scopes, resources, whens


enum_names = ["ETileIcon", "ETileColor", "ETileLetter", "ETileScope", "ETileResource", "ETileWhen"]
enum_values = [icons, colors, letters, scopes, resources, whens]


enum_template = """
public enum %s
{
%s
}
"""

file_header = """
using System;
%s
"""


def create_enum(enum_name, values):
    enum_values = ',\n'.join(values).rstrip(',\n')
    return enum_template % (enum_name, enum_values)


def create_enums():
    i = 0
    for enum_name in enum_names:
        file_name = enum_name + ".cs"
        try:
            with open(file_name, 'w') as f:
                f.write(file_header % create_enum(enum_name, enum_values[i]))
        except Exception as e:
            print "Could not save %s in %s: %s" % (enum_name, file_name, e)
        i += 1


def main():
    return create_enums()


if __name__ == '__main__':
    main()
