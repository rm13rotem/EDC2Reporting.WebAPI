WebApi (Main Startup project - UI) 

Introduction - 
1. Mission statement - UI site
2. Built with Model-View-Controller in mind - it encompasses 
the whole UI, and deals with a manager/repository in each controller


Documentation - 
v 1.0.0.15 - Begin documentation.

List of things to do - (possible forks)
a. ModuleController, VisitController, VisitGroupController, DoctorController, PatientController
b. RegisterController - the site must be viral!
	* Only SiteAdministrator can create new Experiments, and Create new CTLs (ClinicalTrialLeaders) for them
	* CTL can add CTLs in the same experiment
	* CTL can add SiteManagers to a given Site (Todo - create Relation_Experiment_Site - many to many link)
	* SiteManagers can add more SiteManagers to thier respective site
	* CTL can add users to the experiment in all existing Sites (SiteManager)
	* SiteManager can add users to their respective Site
	* quicklookId - admin34, admin22, sitemanager22, or user1144 or something. Create spots so that all 
	  users with the same roles lie in the same pages in the database (more or less). Initialize system with 
	  1000 admins, 25000 sitemanagers, and 94,000 users.
c. Roles - Administrator, ClinicalTrialLeader, SiteManager, MembershipUser - all inherit from 
	MembershipUser (default implementation)
c2. LoginController - Single Sign On using 
	* username, quicklookId and password
d. Seperate Persistent Entities (Administrator only access) from Others
e. Add Exception Layer (current - just the EDC_Exception class)
f. try/catch everything and log using the existing NLOG.
g. mail on every irregularity (to RabbitMq and DB)
h. session - Successful login - doctor is selected. Then select Patient, then select Visit (in that order)
g. by default - Patient is selected from PatientController/Index Action (with filter)
h. by default - Visit is selected as *LastExistingVisitIndex* + 1
i. Expose existing CRF pages (Modules) for anonymous use (the whole point of the Site)
j. Publicise Medical History Term and Labs and Adverse Events Terms as a service (For registered users) build 
   them controllers that expose (with caching) the GET ALL functionality. Base it on Wikipedia (Seperate DLL).

v1.0.0.18 - 2024-06-10
Added a new entity to DB EmailModel for sending emails. Added a new controller EmailController to manage email templates.

v1.0.0.19 - 2024-06-12
Audit Trail:
•	Implemented audit trail creation inside SaveChangesAsync(CancellationToken):
•	On Added: set CreatedAt, LastUpdator, GuidId and record an audit log with an "After" snapshot.
•	On Modified: set UpdatedAt, LastUpdator and record an audit log with Before and After snapshots plus minimal metadata (StudyId, PatientId, CrfPageId).
•	On Deleted: record a "Deleted" audit with the Before snapshot.
•	Audit logs are added to the context (AuditLogs.AddRange) so they are saved in the same transaction as entity changes.
•	User name is resolved from the injected IHttpContextAccessor + UserManager<Investigator> (falls back to "System" when unauthenticated).

v1.0.0.20 - 2024-06-15	

•	I added [Authorize] on DesignExperiment; remove or adjust roles if you want open access.
•	Keep the links as asp-controller/asp-action so they follow your routing and will break early if controllers/actions are renamed.
•	If you want a single entry that shows counts (number of visit groups, visits, patients, CRF pages) you can inject EdcDbContext into the action, build a small view model with counts and pass it to the view.
•	If you want links to specific experiment context (e.g., attach VisitGroups to a selected Experiment), extend the view to accept an experiment id and pass it into the action URLs.
I
