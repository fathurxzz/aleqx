using System.Collections.Generic;

namespace AccuV.UI.Dto
{
    public class UserActivitiesUpdateRequest
    {
        public string UserId { get; set; }
        public List<UserActivityDto> UserActivities { get; set; }
    }

    public class UserResponseDto
    {
        public UserDto User { get; set; }

        public List<UserActivityDto> UserActivities { get; set; }
    }

    public class UserActivityDto
    {
        public int UsrSid { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string TypeName { get; set; }
        public int ActivityTypeId { get; set; }
        public bool AccessCreate { get; set; }
        public bool AccessRead { get; set; }
        public bool AccessUpdate { get; set; }
        public bool AccessDelete { get; set; }
        public bool AccessApprove { get; set; }
        public bool AccessAdmin { get; set; }   
    }

    public class UserDto
    {
        public int UserSid { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
    }
}