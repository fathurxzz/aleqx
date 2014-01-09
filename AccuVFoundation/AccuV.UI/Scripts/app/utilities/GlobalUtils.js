// Application Constants
var API_URI = "http://localhost/ndportal/api";
//var API_URI = "../../../../NDPortalAPI/api";
var PAGE_SIZE = 25; 

// In case the application is accessed using a direct link
// the HostWebUrl will have the same value with AppWebUrl
// So be careful when using these two members
var HostWebUrl = '';
var AppWebUrl = '';

var DOC_TYPE_PHOTO = "photo";
var DOC_TYPE_DOCUMENT = "document";

var GROUP_TRACKER_MANAGERS_ATT = "NDPTrackerManagersATT";
var GROUP_TRACKER_MANAGERS_SAMSUNG = "NDPTrackerManagersSamsung";
var GROUP_ADMINISTRATORS = "NDPAdministrators";
var GROUP_REPORTS_SAMSUNG = "NDPReportsSamsung";
var GROUP_REPORTS_ATT = "NDPReportsATT";
var GROUP_REPORTS_FINANCIAL = "NDPReportsFinancial";
var GROUP_EXECUTIVE_REPORTS = "NDPExecutiveReports";
// Members of this group have full access to the "Milestone Tracker"
// as if they are logged as admins
var GROUP_DATA = "AccuV Data and Documentation";
// Members of this group will have access to the General Contractors Management screen
var GROUP_CLOSEOUT_TEAM = "AccuV Closeout Team";

// Function is checking if the current logged in user is included in the SharePoint Group name sent as parameter
var allowed = function (groupName) {
	if (userGroups != null && userGroups.length > 0) {        
	
		// Exception 1
		// If the user is part of the Data and Documentation
		// and we're verifying the Tracker Managers (ATT or SRT)
		// we should return True because the Data & Doc group are 
		// administrators of the Milestone Trackers
		if((groupName == GROUP_TRACKER_MANAGERS_ATT || groupName == GROUP_TRACKER_MANAGERS_SAMSUNG)
			&& userGroups.indexOf(GROUP_DATA) >= 0)
			return true;
	
		// Exception 2
		// Always allow the user if he is part of the NDPAdministrators group
        if (userGroups.indexOf(groupName) >= 0 || userGroups.indexOf(GROUP_ADMINISTRATORS) >= 0)
            return true;
    }
    return false; 
}


// the template method for the Task columns of the spreadhseet view
// has to be global, cannot be used inside a module
var getTaskValue = function (data, colName) {
    
    var resultContainer = $("<div></div>")
    var paramDate = data[colName];
    var cellHasValue = (paramDate != null);

	var dateStringContainer = $("<div></div>");
	dateStringContainer.css("margin", "-7px -7px -7px -7px");
    dateStringContainer.css("padding", "6px 0px 0px 5px"); 
    dateStringContainer.height(25);

    if (cellHasValue) {
        // paramDate.setMinutes(paramDate.getMinutes() - paramDate.getTimezoneOffset());
        // var taskDate = paramDate;

        var taskDate = noon(paramDate);
        var currentDate = noon(new Date());
        var currentDateFutureFive = new Date();
        currentDateFutureFive.setDate(currentDate.getDate() + 5);
        
        var formattedDate = kendo.toString(taskDate, 'MM/dd/yyyy');
        
        

        // Color the cell ?
        if (isCellColoringRequired(data, colName)) {
            if (taskDate < currentDate) {
                dateStringContainer.css("background-color", "#FFA8A8");
            } else if (taskDate >= currentDate && taskDate <= currentDateFutureFive) {
                dateStringContainer.css("background-color", "#FFF39E");
            }
        }
        dateStringContainer.append(formattedDate);
    }
    
    resultContainer.append(dateStringContainer);
    
    // include the Comments button in the cell
    var cellButton = cellPropertiesButton(data, colName, cellHasValue);
    if(cellButton != null)
	    resultContainer.append(cellButton);
    
    return $("<div/>").append(resultContainer).html();
    
};
var isCellColoringRequired = function(data, colName, cellHasValue){
    if(colName.indexOf("Forecasted") > 0)
    {
        var actualColName = colName.replace("Forecasted","Actual");
        if (data[actualColName] == null || data[actualColName].length == 0)
            return true;
    }
    return false;
}
var cellPropertiesButton = function(data, colName, cellHasValue){
    	
	// add an additional button to every Task Cell
    // and attach the required info to it
    var site = data.Site;
	
	// The column name is XML Encoded: "Task_x0020_105_x002D__x0020_Forecasted"
	// After we decode it we will have "Task 105-SA105 Forecasted"
	// We therefore have to eliminate the the prefix "Task " and suffix " Forecasted"/" Actual"
	
	var taskId = '';
	var commentOn = ''; // This will be ACTUAL or FORECASTED
	
	taskId = XMLDecode(colName).replace("Task ","");
	
	if(taskId.indexOf("Forecasted") > 0)
	{
		taskId = taskId.replace(" Forecasted","");
		commentOn = "FORECASTED";
	}
	else if(taskId.indexOf("Actual") > 0)
	{
		taskId = taskId.replace(" Actual","");
		commentOn = "ACTUAL";
	}
	
	
	if(taskId.length > 0)
	{
		var commentButton = $("<button class='k-button' data-cellproperties data-site='" + site + "' data-task='" + taskId + "' data-commenton='" + commentOn + "'>...</button>")
		commentButton.css("min-width",22);
		commentButton.css("width",22);
		commentButton.css("height",22);
		//commentButton.css("background-image","url('../Content/Assets/comment18x18.png')");
		//commentButton.css("background-repeat","no-repeat");
		commentButton.css("padding","0px");
		commentButton.css("float","right");
		commentButton.css("margin-top","-19px");
		
		return commentButton;
	}
	return null;
};



// adjust the date received as parameter to be at noon
// in order to avoid timezone issues
var noon = function (date) {
    // check if it's a valid date
    if (date && date.getMonth) {
        var dateAtNoon = new Date(date.getFullYear(), date.getMonth(), date.getDate(), 12, 0, 0, 0);
        return dateAtNoon;
    }
    return date;
}

// Takes an URL as input and a params object.
// Each property in the params is added to the url as query string parameters
var encodeURL = function(url, params) {
	var res = url;
	var k, i = 0;
	var firstSeparator = (url.indexOf("?") === -1) ? '?' : '&';
	for(k in params) {
		res += (i++ === 0 ? firstSeparator : '&') + encodeURIComponent(k) + '=' + encodeURIComponent(params[k]);
	}
	return res;
}

// Function to retrieve a query string value.
var getQueryStringParameter = function (paramToRetrieve) {
    if(document.URL.indexOf("?") == -1)
    	return null;
    
    var params =
        document.URL.split("?")[1].split("&");
    var strParams = "";
    for (var i = 0; i < params.length; i = i + 1) {
        var singleParam = params[i].split("=");
        if (singleParam[0] == paramToRetrieve)
            return singleParam[1];
    }
}

var digits = function (input) {
    return parseInt(input.toString().replace(/[^-\d\.]/g, ''));
}

// Encode Fields
var XMLEncode = function (toEncode) {

    var charToEncode = toEncode.split('');
    var encodedString = "";

    for (i = 0; i < charToEncode.length; i++) {
        encodedChar = escape(charToEncode[i]);

        if (encodedChar.length == 3) {
            encodedString += encodedChar.replace(/%/gi, "_x00") + "_";
        }
        else if (encodedChar.length == 5) {
            encodedString += encodedChar.replace(/%u/gi, "_x") + "_";
        }
        else {
            encodedString += encodedChar;
        }
    }
    
    encodedString = encodedString.replace(/\./g,"_x002E_");
    encodedString = encodedString.replace(/\-/g,"_x002D_");

    return encodedString;
}

// Decode Fields
var XMLDecode = function (toDecode) {
	var decodedString = toDecode;
	decodedString = decodedString.replace(/_x002E_/gi,".");
    decodedString = decodedString.replace(/_x002D_/gi,"-");
    decodedString = toDecode.replace(/_x/gi, "%u").replace(/_/gi, "");
    return unescape(decodedString);
}

// Utility to load JavaScript external file
function loadXMLDoc(dname) {
    if (window.XMLHttpRequest) {
        xhttp = new XMLHttpRequest();
    }
    else {
        xhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xhttp.open("GET", dname, false);
    xhttp.send();
    return xhttp.responseXML;
}

// Extract the Host and App URL
var HostWebUrl = getQueryStringParameter("SPHostUrl");
var AppWebUrl = getQueryStringParameter("SPAppWebUrl");

HostWebUrl = decodeURIComponent(HostWebUrl);
AppWebUrl = decodeURIComponent(AppWebUrl);