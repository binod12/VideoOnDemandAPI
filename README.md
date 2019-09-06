 
 # Video On Demand ASP.NET Core Web API Endpoints
 - Register new User (For Admin & Viewer)
	- Viewer: /api/Users/register
	- Admin: /api/Users/registeradmin
 - Login (For Admin & Viewer): /api/Users/authenticate
 - Create new Category of Videos (Only for Admin): /api/VideoCategories
 - Add new videos to a category ( Only for Admin): /api/Videos
 - Get list of categories & videos (For Admin & Viewer): 
   - /api/VideoCategories
   - /api/Videos
 - Delete a category (Only for Admin): /api/VideoCategories/{id}
 - Update video description (Only for Admin): /api/Videos/{id}

# Dependencies
  - ASP.NET Core Web API : .Net Core 2.2
  - AutoMapper.Extensions.Microsoft.DependencyInjection: For mapping objects
  - Swagger: For documentation
  
# Admin user details: 
	-username: admin
	-password: admin
	
	
 # Folder Structure:  
 
 - Api- Contains all the APIS (Users, Video & Video Category)
 - DataContext - Entity Framework Core In-Memory DB Context and Data Generator Component
 - Dto - Data transfer object used by Automapper for Registering and Login
 - Entities - Individual entities (Video, Role, User, VideoCategory along with EntityBase)
 - Helpers - Helpers component for Application setting configuration, Automapper configuration, Custom Exception, and Utility for Password Hashing etc.
 - Services - Services being used for authentication, other services like Video and VideoCategory may be added latter here.
 