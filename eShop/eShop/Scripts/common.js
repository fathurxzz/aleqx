﻿function tableChanged(dictionary, field) {
    var fieldSegments = field.name.split("_");

    var fieldName = fieldSegments[0];
    var id = fieldSegments[1];
    if (dictionary[id.toString()] == null) {
        dictionary[id.toString()] = {};
    }
    if (field.type == "checkbox") {
        dictionary[id.toString()][fieldName] = field.checked;
    }
    else {
        dictionary[id.toString()][fieldName] = field.value;
    }
}

function collectChanges(dictionary, hiddenId) {
    var hidden = $get(hiddenId);
    hidden.value = Sys.Serialization.JavaScriptSerializer.serialize(dictionary);
    return true;
}