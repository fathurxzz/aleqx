define([], function () {

	var _listData = {};
	var _siteNumber = "";
	var _taskID = "";
	var _commentOn = ""; // can be either ACTUAL or FORECASTED
	
	// accessors
	var setListData = function(data){
		if(data == null || data.length == 0)
		{
			$("#noComment").css("display","block");
			$("#commentList").css("display","none");
		}
		else
		{
			$("#noComment").css("display","none");
			$("#commentList").css("display","block");
		}
		_listData = data;
	}
	var getListData = function(){
		return _listData;
	}

	
	/**************************************/
	/********* INTERNAL METHODS ***********/
	/**************************************/

	// Main initialization method
	// Is creating the Comment Popup window and it's content
	var generateCommentTabContentInternal = function(data) {
	
		_siteNumber = data.site;
		_taskID = data.task;
		_commentOn = data.commenton;
		
		var windowContent = $("<div id='commentWindow'></div>");
				
		// Create the indicator that no comments were added
		windowContent.append($("<div id='noComment' style='height:357px;padding:10px;display:none;'>No comments yet</div>"));
		
		// Create the container of the comments list
		windowContent.append($("<div id='commentList' style='height:377px;overflow-y:scroll;display:none;'>"));
		
		// Create the comment input
		var commentInput = $("<div style='position:absolute; bottom:8px; margin-left:-1px;'> \
							  	<textarea id='commentTextArea' style='width:622px;font-family:Tahoma'></textarea><br/> \
							  	<a id='addCommentButton' class='k-button k-button-icontext' href='#'>Add Comment</a> \
							  </div>")
		windowContent.append(commentInput);
		
		// return the window content
		return $("<div></div>").append(windowContent).html();
	};
	
	var initCommentTabInternal = function(){
	
		$("#commentList").kendoListView({
			dataSource:new kendo.data.DataSource({
				transport: {
					read: function(options){
						$.ajax({
				            url: API_URI + "/Comment/Comments",
				            type: "POST",
				            dataType: "json",
				            contentType: "application/json",
				            data: JSON.stringify({SiteNumber:_siteNumber, TaskID: _taskID, CommentOn: _commentOn})
				        }).done(function(data){
			        		setListData(data);
				        	options.success(data);
				        	$("#commentList").scrollTop($("#commentList")[0].scrollHeight);
				        }).fail(function(error){
				        	alert("Error - Could not extract the comments from the DB :(")
				        });
					}
				}
			}),
			template:commentListTemplate
		});
		
		$("#commentList").css("border","none");
		$("#commentList").css("background-color","white");		
		
		$("#addCommentButton").click(onAddCommentClick);
	};
	
	var validComment = function(){
		if($.trim($("#commentTextArea").val()).length == 0)
		{
			alert("Please enter a comment before submitting it.");
			return false;
		}
		return true;
	}
	
	var destroy = function() {
		var commentListControl = $("#commentList").data("kendoListView");
		commentListControl.destroy();
	};
	
	
	/**************************************/
	/*************** EVENTS ***************/
	/**************************************/

	
	var onAddCommentClick = function(e) {
	
		if(validComment())
		{
			$("#commentWindow").showIndicator();
			
			// create the task comment object
			// using the same properties as the TaskComment DTO object
			var newInputComment = {
				Task_ID:_taskID,
				Site_Number:_siteNumber,
				Project_ID: projectId,
				User_ID:userName,
				Comment_On:_commentOn,
				Comment:$.trim($("#commentTextArea").val()),
			};
			
			$.ajax({
	            url: API_URI + "/Comment/CreateComment",
	            type: "POST",
	            dataType: "json",
	            contentType: "application/json",
	            data: JSON.stringify(newInputComment)
	        }).done(function(createdComment){
	        	
	        	// update the internal list of items
	        	var newListData = getListData();
	        	newListData.push(createdComment);
				setListData(newListData);	        	
				
				// update the data of the List
				var commentListDataSource = $("#commentList").data("kendoListView").dataSource;
				commentListDataSource.data(newListData);
				
				// empty the content of the textarea
				$("#commentTextArea").val("");
				
				// scroll to the bottom and hide the loading indicator	
	        	$("#commentList").scrollTop($("#commentList")[0].scrollHeight);
	        	$("#commentWindow").hideIndicator();
	        	
	        }).fail(function(error){
	        	alert("Error - Could not create the comment in the DB :(")
	        	$("#commentWindow").hideIndicator();
	        });
        }

	};
	
	
	/**************************************/
	/************* TEMPLATES **************/
	/**************************************/

	
	var commentListTemplate = function(data) {
		var commentText = data.Comment;
		commentText = commentText.replace(/\n/g,'<br/>');
		//commentText = commentText.replace(/ /g,'&nbsp;');
		
		var template = $("<div class='commentItem'> \
							<b>" + data.User_ID + "</b> <span style='font-size:10px; line-height:18px;'>" + data.Comment_Date_Friendly + "</span></br> \
							<span>" + commentText + "</span> \
						</div>");
		
		return $("<div></div>").append(template).html();
	};
	
	
	
	
	/**************************************/
	/************** PUBLIC ****************/
	/**************************************/

	
	return {

		generateCommentTabContent:generateCommentTabContentInternal,
		initCommentTab:initCommentTabInternal,
		destroy:destroy
	
	};
});