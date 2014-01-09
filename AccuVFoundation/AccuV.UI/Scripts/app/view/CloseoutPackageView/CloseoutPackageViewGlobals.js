// Global functions needed for the Closeout Package View
// Cannot be wrapped inside a Module because some template methods have to be in the global space

// Template method
// "View Files" button

var getCPVOptions = function (data) {
	var docReqId = data[XMLEncode("doc_req_id")];
	var docId = data[XMLEncode("Document ID")];
	var docType = data[XMLEncode("Document Type")];
	var docName = data[XMLEncode("Document Name")];
	var site = data[XMLEncode("Site Number")];
	var task = data[XMLEncode("Task")];	
	var notApplicableValue = data["not_applicable"];
	var notApplicable = (notApplicableValue.toLowerCase() == "false") ? false : true;
	
	var reqDocumentDom = "<a class='k-button CPViewFiles' title='View Files' \
								style='min-width:28px;margin:1px;' \
								data-doc_req_id='{0}' \
								data-doc_id='{1}' \
								data-doc_type='{2}' \
								data-doc_name='{3}'\
								data-site='{4}' \
								data-task='{5}'> \
							<span class='k-icon k-i-folder-up'> \
						  </span></a>";
						  
	reqDocumentDom = reqDocumentDom.format(docReqId, docId, docType,docName, site, task);
	
	if(userLevel == USER_LEVEL_ADMIN)
	{
		var naDom =	"<a class='k-button CPVToggleApplicable' title='{4}' \
							style='min-width:28px;margin:1px;' \
							data-doc_req_id='{0}' \
							data-doc_id='{1}' \
							data-not_applicable='{2}'> \
						<span class='k-icon k-i-{3}'> \
					</span></a>";
		
		if(notApplicable)
		{
			reqDocumentDom += naDom.format(docReqId, docId, "true", "tick", "Mark as Applicable");
		}
		else
		{
			reqDocumentDom += naDom.format(docReqId, docId, "false", "close", "Mark as Not Applicable");
		}

	}
	
	return reqDocumentDom;
};

// Template methods
// "Approved Files"
// "Waiting Approval Files"

var getCPVFilesAndStatus = function(data)
{
	var returnDom = "";
	var propFileName = XMLEncode("files names");
	var propFileID = XMLEncode("files ids");
	var propFileStatus = XMLEncode("files statuses");
	
	if(data.hasOwnProperty(propFileName) && data[propFileName] != null)
	{
		var fileNames = data[propFileName].split("$$$");
		var fileIds = data[propFileID].split("$$$");
		var fileStatuses = data[propFileStatus].split("$$$");
		
		for(var i = 0; i < fileNames.length; i++)
		{	
			returnDom += "<a href='#' \
							data-file_name='{0}' \
							data-file_id='{1}' \
							class='CPVFileNameLink blue'>{0}</a>".format(fileNames[i],fileIds[i]);
			
			returnDom += " <span>({0})</span>".format(fileStatuses[i]);
			
			if(i < fileNames.length - 1)
				returnDom += "<br/>";
		}
	}
	return returnDom;
}

// Template methods
// ACTUAL / FORECASTED Dates
// The values for the actual/forecasted will be displayed in a single column

var getCPVDateColumnValue = function (data) {
	var dateActual = data[XMLEncode("Actual Date")];
	var dateForecast = data[XMLEncode("Forecast Date")];
	
	if(dateActual != null && dateActual != "null")
		return "{0}(A)".format(dateActual);
	else if(dateForecast != null && dateForecast != "null")
		return "{0}(F)".format(dateForecast);
		
	return "";
};