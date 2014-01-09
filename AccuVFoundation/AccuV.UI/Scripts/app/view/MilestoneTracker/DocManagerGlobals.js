// Global functions needed for the General Contractor workflow
// Cannot be wrapped inside a Module because some template methods have to be in the global space

// DOCUMENTS REQUIRED POPUP

var getDocReqOptions = function (data) {
	
	var reqDocumentDom = "<button class='k-button viewfiles' title='View Files' \
								style='min-width:28px;width:28px;' \
								data-doc_req_id='{0}' \
								data-doc_id='{1}' \
								data-doc_type='{2}'> \
							<span class='k-icon k-i-folder-up'> \
						  </span></button>";
						  
	reqDocumentDom = reqDocumentDom.format(data.doc_req_id, data.doc_id, data.type);
	return reqDocumentDom;
};

// Template method for the [Files] column which appears in the 
// documents required data grid - it basically creates a link for each file to be downloaded
var getDocReqFiles = function (data) {
	var cellContainerDom = $("<div></div>");
	if(data.upl_files != null && data.upl_files.length > 0)
	{
		var files = data.upl_files.split("$$$");
		var files_ids = data.upl_files_ids.split("$$$");
		for (var i=0; i < files.length; i++)
		{
			var file_name = files[i];
			var file_id = files_ids[i];
			
			var fileLinkDom = $("<a href='#' \
									data-file_name='{0}' \
									data-file_id='{1}' \
									class='reqFileNameLink blue'>{0}</a>".format(file_name,file_id));
			if(i > 0)
			{
				cellContainerDom.append("<br/>");
			}
			cellContainerDom.append(fileLinkDom[0].outerHTML);
		}
	}
	return cellContainerDom[0].outerHTML;
};

// FILE UPLOAD/DOWNLOAD POPUP

var STATUS_WAITING_APPROVAL = "Waiting Approval";
var STATUS_UPLOADED_TO_CLIENT = "Uploaded to Client Repository";
var STATUS_MODIFIED = "Modified";
var STATUS_APPROVED = "Approved";
var STATUS_REJECTED = "Rejected";
var STATUS_DELETED = "Deleted";

// Template method for the file name column (contains a download link)
var getCurrentUploadedDocFileName = function(data){
	var dom = "<a class='blue uploadedDocDownloadLink' href='#' data-file_name='{0}' data-file_id='{1}'>{2}</a>";
	return dom.format(data.file_name, data.file_id, data.file_name);
};


// Template method for the file options (Accept | Reject | Modify | Delete)
var getCurrentUploadedDocOptions = function(data){

	var completeDom = "";	
	var optionButtonDom = "<a class='k-button {0}' title='{1}' style='min-width:28px;margin:1px;' \
						   			data-file_id='{2}' \
						   			data-file_name='{3}'> \
						   		<span class='k-icon k-i-{4}'/> \
						   </a>";

	var status = data.status_desc;
	var isAdminUser = (userLevel == USER_LEVEL_ADMIN);
	var isUploadedToClientRepository = (status == STATUS_UPLOADED_TO_CLIENT);


	if(!isUploadedToClientRepository
		|| (isUploadedToClientRepository && isAdminUser))
	{

		// append the APPROVE button
		if(userLevel != USER_LEVEL_GENERAL_CONTRACTOR && status != STATUS_APPROVED && status != STATUS_UPLOADED_TO_CLIENT)
			completeDom += optionButtonDom.format("uploadedDocApproveLink", "Approve", data.file_id, data.file_name, "tick");
			
		// append the REJECT button
		if(userLevel != USER_LEVEL_GENERAL_CONTRACTOR && status != STATUS_REJECTED)
			completeDom += optionButtonDom.format("uploadedDocRejectLink", "Reject", data.file_id, data.file_name, "cancel");
	
	
		// append the MODIFY button
		// this button is available also for the General Contractor Users
		if(status == STATUS_REJECTED || status == STATUS_WAITING_APPROVAL || status == STATUS_MODIFIED)
		{
			var modifyButton = $(optionButtonDom.format("uploadedDocModifyLink", "Modify", data.file_id, data.file_name, "pencil"));
			var modifyButtonId = "modify_{0}".format(data.file_id);
			modifyButton.attr("id",modifyButtonId);
			completeDom += modifyButton[0].outerHTML;
		}
		
		// append the UPLOAD TO CLIENT REPOSITORY button
		if(isAdminUser && !isUploadedToClientRepository && status == STATUS_APPROVED)
			completeDom += optionButtonDom.format("uploadedDocUploadToClientLink", "Upload to Client Repository", data.file_id, data.file_name, "folder-up");
	
		// append the DELETE button
		if(userLevel != USER_LEVEL_GENERAL_CONTRACTOR)
			completeDom += optionButtonDom.format("uploadedDocDeleteLink", "Delete", data.file_id, data.file_name, "close");
	}
	
	return completeDom;
};

var getCurrentUploadedDocStatus = function(data){
	return data.status_desc;
};

var getCurrentUploadedDocComment = function(data){

	var comment = "";
	
	try
	{
		var userId = data.user_id;
		var date = "";
		
		if(!data.hasOwnProperty("last_modified") || data.last_modified == null)
		{
			var c = new Date();
			date = "{0}/{1}/{2} {3}:{4}".format(c.getMonth()+1, c.getDate(), c.getFullYear(), c.getHours(), c.getMinutes());
		}
		else 
		{
			date = kendo.toString(new Date(data.last_modified), "MM/dd/yyyy HH:mm");
		}
					
		var commentTemplate = "[{0}] {2} {1}";
		commentTemplate = commentTemplate.format(userId, date, "{0}")
		
		switch(data.status_desc)
		{
			case STATUS_WAITING_APPROVAL:
				comment = commentTemplate.format("added the document on");
				break;
			
			case STATUS_MODIFIED:
				comment = commentTemplate.format("modified the document on");
				break;

			case STATUS_APPROVED:
				comment = commentTemplate.format("approved the document on");
				break;
				
			case STATUS_REJECTED:
				comment = commentTemplate.format("rejected the document on");
				if(data.comment != null)
				{
					comment += "<br/>Comment: '{0}'".format(data.comment);
				}
				break;
			
			case STATUS_UPLOADED_TO_CLIENT:
				comment = commentTemplate.format("uploaded the document on the client repository on");
				break;

		}
	}
	catch(er)
	{
		comment = "";
	}
	
	return comment;

	if(data.comment == null)
		return "";
	return data.comment;
};










































