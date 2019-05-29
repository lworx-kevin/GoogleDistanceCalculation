internal JsonResult GetAllMembers(double Latitude, double Longitude)
        {
            try
            {
                //Error handling to check if variables were passed
                var checkAllMembers = ErrorConstants.CheckAllMembers(Latitude, Longitude);
                if (checkAllMembers.ErrorCode != 2000)
                    return Json(checkAllMembers);
                //select all members from dbo.users as List<UsersApiModel>
                var users = db.Query<UsersApiModel>(GetAllMembersQ).ToList();
                //iterate over users...
                foreach (var user in users)
                {
                    var locations = new List<string>();
                    //...grab string of locationids and convert to List<string>
                    if (!string.IsNullOrEmpty(user.LocationId))
                         locations = user.LocationId.Split(',').ToList();
                         
                    //...iterate over locations...
                    foreach (var location in locations)
                    {
                        //...and check dbo.location to see if locationId exists
                        var loc = db.Query<Location>(GetAllMemLocQ, new { id = int.Parse(location) }).FirstOrDefault();
                        //null check
                        if (user.Location == null)
                        {
                            //add location to user.Location (List<Location>)
                            user.Location = new List<Location>
                            {
                                loc
                            };
                        }
                        //..otherwise if List !null, add new index
                        else
                            user.Location.Add(loc);
                    }
                }

                var filteredMembers = new List<UsersApiModel>();
                //iterate once for all objects
                for (int i = 0; i < 1; i++)
                {
                    //get ind member from users List
                    foreach (var member in users)
                    {
                        //null check location List
                        if (member.Location != null)
                        {
                            // if !null and Longitude & Latitude == !null...
                            if (member.Location[i].Longitude != 0 || member.Location[i].Latitude != 0)
                            {
                                //...calculate GeoCoordinates...
                                var pCoord = new GeoCoordinate(parkerLat, parkerLong);
                                var mCoord = new GeoCoordinate(member.Location[i].Latitude, member.Location[i].Longitude);
                                //...and compare the distances between the 2 users
                                if (pCoord.GetDistanceTo(mCoord) <= Constants.MAX_DISTANCE)
                                    filteredMembers.Add(member);
                            }
                        }
                    }
                }
                //return List of users with injected Location(s) List(s)
                return Json(filteredMembers);
            }
            catch (Exception ex)
            {
                return Json(new Errors
                {
                    ErrorCode = ErrorConstants.DISTANCE_ERROR_CODE,
                    ErrorDesc = ErrorConstants.DISTANCE_ERROR_DESC
                });
            }            
        }

        #region DbStrings
        private static readonly string GetAllMembersQ = "SELECT * FROM users " +
                                                        "where IsDeleted = 0 and role = 1";

        private static readonly string GetAllMemLocQ = "SELECT * from location where locationid = @id";
