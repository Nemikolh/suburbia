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
namespace AssemblyCsharp
{
%s
}
"""


def create_enum(enum_name, values):
    enum_values = ',\n'.join(values).rstrip(',\n')
    return enum_template % (enum_name, enum_values)


def create_enums(enum_loc):
    enums = []
    i = 0
    for enum_name in enum_names:
        enums.append(create_enum(enum_name, enum_values[i]))
        i += 1
    enums = "".join(enums)

    try:
        with open(enum_loc, "w") as f:
            f.write(file_header % enums)
    except Exception as e:
        print "Could not open %s: %s" % (enum_loc, e)
        return


def main():
    if len(sys.argv) >= 2:
        enum_loc = sys.argv[1]
        return create_enums(enum_loc)

    else:
        print "Usage: ./enum_generator.py enum_location"


if __name__ == '__main__':
    main()
