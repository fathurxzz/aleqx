// Global functions needed for the General Contractor workflow
// Cannot be wrapped inside a Module because some template methods have to be in the global space

var _gcTypes = [];
var getGCType = function (data) {

	for(var i = 0; i < _gcTypes.length; i++)
	{
		if(_gcTypes[i].gc_type_id === data.gc_type_id)
		{
			var GCTypeDom = "<span>{0}</span>".format(_gcTypes[i].gc_type_desc);
			return GCTypeDom;
		}
	}
};