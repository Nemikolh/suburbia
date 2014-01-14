#!/usr/bin/env python

import readline
import json
import shutil


icons = ["AIRPORT", "OFFICE", "RESTAURANT", "SCHOOL", "NEW_CAR_DEALERSHIP", "SKYSCRAPER", "NONE"]
colors = ["BLUE", "GREY", "YELLOW", "GREEN", "LAKE"]
letters = ["A", "B", "C", "BASE"]
scopes = ["ADJACENT", "ADJACENT_TO_OWN_LAKE", "GLOBAL", "OWN", "OTHER", "NONE"]
resources = ["INCOME", "REPUTATION", "POPULATION", "MONEY", "NONE"]
whens = ["ALWAYS", "AFTER", "AFTER_RED_LINE"]
types = icons + colors
attributes = ["name", "color", "icon", "price", "letter", "number", "immediate", "triggers"]

attribute_to_values = {
    "icons": icons,
    "colors": colors,
    "letters": letters,
    "scopes": scopes,
    "resources": resources,
    "whens": whens,
    "types": types,
    "names": None,
    "prices": None,
    "numbers": None
}


class Completer(object):
    def __init__(self, options):
        self.options = sorted(options + map(lambda s: s.lower(), options))

    def complete(self, text, state):
        if state == 0:
            if text:
                self.matches = [s for s in self.options if s and s.startswith(text)]
            else:
                self.matches = self.options[:]

        try:
            return self.matches[state]
        except IndexError:
            return None


def prompt_value(label, values, upper=True):
    if values is not None:
        completer = Completer(values)
        readline.set_completer(completer.complete)
    else:
        readline.set_completer(None)

    while True:
        value = raw_input(label)
        if values is None:
            return value
        if "NONE" in values and value == '':
            return "NONE"
        if value.upper() in values:
            return value.upper()
        if not upper:
            v = filter(lambda v: v.lower() == value.lower(), values)
            if len(v) == 1:
                return v[0]
        print "Invalid value. Possible values: %s" % values


def prompt_value_int(label):
    readline.set_completer(None)
    while True:
        try:
            return int(raw_input(label))
        except ValueError:
            print "Not a valid integer"


def create_effect(resource, value):
    return {
        "resource": resource,
        "value": value
    }


def create_trigger(type, scope, when, effect):
    return {
        "type": type,
        "scope": scope,
        "when": when,
        "effect": effect
    }


def prompt_effect(ask=True):
    if ask and raw_input("New effect?[y/N]") not in ["y", "Y"]:
        return "NONE"
    resource = prompt_value("Resource: ", resources)
    value = prompt_value_int("Value: ")
    return create_effect(resource, value)


def prompt_trigger():
    if raw_input("New trigger?[y/N]") not in ["y", "Y"]:
        return
    type = prompt_value("Type: ", types)
    scope = prompt_value("Scope: ", scopes)
    when = prompt_value("When: ", whens)
    print "Effect:"
    effect = prompt_effect(ask=False)
    return create_trigger(type, scope, when, effect)


def prompt_triggers():
    triggers = []
    while True:
        trigger = prompt_trigger()
        if trigger is None:
            break
        triggers += [trigger]
    return triggers


def prompt_tile():
    print "New Tile:"
    name = prompt_value("Name: ", None)
    color = prompt_value("Color: ", colors)
    icon = prompt_value("Icon: ", icons)
    price = prompt_value_int("Price: ")
    letter = prompt_value("Letter: ", letters)
    number = prompt_value_int("Number: ")

    print "Immediate effect:"
    immediate = prompt_effect()

    triggers = prompt_triggers()

    return {
        "name": name,
        "color": color,
        "icon": icon,
        "price": price,
        "letter": letter,
        "number": number,
        "immediate": immediate,
        "triggers": triggers
    }


def main():
    shutil.copyfile("tiles.json", "tiles.json_backup")

    with open("tiles.json") as f:
        tiles = json.load(f)

    while(True):
        try:
            operation = prompt_value("Create/Edit/Remove/List/Quit? ", ["CREATE", "EDIT", "REMOVE", "LIST", "QUIT"])

            if operation == "CREATE":
                tiles.append(prompt_tile())
            elif operation in ["EDIT", "REMOVE"]:
                name = prompt_value("Name: ", map(lambda tile: str(tile['name']), tiles), upper=False)
                tile = filter(lambda tile: tile['name'] == name, tiles)[0]
                print "Tile %s:" % name
                print tile

                if operation == "REMOVE":
                    if raw_input("Remove tile?[y/N]") in ["y", "Y"]:
                        tiles.remove(tile)

                else:
                    attribute = prompt_value("Attribute to edit:", attributes, upper=False)
                    if attribute == "triggers":
                        triggers = prompt_triggers()
                        tile["triggers"] = triggers

                    elif attribute == "immediate":
                        immediate = prompt_effect()
                        tile["immediate"] = immediate

                    else:
                        values = attribute_to_values[attribute + "s"]
                        value = prompt_value("%s:" % attribute, values)
                        tile[attribute] = value

            elif operation == "LIST":
                print "%d tiles in json file:" % len(tiles)
                print '; '.join(sorted(map(lambda tile: tile['name'], tiles)))

            elif operation == "QUIT":
                raise Exception

        except:
            break

    with open("tiles.json", "w") as f:
        f.write(json.dumps(tiles))
    print "Successfully saved %d tiles in %s" % (len(tiles), "tiles.json")


if __name__ == '__main__':
    readline.parse_and_bind('tab: complete')
    main()
