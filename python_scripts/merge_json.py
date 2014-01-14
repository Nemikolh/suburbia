#!/usr/bin/env python

import json
import sys


def get_diff_between(a, b, depth):
    if isinstance(a, dict) and isinstance(b, dict):
        return get_diff_between_dict(a, b, depth)
    else:
        if a == b:
            return None
        else:
            return depth


def get_diff_between_dict(dic1, dic2, depth):
    keys = set(dic1.keys() + dic2.keys())
    for key in keys:
        if not key in dic1 or not key in dic2:
            return key
        diff = get_diff_between(dic1[key], dic2[key], depth + ".%s" % key)
        if diff is not None:
            return diff


def get_diff_between_tile(tile1, tile2):
    return get_diff_between(tile1, tile2, tile1['name'])


def merge_json(src1, src2, dest):
    try:
        f1 = open(src1, 'r')
        f2 = open(src2, 'r')
        f3 = open(dest, 'w')
    except Exception as e:
        print "Could not open one of the files: %s" % e
        return

    try:
        tiles1 = json.load(f1)
        tiles2 = json.load(f2)
    except Exception as e:
        print "Could not load valid json from file: %s" % e
        return

    # We concatenate the tiles
    tiles_ = tiles1 + tiles2
    # We remove pure duplicates
    tiles = []
    for tile in tiles_:
        if tile not in tiles:
            tiles.append(tile)

    # We look for "false" duplicates (ie same name but different values)
    name_to_tile = dict()
    duplicate_names = []
    for tile in tiles:
        name = tile['name']
        if name in name_to_tile:
            # We already encountered this tile
            print "*Warning* Found difference in \"%s\", deleting tile" % get_diff_between_tile(tile, name_to_tile[name])
            duplicate_names.append(name)
        name_to_tile[name] = tile

    # We remove the tiles to delete
    tiles = filter(lambda tile: tile['name'] not in duplicate_names, tiles)

    json.dump(tiles, f3)

    print "Successfully merged %d tiles in %s" % (len(tiles), dest)

    f1.close()
    f2.close()
    f3.close()


def main():
    if len(sys.argv) >= 4:
        src1 = sys.argv[1]
        src2 = sys.argv[2]
        dest = sys.argv[3]
        return merge_json(src1, src2, dest)

    else:
        print "Usage: ./merge_json.py src1 src2 dest"


if __name__ == '__main__':
    main()
